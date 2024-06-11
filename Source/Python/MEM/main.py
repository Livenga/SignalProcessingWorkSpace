import sys
import numpy as np
import matplotlib.pyplot as plt
from spectrum import arburg, arma2psd
from datetime import datetime


if __name__ == '__main__':
    # ファイル名で使用する, 現在日時の文字列を作成
    now     = datetime.now()
    str_now = now.strftime('%Y%m%d_%H%M%S')


    # サンプル信号の生成
    fs     = 1000  # サンプリング周波数
    t      = np.linspace(start=0, stop=1, num=fs)

    if len(sys.argv) > 1:
        # CSV ファイル(signal.csv) からの入力
        signal = np.loadtxt(fname = sys.argv[1])
    else:
        # 確認用: 25[Hz] と 100[Hz] 正弦波とノイズを加算した波形
        signal = np.sin(2 * np.pi * 50 * t) + np.sin(2 * np.pi * 120 * t) + np.random.normal(0, 0.1, fs)

    # signal を CSV ファイルへ保存
    #np.savetxt(
    #        fname     = f'{str_now}_signal.csv',
    #        X         = signal,
    #       delimiter = ',')


    # 最大エントロピースペクトル推定（Burgの方法）
    order = 15 # モデルの次数
    ar, _, _ = arburg(signal, order)

    # 周波数軸の設定
    frequencies = np.linspace(0, fs / 2, 512)
    psd         = np.zeros(len(frequencies))

    # パワースペクトル密度の計算
    for i, f in enumerate(frequencies):
        psd[i] = 1.0 / np.abs(np.polyval(ar, np.exp(-1j * 2 * np.pi * f / fs))) ** 2

    # パワースペクトラムをCSVファイルへ保存
    np.savetxt(fname = f'{str_now}_psd.csv', X = psd, delimiter = ',')


    # グラフの描画
    plt.figure(figsize=(10, 6))
    plt.plot(frequencies, 10 * np.log10(psd), label = 'Maximum Entropy Method')

    plt.title('Frequency Analysis using Maximum Entropy Method')
    plt.xlabel('Frequency [Hz]')
    plt.ylabel('Power Spectral Density [dB]')
    plt.grid()
    plt.legend()

    # 画像へ保存
    #fmt = 'png'
    #plt.savefig(fname  = f'{str_now}.{fmt}', format = fmt)

    plt.show()
