
--SET FOREIGN_KEY_CHECKS=0;


-- ----------------------------
-- Table structure for GameZone
-- ----------------------------
DROP TABLE IF EXISTS `GameZone`;
CREATE TABLE `GameZone` (
  `Id` int(11) NOT NULL,
  `Name` tinytext,
  `Servers` varchar(255) DEFAULT NULL,
  `Mods` int(11) DEFAULT NULL,
  `Type` int(11) DEFAULT '0',
  `Status` int(11) DEFAULT '0',
  `IsOpen` int(11) DEFAULT '0' COMMENT '普通玩家是否可见',
  `LimitActive` int(11) DEFAULT '0' COMMENT '限制注册',
  `ChannelBlacklist` longtext COMMENT '渠道黑名单',
  `ChannelWhitelist` longtext COMMENT '渠道白名单',
  `PhonePlatformType` int(11) DEFAULT '0' COMMENT '手机设备平台类型 0安卓 1iphone',
  `PlatformBlacklist` mediumtext COMMENT 'SDK黑名单',
  `PlatformWhitelist` mediumtext COMMENT 'SDK白名单',
  `VersionBlacklist` mediumtext COMMENT '版本黑名单',
  `VersionWhitelist` mediumtext COMMENT '版本白名单',
  `MergeGameZoneId` int(11) DEFAULT '0' COMMENT '合服id',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of GameZone
-- ----------------------------
INSERT INTO `GameZone` VALUES ('1', '神之秘境', '0', '0', '0', '1', '0', '0', '', '', '0', '', '', '', '', '0');
INSERT INTO `GameZone` VALUES ('2', '时空走廊', '0', '0', '0', '-1', '0', '0', '', '', '0', '', '', '', '', '0');
INSERT INTO `GameZone` VALUES ('3', '无尽沙漠', '0', '0', '0', '-1', '0', '0', '', '', '0', '', '', '', '', '0');
INSERT INTO `GameZone` VALUES ('4', '虚空荒原', '0', '0', '0', '-1', '0', '0', '', '', '0', '', '', '', '', '0');
INSERT INTO `GameZone` VALUES ('5', '以太空间', '0', '0', '0', '-1', '0', '0', '', '', '0', '', '', '', '', '0');
INSERT INTO `GameZone` VALUES ('6', '永恒神殿', '0', '0', '0', '-1', '0', '0', '', '', '0', '', '', '', '', '0');
INSERT INTO `GameZone` VALUES ('7', '奥创纪元', '0', '0', '0', '-1', '0', '0', '', '', '0', '', '', '', '', '0');
INSERT INTO `GameZone` VALUES ('8', '苍蓝平原', '0', '0', '0', '-1', '0', '0', '', '', '0', '', '', '', '', '1');
INSERT INTO `GameZone` VALUES ('9', '无尽之海', '0', '0', '0', '-1', '0', '0', '', '', '0', '', '', '', '', '1');
INSERT INTO `GameZone` VALUES ('10', '魔铁山脉', '0', '0', '0', '-1', '0', '0', '', '', '0', '', '', '', '', '0');

-- ----------------------------
-- Table structure for ServerInfo
-- ----------------------------
DROP TABLE IF EXISTS `ServerInfo`;
CREATE TABLE `ServerInfo` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ZoneId` int(11) DEFAULT '1',
  `Mod` int(11) DEFAULT '0',
  `ServerName` varchar(255) NOT NULL,
  `MerchantId` int(11) DEFAULT NULL,
  `Domain` varchar(255) DEFAULT NULL,
  `DatabaseHost` varchar(255) DEFAULT NULL,
  `DatabaseName` varchar(255) DEFAULT NULL,
  `AnalysisDatabaseHost` varchar(255) DEFAULT NULL,
  `AnalysisDatabaseName` varchar(255) DEFAULT NULL,
  `ServerHost` varchar(255) DEFAULT NULL,
  `ServerPort` int(11) DEFAULT NULL,
  `LanServerHost` varchar(255) DEFAULT NULL,
  `ManagerPort` int(11) DEFAULT NULL,
  `StartServer` datetime DEFAULT NULL,
  `SecureKey` varchar(255) DEFAULT NULL COMMENT '充值加密用的Key',
  `State` int(11) DEFAULT NULL,
  `ServerType` int(11) DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ServerInfo
-- ----------------------------
INSERT INTO `ServerInfo` VALUES ('1', '1', '0', 's1', '0', '', '192.168.2.83', 'anygamedb_s1', '127.0.0.1', 'AnygameAnalysis_s1', '127.0.0.1', '4601', '127.0.0.1', '9001', '2015-04-22 00:00:00', '', '0', '0');
INSERT INTO `ServerInfo` VALUES ('2', '2', '0', 's2', '0', '', '192.168.2.83', 'gamedb_s2', '127.0.0.1', 'AnyAnalysis_s2', '127.0.0.1', '4502', '127.0.0.1', '9002', '2015-04-15 00:00:00', '', '0', '0');
INSERT INTO `ServerInfo` VALUES ('3', '3', '0', 's3', '0', '', '192.168.2.83', 'gamedb_s3', '127.0.0.1', 'AnyAnalysis_s3', '127.0.0.1', '4503', '127.0.0.1', '9206', '2015-04-15 00:00:00', '', '0', '0');
INSERT INTO `ServerInfo` VALUES ('4', '4', '0', 's4', '0', '', '192.168.2.83', 'gamedb_s4', '127.0.0.1', 'AnyAnalysis_s4', '127.0.0.1', '4706', '127.0.0.1', '9206', '2015-04-15 00:00:00', '', '0', '0');
INSERT INTO `ServerInfo` VALUES ('5', '5', '0', 's5', '0', '', '192.168.2.83', 'gamedb_s5', '127.0.0.1', 'AnyAnalysis_s5', '127.0.0.1', '4505', '127.0.0.1', '9005', '2015-04-15 00:00:00', '', '0', '0');
INSERT INTO `ServerInfo` VALUES ('6', '6', '0', 's6', null, '', '192.168.2.83', 'gamedb_s6', '127.0.0.1', 'AnyAnalysis_s6', '127.0.0.1', '4506', '127.0.0.1', '9006', '2016-11-10 14:23:17', '', null, '0');
INSERT INTO `ServerInfo` VALUES ('7', '7', '0', 's7', '0', '', '192.168.2.83', 'gamedb_s7', '127.0.0.1', 'AnyAnalysis_s7', '127.0.0.1', '4508', '127.0.0.1', '9008', '0001-01-01 00:00:00', '', '0', '0');
INSERT INTO `ServerInfo` VALUES ('8', '8', '0', 's8', '0', '', '192.168.2.83', 'rr_new', '127.0.0.1', 'AnyAnalysis_s8', '127.0.0.1', '4508', '127.0.0.1', '9008', '0001-01-01 00:00:00', '', '0', '0');
INSERT INTO `ServerInfo` VALUES ('9', '9', '0', 's9', null, '', '192.168.2.83', 'gamedb_s9', '127.0.0.1', 'AnyAnalysis_s9', '127.0.0.1', '4509', '127.0.0.1', '9009', '2016-11-05 14:23:22', '', null, '0');
INSERT INTO `ServerInfo` VALUES ('10', '10', '0', 's10', '0', '', '192.168.2.83', 'gamedb_s10', '127.0.0.1', 'AnyAnalysis_s10', '127.0.0.1', '4506', '127.0.0.1', '9006', '0001-01-01 00:00:00', '', '0', '0');

-- ----------------------------
-- Table structure for Notice
-- ----------------------------
DROP TABLE IF EXISTS `Notice`;
CREATE TABLE `Notice` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Content` mediumtext,
  `PhonePlatform` int(11) DEFAULT NULL,
  `IsDefaultNotice` int(11) DEFAULT NULL,
  `PlatformIds` mediumtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

INSERT INTO `Notice` VALUES ('1', '神之秘境 服公告', '0', '1', '');
INSERT INTO `Notice` VALUES ('2', '《勇者逗饿龙》', '1', '1', '');