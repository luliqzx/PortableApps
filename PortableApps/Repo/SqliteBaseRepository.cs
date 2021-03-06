﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using Dapper;

namespace PortableApps.Repo
{
    public interface ISqLiteBaseRepository
    {
        string DbFile { get; }
        SQLiteConnection MySQLiteConnection();
        int SetupAppInfo(SQLiteConnection cnn);
        int SetupDun(SQLiteConnection cnn);
        int SetupMakkebun(SQLiteConnection cnn);
        int SetupParlimen(SQLiteConnection cnn);
        int SetupVariables(SQLiteConnection cnn);
        int SetupDaerah(SQLiteConnection cnn);
        int SetupSemakTapak(SQLiteConnection cnn);

        int SetupVariableSetting(SQLiteConnection cnn);

        int SetupTBangsa(SQLiteConnection cnn);

        bool CheckExistingDB(string DbFile);

        int ResetDefault(SQLiteConnection cnn);
    }
    public class SqLiteBaseRepository : CommonRepo, ISqLiteBaseRepository
    {
        public SQLiteConnection MySQLiteConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }

        public int SetupAppInfo(SQLiteConnection cnn)
        {
            int i = 0;
            using (SQLiteCommand cmd = cnn.CreateCommand())
            {
                #region Query
                cmd.CommandText = @"
 DROP TABLE IF  EXISTS `appinfo`;
-- Dumping structure for table a1_tsspk1511.appinfo
CREATE TABLE IF NOT EXISTS `appinfo` (
  `id` int(11) NOT NULL PRIMARY KEY,
  `refno_new` varchar(100) COLLATE NOCASE DEFAULT NULL,
  `nama` varchar(100) COLLATE NOCASE NOT NULL,
  --`type_id` int(11) NOT NULL,
  `icno` varchar(100) COLLATE NOCASE NOT NULL,
  `nolesen` varchar(30) COLLATE NOCASE DEFAULT NULL,
  `bangsa` varchar(30) COLLATE NOCASE DEFAULT NULL,
  `addr1` varchar(100) COLLATE NOCASE DEFAULT NULL,
  `addr2` varchar(100) COLLATE NOCASE DEFAULT NULL,
  `addr3` varchar(100) COLLATE NOCASE DEFAULT NULL,
  `bandar` varchar(100) COLLATE NOCASE NOT NULL,
  `daerah` varchar(30) COLLATE NOCASE NOT NULL,
  `dun` varchar(100) COLLATE NOCASE NOT NULL,
  `parlimen` int(11) DEFAULT NULL,
  `poskod` varchar(5) COLLATE NOCASE DEFAULT NULL,
  `negeri` varchar(20) COLLATE NOCASE DEFAULT NULL,
  `hometel` varchar(20) COLLATE NOCASE DEFAULT NULL,
  `officetel` varchar(20) COLLATE NOCASE DEFAULT NULL,
  `hptel` varchar(20) COLLATE NOCASE DEFAULT NULL,
  `faks` varchar(20) COLLATE NOCASE DEFAULT NULL,
  `email` varchar(50) COLLATE NOCASE DEFAULT NULL,
  --`kelompok` varchar(5) COLLATE NOCASE DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `createdby` varchar(100) COLLATE NOCASE DEFAULT NULL,
  `appdate` varchar(50) COLLATE NOCASE DEFAULT NULL,
  --`semak_tapak` varchar(1) COLLATE NOCASE DEFAULT NULL,
  --`keputusan` varchar(20) COLLATE NOCASE DEFAULT NULL,
  --`sts_bck` int(11) NOT NULL,
  --`status` int(11) NOT NULL,
  --`date_approved` datetime NOT NULL,
  --`approved_by` varchar(255) COLLATE NOCASE NOT NULL,
  --`sop` tinyint(4) NOT NULL,

    newrefno varchar(100) COLLATE NOCASE NULL,
    syncdate datetime NULL
);";
                #endregion

                cmd.CommandType = CommandType.Text;
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }

        public int SetupDun(SQLiteConnection cnn)
        {
            int i = 0;
            using (SQLiteCommand cmd = cnn.CreateCommand())
            {
                //                cmd.CommandText = @"
                //                                CREATE TABLE `dun` (`id` int(11) DEFAULT NULL PRIMARY KEY ,`kod_negeri` varchar(10) DEFAULT NULL 
                //                                    ,`kod_dun` varchar(10) DEFAULT NULL ,`dun_desc` varchar(100) DEFAULT NULL )
                //                           ";
                #region Create & Insert Dun
                cmd.CommandText = @"
 DROP TABLE IF  EXISTS `dun`;

                                        -- --------------------------------------------------------
                                        -- Host:                         127.0.0.1
                                        -- Server version:               5.6.25 - MySQL Community Server (GPL)
                                        -- Server OS:                    Win32
                                        -- HeidiSQL Version:             9.2.0.4947
                                        -- --------------------------------------------------------
                                        -- Dumping structure for table a1_tsspk1511.dun
                                        -- Dumping structure for table a1_tsspk1511.dun
                                        CREATE TABLE IF NOT EXISTS `dun` (
                                            `id` int(10) NOT NULL PRIMARY KEY,
                                            `kod_negeri` varchar(10) COLLATE NOCASE NOT NULL,
                                            `kod_dun` varchar(10) COLLATE NOCASE NOT NULL,
                                            `dun_desc` varchar(100) COLLATE NOCASE NOT NULL
                                        );

                                        DELETE from dun;

                                        -- Dumping data for table a1_tsspk1511.dun: 590 rows
                                        INSERT INTO `dun` (`id`, `kod_negeri`, `kod_dun`, `dun_desc`) VALUES
	                                        (2, 'PL', 'PL_N1', 'TITI TINGGI'),
	                                        (3, 'PL', 'PL_N2', 'BESERI'),
	                                        (4, 'PL', 'PL_N3', 'CHUPING'),
	                                        (5, 'PL', 'PL_N4', 'MATA AYER'),
	                                        (6, 'PL', 'PL_N5', 'SANTAN'),
	                                        (7, 'PL', 'PL_N6', 'BINTONG'),
	                                        (8, 'PL', 'PL_N7', 'SENA'),
	                                        (9, 'PL', 'PL_N8', 'INDERA KAYANGAN'),
	                                        (10, 'PL', 'PL_N9', 'KUALA PERLIS'),
	                                        (11, 'PL', 'PL_N10', 'KAYANG'),
	                                        (12, 'PL', 'PL_N11', 'PAUH'),
	                                        (13, 'PL', 'PL_N12', 'TAMBUN TULANG'),
	                                        (14, 'PL', 'PL_N13', 'GUAR SANJI'),
	                                        (15, 'PL', 'PL_N14', 'SIMPANG EMPAT'),
	                                        (16, 'PL', 'PL_N15', 'SANGLANG'),
	                                        (17, 'KDH', 'KDH_N1', 'AYER HANGAT'),
	                                        (18, 'KDH', 'KDH_N2', 'KUAH'),
	                                        (19, 'KDH', 'KDH_N3', 'KOTA SIPUTEH'),
	                                        (20, 'KDH', 'KDH_N4', 'AYER HITAM'),
	                                        (21, 'KDH', 'KDH_N5', 'BUKIT KAYU HITAM'),
	                                        (22, 'KDH', 'KDH_N6', 'JITRA'),
	                                        (23, 'KDH', 'KDH_N7', 'KUALA NERANG'),
	                                        (24, 'KDH', 'KDH_N8', 'PEDU'),
	                                        (25, 'KDH', 'KDH_N9', 'BUKIT LADA'),
	                                        (26, 'KDH', 'KDH_N10', 'BUKIT PINANG'),
	                                        (27, 'KDH', 'KDH_N11', 'DERGA'),
	                                        (28, 'KDH', 'KDH_N12', 'BAKAR BATA'),
	                                        (29, 'KDH', 'KDH_N13', 'KOTA DARUL AMAN'),
	                                        (30, 'KDH', 'KDH_N14', 'ALOR MENGKUDU'),
	                                        (31, 'KDH', 'KDH_N15', 'ANAK BUKIT'),
	                                        (32, 'KDH', 'KDH_N16', 'KUBANG ROTAN'),
	                                        (33, 'KDH', 'KDH_N17', 'PENGKALAN KUNDOR'),
	                                        (34, 'KDH', 'KDH_N18', 'TOKAI'),
	                                        (35, 'KDH', 'KDH_N19', 'SUNGAI TIANG'),
	                                        (36, 'KDH', 'KDH_N20', 'SUNGAI LIMAU'),
	                                        (37, 'KDH', 'KDH_N21', 'GUAR CHEMPEDAK'),
	                                        (38, 'KDH', 'KDH_N22', 'GURUN'),
	                                        (39, 'KDH', 'KDH_N23', 'BELANTEK'),
	                                        (40, 'KDH', 'KDH_N24', 'JENERI'),
	                                        (41, 'KDH', 'KDH_N25', 'BUKIT SELAMBAU'),
	                                        (42, 'KDH', 'KDH_N26', 'TANJONG DAWAI'),
	                                        (43, 'KDH', 'KDH_N27', 'PANTAI MERDEKA'),
	                                        (44, 'KDH', 'KDH_N28', 'BAKAR ARANG'),
	                                        (45, 'KDH', 'KDH_N29', 'SIDAM'),
	                                        (46, 'KDH', 'KDH_N30', 'BAYU'),
	                                        (47, 'KDH', 'KDH_N31', 'KUPANG'),
	                                        (48, 'KDH', 'KDH_N32', 'KUALA KETIL'),
	                                        (49, 'KDH', 'KDH_N33', 'MERBAU PULAS'),
	                                        (50, 'KDH', 'KDH_N34', 'LUNAS'),
	                                        (51, 'KDH', 'KDH_N35', 'KULIM'),
	                                        (52, 'KDH', 'KDH_N36', 'BANDAR BAHARU'),
	                                        (53, 'KEL', 'KEL_N1', 'PENGKALAN KUBOR'),
	                                        (54, 'KEL', 'KEL_N2', 'KELABORAN'),
	                                        (55, 'KEL', 'KEL_N3', 'PASIR PEKAN'),
	                                        (56, 'KEL', 'KEL_N4', 'WAKAF BHARU'),
	                                        (57, 'KEL', 'KEL_N5', 'KIJANG'),
	                                        (58, 'KEL', 'KEL_N6', 'CHEMPAKA'),
	                                        (59, 'KEL', 'KEL_N7', 'PANCHOR'),
	                                        (60, 'KEL', 'KEL_N8', 'TANJONG MAS'),
	                                        (61, 'KEL', 'KEL_N9', 'KOTA LAMA'),
	                                        (62, 'KEL', 'KEL_N10', 'BUNUT PAYONG'),
	                                        (63, 'KEL', 'KEL_N11', 'TENDONG'),
	                                        (64, 'KEL', 'KEL_N12', 'PENGKALAN PASIR'),
	                                        (65, 'KEL', 'KEL_N13', 'CHETOK'),
	                                        (66, 'KEL', 'KEL_N14', 'MERANTI'),
	                                        (67, 'KEL', 'KEL_N15', 'GUAL PERIOK'),
	                                        (68, 'KEL', 'KEL_N16', 'BUKIT TUKU'),
	                                        (69, 'KEL', 'KEL_N17', 'SALOR'),
	                                        (70, 'KEL', 'KEL_N18', 'PASIR TUMBOH'),
	                                        (71, 'KEL', 'KEL_N19', 'DEMIT'),
	                                        (72, 'KEL', 'KEL_N20', 'TAWANG'),
	                                        (73, 'KEL', 'KEL_N21', 'PERUPOK'),
	                                        (74, 'KEL', 'KEL_N22', 'JELAWAT'),
	                                        (75, 'KEL', 'KEL_N23', 'MELOR'),
	                                        (76, 'KEL', 'KEL_N24', 'KADOK'),
	                                        (77, 'KEL', 'KEL_N25', 'KOK LANAS'),
	                                        (78, 'KEL', 'KEL_N26', 'BUKIT PANAU'),
	                                        (79, 'KEL', 'KEL_N27', 'GUAL IPOH'),
	                                        (80, 'KEL', 'KEL_N28', 'KEMAHANG'),
	                                        (81, 'KEL', 'KEL_N29', 'SELISING'),
	                                        (82, 'KEL', 'KEL_N30', 'LIMBONGAN'),
	                                        (83, 'KEL', 'KEL_N31', 'SEMERAK'),
	                                        (84, 'KEL', 'KEL_N32', 'GAAL'),
	                                        (85, 'KEL', 'KEL_N33', 'PULAI CHONDONG'),
	                                        (86, 'KEL', 'KEL_N34', 'TEMANGAN'),
	                                        (87, 'KEL', 'KEL_N35', 'KEMUNING'),
	                                        (88, 'KEL', 'KEL_N36', 'BUKIT BUNGA'),
	                                        (89, 'KEL', 'KEL_N37', 'AIR LANAS'),
	                                        (90, 'KEL', 'KEL_N38', 'KUALA BALAH'),
	                                        (91, 'KEL', 'KEL_N39', 'MENGKEBANG'),
	                                        (92, 'KEL', 'KEL_N40', 'GUCHIL'),
	                                        (93, 'KEL', 'KEL_N41', 'MANEK URAI'),
	                                        (94, 'KEL', 'KEL_N42', 'DABONG'),
	                                        (95, 'KEL', 'KEL_N43', 'NENGGIRI'),
	                                        (96, 'KEL', 'KEL_N44', 'PALOH'),
	                                        (97, 'KEL', 'KEL_N45', 'GALAS'),
	                                        (98, 'TRG', 'TRG_N1', 'KUALA BESUT'),
	                                        (99, 'TRG', 'TRG_N2', 'KOTA PUTERA'),
	                                        (100, 'TRG', 'TRG_N3', 'JERTIH'),
	                                        (101, 'TRG', 'TRG_N4', 'HULU BESUT'),
	                                        (102, 'TRG', 'TRG_N5', 'JABI'),
	                                        (103, 'TRG', 'TRG_N6', 'PERMAISURI'),
	                                        (104, 'TRG', 'TRG_N7', 'LANGKAP'),
	                                        (105, 'TRG', 'TRG_N8', 'BATU RAKIT'),
	                                        (106, 'TRG', 'TRG_N9', 'TEPUH'),
	                                        (107, 'TRG', 'TRG_N10', 'TELUK PASU'),
	                                        (108, 'TRG', 'TRG_N11', 'SEBERANG TAKIR'),
	                                        (109, 'TRG', 'TRG_N12', 'BUKIT TUNGGAL'),
	                                        (110, 'TRG', 'TRG_N13', 'WAKAF MEMPELAM'),
	                                        (111, 'TRG', 'TRG_N14', 'BANDAR'),
	                                        (112, 'TRG', 'TRG_N15', 'LADANG'),
	                                        (113, 'TRG', 'TRG_N16', 'BATU BURUK'),
	                                        (114, 'TRG', 'TRG_N17', 'ALUR LIMBAT'),
	                                        (115, 'TRG', 'TRG_N18', 'BUKIT PAYUNG'),
	                                        (116, 'TRG', 'TRG_N19', 'RU RENDANG'),
	                                        (117, 'TRG', 'TRG_N20', 'PENGKALAN BERANGAN'),
	                                        (118, 'TRG', 'TRG_N21', 'TELEMUNG'),
	                                        (119, 'TRG', 'TRG_N22', 'MANIR'),
	                                        (120, 'TRG', 'TRG_N23', 'KUALA BERANG'),
	                                        (121, 'TRG', 'TRG_N24', 'AJIL'),
	                                        (122, 'TRG', 'TRG_N25', 'BUKIT BESI'),
	                                        (123, 'TRG', 'TRG_N26', 'RANTAU ABANG'),
	                                        (124, 'TRG', 'TRG_N27', 'SURA'),
	                                        (125, 'TRG', 'TRG_N28', 'PAKA'),
	                                        (126, 'TRG', 'TRG_N29', 'KEMASIK'),
	                                        (127, 'TRG', 'TRG_N30', 'KIJAL'),
	                                        (128, 'TRG', 'TRG_N31', 'CUKAI'),
	                                        (129, 'TRG', 'TRG_N32', 'AIR PUTIH'),
	                                        (130, 'PP', 'PP_N1', 'PENAGA'),
	                                        (131, 'PP', 'PP_N2', 'BERTAM'),
	                                        (132, 'PP', 'PP_N3', 'PINANG TUNGGAL'),
	                                        (133, 'PP', 'PP_N4', 'PERMATANG BERANGAN'),
	                                        (134, 'PP', 'PP_N5', 'SUNGAI DUA'),
	                                        (135, 'PP', 'PP_N6', 'TELOK AYER TAWAR'),
	                                        (136, 'PP', 'PP_N7', 'SUNGAI PUYU'),
	                                        (137, 'PP', 'PP_N8', 'BAGAN JERMAL'),
	                                        (138, 'PP', 'PP_N9', 'BAGAN DALAM'),
	                                        (139, 'PP', 'PP_N10', 'SEBERANG JAYA'),
	                                        (140, 'PP', 'PP_N11', 'PERMATANG PASIR'),
	                                        (141, 'PP', 'PP_N12', 'PENANTI'),
	                                        (142, 'PP', 'PP_N13', 'BERAPIT'),
	                                        (143, 'PP', 'PP_N14', 'MACHANG BUBUK'),
	                                        (144, 'PP', 'PP_N15', 'PADANG LALANG'),
	                                        (145, 'PP', 'PP_N16', 'PERAI'),
	                                        (146, 'PP', 'PP_N17', 'BUKIT TENGAH'),
	                                        (147, 'PP', 'PP_N18', 'BUKIT TAMBUN'),
	                                        (148, 'PP', 'PP_N19', 'JAWI'),
	                                        (149, 'PP', 'PP_N20', 'SUNGAI BAKAP'),
	                                        (150, 'PP', 'PP_N21', 'SUNGAI ACHEH'),
	                                        (151, 'PP', 'PP_N22', 'TANJONG BUNGA'),
	                                        (152, 'PP', 'PP_N23', 'AIR PUTIH'),
	                                        (153, 'PP', 'PP_N24', 'KEBUN BUNGA'),
	                                        (154, 'PP', 'PP_N25', 'PULAU TIKUS'),
	                                        (155, 'PP', 'PP_N26', 'PADANG KOTA'),
	                                        (156, 'PP', 'PP_N27', 'PENGKALAN KOTA'),
	                                        (157, 'PP', 'PP_N28', 'KOMTAR'),
	                                        (158, 'PP', 'PP_N29', 'DATOK KERAMAT'),
	                                        (159, 'PP', 'PP_N30', 'SUNGAI PINANG'),
	                                        (160, 'PP', 'PP_N31', 'BATU LANCANG'),
	                                        (161, 'PP', 'PP_N32', 'SERI DELIMA'),
	                                        (162, 'PP', 'PP_N33', 'AIR ITAM'),
	                                        (163, 'PP', 'PP_N34', 'PAYA TERUBONG'),
	                                        (164, 'PP', 'PP_N35', 'BATU UBAN'),
	                                        (165, 'PP', 'PP_N36', 'PANTAI JEREJAK'),
	                                        (166, 'PP', 'PP_N37', 'BATU MAUNG'),
	                                        (167, 'PP', 'PP_N38', 'BAYAN LEPAS'),
	                                        (168, 'PP', 'PP_N39', 'PULAU BETONG'),
	                                        (169, 'PP', 'PP_N40', 'TELOK BAHANG'),
	                                        (170, 'PRK', 'PRK_N1', 'PENGKALAN HULU'),
	                                        (171, 'PRK', 'PRK_N2', 'TEMENGOR'),
	                                        (172, 'PRK', 'PRK_N3', 'KENERING'),
	                                        (173, 'PRK', 'PRK_N4', 'KOTA TAMPAN'),
	                                        (174, 'PRK', 'PRK_N5', 'SELAMA'),
	                                        (175, 'PRK', 'PRK_N6', 'KUBU GAJAH'),
	                                        (176, 'PRK', 'PRK_N7', 'BATU KURAU'),
	                                        (177, 'PRK', 'PRK_N8', 'TITI SERONG'),
	                                        (178, 'PRK', 'PRK_N9', 'KUALA KURAU'),
	                                        (179, 'PRK', 'PRK_N10', 'ALOR PONGSU'),
	                                        (180, 'PRK', 'PRK_N11', 'GUNONG SEMANGGOL'),
	                                        (181, 'PRK', 'PRK_N12', 'SELINSING'),
	                                        (182, 'PRK', 'PRK_N13', 'KUALA SAPETANG'),
	                                        (183, 'PRK', 'PRK_N14', 'CHANGKAT JERING'),
	                                        (184, 'PRK', 'PRK_N15', 'TRONG'),
	                                        (185, 'PRK', 'PRK_N16', 'KAMUNTING'),
	                                        (186, 'PRK', 'PRK_N17', 'POKOK ASSAM'),
	                                        (187, 'PRK', 'PRK_N18', 'AULONG'),
	                                        (188, 'PRK', 'PRK_N19', 'CHENDEROH'),
	                                        (189, 'PRK', 'PRK_N20', 'LUBOK MERBAU'),
	                                        (190, 'PRK', 'PRK_N21', 'LINTANG'),
	                                        (191, 'PRK', 'PRK_N22', 'JALONG'),
	                                        (192, 'PRK', 'PRK_N23', 'MANJOI'),
	                                        (193, 'PRK', 'PRK_N24', 'HULU KINTA'),
	                                        (194, 'PRK', 'PRK_N25', 'CANNING'),
	                                        (195, 'PRK', 'PRK_N26', 'TEBING TINGGI'),
	                                        (196, 'PRK', 'PRK_N27', 'PASIR PINJI'),
	                                        (197, 'PRK', 'PRK_N28', 'BERCHAM'),
	                                        (198, 'PRK', 'PRK_N29', 'KEPAYANG'),
	                                        (199, 'PRK', 'PRK_N30', 'BUNTONG'),
	                                        (200, 'PRK', 'PRK_N31', 'JELAPANG'),
	                                        (201, 'PRK', 'PRK_N32', 'MENGLEMBU'),
	                                        (202, 'PRK', 'PRK_N33', 'TRONOH'),
	                                        (203, 'PRK', 'PRK_N34', 'BUKIT CHANDAN'),
	                                        (204, 'PRK', 'PRK_N35', 'MANONG'),
	                                        (205, 'PRK', 'PRK_N36', 'PENGKALAN BAHARU'),
	                                        (206, 'PRK', 'PRK_N37', 'PANTAI REMIS'),
	                                        (207, 'PRK', 'PRK_N38', 'BELANJA'),
	                                        (208, 'PRK', 'PRK_N39', 'BOTA'),
	                                        (209, 'PRK', 'PRK_N40', 'MALIM NAWAR'),
	                                        (210, 'PRK', 'PRK_N41', 'KERANJI'),
	                                        (211, 'PRK', 'PRK_N42', 'TUALANG SEKAH'),
	                                        (212, 'PRK', 'PRK_N43', 'SUNGAI RAPAT'),
	                                        (213, 'PRK', 'PRK_N44', 'SIMPANG PULAI'),
	                                        (214, 'PRK', 'PRK_N45', 'TEJA'),
	                                        (215, 'PRK', 'PRK_N46', 'CHENDERIANG'),
	                                        (216, 'PRK', 'PRK_N47', 'AYER KUNING'),
	                                        (217, 'PRK', 'PRK_N48', 'SUNGAI MANIK'),
	                                        (218, 'PRK', 'PRK_N49', 'KAMPONG GAJAH'),
	                                        (219, 'PRK', 'PRK_N50', 'SITIAWAN'),
	                                        (220, 'PRK', 'PRK_N51', 'PASIR PANJANG'),
	                                        (221, 'PRK', 'PRK_N52', 'PANGKOR'),
	                                        (222, 'PRK', 'PRK_N53', 'RUNGKUP'),
	                                        (223, 'PRK', 'PRK_N54', 'HUTAN MELINTANG'),
	                                        (224, 'PRK', 'PRK_N55', 'PASIR BEDAMAR'),
	                                        (225, 'PRK', 'PRK_N56', 'CHANGKAT JONG'),
	                                        (226, 'PRK', 'PRK_N57', 'SUNGKAI'),
	                                        (227, 'PRK', 'PRK_N58', 'SLIM'),
	                                        (228, 'PRK', 'PRK_N59', 'BEHRANG'),
	                                        (229, 'PHG', 'PHG_N1', 'TANAH RATA'),
	                                        (230, 'PHG', 'PHG_N2', 'JELAI'),
	                                        (231, 'PHG', 'PHG_N3', 'PADANG TENGKU'),
	                                        (232, 'PHG', 'PHG_N4', 'CHEKA'),
	                                        (233, 'PHG', 'PHG_N5', 'BENTA'),
	                                        (234, 'PHG', 'PHG_N6', 'BATU TALAM'),
	                                        (235, 'PHG', 'PHG_N7', 'TRAS'),
	                                        (236, 'PHG', 'PHG_N8', 'DONG'),
	                                        (237, 'PHG', 'PHG_N9', 'TAHAN'),
	                                        (238, 'PHG', 'PHG_N10', 'DAMAK'),
	                                        (239, 'PHG', 'PHG_N11', 'PULAU TAWAR'),
	                                        (240, 'PHG', 'PHG_N12', 'BESERAH'),
	                                        (241, 'PHG', 'PHG_N13', 'SEMAMBU'),
	                                        (242, 'PHG', 'PHG_N14', 'TERUNTUM'),
	                                        (243, 'PHG', 'PHG_N15', 'TANJUNG LUMPUR'),
	                                        (244, 'PHG', 'PHG_N16', 'INDERAPURA'),
	                                        (245, 'PHG', 'PHG_N17', 'SUNGAI LEMBING'),
	                                        (246, 'PHG', 'PHG_N18', 'LEPAR'),
	                                        (247, 'PHG', 'PHG_N19', 'PANCHING'),
	                                        (248, 'PHG', 'PHG_N20', 'PULAU MANIS'),
	                                        (249, 'PHG', 'PHG_N21', 'PERAMU JAYA'),
	                                        (250, 'PHG', 'PHG_N22', 'BEBAR'),
	                                        (251, 'PHG', 'PHG_N23', 'CHIN'),
	                                        (252, 'PHG', 'PHG_N24', 'LUIT'),
	                                        (253, 'PHG', 'PHG_N25', 'KUALA SENTUL'),
	                                        (254, 'PHG', 'PHG_N26', 'CHENOR'),
	                                        (255, 'PHG', 'PHG_N27', 'JENDERAK'),
	                                        (256, 'PHG', 'PHG_N28', 'KERDAU'),
	                                        (257, 'PHG', 'PHG_N29', 'JENGKA'),
	                                        (258, 'PHG', 'PHG_N30', 'MENTAKAB'),
	                                        (259, 'PHG', 'PHG_N31', 'LANCHANG'),
	                                        (260, 'PHG', 'PHG_N32', 'KUALA SEMANTAN'),
	                                        (261, 'PHG', 'PHG_N33', 'BILUT'),
	                                        (262, 'PHG', 'PHG_N34', 'KETARI'),
	                                        (263, 'PHG', 'PHG_N35', 'SABAI'),
	                                        (264, 'PHG', 'PHG_N36', 'PELANGAI'),
	                                        (265, 'PHG', 'PHG_N37', 'GUAI'),
	                                        (266, 'PHG', 'PHG_N38', 'TRIANG'),
	                                        (267, 'PHG', 'PHG_N39', 'KEMAYAN'),
	                                        (268, 'PHG', 'PHG_N40', 'BUKIT IBAM'),
	                                        (269, 'PHG', 'PHG_N41', 'MUADZAM SHAH'),
	                                        (270, 'PHG', 'PHG_N42', 'TIOMAN'),
	                                        (271, 'SEL', 'SEL_N1', 'SUNGAI AIR TAWAR'),
	                                        (272, 'SEL', 'SEL_N2', 'SABAK'),
	                                        (273, 'SEL', 'SEL_N3', 'SUNGAI PANJANG'),
	                                        (274, 'SEL', 'SEL_N4', 'SEKINCHAN'),
	                                        (275, 'SEL', 'SEL_N5', 'HULU BERNAM'),
	                                        (276, 'SEL', 'SEL_N6', 'KUALA KUBU BAHARU'),
	                                        (277, 'SEL', 'SEL_N7', 'BATANG KALI'),
	                                        (278, 'SEL', 'SEL_N8', 'SUNGAI BURONG'),
	                                        (279, 'SEL', 'SEL_N9', 'PERMATANG'),
	                                        (280, 'SEL', 'SEL_N10', 'BUKIT MELAWATI'),
	                                        (281, 'SEL', 'SEL_N11', 'IJOK'),
	                                        (282, 'SEL', 'SEL_N12', 'JERAM'),
	                                        (283, 'SEL', 'SEL_N13', 'KUANG'),
	                                        (284, 'SEL', 'SEL_N14', 'RAWANG'),
	                                        (285, 'SEL', 'SEL_N15', 'TAMAN TEMPLER'),
	                                        (286, 'SEL', 'SEL_N16', 'BATU CAVES'),
	                                        (287, 'SEL', 'SEL_N17', 'GOMBAK SETIA'),
	                                        (288, 'SEL', 'SEL_N18', 'HULU KELANG'),
	                                        (289, 'SEL', 'SEL_N19', 'BUKIT ANTARABANGSA'),
	                                        (290, 'SEL', 'SEL_N20', 'LEMBAH JAYA'),
	                                        (291, 'SEL', 'SEL_N21', 'CHEMPAKA'),
	                                        (292, 'SEL', 'SEL_N22', 'TERATAI'),
	                                        (293, 'SEL', 'SEL_N23', 'DUSUN TUA'),
	                                        (294, 'SEL', 'SEL_N24', 'SEMENYIH'),
	                                        (295, 'SEL', 'SEL_N25', 'KAJANG'),
	                                        (296, 'SEL', 'SEL_N26', 'BANGI'),
	                                        (297, 'SEL', 'SEL_N27', 'BALAKONG'),
	                                        (298, 'SEL', 'SEL_N28', 'SERI KEMBANGAN'),
	                                        (299, 'SEL', 'SEL_N29', 'SERI SERDANG'),
	                                        (300, 'SEL', 'SEL_N30', 'KINRARA'),
	                                        (301, 'SEL', 'SEL_N31', 'SUBANG JAYA'),
	                                        (302, 'SEL', 'SEL_N32', 'SERI SETIA'),
	                                        (303, 'SEL', 'SEL_N33', 'TAMAN MEDAN'),
	                                        (304, 'SEL', 'SEL_N34', 'BUKIT GASING'),
	                                        (305, 'SEL', 'SEL_N35', 'KAMPUNG TUNKU'),
	                                        (306, 'SEL', 'SEL_N36', 'DAMANSARA UTAMA'),
	                                        (307, 'SEL', 'SEL_N37', 'BUKIT LANJAN'),
	                                        (308, 'SEL', 'SEL_N38', 'PAYA JARAS'),
	                                        (309, 'SEL', 'SEL_N39', 'KOTA DAMANSARA'),
	                                        (310, 'SEL', 'SEL_N40', 'KOTA ANGGERIK'),
	                                        (311, 'SEL', 'SEL_N41', 'BATU TIGA'),
	                                        (312, 'SEL', 'SEL_N42', 'MERU '),
	                                        (313, 'SEL', 'SEL_N43', 'SEMENTA'),
	                                        (314, 'SEL', 'SEL_N44', 'SUNGAI PINANG'),
	                                        (315, 'SEL', 'SEL_N45', 'SELAT KLANG'),
	                                        (316, 'SEL', 'SEL_N46', 'PELABUHAN KLANG'),
	                                        (317, 'SEL', 'SEL_N47', 'PANDARAMAN'),
	                                        (318, 'SEL', 'SEL_N48', 'KOTA ALAM SHAH'),
	                                        (319, 'SEL', 'SEL_N49', 'SERI ANDALAS'),
	                                        (320, 'SEL', 'SEL_N50', 'SRI MUDA'),
	                                        (321, 'SEL', 'SEL_N51', 'SIJANGKANG'),
	                                        (322, 'SEL', 'SEL_N52', 'TELUK DATUK'),
	                                        (323, 'SEL', 'SEL_N53', 'MORIB'),
	                                        (324, 'SEL', 'SEL_N54', 'TANJONG SEPAT'),
	                                        (325, 'SEL', 'SEL_N55', 'DENGKIL'),
	                                        (326, 'SEL', 'SEL_N56', 'SUNGAI PELEK'),
	                                        (327, '', '', ''),
	                                        (328, '', '', ''),
	                                        (329, 'NS', 'NS_N1', 'CHENNAH'),
	                                        (330, 'NS', 'NS_N2', 'PERTANG'),
	                                        (331, 'NS', 'NS_N3', 'SUNGAI LUI'),
	                                        (332, 'NS', 'NS_N4', 'KLAWANG'),
	                                        (333, 'NS', 'NS_N5', 'SERTING'),
	                                        (334, 'NS', 'NS_N6', 'PALONG'),
	                                        (335, 'NS', 'NS_N7', 'JERAM PADANG'),
	                                        (336, 'NS', 'NS_N8', 'BAHAU'),
	                                        (337, 'NS', 'NS_N9', 'LENGGENG'),
	                                        (338, 'NS', 'NS_N10', 'NILAI'),
	                                        (339, 'NS', 'NS_N11', 'LOBAK'),
	                                        (340, 'NS', 'NS_N12', 'TEMIANG'),
	                                        (341, 'NS', 'NS_N13', 'SIKAMAT'),
	                                        (342, 'NS', 'NS_N14', 'AMPANGAN'),
	                                        (343, 'NS', 'NS_N15', 'JUASSEH'),
	                                        (344, 'NS', 'NS_N16', 'SERI MENANTI'),
	                                        (345, 'NS', 'NS_N17', 'SENALING'),
	                                        (346, 'NS', 'NS_N18', 'PILAH'),
	                                        (347, 'NS', 'NS_N19', 'JOHOL'),
	                                        (348, 'NS', 'NS_N20', 'LABU'),
	                                        (349, 'NS', 'NS_N21', 'BUKIT KEPAYANG'),
	                                        (350, 'NS', 'NS_N22', 'RAHANG'),
	                                        (351, 'NS', 'NS_N23', 'MAMBAU'),
	                                        (352, 'NS', 'NS_N24', 'SENAWANG'),
	                                        (353, 'NS', 'NS_N25', 'PAROI'),
	                                        (354, 'NS', 'NS_N26', 'CHEMBONG'),
	                                        (355, 'NS', 'NS_N27', 'RANTAU'),
	                                        (356, 'NS', 'NS_N28', 'KOTA'),
	                                        (357, 'NS', 'NS_N29', 'CHUAH'),
	                                        (358, 'NS', 'NS_N30', 'LUKUT'),
	                                        (359, 'NS', 'NS_N31', 'BAGAN PINANG'),
	                                        (360, 'NS', 'NS_N32', 'LINGGI'),
	                                        (361, 'NS', 'NS_N33', 'PORT DICKSON'),
	                                        (362, 'NS', 'NS_N34', 'GEMAS'),
	                                        (363, 'NS', 'NS_N35', 'GEMENCHEH'),
	                                        (364, 'NS', 'NS_N36', 'REPAH'),
	                                        (365, 'MLK', 'MLK_N1', 'KUALA LINGGI'),
	                                        (366, 'MLK', 'MLK_N2', 'TANJUNG BIDARA'),
	                                        (367, 'MLK', 'MLK_N3', 'AYER LIMAU'),
	                                        (368, 'MLK', 'MLK_N4', 'LENDU'),
	                                        (369, 'MLK', 'MLK_N5', 'TABOH NANING'),
	                                        (370, 'MLK', 'MLK_N6', 'REMBIA'),
	                                        (371, 'MLK', 'MLK_N7', 'GADEK'),
	                                        (372, 'MLK', 'MLK_N8', 'MACHAP'),
	                                        (373, 'MLK', 'MLK_N9', 'DURIAN TUNGGAL'),
	                                        (374, 'MLK', 'MLK_N10', 'ASAHAN'),
	                                        (375, 'MLK', 'MLK_N11', 'SUNGAI UDANG'),
	                                        (376, 'MLK', 'MLK_N12', 'PANTAI KUNDOR'),
	                                        (377, 'MLK', 'MLK_N13', 'PAYA RUMPUT'),
	                                        (378, 'MLK', 'MLK_N14', 'KELEBANG'),
	                                        (379, 'MLK', 'MLK_N15', 'BACHANG'),
	                                        (380, 'MLK', 'MLK_N16', 'AYER KEROH'),
	                                        (381, 'MLK', 'MLK_N17', 'BUKIT BARU'),
	                                        (382, 'MLK', 'MLK_N18', 'AYER MOLEK'),
	                                        (383, 'MLK', 'MLK_N19', 'KESIDANG'),
	                                        (384, 'MLK', 'MLK_N20', 'KOTA LAKSAMANA'),
	                                        (385, 'MLK', 'MLK_N21', 'DUYONG'),
	                                        (386, 'MLK', 'MLK_N22', 'BANDAR HILIR'),
	                                        (387, 'MLK', 'MLK_N23', 'TELOK MAS'),
	                                        (388, 'MLK', 'MLK_N24', 'BEMBAN'),
	                                        (389, 'MLK', 'MLK_N25', 'RIM'),
	                                        (390, 'MLK', 'MLK_N26', 'SERKAM'),
	                                        (391, 'MLK', 'MLK_N27', 'MERLIMAU'),
	                                        (392, 'MLK', 'MLK_N28', 'SUNGAI RAMBAI'),
	                                        (393, 'JHR', 'JHR_N1', 'BULOH KASAP'),
	                                        (394, 'JHR', 'JHR_N2', 'JEMENTAH'),
	                                        (395, 'JHR', 'JHR_N3', 'PEMANIS'),
	                                        (396, 'JHR', 'JHR_N4', 'KEMELAH'),
	                                        (397, 'JHR', 'JHR_N5', 'TENANG'),
	                                        (398, 'JHR', 'JHR_N6', 'BEKOK'),
	                                        (399, 'JHR', 'JHR_N7', 'BUKIT SERAMPANG'),
	                                        (400, 'JHR', 'JHR_N8', 'JORAK'),
	                                        (401, 'JHR', 'JHR_N9', 'GAMBIR'),
	                                        (402, 'JHR', 'JHR_N10', 'TANGKAK'),
	                                        (403, 'JHR', 'JHR_N11', 'SEROM'),
	                                        (404, 'JHR', 'JHR_N12', 'BENTAYAN'),
	                                        (405, 'JHR', 'JHR_N13', 'SUNGAI ABONG'),
	                                        (406, 'JHR', 'JHR_N14', 'BUKIT NANING'),
	                                        (407, 'JHR', 'JHR_N15', 'MAHARANI'),
	                                        (408, 'JHR', 'JHR_N16', 'SUNGAI BALANG'),
	                                        (409, 'JHR', 'JHR_N17', 'SEMERAH'),
	                                        (410, 'JHR', 'JHR_N18', 'SRI MEDAN'),
	                                        (411, 'JHR', 'JHR_N19', 'YONG PENG'),
	                                        (412, 'JHR', 'JHR_N20', 'SEMARANG'),
	                                        (413, 'JHR', 'JHR_N21', 'PARIT YAANI'),
	                                        (414, 'JHR', 'JHR_N22', 'PARIT RAJA'),
	                                        (415, 'JHR', 'JHR_N23', 'PENGGARAM'),
	                                        (416, 'JHR', 'JHR_N24', 'SENGGARANG'),
	                                        (417, 'JHR', 'JHR_N25', 'RENGIT'),
	                                        (418, 'JHR', 'JHR_N26', 'MACHAP'),
	                                        (419, 'JHR', 'JHR_N27', 'LAYANG-LAYANG'),
	                                        (420, 'JHR', 'JHR_N28', 'MENGKIBOL'),
	                                        (421, 'JHR', 'JHR_N29', 'MAHKOTA'),
	                                        (422, 'JHR', 'JHR_N30', 'PALOH '),
	                                        (423, 'JHR', 'JHR_N31', 'KAHANG'),
	                                        (424, 'JHR', 'JHR_N32', 'ENDAU'),
	                                        (425, 'JHR', 'JHR_N33', 'TENGGAROH'),
	                                        (426, 'JHR', 'JHR_N34', 'PANTI'),
	                                        (427, 'JHR', 'JHR_N35', 'PASIR RAJA'),
	                                        (428, 'JHR', 'JHR_N36', 'SEDILI'),
	                                        (429, 'JHR', 'JHR_N37', 'JOHOR LAMA'),
	                                        (430, 'JHR', 'JHR_N38', 'PENAWAR'),
	                                        (431, 'JHR', 'JHR_N39', 'TANJONG SURAT'),
	                                        (432, 'JHR', 'JHR_N40', 'TIRAM'),
	                                        (433, 'JHR', 'JHR_N41', 'PUTERI WANGSA'),
	                                        (434, 'JHR', 'JHR_N42', 'JOHOR JAYA'),
	                                        (435, 'JHR', 'JHR_N43', 'PERMAS'),
	                                        (436, 'JHR', 'JHR_N44', 'TANJONG PUTERI'),
	                                        (437, 'JHR', 'JHR_N45', 'STULANG'),
	                                        (438, 'JHR', 'JHR_N46', 'PENGKALAN RINTING'),
	                                        (439, 'JHR', 'JHR_N47', 'KEMPAS'),
	                                        (440, 'JHR', 'JHR_N48', 'SKUDAI'),
	                                        (441, 'JHR', 'JHR_N49', 'NUSA JAYA'),
	                                        (442, 'JHR', 'JHR_N50', 'BUKIT PERMAI'),
	                                        (443, 'JHR', 'JHR_N51', 'BUKIT BATU'),
	                                        (444, 'JHR', 'JHR_N52', 'SENAI'),
	                                        (445, 'JHR', 'JHR_N53', 'BENUT'),
	                                        (446, 'JHR', 'JHR_N54', 'PULAI SEBATANG'),
	                                        (447, 'JHR', 'JHR_N55', 'PEKAN NENAS'),
	                                        (448, 'JHR', 'JHR_N56', 'KUKUP'),
	                                        (449, '', '', ''),
	                                        (450, 'SBH', 'SBH_N1', 'BANGGI'),
	                                        (451, 'SBH', 'SBH_N2', 'TANJONG KAPOR'),
	                                        (452, 'SBH', 'SBH_N3', 'PITAS'),
	                                        (453, 'SBH', 'SBH_N4', 'MATUNGGONG'),
	                                        (454, 'SBH', 'SBH_N5', 'TANDEK'),
	                                        (455, 'SBH', 'SBH_N6', 'TEMPASUK'),
	                                        (456, 'SBH', 'SBH_N7', 'KADAMAIAN'),
	                                        (457, 'SBH', 'SBH_N8', 'USUKAN'),
	                                        (458, 'SBH', 'SBH_N9', 'TAMPARULI'),
	                                        (459, 'SBH', 'SBH_N10', 'SULAMAN'),
	                                        (460, 'SBH', 'SBH_N11', 'KIULU'),
	                                        (461, 'SBH', 'SBH_N12', 'KARAMBUNAI'),
	                                        (462, 'SBH', 'SBH_N13', 'INANAM'),
	                                        (463, 'SBH', 'SBH_N14', 'LIKAS'),
	                                        (464, 'SBH', 'SBH_N15', 'API-API'),
	                                        (465, 'SBH', 'SBH_N16', 'LUYANG'),
	                                        (466, 'SBH', 'SBH_N17', 'TANJONG ARU'),
	                                        (467, 'SBH', 'SBH_N18', 'PETAGAS'),
	                                        (468, 'SBH', 'SBH_N19', 'KAPAYAN'),
	                                        (469, 'SBH', 'SBH_N20', 'MOYOG'),
	                                        (470, 'SBH', 'SBH_N21', 'KAWANG'),
	                                        (471, 'SBH', 'SBH_N22', 'PANTAI MANIS'),
	                                        (472, 'SBH', 'SBH_N23', 'BONGAWAN'),
	                                        (473, 'SBH', 'SBH_N24', 'MEMBAKUT'),
	                                        (474, 'SBH', 'SBH_N25', 'KLIAS'),
	                                        (475, 'SBH', 'SBH_N26', 'KUALA PENYU'),
	                                        (476, 'SBH', 'SBH_N27', 'LUMADAN'),
	                                        (477, 'SBH', 'SBH_N28', 'SINDUMIN'),
	                                        (478, 'SBH', 'SBH_N29', 'KUNDASANG'),
	                                        (479, 'SBH', 'SBH_N30', 'KARANAAN'),
	                                        (480, 'SBH', 'SBH_N31', 'PAGINATAN'),
	                                        (481, 'SBH', 'SBH_N32', 'TAMBUNAN'),
	                                        (482, 'SBH', 'SBH_N33', 'BINGKOR'),
	                                        (483, 'SBH', 'SBH_N34', 'LIAWAN'),
	                                        (484, 'SBH', 'SBH_N35', 'MELALAP'),
	                                        (485, 'SBH', 'SBH_N36', 'KEMABONG'),
	                                        (486, 'SBH', 'SBH_N37', 'SOOK'),
	                                        (487, 'SBH', 'SBH_N38', 'NABAWAN'),
	                                        (488, 'SBH', 'SBH_N39', 'SUGUT'),
	                                        (489, 'SBH', 'SBH_N40', 'LABUK'),
	                                        (490, 'SBH', 'SBH_N41', 'GUM-GUM'),
	                                        (491, 'SBH', 'SBH_N42', 'SUNGAI SIBUGA'),
	                                        (492, 'SBH', 'SBH_N43', 'SEKONG'),
	                                        (493, 'SBH', 'SBH_N44', 'KARAMUNTING'),
	                                        (494, 'SBH', 'SBH_N45', 'ELOPURA'),
	                                        (495, 'SBH', 'SBH_N46', 'TANJONG PAPAT'),
	                                        (496, 'SBH', 'SBH_N47', 'KUAMUT'),
	                                        (497, 'SBH', 'SBH_N48', 'SUKAU'),
	                                        (498, 'SBH', 'SBH_N49', 'TUNGKU'),
	                                        (499, 'SBH', 'SBH_N50', 'LAHAD DATU'),
	                                        (500, 'SBH', 'SBH_N51', 'KUNAK'),
	                                        (501, 'SBH', 'SBH_N52', 'SULABAYAN'),
	                                        (502, 'SBH', 'SBH_N53', 'SENALLANG'),
	                                        (503, 'SBH', 'SBH_N54', 'BUGAYA'),
	                                        (504, 'SBH', 'SBH_N55', 'BALUNG'),
	                                        (505, 'SBH', 'SBH_N56', 'APAS'),
	                                        (506, 'SBH', 'SBH_N57', 'SRI TANJUNG'),
	                                        (507, 'SBH', 'SBH_N58', 'MEROTAI'),
	                                        (508, 'SBH', 'SBH_N59', 'TANJUNG BATU'),
	                                        (509, 'SBH', 'SBH_N60', 'SEBATIK'),
	                                        (510, 'SWK', 'SWK_N1', 'OPAR'),
	                                        (511, 'SWK', 'SWK_N2', 'TASIK BARU'),
	                                        (512, 'SWK', 'SWK_N3', 'TANJUNG DATU'),
	                                        (513, 'SWK', 'SWK_N4', 'PANTAI DAMAI'),
	                                        (514, 'SWK', 'SWK_N5', 'DEMAK LAUT'),
	                                        (515, 'SWK', 'SWK_N6', 'TUPONG'),
	                                        (516, 'SWK', 'SWK_N7', 'SAMARIANG'),
	                                        (517, 'SWK', 'SWK_N8', 'SATOK'),
	                                        (518, 'SWK', 'SWK_N9', 'PADUNGAN'),
	                                        (519, 'SWK', 'SWK_N10', 'PENDING'),
	                                        (520, 'SWK', 'SWK_N11', 'BATU LINTANG'),
	                                        (521, 'SWK', 'SWK_N12', 'KOTA SENTOSA'),
	                                        (522, 'SWK', 'SWK_N13', 'BATU KITANG'),
	                                        (523, 'SWK', 'SWK_N14', 'BATU KAWAH'),
	                                        (524, 'SWK', 'SWK_N15', 'ASAJAYA'),
	                                        (525, 'SWK', 'SWK_N16', 'MUARA TUANG'),
	                                        (526, 'SWK', 'SWK_N17', 'STAKAN'),
	                                        (527, 'SWK', 'SWK_N18', 'SEREMBU'),
	                                        (528, 'SWK', 'SWK_N19', 'MAMBONG'),
	                                        (529, 'SWK', 'SWK_N20', 'TARAT'),
	                                        (530, 'SWK', 'SWK_N21', 'TEBEDU'),
	                                        (531, 'SWK', 'SWK_N22', 'KEDUP'),
	                                        (532, 'SWK', 'SWK_N23', 'BUKIT SEMUJA'),
	                                        (533, 'SWK', 'SWK_N24', 'SADONG JAYA'),
	                                        (534, 'SWK', 'SWK_N25', 'SIMUNJAN'),
	                                        (535, 'SWK', 'SWK_N26', 'GEDONG'),
	                                        (536, 'SWK', 'SWK_N27', 'SEBUYAU'),
	                                        (537, 'SWK', 'SWK_N28', 'LINGGA'),
	                                        (538, 'SWK', 'SWK_N29', 'BETING MARO'),
	                                        (539, 'SWK', 'SWK_N30', 'BALAI RINGIN'),
	                                        (540, 'SWK', 'SWK_N31', 'BUKIT BEGUNAN'),
	                                        (541, 'SWK', 'SWK_N32', 'SIMANGGANG'),
	                                        (542, 'SWK', 'SWK_N33', 'ENGKILLI'),
	                                        (543, 'SWK', 'SWK_N34', 'BATANG AI'),
	                                        (544, 'SWK', 'SWK_N35', 'SARIBAS'),
	                                        (545, 'SWK', 'SWK_N36', 'LAYAR'),
	                                        (546, 'SWK', 'SWK_N37', 'BUKIT SABAN'),
	                                        (547, 'SWK', 'SWK_N38', 'KALAKA'),
	                                        (548, 'SWK', 'SWK_N39', 'KRIAN'),
	                                        (549, 'SWK', 'SWK_N40', 'KABONG'),
	                                        (550, 'SWK', 'SWK_N41', 'KUALA RAJANG'),
	                                        (551, 'SWK', 'SWK_N42', 'SEMOP'),
	                                        (552, 'SWK', 'SWK_N43', 'DARO'),
	                                        (553, 'SWK', 'SWK_N44', 'JEMORENG'),
	                                        (554, 'SWK', 'SWK_N45', 'REPOK'),
	                                        (555, 'SWK', 'SWK_N46', 'MERADONG'),
	                                        (556, 'SWK', 'SWK_N47', 'PAKAN'),
	                                        (557, 'SWK', 'SWK_N48', 'MELUAN'),
	                                        (558, 'SWK', 'SWK_N49', 'NGEMAH'),
	                                        (559, 'SWK', 'SWK_N50', 'MACHAN'),
	                                        (560, 'SWK', 'SWK_N51', 'BUKIT ASSEK'),
	                                        (561, 'SWK', 'SWK_N52', 'DUDONG'),
	                                        (562, 'SWK', 'SWK_N53', 'BAWANG ASSAN'),
	                                        (563, 'SWK', 'SWK_N54', 'PELAWN'),
	                                        (564, 'SWK', 'SWK_N55', 'NANGKA'),
	                                        (565, 'SWK', 'SWK_N56', 'DALAT'),
	                                        (566, 'SWK', 'SWK_N57', 'TELLIAN'),
	                                        (567, 'SWK', 'SWK_N58', 'BALINGIAN'),
	                                        (568, 'SWK', 'SWK_N59', 'TAMIN'),
	                                        (569, 'SWK', 'SWK_N60', 'KAKUS'),
	                                        (570, 'SWK', 'SWK_N61', 'PELAGUS'),
	                                        (571, 'SWK', 'SWK_N62', 'KATIBAS'),
	                                        (572, 'SWK', 'SWK_N63', 'BUKIT GORAM'),
	                                        (573, 'SWK', 'SWK_N64', 'BALEH'),
	                                        (574, 'SWK', 'SWK_N65', 'BELAGA'),
	                                        (575, 'SWK', 'SWK_N66', 'MURUM'),
	                                        (576, 'SWK', 'SWK_N67', 'JEPAK'),
	                                        (577, 'SWK', 'SWK_N68', 'TANJONG BATU'),
	                                        (578, 'SWK', 'SWK_N69', 'KEMENA'),
	                                        (579, 'SWK', 'SWK_N70', 'SAMALAJU'),
	                                        (580, 'SWK', 'SWK_N71', 'BEKENU'),
	                                        (581, 'SWK', 'SWK_N72', 'LAMBIR'),
	                                        (582, 'SWK', 'SWK_N73', 'PIASAU'),
	                                        (583, 'SWK', 'SWK_N74', 'PUJUT'),
	                                        (584, 'SWK', 'SWK_N75', 'SENADIN'),
	                                        (585, 'SWK', 'SWK_N76', 'MARUDI'),
	                                        (586, 'SWK', 'SWK_N77', 'TELANG USAN'),
	                                        (587, 'SWK', 'SWK_N78', 'MULU'),
	                                        (588, 'SWK', 'SWK_N79', 'BUKIT KOTA'),
	                                        (589, 'SWK', 'SWK_N80', 'BATU DANAU'),
	                                        (590, 'SWK', 'SWK_N81', 'BA''KELALAN'),
	                                        (591, 'SWK', 'SWK_N82', 'BUKIT SARI');
                                    ";
                #endregion
                cmd.CommandType = CommandType.Text;
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }

        public int SetupMakkebun(SQLiteConnection cnn)
        {
            int i = 0;
            using (SQLiteCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandText = @"
                                     DROP TABLE IF  EXISTS `makkebun`;

                                    -- Dumping structure for table a1_tsspk1511.makkebun
                                    CREATE TABLE IF NOT EXISTS `makkebun` (
                                      `id_makkebun` INTEGER PRIMARY KEY AUTOINCREMENT,
                                      `appinfo_id` int(11) NOT NULL,
                                      `addr1` varchar(100) DEFAULT NULL,
                                      `addr2` varchar(100) DEFAULT NULL,
                                      `addr3` varchar(100) DEFAULT NULL,
                                      `daerah` varchar(30) COLLATE NOCASE NOT NULL,
                                      `dun` varchar(100) COLLATE NOCASE NOT NULL,
                                      `parlimen` int(11) DEFAULT NULL,
                                      `poskod` varchar(5) DEFAULT NULL,
                                      `negeri` varchar(20) COLLATE NOCASE DEFAULT NULL,
                                      `nolot` varchar(20) DEFAULT NULL,
                                      `hakmiliktanah` varchar(30) NOT NULL,
                                      `tncr` varchar(30) NOT NULL,
                                      `luasmatang` double DEFAULT NULL,
                                      `tebang` varchar(5) DEFAULT NULL,
                                      `tarikhtebang` varchar(20) DEFAULT NULL,
                                      `nolesen` varchar(50) DEFAULT NULL,
                                      `syarattanah` varchar(22) NOT NULL,
                                      `pemilikan` varchar(10) DEFAULT NULL,
                                      `pengurusan` varchar(20) DEFAULT NULL,
                                      `luaslesen` double DEFAULT NULL,
                                      `catatan` text,
                                      `created` datetime DEFAULT NULL,
                                      `createdby` varchar(100) DEFAULT NULL,
                                       newid_makkebun INTEGER null,
                                        syncdate datetime null
                                    );
                           ";
                cmd.CommandType = CommandType.Text;
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }

        public int SetupParlimen(SQLiteConnection cnn)
        {
            int i = 0;
            using (SQLiteCommand cmd = cnn.CreateCommand())
            {
                #region Query Create & Insert
                cmd.CommandText = @"
                                 DROP TABLE IF  EXISTS `parlimen`;

                                    -- --------------------------------------------------------
                                -- Host:                         127.0.0.1
                                -- Server version:               5.6.25 - MySQL Community Server (GPL)
                                -- Server OS:                    Win32
                                -- HeidiSQL Version:             9.2.0.4947
                                -- --------------------------------------------------------
                                -- Dumping structure for table a1_tsspk1511.parlimen
                                CREATE TABLE IF NOT EXISTS `parlimen` (
                                  `id` int(11) NULL PRIMARY KEY,
                                  `negeri` varchar(100) COLLATE NOCASE NULL,
                                  `kawasan` varchar(100) COLLATE NOCASE NULL
                                );

                                DELETE FROM parlimen;

                                -- Dumping data for table a1_tsspk1511.parlimen: 222 rows
                                INSERT INTO `parlimen` (`id`, `negeri`, `kawasan`) VALUES
	                                (1, 'PL', 'PADANG BESAR'),
	                                (2, 'PL', 'KANGAR'),
	                                (3, 'PL', 'ARAU'),
	                                (4, 'TRG', 'BESUT'),
	                                (5, 'TRG', 'SETIU'),
	                                (6, 'KEL', 'KOTA BHARU'),
	                                (7, 'KEL', 'PASIR MAS'),
	                                (8, 'KDH', 'JERLUN'),
	                                (9, 'KDH', 'LANGKAWI'),
	                                (10, 'KDH', 'KUBANG PASU'),
	                                (11, 'KDH', 'PADANG TERAP'),
	                                (12, 'KDH', 'POKOK SENA'),
	                                (13, 'KDH', 'ALOR STAR'),
	                                (14, 'PRK', 'GERIK'),
	                                (15, 'PRK', 'LENGGONG'),
	                                (19, 'PRK', 'PARIT BUNTAR'),
	                                (18, 'PRK', 'LARUT'),
	                                (20, 'PRK', 'BAGAN SERAI'),
	                                (21, 'PRK', 'BUKIT GANTANG'),
	                                (22, 'KDH', 'KUALA KEDAH'),
	                                (26, 'PRK', 'SUNGAI SIPUT'),
	                                (24, 'PRK', 'TAIPING'),
	                                (25, 'PRK', 'PADANG RENGAS'),
	                                (27, 'PRK', 'TAMBUN'),
	                                (32, 'PRK', 'BATU GAJAH'),
	                                (29, 'KDH', 'PENDANG'),
	                                (30, 'PRK', 'IPOH TIMOR'),
	                                (31, 'PRK', 'IPOH BARAT'),
	                                (33, 'PRK', 'KUALA KANGSAR'),
	                                (34, 'SWK', 'MAS GADING'),
	                                (35, 'SWK', 'SANTUBONG'),
	                                (37, 'PRK', 'BERUAS'),
	                                (38, 'PRK', 'PARIT'),
	                                (39, 'WPP', 'PUTRAJAYA'),
	                                (56, 'SWK', 'BANDAR KUCHING'),
	                                (41, 'PRK', 'KAMPAR'),
	                                (42, 'PRK', 'GOPENG'),
	                                (43, 'SWK', 'PETRA JAYA'),
	                                (68, 'KDH', 'SUNGAI PETANI'),
	                                (46, 'PRK', 'TAPAH'),
	                                (47, 'PRK', 'PASIR SALAK'),
	                                (48, 'PRK', 'LUMUT'),
	                                (49, 'PRK', 'BAGAN DATOK'),
	                                (50, 'PRK', 'TELUK INTAN'),
	                                (51, 'PRK', 'TANJONG MALIM'),
	                                (52, 'KDH', 'JERAI'),
	                                (53, 'KDH', 'SIK'),
	                                (54, 'MLK', 'MASJID TANAH'),
	                                (55, 'MLK', 'ALOR GAJAH'),
	                                (57, 'SWK', 'STAMPIN'),
	                                (58, 'MLK', 'TANGGA BATU'),
	                                (65, 'MLK', 'KOTA MELAKA'),
	                                (67, 'KDH', 'MERBOK'),
	                                (61, 'SWK', 'KOTA SAMARAHAN'),
	                                (62, 'SWK', 'MAMBONG'),
	                                (221, 'PP', 'KEPALA BATAS'),
	                                (64, 'MLK', 'BUKIT KATIL'),
	                                (66, 'MLK', 'JASIN'),
	                                (69, 'JHR', 'SEGAMAT'),
	                                (70, 'JHR', 'SEKIJANG'),
	                                (71, 'JHR', 'LABIS'),
	                                (72, 'JHR', 'PAGOH'),
	                                (73, 'PHG', 'CAMERON HIGHLANDS'),
	                                (74, 'PHG', 'LIPIS'),
	                                (75, 'JHR', 'LEDANG'),
	                                (76, 'JHR', 'BAKRI'),
	                                (77, 'SWK', 'SERIAN'),
	                                (78, 'SWK', 'BATANG SADONG'),
	                                (79, 'SWK', 'BATANG LUPAR'),
	                                (80, 'SWK', 'SRI AMAN'),
	                                (81, 'SWK', 'LUBOK ANTU'),
	                                (82, 'KDH', 'BALING'),
	                                (83, 'KDH', 'PADANG SERAI'),
	                                (84, 'PHG', 'RAUB'),
	                                (85, 'PHG', 'JERANTUT'),
	                                (86, 'JHR', 'MUAR'),
	                                (87, 'JHR', 'PARIT SULONG'),
	                                (88, 'PHG', 'INDERA MAHKOTA'),
	                                (89, 'PHG', 'KUANTAN'),
	                                (90, 'JHR', 'AYER HITAM'),
	                                (91, 'JHR', 'SRI GADING'),
	                                (92, 'PHG', 'PAYA BESAR'),
	                                (93, 'PHG', 'PEKAN'),
	                                (94, 'JHR', 'BATU PAHAT'),
	                                (95, 'JHR', 'SIMPANG RENGGAM'),
	                                (96, 'SWK', 'BETONG '),
	                                (97, 'SWK', 'SARATOK'),
	                                (98, 'SWK', 'TANJONG MANIS'),
	                                (99, 'SWK', 'IGAN'),
	                                (100, 'SWK', 'SARIKEI'),
	                                (101, 'PHG', 'MARAN'),
	                                (102, 'PHG', 'KUALA KRAU'),
	                                (103, 'JHR', 'KLUANG'),
	                                (104, 'JHR', 'SEMBRONG'),
	                                (105, 'JHR', 'MERSING'),
	                                (106, 'JHR', 'TENGGARA'),
	                                (107, 'PHG', 'TEMERLOH'),
	                                (108, 'PHG', 'BENTONG'),
	                                (109, 'JHR', 'KOTA TINGGI'),
	                                (110, 'JHR', 'PENGERANG'),
	                                (111, 'PHG', 'BERA'),
	                                (112, 'PHG', 'ROMPIN'),
	                                (113, 'KDH', 'KULIM-BANDAR BAHARU'),
	                                (114, 'JHR', 'TEBRAU'),
	                                (115, 'JHR', 'PASIR GUDANG'),
	                                (116, 'JHR', 'JOHOR BAHRU'),
	                                (117, 'JHR', 'PULAI'),
	                                (118, 'SWK', 'JULAU'),
	                                (119, 'SWK', 'KANOWIT'),
	                                (120, 'SWK', 'LANANG'),
	                                (121, 'SWK', 'SIBU'),
	                                (122, 'SWK', 'MUKAH'),
	                                (123, 'SWK', 'SELANGAU'),
	                                (124, 'SWK', 'KAPIT'),
	                                (125, 'SWK', 'HULU RAJANG'),
	                                (126, 'SWK', 'BINTULU'),
	                                (127, 'SWK', 'SIBUTI'),
	                                (128, 'JHR', 'GELANG PATAH'),
	                                (129, 'JHR', 'KULAI'),
	                                (130, 'SEL', 'SABAK BERNAM'),
	                                (131, 'SEL', 'SUNGAI BESAR'),
	                                (132, 'JHR', 'PONTIAN'),
	                                (133, 'JHR', 'TANJONG PIAI'),
	                                (134, 'SEL', 'HULU SELANGAOR'),
	                                (135, 'SEL', 'TANJONG KARANG'),
	                                (136, 'SWK', 'MIRI'),
	                                (137, 'SWK', 'BARAM'),
	                                (138, 'SWK', 'LIMBANG'),
	                                (139, 'SWK', 'LAWAS'),
	                                (143, 'SEL', 'GOMBAK'),
	                                (141, 'SEL', 'KUALA SELANGOR'),
	                                (142, 'SEL', 'SELAYANG'),
	                                (144, 'SEL', 'AMPANG'),
	                                (145, 'SEL', 'PANDAN'),
	                                (146, 'SEL', 'HULU LANGAT'),
	                                (147, 'SEL', 'SERDANG'),
	                                (148, 'SEL', 'PUCHONG'),
	                                (149, 'SEL', 'KELANA JAYA'),
	                                (150, 'SEL', 'PJ SELATAN'),
	                                (151, 'KEL', 'TUMPAT'),
	                                (152, 'KEL', 'PENGKALAN CHEPA'),
	                                (153, 'SEL', 'PJ UTARA'),
	                                (154, 'SEL', 'SUBANG'),
	                                (155, 'KEL', 'RANTAU PANJANG'),
	                                (156, 'KEL', 'KUBANG KERIAN'),
	                                (157, 'SEL', 'SHAH ALAM'),
	                                (158, 'SEL', 'KAPAR'),
	                                (159, 'KEL', 'BACHOK'),
	                                (160, 'KEL', 'KETEREH'),
	                                (161, 'SEL', 'KLANG'),
	                                (162, 'SEL', 'KOTA RAJA'),
	                                (163, 'KEL', 'TANAH MERAH'),
	                                (164, 'KEL', 'PASIR PUTEH'),
	                                (165, 'SEL', 'KUALA LANGAT'),
	                                (166, 'SEL', 'SEPANG'),
	                                (167, 'KEL', 'MACHANG'),
	                                (168, 'KEL', 'JELI'),
	                                (169, 'KEL', 'KUALA KERAI'),
	                                (170, 'KEL', 'GUA MUSANG'),
	                                (171, 'SBH', 'KUDAT'),
	                                (172, 'SBH', 'KOTA MARUDU'),
	                                (173, 'SBH', 'KOTA BELUD'),
	                                (174, 'SBH', 'TUARAN'),
	                                (175, 'SBH', 'SEPANGGAR'),
	                                (176, 'SBH', 'KOTA KINABALU'),
	                                (177, 'SBH', 'PUTATAN'),
	                                (178, 'SBH', 'PENAMPANG'),
	                                (179, 'SBH', 'PAPAR'),
	                                (180, 'SBH', 'KIMANIS'),
	                                (181, 'WKL', 'KEPONG '),
	                                (182, 'WKL', 'BATU'),
	                                (183, 'NS', 'JELEBU'),
	                                (184, 'NS', 'JEMPOL'),
	                                (185, 'WKL', 'WANGSA MAJU'),
	                                (186, 'WKL', 'SEGAMBUT'),
	                                (187, 'NS', 'SEREMBAN'),
	                                (188, 'NS', 'KUALA PILAH'),
	                                (189, 'TRG', 'KUALA NERUS'),
	                                (190, 'TRG', 'KUALA TERENGGANU'),
	                                (191, 'WKL', 'SETIAWANGSA'),
	                                (192, 'WKL', 'TITIWANGSA'),
	                                (193, 'NS', 'RASAH'),
	                                (194, 'NS', 'REMBAU'),
	                                (195, 'TRG', 'MARANG'),
	                                (196, 'TRG', 'HULU TERENGGANU'),
	                                (197, 'NS', 'TELOK KEMANG'),
	                                (198, 'NS', 'TAMPIN'),
	                                (199, 'WKL', 'BUKIT BINTANG'),
	                                (200, 'WKL', 'LEMBAH PANTAI'),
	                                (201, 'TRG', 'DUNGUN'),
	                                (202, 'TRG', 'KEMAMAN'),
	                                (203, 'WKL', 'SEPUTEH'),
	                                (204, 'WKL', 'CHERAS'),
	                                (205, 'WKL', 'BANDAR TUN RAZAK'),
	                                (206, 'SBH', 'BEAUFORT'),
	                                (207, 'SBH', 'SIPITANG'),
	                                (208, 'SBH', 'RANAU'),
	                                (209, 'SBH', 'KENINGAU '),
	                                (210, 'SBH', 'TENOM'),
	                                (211, 'SBH', 'PENSIANGAN'),
	                                (212, 'SBH', 'BELURAN'),
	                                (213, 'SBH', 'LIBARAN'),
	                                (214, 'SBH', 'BATU SAPI'),
	                                (215, 'SBH', 'SANDAKAN'),
	                                (216, 'SBH', 'KINABATANGAN'),
	                                (217, 'SBH', 'SILAM'),
	                                (218, 'SBH', 'SEMPORNA'),
	                                (219, 'SBH', 'TAWAU'),
	                                (220, 'SBH', 'KALABAKAN'),
	                                (222, 'PP', 'TASEK GELUGOR'),
	                                (223, 'PP', 'BAGAN'),
	                                (224, 'PP', 'PERMATANG PAUH'),
	                                (225, 'PP', 'BUKIT MERTAJAM'),
	                                (226, 'PP', 'BATU KAWAN'),
	                                (227, 'PP', 'NIBONG TEBAL'),
	                                (228, 'PP', 'BUKIT BENDERA'),
	                                (229, 'PP', 'TANJONG'),
	                                (230, 'PP', 'JELUTONG'),
	                                (231, 'PP', 'BUKIT GELUGOR'),
	                                (232, 'PP', 'BAYAN BARU'),
	                                (233, 'PP', 'BALIK PULAU'),
	                                (234, 'WPL', 'LABUAN');
                                 ";
                #endregion
                cmd.CommandType = CommandType.Text;
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }

        public int SetupVariables(SQLiteConnection cnn)
        {
            int i = 0;
            using (SQLiteCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                #region Query Create & Insert
                cmd.CommandText = @"
                                 DROP TABLE IF  EXISTS `variables`;

                                -- --------------------------------------------------------
                                -- Host:                         127.0.0.1
                                -- Server version:               5.6.25 - MySQL Community Server (GPL)
                                -- Server OS:                    Win32
                                -- HeidiSQL Version:             9.2.0.4947
                                -- --------------------------------------------------------
                                -- Dumping structure for table a1_tsspk1511.variables
                                CREATE TABLE IF NOT EXISTS `variables` (
                                  `code` varchar(30) COLLATE NOCASE NOT NULL,
                                  `value` varchar(50) COLLATE NOCASE NOT NULL,
                                  `type` varchar(50) COLLATE NOCASE DEFAULT NULL,
                                  `parent` varchar(5) COLLATE NOCASE DEFAULT NULL
                                );

                                DELETE FROM variables;

                                -- Dumping data for table a1_tsspk1511.variables: 120 rows
                                INSERT INTO `variables` (`code`, `value`, `type`, `parent`) VALUES
	                                ('PP', 'PULAU PINANG', 'NEGERI', 'UTR'),
	                                ('KDH', 'KEDAH', 'NEGERI', 'UTR'),
	                                ('KL', 'TANAH KEBUN LAIN YANG DITUAI', NULL, NULL),
	                                ('PRK', 'PERAK', 'NEGERI', 'UTR'),
	                                ('KS', 'KEBUN SENDIRI', NULL, NULL),
	                                ('SEL ', 'SELANGOR', 'NEGERI', 'TGH'),
	                                ('G', 'FELCRA', NULL, NULL),
	                                ('WKL', 'WILAYAH PERSEKUTUAN KUALA LUMPUR', 'NEGERI', 'TGH'),
	                                ('F', 'FELDA', NULL, NULL),
	                                ('WPP', 'WILAYAH PERSEKUTUAN PUTRAJAYA', 'NEGERI', 'TGH'),
	                                ('MLK', 'MELAKA', 'NEGERI', 'TGH'),
	                                ('E ', 'SYARIKAT SDN BHD', NULL, NULL),
	                                ('JHR', 'JOHOR', 'NEGERI', 'SEL'),
	                                ('D', 'PERSEORANGAN', NULL, NULL),
	                                ('PHG', 'PAHANG', 'NEGERI', 'TMR'),
	                                ('C', 'KOPERASI', NULL, NULL),
	                                ('TRG', 'TERENGGANU', 'NEGERI', 'TMR'),
	                                ('KEL', 'KELANTAN', 'NEGERI', 'TMR'),
	                                ('SWK', 'SARAWAK', 'NEGERI', 'SWK'),
	                                ('B', 'PERNIAGAAN PERKONGSIAN', NULL, NULL),
	                                ('SBH', 'SABAH', 'NEGERI', 'SBH'),
	                                ('A', 'INDIVIDU', NULL, NULL),
	                                ('WPL', 'WILAYAH PERSEKUTUAN LABUAN', 'NEGERI', 'SBH'),
	                                ('R2', 'Pegawai TUNAS Kawasan', NULL, NULL),
	                                ('R1', 'Admin', NULL, NULL),
	                                ('SA', 'AKTIF', NULL, NULL),
	                                ('SB', 'TAK AKTIF', NULL, NULL),
	                                ('UTR', 'UTARA', 'WILAYAH', NULL),
	                                ('TMR', 'TIMUR', 'WILAYAH', NULL),
	                                ('SEL', 'SELATAN', 'WILAYAH', NULL),
	                                ('TGH', 'TENGAH', 'WILAYAH', NULL),
	                                ('NS', 'NEGERI SEMBILAN', 'NEGERI', 'TGH'),
	                                ('PL', 'PERLIS', 'NEGERI', 'UTR'),
	                                ('SBH', 'SABAH', 'WILAYAH', NULL),
	                                ('SWK', 'SARAWAK', 'WILAYAH', NULL),
	                                ('WPL', 'LABUAN', 'WILAYAH', NULL),
	                                ('ALL', 'SEMUA WILAYAH', 'WILAYAH', NULL),
	                                ('032', 'AFFIN BANK BERHAD \r', NULL, NULL),
	                                ('012', 'ALLIANCE BANK MALAYSIA BERHAD \r', NULL, NULL),
	                                ('008', 'AMBANK/AMFINANCE \r', NULL, NULL),
	                                ('045', 'BANK ISLAM MALAYSIA BERHAD \r', NULL, NULL),
	                                ('002', 'BANK KERJASAMA RAKYAT MALAYSIA BERHAD \r', NULL, NULL),
	                                ('041', 'BANK MUAMALAT MALAYSIA BERHAD \r', NULL, NULL),
	                                ('007', 'BANK OF AMERICA \r', NULL, NULL),
	                                ('049', 'BANK PERTANIAN MALAYSIA BERHAD (AGROBANK) \r', NULL, NULL),
	                                ('010', 'BANK SIMPANAN NASIONAL BERHAD \r', NULL, NULL),
	                                ('017', 'CITIBANK BERHAD \r', NULL, NULL),
	                                ('019', 'DEUTSCHE BANK (MALAYSIA) BERHAD \r', NULL, NULL),
	                                ('023', 'EON BANK/EON FINANCE \r', NULL, NULL),
	                                ('024', 'HONG LEONG BANK \r', NULL, NULL),
	                                ('022', 'HSBC BANK MALAYSIA BERHAD \r', NULL, NULL),
	                                ('048', 'J.P. MORGAN CHASE BANK BHD \r', NULL, NULL),
	                                ('047', 'KUWAIT FINANCE HOUSE (MALAYSIA) BERHAD \r', NULL, NULL),
	                                ('027', 'MAYBANK \r', NULL, NULL),
	                                ('029', 'OCBC BANK (MALAYSIA) BERHAD \r', NULL, NULL),
	                                ('033', 'PUBLIC BANK BERHAD/PUBLIC FINANCE BERHAD \r', NULL, NULL),
	                                ('050', 'CIMB BANK', NULL, NULL),
	                                ('018', 'RHB BANK', NULL, NULL),
	                                ('R3', 'Pelawat', NULL, NULL),
	                                ('R4', 'Pegawai Kewangan', NULL, NULL),
	                                ('B_PK', 'Penyediaan Kawasan', NULL, NULL),
	                                ('B_BPK', 'BPK', NULL, NULL),
	                                ('B_BAJA', 'Bekalan Baja Fosfat ', NULL, NULL),
	                                ('B_BENIH', 'Bekalan Anak Benih', NULL, NULL),
	                                ('B_KIMIA', 'Bekalan Bahan Kimia/Racun ', NULL, NULL),
	                                ('B_THN1', 'Bekalan Baja Tahun 1 ', NULL, NULL),
	                                ('B_THN2', 'Bekalan Baja Tahun 2', NULL, NULL),
	                                ('2', 'Belum Terima PR', NULL, NULL),
	                                ('1', 'Dalam Cadangan', NULL, NULL),
	                                ('b_benih', 'Anak Benih', NULL, NULL),
	                                ('b_baja', 'Baja Bantuan Fosfat', NULL, NULL),
	                                ('b_racun1', 'Racun Tahun 1', NULL, NULL),
	                                ('b_racun2', 'Racun Tahun 2', NULL, NULL),
	                                ('026', 'UNITED OVERSEAS BANK BERHAD', NULL, NULL),
	                                ('b_sebatian1', 'Baja Sebatian Tahun 1', NULL, NULL),
	                                ('b_lain', 'Lain - Lain (Isikan maklumat di bahagian catatan)', NULL, NULL),
	                                ('mpob', 'Dibekalkan oleh MPOB', NULL, NULL),
	                                ('sendiri', 'Beli Sendiri', NULL, NULL),
	                                ('jb_serangg', 'Racun serangga', NULL, NULL),
	                                ('jb_rumpaib', 'Racun Rumpai (Basta 15)', NULL, NULL),
	                                ('jb_rumpaig', 'Racun Rumpai (Glyphosate)', NULL, NULL),
	                                ('jb_f1', 'Baja Sebatian MPOB F1', NULL, NULL),
	                                ('jb_f2', 'Baja Sebatian MPOB F2', NULL, NULL),
	                                ('jb_f3', 'Baja Sebatian MPOB F3', NULL, NULL),
	                                ('jb_f4', 'Baja Sebatian MPOB F4', NULL, NULL),
	                                ('jb_lain', 'Lain-lain (Isikan di bahagian catatan)', NULL, NULL),
	                                ('Unit', 'Unit', NULL, NULL),
	                                ('Beg', 'Beg', NULL, NULL),
	                                ('Botol', 'Botol', NULL, NULL),
	                                ('Pokok', 'Pokok', NULL, NULL),
	                                ('103', 'Komitmen', NULL, NULL),
	                                ('102', 'Belanja Sebenar', NULL, NULL),
	                                ('101', 'Unjuran EPP01', NULL, NULL),
	                                ('b_sebatian2', 'Baja Sebatian Tahun 2', NULL, NULL),
	                                ('cadangan', 'Dalam Cadangan', NULL, NULL),
	                                ('3', 'Telah keluar PO', NULL, NULL),
	                                ('belum_terima', 'Belum Terima PR', NULL, NULL),
	                                ('keluar_po', 'Telah keluar PO', NULL, NULL),
	                                ('bayaran_lulus', 'Bayaran Telah Diluluskan', NULL, NULL),
	                                ('4', 'Bayaran Telah Diluluskan', NULL, NULL),
	                                ('051', 'LEMBAGA TABUNG HAJI', NULL, NULL),
	                                ('052', 'AMANAH SAHAM BUMIPUTRA (ASB)', NULL, NULL),
	                                ('TB', 'Telah Bayar', NULL, NULL),
	                                ('DP', 'Dalam Proses Pembayaran', NULL, NULL),
	                                ('TK', 'Telah keluar PO', NULL, NULL),
	                                ('DC', 'Dalam Cadangan', NULL, NULL),
	                                ('014', 'STANDARD CHARTERED BANK MALAYSIA BERHAD', NULL, NULL),
	                                ('046', 'THE ROYAL BANK OF SCOTLAND BERHAD', NULL, NULL),
	                                ('R6', 'Pegawai Penyelia', NULL, NULL),
	                                ('R7', 'Ketua Unit', NULL, NULL),
	                                ('KUPON_BAJA_SEBATIAN', 'Kupon Baja Sebatian', NULL, NULL),
	                                ('tanah_ukur_warta', 'Tanah Yang Diukur Dan Diwarta', NULL, NULL),
	                                ('tanah_ukur_belum_warta', 'Tanah Yang Diukur Dan Belum Diwarta', NULL, NULL),
	                                ('tanah_geran', 'Tanah Geran', NULL, NULL),
	                                ('tanah_luar_kategori', 'Tanah Diluar Kategori 1,2 Dan 3', NULL, NULL),
	                                ('GK', 'Geran Kekal', 'HAKMILIKTANAH', NULL),
	                                ('TNCR', 'Tanah NCR', 'HAKMILIKTANAH', NULL),
	                                ('TPT', 'Tanah PT', 'HAKMILIKTANAH', NULL),
	                                ('ROA', 'Rizab Orang Asli', 'HAKMILIKTANAH', NULL),
	                                ('LL', 'Lain-Lain', 'HAKMILIKTANAH', NULL);
                                ";
                #endregion
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }

        public bool CheckExistingDB(string DbFile)
        {
            if (!File.Exists(DbFile))
            {
                return false;
            }
            return true;
        }

        public int ResetDefault(SQLiteConnection cnn)
        {
            int i = 0;
            using (var cmd = cnn.CreateCommand())
            {
                cmd.CommandText = @"
                                    -- DROP TABLE appinfo;
                                    -- DROP TABLE dun;
                                    -- DROP TABLE makkebun;
                                    -- DROP TABLE parlimen;
                                    -- DROP TABLE variables;
                                    ";
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }

        public int SetupDaerah(SQLiteConnection cnn)
        {
            int i = 0;
            using (SQLiteCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                #region Create & Insert
                string qry = @"
                             DROP TABLE IF  EXISTS `daerah`;

                            -- --------------------------------------------------------
                            -- Host:                         127.0.0.1
                            -- Server version:               5.6.25 - MySQL Community Server (GPL)
                            -- Server OS:                    Win32
                            -- HeidiSQL Version:             9.2.0.4947
                            -- --------------------------------------------------------

                            -- Dumping structure for table tsspk1511.daerah
                            CREATE TABLE IF NOT EXISTS `daerah` (
                                `id` int(11) NOT NULL PRIMARY KEY,
                                `kod_negeri` varchar(10) COLLATE NOCASE  NULL,
                                `kod_daerah` varchar(10) COLLATE NOCASE NULL,
                                `daerah` varchar(100)  COLLATE NOCASE NULL,
                                `daerah_spe` varchar(100) COLLATE NOCASE NULL
                            );
                            
                            DELETE FROM daerah;
                            
                            -- Dumping data for table tsspk1511.daerah: 207 rows
                            INSERT INTO `daerah` (`id`, `kod_negeri`, `kod_daerah`, `daerah`, `daerah_spe`) VALUES
	                            (1, 'JHR', 'BPT', 'BATU PAHAT', 'BATU PAHAT'),
	                            (2, 'JHR', 'JBU', 'JOHOR BAHRU', 'JOHOR BAHRU'),
	                            (3, 'JHR', 'KLG', 'KLUANG', 'KLUANG'),
	                            (4, 'JHR', 'KTI', 'KOTA TINGGI', 'KOTA TINGGI'),
	                            (5, 'JHR', 'MSG', 'MERSING', 'MERSING'),
	                            (6, 'JHR', 'MUA', 'MUAR', 'MUAR'),
	                            (7, 'JHR', 'PTN', 'PONTIAN', 'PONTIAN'),
	                            (8, 'JHR', 'SGT', 'SEGAMAT', 'SEGAMAT'),
	                            (9, 'JHR', 'KJY', 'KULAIJAYA', 'KULAI JAYA'),
	                            (10, 'JHR', 'LDG', 'LEDANG', 'LEDANG'),
	                            (11, 'KDH', 'BLG', 'BALING', 'BALING'),
	                            (12, 'KDH', 'BBU', 'BANDAR BAHARU', 'BANDAR BAHARU'),
	                            (13, 'KDH', 'KSR', 'KOTA SETAR', 'KOTA SETAR'),
	                            (14, 'KDH', 'KMA', 'KUALA MUDA', 'KUALA MUDA'),
	                            (15, 'KDH', 'KPU', 'KUBANG PASU', 'KUBANG PASU'),
	                            (16, 'KDH', 'KLM', 'KULIM', 'KULIM'),
	                            (17, 'KDH', 'LKI', 'LANGKAWI', 'PULAU LANGKAWI'),
	                            (18, 'KDH', 'PTP', 'PADANG TERAP', 'PADANG TERAP'),
	                            (19, 'KDH', 'PDG', 'PENDANG', 'PENDANG'),
	                            (20, 'KDH', 'PSA', 'POKOK SENA', 'POKOK SENA'),
	                            (21, 'KDH', 'SIK', 'SIK', 'SIK'),
	                            (22, 'KDH', 'YAN', 'YAN', 'YAN'),
	                            (23, 'KEL', 'BCK', 'BACHOK', 'BACHOK'),
	                            (24, 'KEL', 'KBU', 'KOTA BHARU', 'KOTA BHARU'),
	                            (25, 'KEL', 'MCG', 'MACHANG', 'MACHANG'),
	                            (26, 'KEL', 'PMS', 'PASIR MAS', 'PASIR MAS'),
	                            (27, 'KEL', 'PPH', 'PASIR PUTEH', 'PASIR PUTEH'),
	                            (28, 'KEL', 'TMH', 'TANAH MERAH', 'TANAH MERAH'),
	                            (29, 'KEL', 'TMT', 'TUMPAT', 'TUMPAT'),
	                            (30, 'KEL', 'GMG', 'GUA MUSANG', 'GUA MUSANG'),
	                            (31, 'KEL', 'KKI', 'KUALA KRAI', 'KUALA KRAI'),
	                            (32, 'KEL', 'JEL', 'JELI', 'JELI'),
	                            (33, 'MLK', 'AGH', 'ALOR GAJAH', 'ALOR GAJAH'),
	                            (34, 'MLK', 'JSN', 'JASIN', 'JASIN'),
	                            (35, 'MLK', 'MTH', 'MELAKA TENGAH', 'MELAKA TENGAH'),
	                            (36, 'NS', 'JEB', 'JELEBU', 'JELEBU'),
	                            (37, 'NS', 'KPH', 'KUALA PILAH', 'KUALA PILAH'),
	                            (38, 'NS', 'PDN', 'PORT DICKSON', 'PORT DICKSON'),
	                            (39, 'NS', 'RBU', 'REMBAU', 'REMBAU'),
	                            (40, 'NS', 'SRM', 'SEREMBAN', 'SEREMBAN'),
	                            (41, 'NS', 'TPN', 'TAMPIN', 'TAMPIN'),
	                            (42, 'NS', 'JPL', 'JEMPOL', 'JEMPOL'),
	                            (43, 'NS', 'GMS', 'GEMAS', 'GEMAS'),
	                            (44, 'PHG', 'BTG', 'BENTONG', 'BENTONG'),
	                            (45, 'PHG', 'CHS', 'CAMERON HIGLANDS', 'CAMERON HIGHLANDS'),
	                            (46, 'PHG', 'JRT', 'JERANTUT', 'JERANTUT'),
	                            (47, 'PHG', 'KTN', 'KUANTAN', 'KUANTAN'),
	                            (48, 'PHG', 'KLS', 'KUALA LIPIS', 'LIPIS'),
	                            (49, 'PHG', 'PKN', 'PEKAN', 'PEKAN'),
	                            (50, 'PHG', 'RAU', 'RAUB', 'RAUB'),
	                            (51, 'PHG', 'TML', 'TEMERLOH', 'TEMERLOH'),
	                            (52, 'PHG', 'RMN', 'ROMPIN', 'ROMPIN'),
	                            (53, 'PHG', 'MRN', 'MARAN', 'MARAN'),
	                            (54, 'PHG', 'BER', 'BERA', 'BERA'),
	                            (55, 'PL', 'PRS', 'PERLIS', 'PERLIS'),
	                            (56, 'PP', 'BMM', 'SEBERANG PERAI TENGAH', 'SEBERANG PERAI TENGAH'),
	                            (57, 'PP', 'BWH', 'SEBERANG PERAI UTARA', 'SEBERANG PERAI UTARA'),
	                            (58, 'PP', 'NTL', 'SEBERANG PERAI SELATAN', 'SEBERANG PERAI SELATAN'),
	                            (59, 'PP', 'DTL', 'TIMUR LAUT PULAU PINANG', 'TIMUR LAUT PULAU PINANG'),
	                            (60, 'PP', 'DBD', 'BARAT DAYA PULAU PINANG', 'BARAT DAYA PULAU PINANG'),
	                            (61, 'PRK', 'BPG', 'BATANG PADANG', 'BATANG PADANG'),
	                            (62, 'PRK', 'MJG', 'MANJUNG', 'MANJUNG'),
	                            (63, 'PRK', 'KNA', 'KINTA', 'KINTA'),
	                            (64, 'PRK', 'KRN', 'KERIAN', 'KERIAN'),
	                            (65, 'PRK', 'KKR', 'KUALA KANGSAR', 'KUALA KANGSAR'),
	                            (66, 'PRK', 'LDM', 'LARUT, MATANG DAN SELAMA', 'LARUT, MATANG DAN SELAMA'),
	                            (67, 'PRK', 'HRP', 'HILIR PERAK', 'HILIR PERAK'),
	                            (68, 'PRK', 'HUP', 'HULU PERAK', 'HULU PERAK'),
	                            (69, 'PRK', 'PTG', 'PERAK TENGAH', 'PERAK TENGAH'),
	                            (70, 'PRK', 'SLM', 'SELAMA', 'LARUT, MATANG DAN SELAMA'),
	                            (71, 'SBH', 'TWU', 'TAWAU', 'TAWAU'),
	                            (72, 'SBH', 'LDU', 'LAHAD DATU', 'LAHAD DATU'),
	                            (73, 'SBH', 'SMA', 'SEMPORNA', 'SEMPORNA'),
	                            (74, 'SBH', 'SDN', 'SANDAKAN', 'SANDAKAN'),
	                            (75, 'SBH', 'TGD', 'TONGOD', 'TONGOD'),
	                            (76, 'SBH', 'LDS', 'LABUK DAN SUGUT', 'LABUK & SUGUT'),
	                            (77, 'SBH', 'KKU', 'KOTA KINABALU', 'KOTA KINABALU'),
	                            (78, 'SBH', 'RNU', 'RANAU', 'RANAU'),
	                            (79, 'SBH', 'KBD', 'KOTA BELUD', 'KOTA BELUD'),
	                            (80, 'SBH', 'TRI', 'TAMPARULI', 'TAMPARULI'),
	                            (81, 'SBH', 'PMG', 'PENAMPANG', 'PENAMPANG'),
	                            (82, 'SBH', 'PAR', 'PAPAR', 'PAPAR'),
	                            (83, 'SBH', 'KDT', 'KUDAT', 'KUDAT'),
	                            (84, 'SBH', 'KMU', 'KOTA MARUDU', 'KOTA MARUDU'),
	                            (85, 'SBH', 'PTS', 'PITAS', 'PITAS'),
	                            (86, 'SBH', 'BFT', 'BEAUFORT', 'BEAUFORT'),
	                            (87, 'SBH', 'MNK', 'MENUMBUK', 'MENUMBUK'),
	                            (88, 'SBH', 'STG', 'SIPITANG', 'SIPITANG'),
	                            (89, 'SBH', 'TNM', 'TENOM', 'TENOM'),
	                            (90, 'SBH', 'NBM', 'NABAWAN', 'NABAWAN'),
	                            (91, 'SBH', 'KGU', 'KENINGAU', 'KENINGAU'),
	                            (92, 'SBH', 'TBN', 'TAMBUNAN', 'TAMBUNAN'),
	                            (93, 'SBH', 'KNK', 'KUNAK', 'KUNAK'),
	                            (94, 'SBH', 'BRN', 'BELURAN', 'BELURAN'),
	                            (95, 'SBH', 'THN', 'TENGHILAN', 'TENGHILAN'),
	                            (96, 'SBH', 'BTN', 'BUNDU TUHAN', 'BUNDU TUHAN'),
	                            (97, 'SBH', 'MGL', 'MENGGATAL/ INANAM', 'MENGGATAL/ INANAM'),
	                            (98, 'SBH', 'KBN', 'KINABATANGAN', 'KINABATANGAN'),
	                            (99, 'SBH', 'BGG', 'BANGGI', 'BANGGI'),
	                            (100, 'SBH', 'TUR', 'TUARAN', 'TUARAN'),
	                            (101, 'SBH', 'KYU', 'KUALA PENYU', 'KUALA PENYU'),
	                            (102, 'SBH', 'TPD', 'TELUPID', 'TELUPID'),
	                            (103, 'SEL', 'GMK', 'GOMBAK', 'GOMBAK'),
	                            (104, 'SEL', 'KNG', 'KLANG', 'KLANG'),
	                            (105, 'SEL', 'KLT', 'KUALA LANGAT', 'KUALA LANGAT'),
	                            (106, 'SEL', 'KSG', 'KUALA SELANGOR', 'KUALA SELANGOR'),
	                            (107, 'SEL', 'PLG', 'PETALING', 'PETALING'),
	                            (108, 'SEL', 'SBM', 'SABAK BERNAM', 'SABAK BERNAM'),
	                            (109, 'SEL', 'SPG', 'SEPANG', 'SEPANG'),
	                            (110, 'SEL', 'HUL', 'HULU LANGAT', 'HULU LANGAT'),
	                            (111, 'SEL', 'HUS', 'HULU SELANGOR', 'HULU SELANGOR'),
	                            (112, 'SEL', 'AJY', 'AMPANG JAYA', 'AMPANG JAYA'),
	                            (113, 'SWK', 'KCG', 'KUCHING', 'KUCHING'),
	                            (114, 'SWK', 'BAU', 'BAU', 'BAU'),
	                            (115, 'SWK', 'SRN', 'SERIAN', 'SERIAN'),
	                            (116, 'SWK', 'SMJ', 'SIMUNJAN', 'SIMUNJAN'),
	                            (117, 'SWK', 'LND', 'LUNDU', 'LUNDU'),
	                            (118, 'SWK', 'SGG', 'SIMANGGANG', 'SRI AMAN/SIMANGGANG'),
	                            (119, 'SWK', 'LAU', 'LUBOK ANTU', 'LUBOK ANTU'),
	                            (120, 'SWK', 'SRS', 'SARIBAS', 'SARIBAS'),
	                            (121, 'SWK', 'KLK', 'KALAKA', 'KALAKA'),
	                            (122, 'SWK', 'SIB', 'SIBU', 'SIBU'),
	                            (123, 'SWK', 'MKH', 'MUKAH', 'MUKAH'),
	                            (124, 'SWK', 'KWT', 'KANOWIT', 'KANOWIT'),
	                            (125, 'SWK', 'OYA', 'OYA/ DALAT', 'DALAT'),
	                            (126, 'SWK', 'MIR', 'MIRI', 'MIRI'),
	                            (127, 'SWK', 'BTL', 'BINTULU', 'BINTULU'),
	                            (128, 'SWK', 'BRM', 'BARAM/MARUDI', 'BARAM'),
	                            (129, 'SWK', 'LBG', 'LIMBANG', 'LIMBANG'),
	                            (130, 'SWK', 'LWS', 'LAWAS', 'LAWAS'),
	                            (131, 'SWK', 'SRI', 'SARIKEI', 'SARIKEI'),
	                            (132, 'SWK', 'BTR', 'BINTANGGOR', 'BINTANGGOR'),
	                            (133, 'SWK', 'MAT', 'MATU', 'MATU'),
	                            (134, 'SWK', 'JLU', 'JULAU', 'JULAU'),
	                            (135, 'SWK', 'KPT', 'KAPIT', 'KAPIT'),
	                            (136, 'SWK', 'SON', 'SONG', 'SONG'),
	                            (137, 'SWK', 'SHN', 'SAMARAHAN', 'SAMARAHAN'),
	                            (138, 'SWK', 'MDG', 'MERADONG', 'MERADONG'),
	                            (139, 'SWK', 'SAN', 'SRI AMAN/SIMANGGANG', 'SRI AMAN/SIMANGGANG'),
	                            (140, 'SWK', 'DBK', 'DEBAK', 'DEBAK'),
	                            (141, 'SWK', 'SBN', 'SIBURAN', 'SIBURAN'),
	                            (142, 'SWK', 'BUD', 'BUDU(DK)', 'BUDU'),
	                            (143, 'SWK', 'GDG', 'GEDUNG', 'GEDUNG'),
	                            (144, 'SWK', 'MLD', 'MELUDANG', 'MELUDANG'),
	                            (145, 'SWK', 'NMD', 'NANGA MEDAMIT', 'NANGA MEDAMIT'),
	                            (146, 'SWK', 'NMR', 'NANGA MERIT', 'NANGA MERIT'),
	                            (147, 'SWK', 'PNT', 'PANTU', 'PANTU'),
	                            (148, 'SWK', 'PEM', 'PENDAM', 'PENDAM'),
	                            (149, 'SWK', 'SJY', 'SADONG JAYA', 'SADONG JAYA'),
	                            (150, 'SWK', 'TBD', 'TABEDU', 'TABEDU'),
	                            (151, 'TRG', 'BST', 'BESUT', 'BESUT'),
	                            (152, 'TRG', 'DGN', 'DUNGUN', 'DUNGUN'),
	                            (153, 'TRG', 'KMM', 'KEMAMAN', 'KEMAMAN'),
	                            (154, 'TRG', 'KTU', 'KUALA TERENGGANU', 'KUALA TERENGGANU'),
	                            (155, 'TRG', 'MRG', 'MARANG', 'MARANG'),
	                            (156, 'TRG', 'HUT', 'HULU TERENGGANU', 'HULU TERENGGANU'),
	                            (157, 'TRG', 'STU', 'SETIU', 'SETIU'),
	                            (158, 'WKL', 'KLR', 'KUALA LUMPUR', 'KUALA LUMPUR'),
	                            (159, 'WPL', 'LBN', 'LABUAN', 'LABUAN'),
	                            (160, 'WPP', 'PTR', 'PUTRAJAYA', 'PUTRAJAYA'),
	                            (161, 'SWK', 'SRT', 'SARATOK', 'SARATOK'),
	                            (162, 'KEL', 'LJG', 'LOJING', 'LOJING'),
	                            (163, 'KDH', 'ASR', 'ALOR SETAR', 'ALOR SETAR'),
	                            (164, 'KDH', 'SPI', 'SUNGAI PETANI', 'SUNGAI PETANI'),
	                            (165, 'PHG', 'LPS', 'LIPIS', 'LIPIS'),
	                            (166, 'PRK', 'KPR', 'KAMPAR', 'KAMPAR'),
	                            (167, 'SBH', 'PNS', 'PENSIANGAN', 'PENSIANGAN'),
	                            (168, 'SBH', 'LBA', 'LABUAN', 'LABUAN'),
	                            (169, 'SWK', 'ASJ', '	\r\nASAJAYA', '	\r\nASAJAYA'),
	                            (170, 'SWK', 'BLM', 'BALINGAN MUKAH', 'BALINGAN MUKAH'),
	                            (171, 'SWK', 'BAR', 'BARAM', 'BARAM'),
	                            (172, 'SWK', 'BKN', 'BEKENU', 'BEKENU'),
	                            (173, 'SWK', 'BLA', 'BELAGA', 'BELAGA'),
	                            (174, 'SWK', 'BTO', 'BETONG', 'BETONG'),
	                            (175, 'SWK', 'DLT', 'DALAT', 'DALAT'),
	                            (176, 'SWK', 'DRO', 'DARO', 'DARO'),
	                            (177, 'SWK', 'ENL', 'ENGKILI', 'ENGKILI'),
	                            (178, 'SWK', 'KBG', 'KABONG', 'KABONG'),
	                            (179, 'SWK', 'KKS', 'KECIL KABONG SARATOK', 'KECIL KABONG SARATOK'),
	                            (180, 'SWK', 'KNB', 'KECIL NIAH BINTULU', 'KECIL NIAH BINTULU'),
	                            (181, 'SWK', 'KSB', 'KECIL SADONG BATANG SADONG', 'KECIL SADONG BATANG SADONG'),
	                            (182, 'SWK', 'KTS', 'KOTA SAMARAHAN', 'KOTA SAMARAHAN'),
	                            (183, 'SWK', 'KRI', 'KRIAN', 'KRIAN'),
	                            (184, 'SWK', 'LMB', 'LAMBIR', 'LAMBIR'),
	                            (185, 'SWK', 'LOL', 'LONG LAMA BARAM', 'LONG LAMA BARAM'),
	                            (186, 'SWK', 'MAL', 'MALUDAM', 'MALUDAM'),
	                            (187, 'SWK', 'MRD', 'MARUDI', 'MARUDI'),
	                            (188, 'SWK', 'MUK', 'MUKAH', 'MUKAH'),
	                            (189, 'SWK', 'NNM', 'NANGA MEDAMIT', 'NANGA MEDAMIT'),
	                            (190, 'SWK', 'NGM', 'NANGA MERIT', 'NANGA MERIT'),
	                            (191, 'SWK', 'NIA', 'NIAH', 'NIAH'),
	                            (192, 'SWK', 'PDW', 'PADAWAN', 'PADAWAN'),
	                            (193, 'SWK', 'PKA', 'PAKAN', 'PAKAN'),
	                            (194, 'SWK', 'PUS', 'PUSA', 'PUSA'),
	                            (195, 'SWK', 'RBN', 'ROBAN', 'ROBAN'),
	                            (196, 'SWK', 'SEB', 'SEBAUH', 'SEBAUH'),
	                            (197, 'SWK', 'SBY', 'SEBUYAU', 'SEBUYAU'),
	                            (198, 'SWK', 'SEL', 'SELANGAU', 'SELANGAU'),
	                            (199, 'SWK', 'SMT', 'SEMANTAN', 'SEMANTAN'),
	                            (200, 'SWK', 'SRA', 'SRI AMAN', 'SRI AMAN'),
	                            (201, 'SWK', 'SPA', 'SPAOH', 'SPAOH'),
	                            (202, 'SWK', 'SUA', 'SUAI', 'SUAI'),
	                            (203, 'SWK', 'SUB', 'SUBIS', 'SUBIS'),
	                            (204, 'SWK', 'TAT', 'TATAU', 'TATAU'),
	                            (205, 'SWK', 'TEB', 'TEBAKANG', 'TEBAKANG'),
	                            (206, 'SWK', 'TIN', 'TINJAR', 'TINJAR'),
	(207, 'SWK', 'ULN', 'ULU NIAH', 'ULU NIAH');
                            ";
                #endregion
                cmd.CommandText = qry;
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }

        public int SetupVariableSetting(SQLiteConnection cnn)
        {
            int i = 0;
            using (SQLiteCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                #region Create & Insert
                string qry = @"
                             DROP TABLE IF  EXISTS `VariableSetting`;

                            CREATE TABLE IF NOT EXISTS VariableSetting
                            (
                                Key varchar(50) NOT NULL PRIMARY KEY,
                                Value varchar(250) COLLATE NOCASE NOT NULL,
                                Description varchar(250) COLLATE NOCASE,
                                CanModify int NOT NULL,
                                IsEncrypt bit NOT NULL
                            );
                            
                             DELETE FROM VariableSetting;

                            INSERT INTO VariableSetting (Key, Value, Description, CanModify, IsEncrypt) VALUES 
                            --    ('passPhrase', '#1UWLyP1','', 0, 0)
                            --, ('saltValue', 's@1tValue','', 0, 0)
                            --, ('hashAlgorithm', 'SHA1','', 0, 0)
                            --, ('passwordIterations', '2','', 0, 0)
                            --, ('initVector', '@1B2c3D4e5F6g7H8','', 0, 0)
                            --, ('keySize', '256','', 0, 0)
                            ('Status', 'Development','', 0, 0)
                            , ('UserKeyIn', 'System','', 0, 0)
                            , ('MySQLConn', '','Connection To MySQL Server', 0, 1)
                            , ('HiddenDataSync', 'true','Hidden Data Sync', 0, 0)
                    ";
                #endregion
                cmd.CommandText = qry;
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }

        public int SetupTBangsa(SQLiteConnection cnn)
        {
            int i = 0;
            using (SQLiteCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                #region Create & Insert
                string qry = @"
                             DROP TABLE IF  EXISTS `TBANGSA`;
                            CREATE TABLE IF NOT EXISTS TBANGSA
                            (
                                BANGSA varchar(100) COLLATE NOCASE
                            );

                            DELETE FROM TBANGSA;

                            INSERT INTO TBANGSA (BANGSA) VALUES
                                ('BUMIPUTERA SABAH'), ('BUMIPUTERA SARAWAK'), ('BUMIPUTERA SEMENANJUNG'), ('CINA'), ('INDIA'), ('LAIN- LAIN'), ('MELAYU')";
                #endregion
                cmd.CommandText = qry;
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }

        public int SetupSemakTapak(SQLiteConnection cnn)
        {
            int i = 0;
            using (SQLiteCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandText = @"
                                 DROP TABLE IF  EXISTS `semak_tapak`;

                                -- Dumping structure for table tsspk1511.semak_tapak
                                CREATE TABLE IF NOT EXISTS `semak_tapak` (
                                  `id` integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                                  `makkebun_id` int(11) NOT NULL,
                                  `appinfo_id` int(11) DEFAULT NULL,
                                  `kaedah` varchar(150) DEFAULT NULL,
                                  `bantuan` varchar(150) DEFAULT NULL,
                                  `jenis_tanah` varchar(150) DEFAULT NULL,
                                  `kecerunan` varchar(40) DEFAULT NULL,
                                  `jentera` char(5) DEFAULT NULL,
                                  `ganoderma` char(5) DEFAULT NULL,
                                  `peratusan_serangan` double DEFAULT NULL,
                                  `umr_pokok_tua` varchar(100) DEFAULT NULL,
                                  `hasil` varchar(50) DEFAULT NULL,
                                  `bil_pokok_tua` varchar(10) DEFAULT NULL,
                                  `tarikh_lawat` varchar(15) DEFAULT NULL,
                                  `ptk_lawat` varchar(150) DEFAULT NULL,
                                  `luas` double DEFAULT NULL,
                                  `catatan` text,
                                  `created` datetime DEFAULT NULL,
                                  `createdby` varchar(100) DEFAULT NULL,
                                  `lampiran` varchar(100) NOT NULL,
                                    newid integer null,
                                    newmakkebun_id integer null,
                                    syncdate datetime null
                                );
                           ";
                cmd.CommandType = CommandType.Text;
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
    }
}
