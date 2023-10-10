

using SimpleMapperProject;

SrcCourse src =  new SrcCourse(1,"Asp.net") ;

SrcCourse dst = new SrcCourse(0, "");

SimpleMapper simpleMapper = new SimpleMapper() ;

simpleMapper.Copy(src, dst);