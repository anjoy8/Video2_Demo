using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Video2_Demo.Models;

namespace Video2_Demo.Configs
{
    public class GoodsProfile : Profile
    {
        // 添加你的实体映射关系.
        public GoodsProfile()
        {

            // GoodsEntity转GoodsDto. 
            CreateMap<GoodsEntity, GoodsDto>()

                // 映射发生之前
                // 映射之前统一处理
                .BeforeMap((src, dest) => src.Price = src.Price + 10)
                // 默认赋值
                .BeforeMap((src, dest) => src.CreateTime = src.CreateTime == null ? (new DateTime(2012, 12, 12)) : src.CreateTime)


                // 映射匹配
                .ForMember(dest => dest.GoodsName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CreateTime,
                    opt => opt.MapFrom(src => ((DateTime)src.CreateTime).ToString("yyyy-MM-dd")))
                // 匹配的过程中赋值
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price + 10))
                // 忽略某个属性的映射
                //.ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                // 合并
                //.ForMember(dest => dest.GoodsName, opt => opt.MapFrom(src => src.Brands.Name +" "+ src.Name))




                // 映射发生之后
                .AfterMap((src, dest) => dest.GoodsName = dest.Price < 40 ? "N/A." : dest.GoodsName)
                .AfterMap((src, dest) => dest.Content = "数据传输对象（DTO)(DataTransfer Object)，是一种设计模式之间传输数据的软件应用系统。数据传输目标往往是数据访问对象从而从数据库中检索数据。数据传输对象与数据交互对象或数据访问对象之间的差异是一个以不具有任何行为除了存储和检索的数据（访问和存取器）数据传输对象（DTO)(DataTransfer Object)，是一种设计模式之间传输数据的软件应用系统。数据传输目标往往是数据访问对象从而从数据库中检索数据。数据传输对象与数据交互对象或数据访问对象之间的差异是一个以不具有任何行为除了存储和检索的数据（访问和存取器）数据传输对象（DTO)(DataTransfer Object)，是一种设计模式之间传输数据的软件应用系统。数据传输目标往往是数据访问对象从而从数据库中检索数据。数据传输对象与数据交互对象或数据访问对象之间的差异是一个以不具有任何行为除了存储和检索的数据（访问和存取器）数据传输对象（DTO)(DataTransfer Object)，是一种设计模式之间传输数据的软件应用系统。数据传输目标往往是数据访问对象从而从数据库中检索数据。数据传输对象与数据交互对象或数据访问对象之间的差异是一个以不具有任何行为除了存储和检索的数据（访问和存取器）数据传输对象（DTO)(DataTransfer Object)，是一种设计模式之间传输数据的软件应用系统。数据传输目标往往是数据访问对象从而从数据库中检索数据。数据传输对象与数据交互对象或数据访问对象之间的差异是一个以不具有任何行为除了存储和检索的数据（访问和存取器）数据传输对象（DTO)(DataTransfer Object)，是一种设计模式之间传输数据的软件应用系统。数据传输目标往往是数据访问对象从而从数据库中检索数据。数据传输对象与数据交互对象或数据访问对象之间的差异是一个以不具有任何行为除了存储和检索的数据（访问和存取器）数据传输对象（DTO)(DataTransfer Object)，是一种设计模式之间传输数据的软件应用系统。数据传输目标往往是数据访问对象从而从数据库中检索数据。数据传输对象与数据交互对象或数据访问对象之间的差异是一个以不具有任何行为除了存储和检索的数据（访）")

                ;

            // 最简单的匹配，属性字段/类型等完全一致
            // GoodsDto转GoodsEntity.
            CreateMap<GoodsDto, GoodsEntity>();
        }
    }
}
