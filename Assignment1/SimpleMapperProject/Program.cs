

using SimpleMapperProject;

SrcCourse src =  new SrcCourse() ;
src.Name = "Asp.net";
src.Id = 1 ;

SrcCourse dst = new SrcCourse();

SimpleMapper simpleMapper = new SimpleMapper() ;

simpleMapper.Copy(src, dst);