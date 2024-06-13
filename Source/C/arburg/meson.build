project('arburg', 'c')

cc    = meson.get_compiler('c')

dep_m = cc.find_library('m')
dep_sqlite = cc.find_library('sqlite3')

src   = [ 
  'main.c'
  , 'model.c'
  , 'io.c'
  , 'db.c'
]


executable(
  'arburg'
  , src
  , c_args: [ '-O0', '-g' ]
  , dependencies: [ dep_m, dep_sqlite ])
