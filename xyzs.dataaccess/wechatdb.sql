/*
Navicat MySQL Data Transfer

Source Server         : MySqlFreshMan
Source Server Version : 50709
Source Host           : 127.0.0.1:3306
Source Database       : wechatdb

Target Server Type    : MYSQL
Target Server Version : 50709
File Encoding         : 65001

Date: 2019-03-20 23:39:18
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for syscontent
-- ----------------------------
DROP TABLE IF EXISTS `syscontent`;
CREATE TABLE `syscontent` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间',
  `CreateUserId` bigint(20) DEFAULT NULL COMMENT '创建者id（sysuser表id）',
  `Title` varchar(255) DEFAULT NULL COMMENT '标题',
  `Content` mediumtext COMMENT '文章内容',
  `IsDel` int(255) DEFAULT '0' COMMENT '是否已经删除0：未删除，1：已经删除',
  `ContentSource` varchar(255) DEFAULT NULL COMMENT '文章来源',
  `ContentType` int(255) DEFAULT NULL COMMENT '内容类型，关联文章类型',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of syscontent
-- ----------------------------

-- ----------------------------
-- Table structure for sysmenumodel
-- ----------------------------
DROP TABLE IF EXISTS `sysmenumodel`;
CREATE TABLE `sysmenumodel` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Name` varchar(255) DEFAULT NULL COMMENT '英文名称',
  `Title` varchar(255) DEFAULT NULL COMMENT '标题',
  `Icon` varchar(255) DEFAULT NULL COMMENT '图标',
  `Url` varchar(255) DEFAULT NULL COMMENT '连接',
  `OrderNo` varchar(255) DEFAULT NULL COMMENT '序号',
  `MenuType` varchar(255) DEFAULT NULL COMMENT '权限类型',
  `Remark` varchar(255) DEFAULT NULL COMMENT '备注',
  `HasPermission` bit(8) DEFAULT NULL COMMENT '是否有权限',
  `ParentId` int(11) DEFAULT NULL COMMENT '上级菜单id',
  `IsDel` int(255) DEFAULT '0' COMMENT '是否已经删除0：未删除，1：已经删除',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of sysmenumodel
-- ----------------------------
INSERT INTO `sysmenumodel` VALUES ('1', 'SystemSet', '系统设置', 'fa-cog', null, '0', '1001', null, '\0', '0', '0');
INSERT INTO `sysmenumodel` VALUES ('2', 'MenuSet', '菜单权限设置', null, '/SysSet/MenuSet', '1', '1002', '菜单权限设置', null, '1', '0');
INSERT INTO `sysmenumodel` VALUES ('3', 'MenuAdmin', '菜单配置', null, '/SysSet/MenuAdmin', '2', '1003', '菜单配置', null, '1', '0');
INSERT INTO `sysmenumodel` VALUES ('5', 'MenuUsers', '管理员列表', 'fa-users', '/SysSet/MenuUsers', '3', '1004', null, '\0', '1', '0');
INSERT INTO `sysmenumodel` VALUES ('6', 'ContentManage', '内容管理', 'fa-coffee', null, '4', '2001', null, '\0', '0', '0');
INSERT INTO `sysmenumodel` VALUES ('7', 'ContentEditPage', '内容编辑', null, '/Content/ContentEdit', '5', '2002', null, '\0', '6', '0');

-- ----------------------------
-- Table structure for sysuser
-- ----------------------------
DROP TABLE IF EXISTS `sysuser`;
CREATE TABLE `sysuser` (
  `Id` bigint(11) NOT NULL AUTO_INCREMENT COMMENT '主键，用户id',
  `UserName` varchar(255) DEFAULT NULL COMMENT '用户名',
  `Password` varchar(255) DEFAULT NULL COMMENT '密码',
  `Sex` int(255) DEFAULT NULL COMMENT '性别：0保密；1：男；2：女',
  `Birthday` datetime DEFAULT NULL COMMENT '出生日期',
  `HeadUrl` varchar(255) DEFAULT NULL COMMENT '头像地址',
  `TelPhone` varchar(255) DEFAULT NULL COMMENT '手机号',
  `CreateTime` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '修改时间',
  `CreateAuth` bigint(255) DEFAULT NULL COMMENT '创建人id',
  `UpdateAuth` bigint(11) DEFAULT NULL COMMENT '更新人id',
  `UserType` int(255) DEFAULT '0' COMMENT '用户类型0：普通用户；1：超级管理员；2：普通管理员',
  `IsDel` int(255) DEFAULT '0' COMMENT '是否已经删除0：未删除，1：已经删除',
  `TrueName` varchar(255) DEFAULT NULL COMMENT '真实姓名',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of sysuser
-- ----------------------------
INSERT INTO `sysuser` VALUES ('1', 'FreshMan', '11982e4228792c2c0c9a6e53261dbec4', '1', '1991-07-31 20:56:15', null, '18883257311', '2018-11-25 20:57:34', '2018-11-29 21:05:56', '1', '1', '1', '0', 'FreshMan');
INSERT INTO `sysuser` VALUES ('2', 'YangHongJun', '11982e4228792c2c0c9a6e53261dbec4', '2', '2016-11-29 21:06:15', null, '18523989760', '2018-11-29 21:06:41', '2018-11-29 21:06:41', '1', '1', '2', '0', '杨洪俊');

-- ----------------------------
-- Table structure for sysusermenu
-- ----------------------------
DROP TABLE IF EXISTS `sysusermenu`;
CREATE TABLE `sysusermenu` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `UserId` int(11) DEFAULT NULL COMMENT '用户id',
  `MenuId` int(11) DEFAULT NULL COMMENT '菜单id',
  `IsDel` int(255) DEFAULT '0' COMMENT '是否已经删除0：未删除，1：已经删除',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of sysusermenu
-- ----------------------------
INSERT INTO `sysusermenu` VALUES ('1', '1', '1', '1');
INSERT INTO `sysusermenu` VALUES ('2', '1', '2', '1');
INSERT INTO `sysusermenu` VALUES ('3', '1', '3', '1');
INSERT INTO `sysusermenu` VALUES ('4', '1', '5', '1');
INSERT INTO `sysusermenu` VALUES ('5', '2', '1', '1');
INSERT INTO `sysusermenu` VALUES ('6', '2', '5', '1');
INSERT INTO `sysusermenu` VALUES ('7', '2', '1', '0');
INSERT INTO `sysusermenu` VALUES ('8', '2', '5', '0');
INSERT INTO `sysusermenu` VALUES ('9', '1', '1', '0');
INSERT INTO `sysusermenu` VALUES ('10', '1', '2', '0');
INSERT INTO `sysusermenu` VALUES ('11', '1', '3', '0');
INSERT INTO `sysusermenu` VALUES ('12', '1', '5', '0');
INSERT INTO `sysusermenu` VALUES ('13', '1', '6', '0');
INSERT INTO `sysusermenu` VALUES ('14', '1', '7', '0');
