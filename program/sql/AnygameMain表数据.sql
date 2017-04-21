
-- SET FOREIGN_KEY_CHECKS=0;

DROP DATABASE IF EXISTS AnygameMain;
CREATE DATABASE AnygameMain;
USE AnygameMain;

-- ----------------------------
-- Table structure for PlatformAccount
-- ----------------------------
DROP TABLE IF EXISTS `PlatformAccount`;
CREATE TABLE `PlatformAccount` (
  `Id` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '平台id',
  `PhoneId` varchar(50) DEFAULT NULL COMMENT '手机id信息（初次创建的手机id）',
  `UId` varchar(50) DEFAULT NULL COMMENT '每个平台的平台id',
  `ThreeUId` varchar(50) DEFAULT NULL COMMENT '第三方平台id(实际是主登陆id）',
  `PlatformId` int(11) DEFAULT NULL COMMENT '平台id',
  `ThreePlatformId` int(11) DEFAULT NULL COMMENT '第三方平台id（棱镜，sharesdk的id）',
  `GameZoneIds` varchar(50) DEFAULT NULL,
  `IsSuperMan` tinyint(4) DEFAULT '0',
  `CreateTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `NewIndex1` (`ThreeUId`,`ThreePlatformId`),
  KEY `NewIndex2` (`UId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;


-- ----------------------------
-- Table structure for GameAccount
-- ----------------------------
DROP TABLE IF EXISTS `GameAccount`;
CREATE TABLE `GameAccount` (
  `Id` int(22) NOT NULL AUTO_INCREMENT COMMENT '玩家id',
  `PlatformAccountId` int(22) DEFAULT NULL COMMENT '平台账号Id',
  `PlatformId` int(11) DEFAULT NULL COMMENT '平台id，只用于存储',
  `ZoneId` int(22) DEFAULT NULL COMMENT '服务器分区（服务器id)',
  `IsSuperMan` int(11) DEFAULT NULL COMMENT '是否是GM账号',
  `CharacterName` varchar(60) DEFAULT NULL COMMENT '角色名',
  `CreateTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `IsFrozen` int(11) DEFAULT '0',
  `ChannelId` varchar(32) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `NewIndex1` (`PlatformAccountId`,`ZoneId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;


-- ----------------------------
-- Table structure for Account
-- ----------------------------
DROP TABLE IF EXISTS `Account`;
CREATE TABLE `Account` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '账号id',
  `Name` varchar(50) NOT NULL COMMENT '账号名字',
  `Password` varchar(50) NOT NULL COMMENT '账号密码',
  `CreateTime` datetime NOT NULL COMMENT '账号创建时间',
  `ServerId` int(11) DEFAULT NULL COMMENT '服务器id',
  `PlayerId` int(11) DEFAULT NULL COMMENT '玩家id',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Account
-- ----------------------------
-- INSERT INTO `Account` VALUES ('1', 'test001', '111111', '2016-10-30 22:36:01', '1001', '1');
-- INSERT INTO `Account` VALUES ('2', 'test002', '111111', '2016-10-30 22:36:44', '1001', null);