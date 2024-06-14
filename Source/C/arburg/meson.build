project('arburg', 'c')

cc    = meson.get_compiler('c')

dep_m = cc.find_library('m')
dep_sqlite = cc.find_library('sqlite3')

src   = [ 
  'main.c'
  , 'model.c'
  , 'model.power_spectrum.c'
  , 'io.c'
  , 'db.c'
]


executable(
  'arburg'
  , src
  , c_args: [ '-O0', '-g' ]
  , dependencies: [ dep_m, dep_sqlite ])


spectrum_test_src = [
  'power_spectrum_test.c'
  , 'model.c'
  , 'model.power_spectrum.c'
  , 'io.c'
]

exe_spectrum_test = executable(
  'power_spectrum_test'
  , spectrum_test_src
  , c_args: [ '-O0', '-g' ]
  , dependencies: [ dep_m ])


# テストプログラムの引数にモデル次数のファイルを与えている.
# テスト実行時の作業ディレクトリは `meson test -C [builddir]` オプション `-C` で指定された位置になる為
# 1つ下のディレクトリを参照するように調整している
test(
  'power_spectrum_test'
  , exe_spectrum_test
  , args: [ '../test_ar_model.csv' ])
