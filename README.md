﻿QG
==
==============================================================
新增系统邮件提醒服务

MessageService:一个独立的邮件消息服务项目，Setup项目为邮件服务的安装程序。
安装后，在菜单栏（开始/Message Service）有三个快捷方式：启动服务、停止服务、卸载服务。

同时新增服务日志系统，可文件日志、Windows事件日志。

服务安装后，进行数据库、邮件服务器、服务日志系统配置。
配置完成后，启动服务（开始/Message Service/启动服务）则系统自动根据设置的扫描间隔检查并发送邮件。
目前邮件提醒原型：在新增一条订单数据时，会向员工发送邮件提醒。

实现原理：
1. 新增订单数据同时，生成一条提醒消息存入数据库中MD_Message表；
2. 邮件服务系统依托一个Window服务，定时扫描数据库中MD_Message表；
3. 当发现有未发送的提醒消息时，则发送邮件；
4. 将邮件发送结果（成功、失败）存入MD_MessageLog表；
5. 如果发送成功，则从MD_Message表中标记消息已发送。
邮件服务系统周期性执行2-5过程，完成邮件提醒工作。

如需修改邮件提醒相关配置，则停止服务、修改、启动服务生效。


所需配置：
1. 数据库配置
需要部署在SQL 2008上。
目前配置：数据库名：SOAWLIMSDB 账号：sa 密码：123456
配置文件：SOA.WLIMS.Service/web.config；MessageService安装目录下的config两个文件。修改对应数据库连接字符串。
2. 邮件服务器配置
MessageService安装目录下的config文件中 邮件服务器地址（smtp 服务器地址） 发送邮件的账号、密码
3. 日志系统配置
MessageService安装目录下的config文件中 日志记录设置

==============================================================


用户管理：包含用户注册，登陆，修改密码，安全退出，用户列表，用户信息修改/查看/删除

订单管理：订单增加，删除，修改，查询

车辆管理：车辆信息查询，修改，车辆增加，删除，车辆状态查询，修改（在库，在途）

仓库管理：货物位置管理（根据订单号查询货物在几号库）出入库单管理（根据订单号，操作修改出入库状态）
    

用户名/ 密码/角色
admin/123456/管理员
yuangong1/123456/员工
yuangong2/123456/员工
kehu1/123456/客户
kehu2/123456/客户

权限划分
管理员：用户管理/订单管理/车辆管理/仓库管理/修改密码
员工：订单管理/车辆管理/仓库管理/修改密码
客户：订单管理/修改密码
无限制：用户注册/系统介绍
