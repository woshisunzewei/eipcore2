EIP Core2 权限管理系统 (交流群:495070603，作者:1039318332) 
一、源码介绍(注意此版本基于2.0开发)

       1、开发工具:Vs2017+SqlServer 2008R2 及以上版本
       
       2、系统采用前后端分离模式开发,后端使用最新的Core2框架,使用MongoDb作为缓存工具,前端使用Adminlte框架进行二次开发,可自由换肤,自己进行扩展
       
       2、系统设计为通用平台框架,权限模块已成熟,可用作ERP、OA、CRM等系统的基础开发框架,具有成熟的数据访问(Dapper)、作业调度(Quartz.net)、日志记录、权限控制等解决方案。
       
       3、系统采用多层架构,简单实用,内置T4代码生成器,可快速生成业务代码。
二、特色功能     

       1、完善的架构:
           1.1、系统前端采用Asp.net Core2 Mvc区域模式进行开发,将需要开发的子系统使用区域分割,使各个模块进行完美的分割,对以后系统升级等提供可扩展性
           1.2、系统前端基于Adminlte框架,使用了各种成熟的Jquery插件,为系统界面的友好交互性提供了保障。
           1.3、系统采用前后端分离模式开发,实现界面与后端解耦,加快开发效率。系统采用接口编程方式,使用Autofac进行依赖注入进行接口解耦,系统中无需充斥各种对象的实例化。
           1.4、系统中已采用T4写了一套针对本框架开发的代码生成器,可快速生成需要使用的业务、数据访问、实体代码,减少开发量,节约时间。
       2、权限控制:针对系统中大多数权限控制情况,系统中可针对角色、组织机构、岗位、工作组进行授权。权限由粗粒度到细粒度均可进行控制,菜单模块权限、功能按钮权限、字段权限、数据权限等
       3、完善的日志:全方位的记录日志。日志包括:登录日志、操作日志、数据日志、访问日志、异常日志。可快速定位到系统中每一个角落。
三、注意事项

       1、开发环境：VS2017 + SQLServer2008R2 + Core2
       2、登录用户名admin 密码 123456 ，
       3、数据库文件在Data目录下,可通过里面的Sql脚本语句进行创建
       4、默认在EIP.Api项目下appsettings.json文件中修改数据库连接
       5、运行系统前请安装Mongodb，否则日志无法进行记录:
	可参考http://blog.csdn.net/heshushun/article/details/77776706
	      https://www.cnblogs.com/jiaxuekai/p/7204716.html
	      httpsk://www.cnblogs.com/ljwk/p/8014164.html
四，项目截图
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143545_36bfefaf_368165.png "在这里输入图片标题")
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143504_2f23f5fd_368165.png "在这里输入图片标题")
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143453_5da63bcd_368165.png "在这里输入图片标题")
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143613_dea1e6cf_368165.png "在这里输入图片标题")
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143620_6c0e6208_368165.png "在这里输入图片标题")
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143632_0f4f5054_368165.png "在这里输入图片标题")
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143639_5e0ee10a_368165.png "在这里输入图片标题")
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143646_7c7ff1bd_368165.png "在这里输入图片标题")
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143702_0da758c9_368165.png "在这里输入图片标题")
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143708_e1dfcc94_368165.png "在这里输入图片标题")
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143715_68c6b738_368165.png "在这里输入图片标题")
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143720_bc504328_368165.png "在这里输入图片标题")
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143729_80f9b49d_368165.png "在这里输入图片标题")
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143735_619e3d5b_368165.png "在这里输入图片标题")
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143740_c4ba9e02_368165.png "在这里输入图片标题")
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143747_08cf7e9e_368165.png "在这里输入图片标题")
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143753_f6c946e7_368165.png "在这里输入图片标题")
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143802_ed7b941f_368165.png "在这里输入图片标题")
![输入图片说明](https://images.gitee.com/uploads/images/2019/0828/143858_8c731e08_368165.png "在这里输入图片标题")
