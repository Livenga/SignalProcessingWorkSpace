project('arburg', 'c')

cc    = meson.get_compiler('c')
dep_m = cc.find_library('m')
src   = [ 
  'main.c'
  , 'model.c'
  , 'io.c'
]


executable(
  'arburg'
  , src
  , dependencies: [ dep_m ])
