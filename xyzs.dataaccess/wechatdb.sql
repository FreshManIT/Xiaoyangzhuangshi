/*
Navicat MySQL Data Transfer

Source Server         : MySqlRoot
Source Server Version : 50709
Source Host           : localhost:3306
Source Database       : wechatdb

Target Server Type    : MYSQL
Target Server Version : 50709
File Encoding         : 65001

Date: 2019-03-23 16:07:15
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
  `ContentType` varchar(255) DEFAULT NULL COMMENT '内容类型，关联文章类型',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of syscontent
-- ----------------------------
INSERT INTO `syscontent` VALUES ('1', '2019-03-22 14:03:04', '1', '五一放假调整通知', '<p>&nbsp; &nbsp; &nbsp; &nbsp;五一放假调整通知专题为您提供最新最全的五一放假调整通知相关法律知识，以及提供全国各地的五一放假调整通知最快捷律师在线为您提供相关的法律咨询.<br/></p>', '0', '华律网', '75b25bc195304ac381d95ebb00836be4');
INSERT INTO `syscontent` VALUES ('2', '2019-03-23 15:58:42', '1', '根据数组对象的某个属性值找到指定的元素', '<p style=\"margin-top: 0px; margin-bottom: 0px; padding: 0px; line-height: 1.5; font-size: 13px; font-family: Verdana, Arial, Helvetica, sans-serif; white-space: normal; background-color: rgb(254, 254, 242);\">现在有一个数组对象,也就是数组元素是对象类型,现在的需求是根据对象的某个属性值,找到该数组对应的元素(对象),比如根据&quot;bianma&quot;==&quot;11&quot;,找到对应的&quot;name&quot;为&quot;商品房&quot; :&nbsp;</p><p style=\"margin-top: 0px; margin-bottom: 0px; padding: 0px; line-height: 1.5; font-size: 13px; font-family: Verdana, Arial, Helvetica, sans-serif; white-space: normal; background-color: rgb(254, 254, 242);\">该数组对象数据如下:&nbsp;</p><p><span class=\"cnblogs_code_copy\" style=\"margin: 0px; padding: 0px; line-height: 1.5;\"><a title=\"复制代码\" style=\"margin: 0px; padding: 0px; color: rgb(7, 93, 179); text-decoration-line: underline;\"><img src=\"/ueditor/net/upload/image/20190323/6368895351695684412792102.gif\" alt=\"复制代码\" style=\"margin: 0px; padding: 0px; border: 0px;\"/></a></span></p><pre style=\"margin-top: 0px; margin-bottom: 0px; padding: 0px;\">var&nbsp;datas&nbsp;=&quot;name&quot;:&nbsp;&quot;商品房&quot;&quot;bianma&quot;:&nbsp;&quot;11&quot;&quot;name&quot;:&nbsp;&quot;商铺&quot;&quot;bianma&quot;:&nbsp;&quot;12&quot;</pre><p><span class=\"cnblogs_code_copy\" style=\"margin: 0px; padding: 0px; line-height: 1.5;\"><a title=\"复制代码\" style=\"margin: 0px; padding: 0px; color: rgb(7, 93, 179); text-decoration-line: underline;\"><img src=\"/ueditor/net/upload/image/20190323/6368895351695684412792102.gif\" alt=\"复制代码\" style=\"margin: 0px; padding: 0px; border: 0px;\"/></a></span></p><p style=\"margin-top: 0px; margin-bottom: 0px; padding: 0px; line-height: 1.5; font-size: 13px; font-family: Verdana, Arial, Helvetica, sans-serif; white-space: normal; background-color: rgb(254, 254, 242);\">我们可以通过对数组进行筛选操作:</p><pre style=\"margin-top: 0px; margin-bottom: 0px; padding: 0px;\">var&nbsp;data=&nbsp;datas.filter(function(item){&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;return&nbsp;item.bianma&nbsp;==&nbsp;&quot;12&quot;;&nbsp;\n})\nconsole.log(data);&nbsp;//&nbsp;[{name:&nbsp;&quot;商铺&quot;,&nbsp;bianma:&nbsp;&quot;12&quot;}]</pre><p style=\"margin-top: 0px; margin-bottom: 0px; padding: 0px; line-height: 1.5; font-size: 13px; font-family: Verdana, Arial, Helvetica, sans-serif; white-space: normal; background-color: rgb(254, 254, 242);\">filter() 方法将匹配元素集合缩减为匹配指定选择器的元素.该方法不改变原数组,返回的是筛选后满足条件的数组.</p><p><br/></p>', '0', '断线の风筝', '75b25bc195304ac381d95ebb00836be4');

-- ----------------------------
-- Table structure for sysdict
-- ----------------------------
DROP TABLE IF EXISTS `sysdict`;
CREATE TABLE `sysdict` (
  `Id` varchar(64) NOT NULL,
  `Value` varchar(100) DEFAULT NULL COMMENT '字典值',
  `Lable` varchar(100) DEFAULT NULL COMMENT '标签',
  `Type` varchar(100) DEFAULT NULL COMMENT '类型',
  `Description` varchar(100) DEFAULT NULL COMMENT '描述',
  `Sort` decimal(10,0) DEFAULT NULL COMMENT '排序值',
  `ParentId` varchar(64) DEFAULT NULL COMMENT '上级id',
  `CreateBy` varchar(64) DEFAULT NULL COMMENT '创建人',
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间',
  `Remarks` varchar(255) DEFAULT NULL COMMENT '备注',
  `IsDel` int(255) DEFAULT NULL COMMENT '是否已经删除0：未删除，1：已经删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of sysdict
-- ----------------------------
INSERT INTO `sysdict` VALUES ('00a13dc801c94427ab0da6cbd7850399', '36', '一般农户补贴', 'SubsidySmallType', '3', '291', null, '1', '2018-11-07 16:20:54', '农业类', '0');
INSERT INTO `sysdict` VALUES ('072bb26423df4c79ba5fd91f3064e6f8', '23', '蔬菜生产便道', 'SubsidySmallType', '4', '230', null, '1', '2018-09-19 11:28:55', '产业类', '0');
INSERT INTO `sysdict` VALUES ('0f53b833eaf54accab08c414c5165392', '6', '计生卫生类', 'SubsidyBigType', '计生卫生类', '0', null, '1', '2018-09-18 16:50:03', null, '0');
INSERT INTO `sysdict` VALUES ('1279976d545e48a7bbb1e378280cee12', '1', 'app首页导航按钮分类', 'appicon_group', 'app首页导航按钮分类', '10', '0', '1', '2018-08-17 14:23:07', '测试数据', '0');
INSERT INTO `sysdict` VALUES ('13233bb1418f41fe860460b9a6997648', '1', '民政类', 'SubsidyBigType', '民政类', '0', null, '1', '2018-09-18 16:34:28', null, '0');
INSERT INTO `sysdict` VALUES ('14ef1bb484e44db7846b70f74e97a3de', '33', '大病统筹报销', 'SubsidySmallType', '6', '330', null, '1', '2018-09-19 11:33:40', '卫生计生类', '0');
INSERT INTO `sysdict` VALUES ('16713bb90eff4d7791bebb707d089bfa', '003', '化妆品', 'GoodsTagType', '用于商品打标', '51', null, '1', '2019-02-18 15:03:34', '用于商品打标', '1');
INSERT INTO `sysdict` VALUES ('190c99cd5d9e4c25830504de31323147', 'q1', '问题1：乡村振兴APP数据录入与乡村振兴网页版数据录入是同步更新的吗？', 'sys_cjwt', '是的，用户可通过APP或者网页端进行数据录入，数据实时同步更新。', '0', null, '1', '2018-10-22 11:03:43', null, '1');
INSERT INTO `sysdict` VALUES ('1af8501b086545f08b2d5e98d9c82870', '2', '安置房', 'housetype', '房屋类型', '0', null, '1', '2018-09-05 17:43:47', null, '0');
INSERT INTO `sysdict` VALUES ('1e04fd43f9a64a61aa4e9325c5e0afb0', '2', '烟酒', 'EBusinessCategory', '电商平台抓取分类', '2', null, '1', '2018-12-20 17:36:51', '电商平台抓取分类', '0');
INSERT INTO `sysdict` VALUES ('2c9da9371bb34ab1ab8909c1c74e1c4d', '5', '大病医疗救助', 'SubsidySmallType', '1', '50', null, '1', '2018-09-19 11:14:04', '民政救助', '0');
INSERT INTO `sysdict` VALUES ('2fedb28d8d8f4c12a70e13767e1f14db', '1', '自建房', 'housetype', '房屋类型', '0', null, '1', '2018-09-05 17:43:16', null, '0');
INSERT INTO `sysdict` VALUES ('30bb2c71f4374bb0b98bdf0873a5d247', '4', '其他', 'housetype', '房屋类型', '0', null, '1', '2018-09-05 17:44:26', null, '0');
INSERT INTO `sysdict` VALUES ('37f2a335c2c64eefa78c6a7466ee5afc', '002', '保健品', 'GoodsTagType', '用于商品打标', '41', null, '1', '2019-02-18 15:03:16', '用于商品打标', '0');
INSERT INTO `sysdict` VALUES ('3b9d9f1ebb4a46b6a0a51ce23f590296', '5', '林业类', 'SubsidyBigType', '林业类', '0', null, '1', '2018-09-18 16:46:44', null, '0');
INSERT INTO `sysdict` VALUES ('3c8bd9f0ec0f4d5e9a17ead2851c46de', '17', '就业困难人员社保补贴', 'SubsidySmallType', '2', '170', null, '1', '2018-09-19 11:21:00', '保险类', '0');
INSERT INTO `sysdict` VALUES ('3f7c0d14a39245b89c7f890d5eb969c1', '3', '商品房', 'housetype', '房屋类型', '0', null, '1', '2018-09-05 17:44:08', null, '0');
INSERT INTO `sysdict` VALUES ('42f642e8ae3a49bb9bafa2dd8bbc3ec2', '6', '受灾救助', 'SubsidySmallType', '1', '60', null, '1', '2018-09-19 11:14:17', '民政救助', '0');
INSERT INTO `sysdict` VALUES ('46f9fbc1ddd1448e931792d4114e1ee7', '29', '退耕还林补偿', 'SubsidySmallType', '5', '290', null, '1', '2018-09-19 11:31:21', '林业类', '0');
INSERT INTO `sysdict` VALUES ('471a19e0a881479fadd4665ed5af68c5', '01', '江小白', 'GoodsTagType', '用于商品打标', '1', null, '1', '2019-02-15 13:50:05', '用于商品打标', '0');
INSERT INTO `sysdict` VALUES ('494a98c678124696b2c62d05ac4c3ce2', '18', '特困企业退休人员慰问金', 'SubsidySmallType', '2', '180', null, '1', '2018-09-19 11:21:21', '保险类', '0');
INSERT INTO `sysdict` VALUES ('498eb330940b414aa8801d4ee965d322', '3', '吨/年', 'UNIT_TYPE', '企业产品产量单位', '20', null, '1', '2018-08-31 10:57:39', null, '0');
INSERT INTO `sysdict` VALUES ('534a00ced6394835aa72de8065819e31', '4', '产业类', 'SubsidyBigType', '产业类', '0', null, '1', '2018-09-18 16:41:18', null, '0');
INSERT INTO `sysdict` VALUES ('55abb632286d4b9db567042ee7be1199', '001', '坚果类', 'GoodsTagType', '用于商品打标', '31', null, '1', '2019-02-18 14:57:50', '用于商品打标', '0');
INSERT INTO `sysdict` VALUES ('583b550e6e6041e49d77c37c0029c3d0', '9', '残疾人建房补助', 'SubsidySmallType', '1', '90', null, '1', '2018-09-19 11:16:50', '残疾补助', '0');
INSERT INTO `sysdict` VALUES ('5da5deaa24f64a459a68623361e4e6f9', '35', '独生子女奖金名称过程啊导致的较好的较好的手机号建华大街说的就', 'SubsidySmallType', '6', '350', null, '1', '2018-09-19 11:34:09', '卫生计生类', '1');
INSERT INTO `sysdict` VALUES ('66076a226e6e4089a5ea04c3a03eaca2', '2', '测试类别', 'category', '测试类别', '1', '0', '1', '2018-08-17 11:41:23', '类别编码固定', '0');
INSERT INTO `sysdict` VALUES ('67a8ad6a0aef4542b2e3b1f4be082280', '20', '耕地地力补贴', 'SubsidySmallType', '3', '200', null, '1', '2018-09-19 11:27:02', '农业类', '0');
INSERT INTO `sysdict` VALUES ('6a4849c9c5d846629cc1320a4f150e07', '1', '低保金', 'SubsidySmallType', '1', '10', null, '1', '2018-09-19 11:11:41', '民政救助', '0');
INSERT INTO `sysdict` VALUES ('6CFFD12474F74DF5E053A90B12AC8220', 'ios', 'OUh7s6_21sf,i-oy7.appkey', 'appcode_key', 'appcode管理，分配key', '20', '0', '1', '2018-05-25 10:49:29', null, '0');
INSERT INTO `sysdict` VALUES ('6CFFD12474F84DF5E053A90B12AC8220', 'android', '7B,x4_y1ss`Tc.miq.appkey', 'appcode_key', 'appcode管理，分配key', '20', '0', '1', '2018-05-25 10:49:30', null, '0');
INSERT INTO `sysdict` VALUES ('6CFFD12474F94DF5E053A90B12AC8220', 'h5', 'MjGr2[214fTec.mGq.appkey', 'appcode_key', 'appcode管理，分配key', '20', '0', '1', '2018-05-25 10:49:30', null, '0');
INSERT INTO `sysdict` VALUES ('6CFFD12474FA4DF5E053A90B12AC8220', 'web', 'i8P98h1ss`h56r.4L.appkey', 'appcode_key', 'appcode管理，分配key', '20', '0', '1', '2018-05-25 10:49:30', null, '0');
INSERT INTO `sysdict` VALUES ('6CFFD12474FB4DF5E053A90B12AC8220', 'cms', 'V7NWdk!0e9-3#Gc.iTappkey', 'appcode_key', 'appcode管理，分配key', '20', '0', '1', '2018-05-25 10:49:30', null, '0');
INSERT INTO `sysdict` VALUES ('6e007168a79c4ecb8ca35a0d413788d8', '27', '农家乐床位补贴', 'SubsidySmallType', '4', '270', null, '1', '2018-09-19 11:30:48', '产业类', '0');
INSERT INTO `sysdict` VALUES ('71214f39178748cdaa6ca624dcc4333d', '19', '外出务工人员意外伤害险补助', 'SubsidySmallType', '2', '190', null, '1', '2018-09-19 11:21:39', '保险类', '0');
INSERT INTO `sysdict` VALUES ('72677a3e1c9c4b429f88e51dd94045b5', '21', '蔬菜种植补贴', 'SubsidySmallType', '4', '210', null, '1', '2018-09-19 11:27:29', '产业类', '0');
INSERT INTO `sysdict` VALUES ('7375e83f33373c0ee053a90b12ac7e15', 'limittimes', '5', 'sysuser_loginerror', '连续登录失败设置', '10', '0', '1', '2018-08-15 16:13:53', null, '0');
INSERT INTO `sysdict` VALUES ('7375e83f33383c0ee053a90b12ac7e15', 'minutes', '30', 'sysuser_loginerror', '连续登录失败设置', '10', '0', '1', '2018-08-15 16:13:53', null, '0');
INSERT INTO `sysdict` VALUES ('7375e83f33393c0ee053a90b12ac7e15', '1', 'http://120.79.170.36/uploadfile', 'sys.uploadpre', '图片上传', '10', '0', '1', '2018-08-15 16:13:53', null, '0');
INSERT INTO `sysdict` VALUES ('7401235afac913f2e053106da8c0b009', '1', '公斤/年', 'UNIT_TYPE', '企业产品产量单位', '10', '0', '1', '2018-08-22 14:20:25', null, '0');
INSERT INTO `sysdict` VALUES ('7401235afaca13f2e053106da8c0b009', '2', '只/年', 'UNIT_TYPE', '企业产品产量单位', '10', '0', '1', '2018-08-22 14:20:31', null, '0');
INSERT INTO `sysdict` VALUES ('7401c1e676ca1835e053106da8c0ab95', '1', '企业行业', 'category', '企业行业类型', '10', '0', '1', '2018-08-22 15:04:45', null, '0');
INSERT INTO `sysdict` VALUES ('74259243407c49d6a6e9d0da3369c655', '2', '特困补助', 'SubsidySmallType', '1', '20', null, '1', '2018-09-19 11:12:37', '民政救助', '0');
INSERT INTO `sysdict` VALUES ('75b25bc195304ac381d95ebb00836be4', '行业新闻', '行业新闻', 'ContentType', '行业新闻内容分类', '1', '0', '1', '2019-03-23 15:54:31', '行业新闻内容分类', '0');
INSERT INTO `sysdict` VALUES ('78ef651ca8e3426faf2fabbab90297ac', '2', '保险类', 'SubsidyBigType', '保险类', '0', null, '1', '2018-09-18 16:35:22', null, '0');
INSERT INTO `sysdict` VALUES ('7c4977e161c44f04b3bdf397f23c4da4', '7', '残疾人两项补贴', 'SubsidySmallType', '1', '70', null, '1', '2018-09-19 11:14:39', '残疾补助', '0');
INSERT INTO `sysdict` VALUES ('81814cbc38bc418b870fbe512a3f6f9c', '25', '林果采摘体验带', 'SubsidySmallType', '4', '250', null, '1', '2018-09-19 11:30:14', '产业类', '0');
INSERT INTO `sysdict` VALUES ('83b8d19fb21445d6be690499aaf2d541', '34', '独生子女父母残疾补助', 'SubsidySmallType', '6', '340', null, '1', '2018-09-19 11:33:51', '卫生计生类', '0');
INSERT INTO `sysdict` VALUES ('894b53ecb9394edbb830dac6c01096be', '37', '精简退职老职', 'SubsidySmallType', '1', '81', null, '1', '2018-11-07 17:10:57', '民政类', '0');
INSERT INTO `sysdict` VALUES ('8ae80c0be3bd4693a1f794f801c338c1', '3', '优抚金', 'SubsidySmallType', '1', '30', null, '1', '2018-09-19 11:13:33', '民政救助', '0');
INSERT INTO `sysdict` VALUES ('8c89200f891d4b5cb80d230ec1354512', '1', '服装', 'EBusinessCategory', '电商平台抓取分类', '1', null, '1', '2018-12-20 17:36:12', '电商平台抓取分类', '0');
INSERT INTO `sysdict` VALUES ('8fc65c8961aa47dfa77bd1f2dacba6f1', '1', '关于我们', 'wywm', '乡村振兴是重庆移动12582基地面向政府倾力打造的社会综合管理的数据录入服务平台，为政府工作人员提供日常办公工具，实时记录辖区内居民信息、房屋土地信息、产业信息等。', '1', null, '1', '2018-10-18 17:08:08', '11', '0');
INSERT INTO `sysdict` VALUES ('8fc65c8961aa47dfa77bd1f2dacba6f2', '28', '星级农家乐补贴', 'SubsidySmallType', '4', '280', null, '1', '2018-09-19 11:30:58', '产业类', '0');
INSERT INTO `sysdict` VALUES ('928ecc190b664c4fb0fa2b48cbaa7212', '10', '残疾人无障碍改造补助', 'SubsidySmallType', '1', '100', null, '1', '2018-09-19 11:17:06', '残疾补助', '0');
INSERT INTO `sysdict` VALUES ('9373cc1c03924c829834b20ba7a8dc8c', '24', '高山水果采摘园', 'SubsidySmallType', '4', '240', null, '1', '2018-09-19 11:29:10', '产业类', '0');
INSERT INTO `sysdict` VALUES ('98b947beb622436bb764fe3a585c9f69', '32', '失子女特殊家庭补助', 'SubsidySmallType', '6', '320', null, '1', '2018-09-19 11:33:23', '卫生计生类', '0');
INSERT INTO `sysdict` VALUES ('a33a98d528c84f1e9a573ba840d66c59', '22', '便道建设', 'SubsidySmallType', '4', '220', null, '1', '2018-09-19 11:28:06', '产业类', '0');
INSERT INTO `sysdict` VALUES ('a5b1b678999646109f85dea2adc75918', '38', '丧葬补助', 'SubsidySmallType', '1', '92', null, '1', '2018-11-07 17:13:53', '民政类', '0');
INSERT INTO `sysdict` VALUES ('ac37d615906348969cafeb4f1f05bd0d', '16', '创业担保贴息补助', 'SubsidySmallType', '2', '160', null, '1', '2018-09-19 11:20:48', '保险类', '0');
INSERT INTO `sysdict` VALUES ('aed24988bc944a86972487e1f584961c', '3', '农业类', 'SubsidyBigType', '农业类', '0', null, '1', '2018-09-18 16:37:02', null, '0');
INSERT INTO `sysdict` VALUES ('b3aa09e7bdee477a820dcb394c0072fa', '14', '特殊人员参保补助', 'SubsidySmallType', '2', '140', null, '1', '2018-09-19 11:20:16', '保险类', '0');
INSERT INTO `sysdict` VALUES ('b5724e29649e44cdb7d311c3b2e45daa', '30', '公益林补偿', 'SubsidySmallType', '5', '300', null, '1', '2018-09-19 11:31:44', '林业类', '0');
INSERT INTO `sysdict` VALUES ('bb675e515730496397dd3a9a3e844836', '11', '残疾人种养大户补助', 'SubsidySmallType', '1', '110', null, '1', '2018-09-19 11:17:24', '残疾补助', '0');
INSERT INTO `sysdict` VALUES ('bf7970ba3ee04f32b448d9dca721da77', '1', '淘宝平台', 'EBusinessPlatform', '电商抓取平台', '1', null, '1', '2018-12-20 17:21:44', '电商抓取平台', '0');
INSERT INTO `sysdict` VALUES ('c03d9ae159aa4d89bcd0d8690225f901', 'q2', '问题2：乡村振兴APP可批量导入数据吗？', 'sys_cjwt', '不能，乡村振兴APP供工作人员外出办公时依照填写内容实时录入数据，不能批量导入。', '0', null, '1', '2018-10-22 11:04:10', null, '0');
INSERT INTO `sysdict` VALUES ('c481f811bd97406fa22a8968bc04f326', '123123', '汽水', 'GoodsTagType', '用于商品打标', '21', null, '1', '2019-02-18 14:45:40', '用于商品打标', '0');
INSERT INTO `sysdict` VALUES ('c4dd71388adb4326be9cea61002b01f9', '15', '独生子女父母领保补助', 'SubsidySmallType', '2', '150', null, '1', '2018-09-19 11:20:31', '保险类', '0');
INSERT INTO `sysdict` VALUES ('c5a42c318c60459397aecf54a508e13e', '12', '城乡居民养老保险', 'SubsidySmallType', '2', '120', null, '1', '2018-09-19 11:17:58', '保险类', '0');
INSERT INTO `sysdict` VALUES ('cf3971be929247f4be313cd8f1c67dd1', '11111', '让一个突然大锅饭大锅饭大锅饭事故的发生关怀和风格和是否合规合', 'SubsidySmallType', '6', '360', null, '1', '2018-12-25 10:07:23', '卫生计生类', '1');
INSERT INTO `sysdict` VALUES ('d033704b9f0b46e6a641638ac84bb5b0', '13', '城乡居民医疗保险', 'SubsidySmallType', '2', '130', null, '1', '2018-09-19 11:19:59', '保险类', '0');
INSERT INTO `sysdict` VALUES ('dcd67565760f435ba0d9a5bf4e8cbdf7', '2', '苏宁易购平台', 'EBusinessPlatform', '电商抓取平台', '2', null, '1', '2018-12-20 17:34:26', '电商抓取平台', '0');
INSERT INTO `sysdict` VALUES ('df18d9ea76cc4f4d8af798cd4d075496', 'etewt', 'etest', 'SubsidySmallType', '6', '360', null, '1', '2019-02-15 11:24:41', '卫生计生类', '1');
INSERT INTO `sysdict` VALUES ('e45e9f8250f2404da4278edf0fa51273', 'meat', '肉类', 'GoodsTagType', '用于商品打标', '11', null, '1', '2019-02-15 16:00:13', '用于商品打标', '0');
INSERT INTO `sysdict` VALUES ('e996f01ed9b04c51a88a686334230a49', '31', '60岁独儿独女双女扶助', 'SubsidySmallType', '6', '310', null, '1', '2018-09-19 11:32:07', '卫生计生类', '0');
INSERT INTO `sysdict` VALUES ('edc5f5065dac45c9946b45041aa39830', '26', '千亩林果基地', 'SubsidySmallType', '4', '260', null, '1', '2018-09-19 11:30:26', '产业类', '0');
INSERT INTO `sysdict` VALUES ('ef151c6257114a299d16d111fe6fe747', '8', '阳光家园补贴', 'SubsidySmallType', '1', '80', null, '1', '2018-09-19 11:14:51', '残疾补助', '0');
INSERT INTO `sysdict` VALUES ('f19e7f760bf946db9c3d41350b56f515', '1', 'banner广告位', 'banner_group', '幻灯片分组', '1', '0', '1', '2018-08-17 11:28:03', null, '0');
INSERT INTO `sysdict` VALUES ('f7b37a8aec1b40b89d1795284003b482', '4', '临时救助', 'SubsidySmallType', '1', '40', null, '1', '2018-09-19 11:13:48', '民政救助', '0');

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
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of sysmenumodel
-- ----------------------------
INSERT INTO `sysmenumodel` VALUES ('1', 'SystemSet', '系统设置', 'fa-cog', null, '0', '1001', null, '\0', '0', '0');
INSERT INTO `sysmenumodel` VALUES ('2', 'MenuSet', '菜单权限设置', null, '/SysSet/MenuSet', '1', '1002', '菜单权限设置', null, '1', '0');
INSERT INTO `sysmenumodel` VALUES ('3', 'MenuAdmin', '菜单配置', null, '/SysSet/MenuAdmin', '2', '1003', '菜单配置', null, '1', '0');
INSERT INTO `sysmenumodel` VALUES ('5', 'MenuUsers', '管理员列表', 'fa-users', '/SysSet/MenuUsers', '3', '1004', null, '\0', '1', '0');
INSERT INTO `sysmenumodel` VALUES ('6', 'ContentManage', '内容管理', 'fa-coffee', null, '4', '2001', null, '\0', '0', '0');
INSERT INTO `sysmenumodel` VALUES ('7', 'ContentEditPage', '内容编辑', null, '/Content/ContentEdit', '5', '2002', null, '\0', '6', '0');
INSERT INTO `sysmenumodel` VALUES ('8', 'ContentEditList', '内容列表', null, '/Content/ContentList', '6', '2003', null, '\0', '6', '0');
INSERT INTO `sysmenumodel` VALUES ('9', 'SysDicManage', '字典配置', null, '/SysSet/SysDicManage', '7', '1005', null, '\0', '1', '0');

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
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC;

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
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC;

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
INSERT INTO `sysusermenu` VALUES ('9', '1', '1', '1');
INSERT INTO `sysusermenu` VALUES ('10', '1', '2', '1');
INSERT INTO `sysusermenu` VALUES ('11', '1', '3', '1');
INSERT INTO `sysusermenu` VALUES ('12', '1', '5', '1');
INSERT INTO `sysusermenu` VALUES ('13', '1', '6', '1');
INSERT INTO `sysusermenu` VALUES ('14', '1', '7', '1');
INSERT INTO `sysusermenu` VALUES ('15', '1', '1', '1');
INSERT INTO `sysusermenu` VALUES ('16', '1', '2', '1');
INSERT INTO `sysusermenu` VALUES ('17', '1', '3', '1');
INSERT INTO `sysusermenu` VALUES ('18', '1', '5', '1');
INSERT INTO `sysusermenu` VALUES ('19', '1', '6', '1');
INSERT INTO `sysusermenu` VALUES ('20', '1', '7', '1');
INSERT INTO `sysusermenu` VALUES ('21', '1', '8', '1');
INSERT INTO `sysusermenu` VALUES ('22', '1', '1', '0');
INSERT INTO `sysusermenu` VALUES ('23', '1', '2', '0');
INSERT INTO `sysusermenu` VALUES ('24', '1', '3', '0');
INSERT INTO `sysusermenu` VALUES ('25', '1', '5', '0');
INSERT INTO `sysusermenu` VALUES ('26', '1', '9', '0');
INSERT INTO `sysusermenu` VALUES ('27', '1', '6', '0');
INSERT INTO `sysusermenu` VALUES ('28', '1', '7', '0');
INSERT INTO `sysusermenu` VALUES ('29', '1', '8', '0');
