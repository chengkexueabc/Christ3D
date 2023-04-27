using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Christ3D.Application.AutoMapper
{
    /// <summary>
    /// 静态全局 AutoMapper 配置文件
    /// </summary>
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                //这个是领域模型 -> 视图模型的映射，是读命令
                cfg.AddProfile(new DomainToViewModelMappingProfile());
                //这里是视图模型 -> 领域模式的映射，是写命令
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
            });
        }
    }
}
