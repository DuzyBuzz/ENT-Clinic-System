CREATE DATABASE  IF NOT EXISTS `ent_clinic_db_2` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `ent_clinic_db_2`;
-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: ent_clinic_db
-- ------------------------------------------------------
-- Server version	8.0.43

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `admit_orders`
--

DROP TABLE IF EXISTS `admit_orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admit_orders` (
  `admit_order_id` int NOT NULL AUTO_INCREMENT,
  `patient_id` int NOT NULL,
  `admit_date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `special_orders` mediumtext NOT NULL,
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`admit_order_id`),
  KEY `patient_id` (`patient_id`),
  CONSTRAINT `admit_orders_ibfk_1` FOREIGN KEY (`patient_id`) REFERENCES `patients` (`patient_id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `admit_orders`
--

LOCK TABLES `admit_orders` WRITE;
/*!40000 ALTER TABLE `admit_orders` DISABLE KEYS */;
/*!40000 ALTER TABLE `admit_orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `admit_template`
--

DROP TABLE IF EXISTS `admit_template`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admit_template` (
  `template_text` mediumtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `admit_template`
--

LOCK TABLES `admit_template` WRITE;
/*!40000 ALTER TABLE `admit_template` DISABLE KEYS */;
INSERT INTO `admit_template` VALUES ('{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang2057{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}{\\f1\\fnil Segoe UI;}{\\f2\\fnil\\fcharset0 Microsoft Sans Serif;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\pard\\b\\f0\\fs18 CC:\\b0\\f1\\par\r\nImpression:\\par\r\nPlease admit to room of choice under my service.\\par\r\nTPR q shift and record\\par\r\nDAT:\\par\r\nIVF:\\par\r\nLABS:\\par\r\nMerch:\\par\r\nSpecial Orders:\\par\r\n\\f2\\fs17\\par\r\n}\r\n');
/*!40000 ALTER TABLE `admit_template` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `appointments`
--

DROP TABLE IF EXISTS `appointments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `appointments` (
  `follow_up_id` int NOT NULL AUTO_INCREMENT,
  `patient_id` int DEFAULT NULL,
  `follow_up_date` date NOT NULL,
  `note` text,
  PRIMARY KEY (`follow_up_id`),
  KEY `appointments_ibfk_1` (`patient_id`),
  CONSTRAINT `appointments_ibfk_1` FOREIGN KEY (`patient_id`) REFERENCES `patients` (`patient_id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `appointments`
--

LOCK TABLES `appointments` WRITE;
/*!40000 ALTER TABLE `appointments` DISABLE KEYS */;
INSERT INTO `appointments` VALUES (10,2072,'2025-12-04',NULL),(11,2073,'2025-10-27',NULL),(12,2086,'2025-10-30','FOLLOW WITH RESULT OF LABORATORY'),(13,2072,'2025-10-26','WHY DO WE USE IT?\nIT IS A LONG ESTABLISHED FACT THAT A READER WILL BE DISTRACTED BY THE READABLE CONTENT OF A PAGE WHEN LOOKING AT ITS LAYOUT. THE POINT OF USING LOREM IPSUM IS THAT IT HAS A MORE-OR-LESS NORMAL DISTRIBUTION OF LETTERS, AS OPPOSED TO USING \'CONTENT HERE, CONTENT HERE\', MAKING IT LOOK LIKE READABLE ENGLISH. MANY DESKTOP PUBLISHING PACKAGES AND WEB PAGE EDITORS NOW USE LOREM IPSUM AS THEIR DEFAULT MODEL TEXT, AND A SEARCH FOR \'LOREM IPSUM\' WILL UNCOVER MANY WEB SITES STILL IN THEIR INFANCY. VARIOUS VERSIONS HAVE EVOLVED OVER THE YEARS, SOMETIMES BY ACCIDENT, SOMETIMES ON PURPOSE (INJECTED HUMOUR AND THE LIKE).'),(14,2020,'2025-10-25','REMOVAL OF EAR WICK'),(15,2072,'2025-10-28','NOTES');
/*!40000 ALTER TABLE `appointments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `attachments`
--

DROP TABLE IF EXISTS `attachments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `attachments` (
  `attachment_id` int NOT NULL AUTO_INCREMENT,
  `consultation_id` int DEFAULT NULL,
  `patient_id` int NOT NULL,
  `file_path` varchar(500) NOT NULL,
  `file_type` enum('Image','Video') NOT NULL,
  `category` varchar(100) DEFAULT 'General',
  `date_added` datetime DEFAULT CURRENT_TIMESTAMP,
  `note` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`attachment_id`),
  KEY `patient_id` (`patient_id`),
  KEY `fk_attachments_consultation` (`consultation_id`),
  CONSTRAINT `attachments_ibfk_1` FOREIGN KEY (`patient_id`) REFERENCES `patients` (`patient_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_attachments_consultation` FOREIGN KEY (`consultation_id`) REFERENCES `consultation` (`consultation_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=195 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `attachments`
--

LOCK TABLES `attachments` WRITE;
/*!40000 ALTER TABLE `attachments` DISABLE KEYS */;
INSERT INTO `attachments` VALUES (152,247,2073,'D:\\ENT_CLINIC_Attachments\\2073\\2025-10-20\\Images\\image_20251020_124846_125019320.png','Image','General','2025-10-20 12:50:19',''),(153,247,2073,'D:\\ENT_CLINIC_Attachments\\2073\\2025-10-20\\Images\\image_20251020_124858_125019320.png','Image','General','2025-10-20 12:50:19',''),(156,250,2077,'D:\\ENT_CLINIC_Attachments\\2077\\2025-10-20\\Images\\image_20251020_131201_131516624.png','Image','General','2025-10-20 13:15:16',''),(157,250,2077,'D:\\ENT_CLINIC_Attachments\\2077\\2025-10-20\\Images\\image_20251020_131214_131516655.png','Image','General','2025-10-20 13:15:16',''),(158,252,2074,'D:\\ENT_CLINIC_Attachments\\2074\\2025-10-20\\Images\\image_20251020_133442_134200824.png','Image','General','2025-10-20 13:42:00',''),(159,252,2074,'D:\\ENT_CLINIC_Attachments\\2074\\2025-10-20\\Images\\image_20251020_133453_134200840.png','Image','General','2025-10-20 13:42:00',''),(160,253,2078,'D:\\ENT_CLINIC_Attachments\\2078\\2025-10-20\\Images\\image_20251020_134317_134738370.png','Image','General','2025-10-20 13:47:38',''),(161,253,2078,'D:\\ENT_CLINIC_Attachments\\2078\\2025-10-20\\Images\\image_20251020_134332_134738386.png','Image','General','2025-10-20 13:47:38',''),(165,258,2056,'D:\\ENT_CLINIC_Attachments\\2056\\2025-10-21\\Images\\image_20251021_141253_141738159.png','Image','General','2025-10-21 14:17:38',''),(166,259,2081,'D:\\ENT_CLINIC_Attachments\\2081\\2025-10-21\\Images\\image_20251021_142007_142608444.png','Image','General','2025-10-21 14:26:08',''),(167,259,2081,'D:\\ENT_CLINIC_Attachments\\2081\\2025-10-21\\Images\\image_20251021_142020_142608444.png','Image','General','2025-10-21 14:26:08',''),(168,260,2082,'D:\\ENT_CLINIC_Attachments\\2082\\2025-10-21\\Images\\image_20251021_143342_143847129.png','Image','General','2025-10-21 14:38:47',''),(169,260,2082,'D:\\ENT_CLINIC_Attachments\\2082\\2025-10-21\\Images\\image_20251021_143354_143847129.png','Image','General','2025-10-21 14:38:47',''),(170,261,2084,'D:\\ENT_CLINIC_Attachments\\2084\\2025-10-21\\Images\\image_20251021_144451_145205022.png','Image','General','2025-10-21 14:52:05',''),(171,261,2084,'D:\\ENT_CLINIC_Attachments\\2084\\2025-10-21\\Images\\image_20251021_144507_145205022.png','Image','General','2025-10-21 14:52:05',''),(172,262,2085,'D:\\ENT_CLINIC_Attachments\\2085\\2025-10-21\\Images\\image_20251021_145617_150108051.png','Image','General','2025-10-21 15:01:08',''),(173,262,2085,'D:\\ENT_CLINIC_Attachments\\2085\\2025-10-21\\Images\\image_20251021_145631_150108051.png','Image','General','2025-10-21 15:01:08',''),(174,269,2088,'D:\\ENT_CLINIC_Attachments\\2088\\2025-10-23\\Images\\image_20251023_115222_120331033.png','Image','General','2025-10-23 12:03:31',''),(175,269,2088,'D:\\ENT_CLINIC_Attachments\\2088\\2025-10-23\\Images\\image_20251023_115239_120331038.png','Image','General','2025-10-23 12:03:31',''),(176,269,2088,'D:\\ENT_CLINIC_Attachments\\2088\\2025-10-23\\Images\\image_20251023_115727_120331043.png','Image','General','2025-10-23 12:03:31',''),(177,270,2090,'D:\\ENT_CLINIC_Attachments\\2090\\2025-10-23\\Images\\image_20251023_120924_121721071.png','Image','General','2025-10-23 12:17:21',''),(178,270,2090,'D:\\ENT_CLINIC_Attachments\\2090\\2025-10-23\\Images\\image_20251023_120936_121721076.png','Image','General','2025-10-23 12:17:21',''),(179,270,2090,'D:\\ENT_CLINIC_Attachments\\2090\\2025-10-23\\Images\\image_20251023_121319_121721080.png','Image','General','2025-10-23 12:17:21',''),(180,271,2091,'D:\\ENT_CLINIC_Attachments\\2091\\2025-10-23\\Images\\image_20251023_130147_130344121.png','Image','General','2025-10-23 13:03:44',''),(181,271,2091,'D:\\ENT_CLINIC_Attachments\\2091\\2025-10-23\\Images\\image_20251023_130158_130344159.png','Image','General','2025-10-23 13:03:44',''),(182,271,2091,'D:\\ENT_CLINIC_Attachments\\2091\\2025-10-23\\Images\\image_20251023_130213_130344163.png','Image','General','2025-10-23 13:03:44',''),(183,272,2092,'D:\\ENT_CLINIC_Attachments\\2092\\2025-10-23\\Images\\image_20251023_131236_131533880.png','Image','General','2025-10-23 13:15:33',''),(184,272,2092,'D:\\ENT_CLINIC_Attachments\\2092\\2025-10-23\\Images\\image_20251023_131252_131533884.png','Image','General','2025-10-23 13:15:33',''),(185,274,2074,'D:\\ENT_CLINIC_Attachments\\2074\\2025-10-23\\Images\\image_20251023_135314_135806578.png','Image','General','2025-10-23 13:58:06',''),(186,274,2074,'D:\\ENT_CLINIC_Attachments\\2074\\2025-10-23\\Images\\image_20251023_135325_135806582.png','Image','General','2025-10-23 13:58:06',''),(187,281,2008,'\\\\SERVER\\Shared\\2008\\2025-10-26\\Images\\image_20251026_231916_231926978.png','Image','General','2025-10-26 23:19:26',''),(188,281,2008,'\\\\SERVER\\Shared\\2008\\2025-10-26\\Images\\image_20251026_231916_231927002.png','Image','General','2025-10-26 23:19:27',''),(189,281,2008,'\\\\SERVER\\Shared\\2008\\2025-10-26\\Images\\image_20251026_231916_231927019.png','Image','General','2025-10-26 23:19:27',''),(190,281,2008,'\\\\SERVER\\Shared\\2008\\2025-10-26\\Images\\image_20251026_231921_231927037.png','Image','General','2025-10-26 23:19:27',''),(191,282,2008,'\\\\SERVER\\Shared\\2008\\2025-10-26\\Images\\image_20251026_232049_232054620.png','Image','General','2025-10-26 23:20:54',''),(192,283,2035,'\\\\SERVER\\Shared\\2035\\2025-10-26\\Images\\image_20251026_232136_232236745.png','Image','General','2025-10-26 23:22:36',''),(193,283,2035,'\\\\SERVER\\Shared\\2035\\2025-10-26\\Images\\image_20251026_232144_232236763.png','Image','General','2025-10-26 23:22:36',''),(194,283,2035,'\\\\SERVER\\Shared\\2035\\2025-10-26\\Images\\image_20251026_232144_232236783.png','Image','General','2025-10-26 23:22:36','');
/*!40000 ALTER TABLE `attachments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `autocomplete_entries`
--

DROP TABLE IF EXISTS `autocomplete_entries`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `autocomplete_entries` (
  `id` int NOT NULL AUTO_INCREMENT,
  `column_name` varchar(100) DEFAULT NULL,
  `value` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `value` (`value`)
) ENGINE=InnoDB AUTO_INCREMENT=912 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `autocomplete_entries`
--

LOCK TABLES `autocomplete_entries` WRITE;
/*!40000 ALTER TABLE `autocomplete_entries` DISABLE KEYS */;
INSERT INTO `autocomplete_entries` VALUES (2,'chief_complaint','Mild nasal congestions '),(3,'chief_complaint','intermittent sore throat '),(4,'history','Sore throat come and go '),(5,'history','tinatus '),(6,'history','nasal congestions '),(7,'ear_exam','normal shape no lesions '),(8,'ear_exam','clear '),(9,'ear_exam','tympanic membrane intact '),(10,'nose_exam','external nose no deformity '),(11,'nose_exam','slightly pale '),(12,'nose_exam','midline, no diviation '),(13,'throat_exam','lips and oral cavity normal '),(14,'throat_exam','neck no lymphadenopathy '),(15,'diagnosis','mild alergic hinitis '),(16,'diagnosis','begign tinnitus(right ear) '),(17,'diagnosis','mild pharyngitis '),(18,'recommendations','Voice care '),(19,'recommendations','dehydration for mild sore throat '),(20,'notes','if syntoms worsen '),(21,'chief_complaint','EAR DISCHARGE '),(22,'history','NOTED EAR INFECTION 3 YEARS PTC '),(23,'ear_exam','TM PERFORATION AD 70% '),(24,'diagnosis','OTITIS MEDIA, AS '),(25,'recommendations','KEEP RIGHT EAR DRY '),(26,'chief_complaint','HEARING LOSS LEFT EAR '),(27,'history','ON AND OFF HEARING LOSS '),(28,'ear_exam','IMPACTED CERUMEN, AS '),(29,'diagnosis','IMPACTED CERUMEN '),(30,'recommendations','PROPER EAR CLEANING '),(31,'history','FOLLOW UP '),(32,'ear_exam','PERFORATED TM PERFORATION AS, 40% '),(33,'ear_exam','WITH MINIMAL DISCHARGE '),(34,'diagnosis','IMPACTED CERUMEN WITH TYMPANIC MEMBRANE PERFORATION, AS '),(35,'recommendations','KEEP LEFT EAR DRY '),(36,'chief_complaint','EAR FULLNESS AS '),(37,'history','3 DAYS ITCHINESS WITH EAR FULLNESS '),(38,'ear_exam','FUNGAL ELEMENTS AS '),(39,'diagnosis','OTOMYCOSIS, AS '),(40,'chief_complaint','EAR CHECK UP '),(41,'history','ITCHINESS X 5 DAYS '),(42,'ear_exam','YELLOWISH DISCHARGE AS, WITH FUNGAL ELEMENTS '),(43,'notes','EAR CLEANING '),(44,'chief_complaint','EAR IRRITATION '),(45,'history','1 MONTH ON AND OFF EAR IRRITATION '),(46,'history','NO HEARING LOSS '),(47,'ear_exam','DRY EARS '),(48,'ear_exam','MILD ITCHINESS '),(49,'diagnosis','MILD OTOMYCOSIS, AS '),(50,'chief_complaint','NASAL CONGESTION '),(51,'history','2 WEEKS CONGESTION '),(52,'nose_exam','YELLOWISH NASAL DISCHARGE BOTH NOSE '),(53,'nose_exam','MEATY MASS ON BOTH NASOPHARYNX MORE ON THE LEFT '),(54,'throat_exam','NO POST NASAL DRIP '),(55,'diagnosis','RHINOSINOSITIS WITH NASO PHARYNGEAL AND INTRA NASAL MASS '),(56,'recommendations','FOR CT SCAN OF THE PARANASL SINUSES, PLAIN '),(57,'notes','WITH CT SCAN RESULT '),(58,'history','5 DAYS NOTED EPISTAXIS '),(59,'history','ASSOCIATED WITH NASAL PAIN ANDCONGESTION '),(60,'ear_exam','INTACT '),(61,'nose_exam','EPISTAXIS '),(62,'diagnosis','RHINOSINUSITIS WITH INACTIVE EPISTAXIS '),(63,'recommendations','X- RAY OF THE PNS '),(64,'chief_complaint','HEARING LOSS '),(65,'history','REFERRED FOR EVALUATION BY SUPERCARE DUE TO INADEQUATE HEARING 2 MONTHS PTC '),(66,'ear_exam','PERFORATED TM, AS '),(67,'diagnosis','MODERATE CONDUCTIVE HEARING LOSS AS '),(69,'recommendations','FOR HEARING AID FITTING AS '),(70,'chief_complaint','EAR PAIN '),(71,'history','3 DAYS EAR PAIN '),(72,'ear_exam','IC, AS '),(73,'ear_exam','SWELLING RIGHT EAR CANAL '),(74,'diagnosis','OTITIS EXTERNA, AD '),(76,'history','1 WEEK NOTED EAR DISCHARGE '),(77,'history','CONSULTED SPH AND GIVEN OTIC DROPS '),(78,'ear_exam','MODERATE DISCHARGE WITH TM PERFORATION '),(79,'diagnosis','CHRONIC OTITIS MEDIA, AD '),(80,'history','1 WEEK NTED EAR PAIN, LEFT '),(81,'history','NO FEVER '),(82,'history','(+) NASAL CATARRH '),(83,'ear_exam','INTACT TM - AU '),(85,'chief_complaint','HEARING DIFFICULTY '),(86,'history','2 DAYS PTC, NOTED HEARING DIFFICULTY '),(87,'ear_exam','FUNGA ELEMENTS, AD '),(88,'diagnosis','OTOMYCOSIS WITH OM, AD '),(89,'chief_complaint','FOLLOW-UP '),(90,'history','NOTED VERTIGO, ON AND OFF '),(91,'diagnosis','CANALOLITHIASIS, MENIERE\'S DISEASE '),(92,'recommendations','LOW SALT DIET '),(94,'history','TREATED WITH ANTIBIOTICS FOR 1 WEEK '),(95,'nose_exam','NASAL POLYP, RIGHT GRADE 2 '),(96,'diagnosis','RHINOSINUSITIS, WITH GRADE 2 NASAL POLYP, RIGHT '),(97,'recommendations','FOR CT SCAN OF THE PARANASAL SINUSES (PLAIN) '),(99,'history','NOTED AURAL POLYP AS, 1 MONTH PTC '),(100,'ear_exam','AURAL POLYP- RESOLVED '),(101,'ear_exam','WHITTISH EAR DEPOSIT, AS '),(102,'ear_exam','HYPEREMIC EAR CANAL '),(103,'nose_exam','FREQUENT NASAL CLEARING '),(104,'diagnosis','COM, WITH AURAL POLYP AND CHOLESTEATOMA, AS '),(105,'recommendations','CONTINUE EAR DROPS OD X 1 WEEK MORE '),(106,'recommendations','MOMETASONE OD X 1 MONTH '),(107,'history','EAR FULLNES X 1 WEEK '),(108,'history','GIVEN ANTIBIOTICS - CO-AMOXICLAV AND OTOBIOTIC '),(109,'ear_exam','INTACT TM,AS '),(110,'ear_exam','CERUMEN, AD '),(111,'nose_exam','FREQUENT SNEEZING '),(112,'diagnosis','ACUTE OTITIS MEDIA WITH IMPACTED CERUMEN AD '),(113,'diagnosis','ALLERGIC RHINITIS '),(114,'chief_complaint','EAR ITCHINESS '),(115,'history','1 WEEK NOTED ON AND OFF EAR IRRITATION '),(116,'ear_exam','INTACT TM, AURAL POLYP- RESOLVED '),(117,'diagnosis','PARTIALLY IMPACTED CERUMEN '),(118,'history','1 MONTH NOTED EAR FULLNESS '),(119,'history','CONSULTED AND NOTED EAR PERFORTION '),(120,'diagnosis','CHRONIC OTITS MEDIA, AD, AURAL POYP '),(121,'history','1 WEEK NOTED ITCHINESS WITH IRRITATION, BOTH EARS '),(122,'ear_exam','BILATERAL EAR DISCHARGE '),(123,'diagnosis','OTOMYCOSIS, AU '),(124,'history','DIAGNOSED WITH NASAL POLYP '),(125,'history','S/P FESS 2016 '),(126,'history','1 WEEK CONSULTED FOR CLEARANCE '),(127,'history','REQUESTED FOR CT SCAN OF THE PNS '),(128,'history','CAME BACK WITH RESULT '),(129,'nose_exam','BILATERAL NASAL POLYP GRADE 3 '),(130,'nose_exam','•MAXILLARY AND ETHMOID SINUSITIS ON CT SCAN '),(131,'diagnosis','NASAL POLYP LEFT WITH MAXILLARY SINUSITIS '),(132,'recommendations','FOR FESS '),(134,'history','WITH HEARING AID '),(135,'ear_exam','INTACT TM AU '),(136,'ear_exam','DRY CERUMEN, AU '),(137,'diagnosis','MILD OE, AU '),(138,'recommendations','CONTINUE HEARING AID USE '),(139,'chief_complaint','THROAT DISCOMFORT X 1 MONTH '),(140,'history','1 MONTH NOTED THROAT DISCOMFORT '),(141,'history','UTZ DONE WHICH REVEALED NORMAL '),(142,'throat_exam','SLIGHTLY HYPEREMIC OROPHARYNX '),(143,'diagnosis','LARYNGOPHARYNGEAL REFLUX '),(144,'recommendations','FOR FNL IF UNRESOLVED AFTER 2 WEEKS '),(145,'chief_complaint','THROAT DISCOMFORT '),(146,'history','FREQUENT THROAT CLEARING ESPECIALLY AT DAYTIME '),(147,'diagnosis','ALLERGIC PHARYNGITIS '),(148,'chief_complaint','FOREIGN BODY CLAY '),(149,'history','1 WEEK PTC, NOTED CLAY ON THE RIGHT EAR '),(150,'diagnosis','FOREIGN BODY, CLAY AD '),(151,'history','1 WEEK NOTED BILATERAL DISCHARGE '),(152,'ear_exam','BILATERAL YELLOWISH DISCHARGE '),(153,'diagnosis','ACUTE OTITIS MEDIA, AU '),(154,'chief_complaint','referrred FOR EALUATION '),(155,'chief_complaint','NO OTHER SUBJECTIVE COMPLAINTS '),(156,'history','BY SUPERCARE AND CT SCAN RESULT REVEALED ETHMOID AND MAXILARY SINUSITIS WITH MUCOUS RETENTION CYST RIGHT '),(157,'nose_exam','MODERATE NASAL DISCHARGE '),(158,'nose_exam','NO OTHER MASSES OR FINDINGS '),(159,'diagnosis','ETHMOID AND MAXILLARY SINUSITIS, WITH RIGHT MUCOUS RETENTION CYST '),(160,'recommendations','FIT FOR WORK '),(162,'history','HEARING LOSS 4 DAYS PTC '),(163,'history','CONSULT AND GIVEN ANTIBIOTICS- CO AMOXICLAV, OTOQURE '),(164,'ear_exam','MODERATE DISCHARGE WITH FUNGAL ELEMENTS, AD '),(165,'ear_exam','INTACT TM AS '),(166,'diagnosis','OTITIS MEDIA AD WITH OTOMYCOSIS '),(167,'ear_exam','INTACT TYMPANIC MEMBRANES, AU '),(168,'diagnosis','IMPACTED CERUMEN AU '),(169,'chief_complaint','FOR CLEARANCE '),(170,'diagnosis','COM WITH CHOLESTEATOMA '),(171,'diagnosis','S/P MASTOIDECTOMY AS (2005) '),(172,'diagnosis','S/P REVISION MASTOIDECTOMY (2019) '),(173,'recommendations','YEARLY CHECK-UP '),(174,'ear_exam','INTACT TM, AU '),(175,'ear_exam','EAR WAX, AS '),(176,'diagnosis','OTITIS EXTERNA, AS '),(177,'chief_complaint','REFERRED BY SUPERCARE '),(178,'history','NOTED FLU-LIKE SYMPTOMS FOR 1 WEEK '),(179,'history','CONSULTED PREVIOUS PHYSICIAN AND GAVE ANTIBIOTICS '),(180,'history','CT SCAN REQUESTED WITH RESULT OF POLYSINUSITIS '),(181,'nose_exam','THICK NASAL DISCHARGE '),(182,'nose_exam','BUGGY TURBINATES '),(183,'diagnosis','POLYSINUSITIS '),(184,'recommendations','FIT TO WORK ONCE ANTIBIOTICS IS COMPLETED '),(185,'history','1 PTC WEEK NOTED EAR PAIN '),(186,'ear_exam','FOREIGN BODY, INSECT LEG, AD '),(188,'recommendations','REMOVAL OF FOREIGN BODY '),(189,'nose_exam','NASAL DISCHARGE '),(190,'recommendations','ALLERGEN AVOIDANCE '),(191,'chief_complaint','EAR FULLNESS RIGHT EAR '),(192,'history','2 MONTHS NOTED EAR FULLNESS '),(193,'history','5 YEARS PTC-  MASS/ LUMP LEFT POST AURICULAR AREA (TEMPORO-OCCIPITAL) '),(194,'diagnosis','EUSTACHIAN TUBE DYSFUNCTION '),(195,'diagnosis','LIPOMA, LEFT TEMPORO-OCCIPITAL AREA '),(196,'recommendations','FOR PTA '),(197,'recommendations','FOR CT SCAN OF THE HEAD IF UNRESOLED '),(198,'recommendations','FOR FNAB OF THE LEFT TEMPORO-OCCIPITAL MASS IF WITH PROGRESSION IN SIZE '),(199,'history','1 MONTH CONSULTED MY CLINIC AND GIVEN ANTIBIOTICS FOR SINUSITIS AND NASAL POLYP-CLARITHROMYCIN, PEPRAZOM, ACETYLCYSTEINE, NACL NASAL SRAY, MOMETASONE NASAL SPRAY '),(200,'history','WITH RESULT OF X-RAY: NASAL CONGESTION '),(201,'diagnosis','RHINOSINUSITIS WITH GRADE 2 POLYP BILATERAL '),(202,'recommendations','CONTINUE NASAL SPRAY AND ANTIBIOTICS '),(203,'recommendations','ADVISED FESS '),(204,'chief_complaint','NASAL CONGESTION X 1 WEEK '),(205,'history','• TOOK SINUPRET X 1 WEEK AND RYALTRIS NASAL SPRAY '),(207,'nose_exam','YELLOWISH NASAL DISCHARGE '),(208,'diagnosis','RHINOSINUSITIS BILATERAL '),(209,'recommendations','FOR ANTIBIOTICS: '),(210,'recommendations','CLINDAMYCIN 300 MG TID '),(211,'recommendations','SERRAPEPTASE 1 TAB TID '),(212,'recommendations','NACLA NASAL SPRAY '),(213,'recommendations','MOMETASONE NASAL SPRAY '),(214,'past_medical_history','NONE'),(215,'history',', NONE '),(216,'diagnosis','vsfssvf'),(217,'chief_complaint','NASAL CONGESTION'),(218,'history','rhinitis'),(219,'history','CONSULT AND GIVEN ANTIBIOTICS- CO AMOXICLAV, OTOQURE'),(220,'past_medical_history','given antibiotics'),(221,'past_medical_history','azithromycin- 5 days'),(222,'procedures','nasal endoscopy'),(223,'nose_exam','polyp, left nose, grade 1'),(224,'diagnosis','RHINOSINOSITIS WITH NASO PHARYNGEAL AND INTRA NASAL MASS'),(225,'diagnosis','rhinosinusitis'),(226,'diagnosis','NASAL POLYP LEFT WITH MAXILLARY SINUSITIS'),(227,'diagnosis','NASAL POLYP LEFT grade 1'),(228,'diagnosis','maxillary sinusitis'),(229,'recommendations','for fess'),(230,'recommendations','ct scan pns'),(231,'throat_exam','SLIGHTLY HYPEREMIC OROPHARYNX'),(232,'history','rhinitis, azithromycin- 5 days '),(233,'nose_exam','polyp, left nose, grade 1 '),(234,'diagnosis','rhinosinusitis, maxillary sinusitis, NASAL POLYP LEFT grade 1 '),(235,'chief_complaint','cough'),(236,'past_medical_history','cough x 1 week'),(237,'diagnosis','LARYNGOPHARYNGEAL REFLUX'),(238,'chief_complaint','THROAT DISCOMFORT X 1 MONTH'),(239,'ear_exam','EAR WAX, AS'),(240,'ear_exam','CERUMEN, AD'),(241,'ear_exam','SWELLING RIGHT EAR CANA'),(242,'ear_exam','INTACT TM, AU'),(243,'ear_exam','SWELLING RIGHT EAR CANAL'),(244,'ear_exam','INTACT TM - AU'),(245,'nose_exam','NORMAL'),(246,'nose_exam','TURBINATES NOT ENLARGED'),(247,'chief_complaint','cough '),(248,'history',', cough x 1 week '),(249,'nose_exam','TURBINATES NOT ENLARGED '),(250,'chief_complaint','vertigo'),(251,'history','1 month noted vertigo with tinnitus'),(252,'past_medical_history','meniere\'s disease 2023'),(253,'history','vertigo with tinnitus x 1 month'),(254,'diagnosis','meniere\'s disease'),(255,'recommendations','LOW SALT DIET'),(256,'recommendations','vertigo exercises'),(257,'recommendations','ptabs'),(258,'chief_complaint','vertigo '),(259,'history','vertigo with tinnitus x 1 month, meniere\'s disease 2023 '),(260,'diagnosis','meniere\'s disease '),(261,'recommendations','LOW SALT DIET, vertigo exercises, ptabs '),(262,'chief_complaint','FOLLOW-UP CHECK-UP'),(278,'nose_exam','HYPEREMIC NASAL MUCOSA'),(279,'nose_exam','HYPEREMIC NASAL MUCOSA '),(280,'diagnosis','rhinosinusitis '),(281,'history','NASAL BLEEDING X 1 WEEK'),(282,'past_medical_history','ON AND OFF NASAL BLEEDING'),(283,'chief_complaint','EPISTAXIS'),(284,'nose_exam','BILATERAL NASAL POLYP GRADE 3'),(285,'nose_exam','BILATERAL NASAL DISCHARGE'),(286,'recommendations','X- RAY OF THE PNS'),(287,'diagnosis','RHINOSINUSITIS BILATERAL'),(289,'history','NASAL BLEEDING X 1 WEEK, ON AND OFF NASAL BLEEDING '),(290,'nose_exam','BILATERAL NASAL DISCHARGE '),(291,'chief_complaint','THROAT DISCOMFORT'),(292,'recommendations','FOR FNL IF UNRESOLVED AFTER 2 WEEKS'),(293,'past_medical_history','ON AND OFF THROAT DISCOMFORT FOR 3 YEARS'),(294,'history',', ON AND OFF THROAT DISCOMFORT FOR 3 YEARS '),(295,'history','CAME IN WITH RESULT OF CT SCAN OF THE PNS'),(296,'nose_exam','MEATY MASS ON BOTH NASOPHARYNX MORE ON THE LEFT'),(297,'nose_exam','MINIMAL NASAL DISCHARGE'),(298,'recommendations','FOR PUNCH BIOPSY OF NASAL MASS'),(299,'history','CAME IN WITH RESULT OF CT SCAN OF THE PNS '),(300,'nose_exam','MEATY MASS ON BOTH NASOPHARYNX MORE ON THE LEFT, MINIMAL NASAL DISCHARGE '),(301,'recommendations','FOR PUNCH BIOPSY OF NASAL MASS '),(302,'diagnosis','IMPACTED CERUMEN'),(303,'recommendations','PROPER EAR CLEANING'),(304,'procedures','EAR CLEANING'),(305,'past_medical_history','GIVEN EAR DROPS X 1 WEEK'),(306,'history',', GIVEN EAR DROPS X 1 WEEK '),(307,'chief_complaint','EAR PAIN'),(308,'history','1 MONTH CONSULTED MY CLINIC AND GIVEN ANTIBIOTICS FOR SINUSITIS AND NASAL POLYP-CLARITHROMYCIN, PEPRAZOM, ACETYLCYSTEINE, NACL NASAL SRAY, MOMETASONE NASAL SPRAY'),(309,'history','EAR PAIN X 1 DAY'),(310,'past_medical_history','ON AND OFF ITCHINESS EARS'),(311,'diagnosis','MILD OTOMYCOSIS, AU'),(351,'history','EAR PAIN X 1 DAY, ON AND OFF ITCHINESS EARS '),(352,'diagnosis','MILD OTOMYCOSIS, AU, meniere\'s disease '),(353,'recommendations','LOW SALT DIET, ptabs '),(354,'nose_exam','CLEAR NASOPHARYNX'),(355,'diagnosis','EUSTACHIAN TUBE DYSFUNCTION'),(356,'nose_exam','CLEAR NASOPHARYNX '),(357,'history','PATIENT IS DIAGNOSED WITH NASAL POLYP'),(358,'diagnosis','S/P FESS BILATERAL (8-14-25) MIHMCI'),(359,'nose_exam','YELLOWISH NASAL DISCHARGE BOTH NOSE'),(360,'nose_exam','GRADE 2 NASAL POLYP'),(361,'recommendations','FOR NASAL DOUCHE'),(362,'recommendations','NACLA NASAL SPRAY'),(363,'recommendations','NACL NASAL SPRAY FLO SINUS CARE'),(364,'recommendations','SAL SPRAY'),(365,'history','PATIENT IS DIAGNOSED WITH NASAL POLYP '),(366,'nose_exam','YELLOWISH NASAL DISCHARGE BOTH NOSE, GRADE 2 NASAL POLYP '),(367,'diagnosis','NASAL POLYP LEFT WITH MAXILLARY SINUSITIS, S/P FESS BILATERAL (8-14-25) MIHMCI '),(368,'recommendations','NACL NASAL SPRAY FLO SINUS CARE '),(369,'chief_complaint','FOR EAR CLEANING'),(370,'diagnosis','IMPACTED CERUMEN AU'),(371,'chief_complaint','FOR EAR CLEANING '),(372,'history','EAR PAIN X 3 DAYS'),(373,'chief_complaint','EAR CHECK UP'),(374,'ear_exam','BILATERAL FUNGAL INFECTION'),(375,'diagnosis','OTOMYCOSIS, AU'),(376,'diagnosis','OTITIS MEDIA WITH OTOMYCOSIS, AS'),(377,'history','EAR PAIN X 3 DAYS '),(378,'ear_exam','BILATERAL FUNGAL INFECTION '),(379,'diagnosis','OTOMYCOSIS, AU, OTITIS MEDIA WITH OTOMYCOSIS, AS '),(380,'chief_complaint','HEARING LOSS'),(381,'history','1 month noted difficulty of hearing'),(382,'ear_exam','INTACT tm, ad'),(383,'ear_exam','YELLOWISH DISCHARGE AS, WITH FUNGAL ELEMENTS'),(384,'recommendations','KEEP LEFT EAR DRY'),(385,'history','1 month noted difficulty of hearing '),(386,'ear_exam','INTACT tm, ad, YELLOWISH DISCHARGE AS, WITH FUNGAL ELEMENTS '),(387,'diagnosis','OTITIS MEDIA WITH OTOMYCOSIS, AS '),(388,'diagnosis','RHINOSINUSITIS WITH GRADE 2 POLYP BILATERAL'),(389,'recommendations','ADVISED FESS'),(390,'diagnosis','S/P FESS (2016)'),(391,'history','CONSULTED MY CLINIC FOR CLEARANCE FROM SUPERCARE'),(392,'past_medical_history','S/P FESS - 2016'),(393,'recommendations','FIT FOR WORK'),(394,'history','CONSULTED MY CLINIC FOR CLEARANCE FROM SUPERCARE, S/P FESS - 2016 '),(395,'nose_exam','GRADE 2 NASAL POLYP '),(396,'diagnosis','RHINOSINUSITIS WITH GRADE 2 POLYP BILATERAL, S/P FESS (2016) '),(397,'recommendations','ADVISED FESS, FIT FOR WORK '),(398,'history','EAR FULLNESS X 3 DAYS'),(399,'recommendations','REVERSE VALSALVA'),(400,'recommendations','MOMETASONE NASAL SPRAY'),(401,'recommendations','PTABS IF UNRESOLVED'),(402,'history','EAR FULLNESS X 3 DAYS '),(403,'recommendations','REVERSE VALSALVA, MOMETASONE NASAL SPRAY, PTABS IF UNRESOLVED '),(404,'history','4 DAYS EAR PAIN'),(405,'history','4 DAYS EAR PAIN '),(406,'diagnosis','MILD OTOMYCOSIS, AU '),(407,'chief_complaint','parotid mass'),(408,'ear_exam','perforaTED TM PERFORATION AS, 40%'),(409,'ear_exam','perforated tm, ad'),(410,'ear_exam','ear discharge, as'),(411,'diagnosis','pleomorphic adenoma, right parotid'),(412,'diagnosis','OTITIS MEDIA, AU'),(413,'recommendations','FOR SUPERFICIAL PAROTIDECTOMY, RIGHT'),(414,'past_medical_history','20 YEARS PTC, NOTED RIGHT PAROTID MAS'),(415,'past_medical_history','2015, EXCISION DONE'),(416,'past_medical_history','10 MONTHS PTC, NOTED SUDDEN INCREASE IN SIZE'),(417,'chief_complaint','parotid mass '),(418,'history',', 20 YEARS PTC, NOTED RIGHT PAROTID MAS, 2015, EXCISION DONE, 10 MONTHS PTC, NOTED SUDDEN INCREASE IN SIZE '),(419,'ear_exam','perforated tm, ad, ear discharge, as '),(420,'diagnosis','pleomorphic adenoma, right parotid, OTITIS MEDIA, AU '),(421,'recommendations','FOR SUPERFICIAL PAROTIDECTOMY, RIGHT '),(422,'diagnosis','PRE-AURICULAR SINUS'),(423,'diagnosis','1ST BRANCHIAL CLEFT RIGHT'),(424,'procedures','REMOVAL OF SUTURES'),(425,'history','NOTED INFECTED PRE- AURICULAR SINUS, RIGHT'),(426,'past_medical_history','GIVEN ANTIBIOTICS X 2 WEEKS'),(427,'recommendations','CONTINUE MUPIROCIN OINTMENT OD'),(428,'history','NOTED INFECTED PRE- AURICULAR SINUS, RIGHT, GIVEN ANTIBIOTICS X 2 WEEKS '),(429,'diagnosis','PRE-AURICULAR SINUS, 1ST BRANCHIAL CLEFT RIGHT '),(430,'recommendations','CONTINUE MUPIROCIN OINTMENT OD '),(431,'history','EAR DISCOMFORT'),(432,'history','EAR DISCOMFORT '),(433,'diagnosis','MILD OE, AU'),(434,'chief_complaint','THROAT PAIN X 3 DAYS'),(435,'history','1 WEEK PTC, NOTED DRY COUGH'),(436,'history','GIVEN CO-AMOXICLAV'),(437,'diagnosis','UPPER RESPIRATORY TRACT INFECTION'),(438,'diagnosis','ACUTE TONSILLOPHARYNGITIS'),(439,'recommendations','CHEST X-RAY PAL'),(440,'recommendations','X-RAY OF THE NECK APL'),(441,'throat_exam','GRADE 3 TONSILS BILATERAL WITH TONSILLOLITH'),(442,'chief_complaint','THROAT PAIN X 3 DAYS '),(443,'history','1 WEEK PTC, NOTED DRY COUGH, GIVEN CO-AMOXICLAV '),(444,'throat_exam','GRADE 3 TONSILS BILATERAL WITH TONSILLOLITH '),(445,'diagnosis','UPPER RESPIRATORY TRACT INFECTION, ACUTE TONSILLOPHARYNGITIS '),(446,'recommendations','CHEST X-RAY PAL, X-RAY OF THE NECK APL '),(447,'diagnosis','ALLERGIC RHINITIS'),(448,'chief_complaint','headache'),(449,'history','ON AND OFF HEADACHE WITH NASAL CONGESTION'),(450,'chief_complaint','headache '),(451,'history','ON AND OFF HEADACHE WITH NASAL CONGESTION '),(452,'history','4 days ptc, cough noted associated with difficulty of breathing'),(453,'recommendations','chest x-ray pa view'),(454,'history','4 days ptc, cough noted associated with difficulty of breathing '),(455,'diagnosis','UPPER RESPIRATORY TRACT INFECTION '),(456,'recommendations','chest x-ray pa view '),(457,'chief_complaint','for clearance'),(458,'diagnosis','OTITIS MEDIA, AU '),(459,'chief_complaint','EAR FULLNESS AS'),(460,'past_medical_history','IRON CHELATION DONE BY HEMATOLOGIST'),(461,'nose_exam','NASAL DISCHARGE'),(462,'history',', ON AND OFF NASAL BLEEDING '),(463,'diagnosis','maxillary sinusitis '),(464,'chief_complaint','foreign body (throat) fishbone'),(465,'chief_complaint','foreign body (throat) fishbone '),(466,'chief_complaint','anterior neck mass'),(467,'history','4 months noted anterior neck mass'),(468,'history','utz done revealed thyroid mass'),(469,'diagnosis','thyroid mass bilateral'),(470,'recommendations','for utz-guided fine needle aspiration biopsy'),(471,'chief_complaint','anterior neck mass '),(472,'history','4 months noted anterior neck mass '),(473,'diagnosis','thyroid mass bilateral '),(474,'recommendations','for utz-guided fine needle aspiration biopsy '),(475,'nose_exam','nasal mass left with extention to nasopharynx'),(476,'recommendations','follow up with biopsy result'),(477,'diagnosis','intra nasal mass with maxillary sinusitis'),(478,'nose_exam','nasal mass left with extention to nasopharynx '),(479,'recommendations','follow up with biopsy result '),(627,'chief_complaint','THIS IS MY CHIEF COMPLAINTS '),(628,'history','MY RECENT ILLNESS '),(629,'nose_exam','THICK NASAL DISCHARGE, TURBINATES NOT ENLARGED '),(630,'diagnosis','S/P FESS BILATERAL (8-14-25) MIHMCI, S/P FESS (2016) '),(631,'recommendations','SAL SPRAY, SERRAPEPTASE 1 TAB TID '),(635,'ear_exam','DRY CERUMEN, AU'),(636,'nose_exam','THICK NASAL DISCHARGE'),(637,'recommendations','SERRAPEPTASE 1 TAB TID'),(644,'chief_complaint','ASDASD '),(645,'history','ADSADS '),(649,'history','1 MONTH PTC- NOTED ON AND OFF HEARING LOSS '),(650,'history','ADMITTED AT METRO ILOILO FOR TONSILLITIS '),(651,'history','NIGHT PTC- NOTED EPISTAXIS '),(652,'nose_exam','POST-NASAL DRIP, ABRASION NASAL SEPTUM RIGHT '),(653,'throat_exam','GRADE 2 TONSILS- BILATERAL '),(654,'diagnosis','ACUTE TONSILLOPHARYNGITIS, RHINOSINUSITIS WITH POST NASAL DRIP, INACTIVE EPISTAXIS '),(655,'nose_exam','POST-NASAL DRIP'),(656,'nose_exam','ABRASION NASAL SEPTUM RIGHT'),(657,'throat_exam','GRADE 2 TONSILS- BILATERAL'),(658,'diagnosis','RHINOSINUSITIS WITH POST NASAL DRIP'),(659,'diagnosis','INACTIVE EPISTAXIS'),(660,'chief_complaint','FOLLOW-UP CHECKUP '),(661,'history','EPISTAXIS NOTED NIGHT PTC '),(662,'nose_exam','ABRASION RIGHT SEPTUM '),(663,'diagnosis','RHINOSINUSITIS WITH POST NASAL DRIP, ACUTE TONSILLOPHARYNGITIS, RHINOSINUSITIS WITH POST NASAL DRIP, INACTIVE EPISTAXIS, EPISTAXIS INACTIVE '),(664,'nose_exam','ABRASION RIGHT SEPTUM'),(665,'diagnosis','ACUTE TONSILLOPHARYNGITIS, RHINOSINUSITIS WITH POST NASAL DRIP, INACTIVE EPISTAXIS'),(666,'diagnosis','EPISTAXIS INACTIVE'),(667,'diagnosis','IMPACTED CERUMEN AU, MILD OTOMYCOSIS AS '),(668,'past_medical_history','NOTED DRY SKIN AND FLAKES, LEFT EAR'),(669,'diagnosis','MILD OTOMYCOSIS AS'),(670,'chief_complaint','NASAL DEFORMITY '),(671,'history','3 DAYS PTC- FALL '),(672,'nose_exam','NASAL DEFORMITY WITH SUTURED LACERATED WOUND '),(673,'diagnosis','NASAL BONE FRACTURE SECONDARY TO TRAUMA '),(674,'recommendations','FOR CLOSED REDUCTION OF NBF, FOR CP CLEARANCE PRIOR TO PROCEDURE '),(675,'past_medical_history','3 DAYS PTC FALL'),(676,'past_medical_history','CONSULT AT MIHMCI'),(677,'nose_exam','NASAL DEFORMITY WITH SUTURED LACERATED WOUND'),(678,'diagnosis','NASAL BONE FRACTURE SECONDARY TO TRAUMA'),(679,'recommendations','FOR CLOSED REDUCTION OF NBF'),(680,'recommendations','FOR CP CLEARANCE PRIOR TO PROCEDURE'),(681,'history','1 DAY PTC, NOED HEARING LOSS ASSOCIATED WITH HEADACHE '),(682,'history','1 WEEK PTC- NOTED EAR PAIN '),(683,'diagnosis','NEURALGIA '),(684,'past_medical_history','NO PREVIOUS EAR INFECTIONS'),(685,'diagnosis','NEURALGIA'),(686,'diagnosis','RHINOSINUSITIS, POST NASAL DRIP '),(687,'diagnosis','POST NASAL DRIP'),(688,'chief_complaint','REFERRED BY SUPERCARE FOR THYROID NODULES '),(689,'history','NOTED THYROID NODULES- 2009 '),(690,'history','FOR YEARLY MONITORING '),(691,'diagnosis','NON TOXIC NODULAR GOITER '),(692,'past_medical_history','UNREMARKABLE'),(693,'diagnosis','NON TOXIC NODULAR GOITER'),(694,'ear_exam','FUNGAL ELEMENTS, AU, SWOLLEN EXTERNAL CANAL AD '),(695,'diagnosis','OTITIS EXTERNA, AS, OTOMYCOSIS, AU '),(696,'recommendations','KEEP BOTH EARS DRY '),(697,'ear_exam','FUNGAL ELEMENTS, AU'),(698,'ear_exam','SWOLLEN EXTERNAL CANAL AD'),(699,'diagnosis','OTITIS EXTERNA, AS'),(700,'recommendations','KEEP BOTH EARS DRY'),(701,'history','1 WEEK PTC- REFERRED FOR HEARING LOSS AS '),(702,'ear_exam','FUNGAL ELEMENTS, AS '),(703,'ear_exam','FUNGAL ELEMENTS, AS'),(704,'chief_complaint','EAR CHARGES '),(705,'history','5 DAYS PTC, NOTED EAR DISCHARGE BILATERAL '),(706,'ear_exam','YELLOWISH FOUL-SMELLING DISCHARGE, AS, TYMPANIC MEMBRANE PERFORATION 40% AD '),(707,'past_medical_history','ON AND OFF EAR DISCHARGE SINCE 2017'),(708,'ear_exam','YELLOWISH FOUL-SMELLING DISCHARGE, AS'),(709,'ear_exam','TYMPANIC MEMBRANE PERFORATION 40% AD'),(710,'chief_complaint','SWELLING LEFT EAR '),(711,'history','1 WEEK PTC NOTED RIGHT EAR PAIN '),(712,'history','2 DAYS PTC LEFT EAR PAIN NOTED '),(713,'ear_exam','SWOLLEN EAR CANAL AS, MODERATE DISCHARGE AD, WITH INTACT TYMPANIC MEMBRANES '),(714,'diagnosis','OTITIS EXTERNA, AU '),(715,'recommendations','KEEP BOTH EARS DRY, FOLLOW UP AFTER 3 DAYS FOR REMOVAL OF EAR WICK '),(716,'ear_exam','SWOLLEN EAR CANAL AS'),(717,'ear_exam','MODERATE DISCHARGE AD, WITH INTACT TYMPANIC MEMBRANES'),(718,'diagnosis','OTITIS EXTERNA, AU'),(719,'recommendations','KEEP BOTH EARS DRY, FOLLOW UP AFTER 3 DAYS FOR REMOVAL OF EAR WICK'),(720,'chief_complaint','EAR FULLNES '),(721,'history','2 DAYS EAR ITCHINESS WITH DIFFICULTY IN HEARING '),(722,'ear_exam','BILATERAL YELLOWISH DISCHARGE, FUNGAL ELEMENTS, AU '),(723,'diagnosis','OTITIS EXTERNA WITH OTOMYCOSIS, AU '),(724,'ear_exam','BILATERAL YELLOWISH DISCHARGE'),(725,'diagnosis','OTITIS EXTERNA WITH OTOMYCOSIS, AU'),(726,'chief_complaint','EAR PAIN AU '),(727,'history','3 DAYS PTC, NOTED EAR PAIN '),(728,'ear_exam','BILATERAL SWOLLEN EAR CANAL '),(729,'notes','for removal of ear wick '),(731,'ear_exam','BILATERAL SWOLLEN EAR CANAL'),(732,'chief_complaint','CHIEF COMPLAINTS '),(733,'history','RECENT ILLNESS '),(734,'ear_exam','EAR DISCHARGE, AS, SWELLING RIGHT EAR CANA '),(735,'recommendations','SAL SPRAY '),(736,'past_medical_history','PAST MEDICAL HISTORY'),(740,'nose_exam','SLIGHTLY PALE'),(741,'diagnosis','CANALOLITHIASIS, MENIERE\'S DISEASE'),(748,'chief_complaint','CHIEF '),(749,'history','RECENT '),(750,'ear_exam','EAR DISCHARGE, AS, DRY CERUMEN, AU '),(751,'throat_exam','GRADE 2 TONSILS- BILATERAL, GRADE 3 TONSILS BILATERAL WITH TONSILLOLITH '),(752,'recommendations','SAL SPRAY, SAL SPRAY, SERRAPEPTASE 1 TAB TID '),(757,'diagnosis','S/P FESS BILATERAL (8-14-25) MIHMCI, S/P FESS (2016)'),(758,'recommendations','SAL SPRAY, SERRAPEPTASE 1 TAB TID'),(759,'history','3 WEEKS PTC NOTED THROAT DISCOMFORT '),(760,'throat_exam','NON HYPEREMIC TONSILS '),(761,'recommendations','FOR FLEXIBLE LARYNGOSCOPY IF WITH PHILHEALTH, THYROID ULTRASOUND '),(762,'notes','follow with result of laboratory '),(764,'past_medical_history','NOTED THYROID NODULES 10 YEARS PTC'),(765,'allergies','CLINDAMYCIN'),(766,'throat_exam','NON HYPEREMIC TONSILS'),(767,'recommendations','FOR FLEXIBLE LARYNGOSCOPY IF WITH PHILHEALTH'),(768,'recommendations','THYROID ULTRASOUND'),(769,'chief_complaint','HYPERTROPHIC TONSILS '),(770,'history','NOTED ENLARGED TONSILS SINCE CHILDHOOD '),(771,'history','NO RECURRENT INFECTION '),(772,'history','NO OSA SYMPTOMS AS CLAIMED '),(773,'throat_exam','GRADE 3 TONSILS- BILATERAL, NO TONSILLOLITH, NO HYPEREMIA '),(774,'diagnosis','TONSILLAR HYPERTROPHY- BILATERAL '),(775,'recommendations','OBSERVE FOR OSA SYMPTOMS, OBSERVE FOR RECURRENT INFECTION, FIT TO WORK '),(776,'throat_exam','GRADE 3 TONSILS- BILATERAL'),(777,'throat_exam','NO TONSILLOLITH'),(778,'throat_exam','NO HYPEREMIA'),(779,'diagnosis','TONSILLAR HYPERTROPHY- BILATERAL'),(780,'recommendations','OBSERVE FOR OSA SYMPTOMS'),(781,'recommendations','OBSERVE FOR RECURRENT INFECTION'),(782,'recommendations','FIT TO WORK'),(783,'history','1 WEEK PTC, NOTED HEARING LOSS '),(784,'history','3 DAYS NOTED EAR DISCOMFORT '),(785,'ear_exam','INFLAMED EAR CANAL AD, FUNGAL ELEMENTS, AU '),(786,'diagnosis','OTITIS EXTERNA, AU, OTOMYCOSIS, AU '),(787,'recommendations','PROPER EAR CLEANING, KEEP BOTH EARS DRY '),(788,'allergies','CLOTRIMOXAZOLE'),(789,'ear_exam','INFLAMED EAR CANAL AD'),(790,'history','1 MONTH TREATED FOR FUNGAL INFECTION '),(791,'history','GIVEN ANTIBIOTICS 2 COURSES - UNRECALLED '),(792,'ear_exam','YELLOWISH DISCHARGE AS, TYMPANIC MEMBRANE PERFORATION 30% '),(793,'diagnosis','RECURRENT OTITIS MEDIA AS '),(794,'past_medical_history','3 YEARS NOTED FUNGAL INFECTION'),(795,'ear_exam','YELLOWISH DISCHARGE AS'),(796,'ear_exam','TYMPANIC MEMBRANE PERFORATION 30%'),(797,'diagnosis','RECURRENT OTITIS MEDIA AS'),(798,'chief_complaint','OTORRHAGIA '),(799,'history','2 DAYS NOTED EAR BLEEDING '),(800,'ear_exam','INTACT TM - AU, ABRASION LEFT EAR CANAL '),(801,'diagnosis','ABRASION LEFT EAR CNAL '),(802,'ear_exam','ABRASION LEFT EAR CANAL'),(803,'diagnosis','ABRASION LEFT EAR CNAL'),(804,'chief_complaint','EAR PAIN AND TINNITUS '),(805,'history','3 DAYS NOTED EAR PAIN ASSOCIATED WITH VERTIGO '),(806,'history','5 DAYS PTC- CONSULT DONE AND GIVEN ANTI-VERTIGO BTAHISTINE 16MG X 5 DAYS '),(807,'ear_exam','INTACT TM - AU, DRY SCALY EAR WAX AU '),(808,'ear_exam','DRY SCALY EAR WAX AU'),(809,'recommendations','LOW SALT DIET, PTABS'),(810,'history','1 YEAR PTC NOTED ON AND OFF NASAL CONGESTION '),(811,'nose_exam','HYPERTROPHIC TURBINATES, NO POLYP '),(812,'nose_exam','HYPERTROPHIC TURBINATES'),(813,'nose_exam','NO POLYP'),(814,'recommendations','ALLERGEN AVOIDANCE'),(815,'chief_complaint','FOLLOW-UP CHECK UP- WITH RESULT OF PTABS '),(816,'history','CONSULTED 1 WEEK PTC FOR HEADACHE AND VERTIGO '),(817,'diagnosis','MENIERE\'S DISEASE, NOISE-INDUCED HEARING LOSS - LEFT EAR '),(818,'recommendations','LOW SALT DIET, CONTINUE VERTIGO EXERCISES AND LOW SALT DIET, AVOID LOUD NOISES '),(819,'past_medical_history','HYPERTENSION - ON LOSARTAN 50MG'),(820,'past_medical_history','DIABETIC - METFORMIN 500 MG, GLICLASIDE 80 MG'),(821,'past_medical_history','HYERCHOLESTEROLEMIA - ATORVASTATIN 10 MG'),(822,'past_medical_history','ON PHAREX B COMPLEX'),(823,'allergies','CO-TRIMOXAZOLE'),(824,'personal_social_history','WORKS IN COAL FIRE POWER PLANT'),(825,'personal_social_history','EXPOSD TO LOUD NOISE'),(826,'diagnosis','NOISE-INDUCED HEARING LOSS - LEFT EAR'),(827,'recommendations','CONTINUE VERTIGO EXERCISES AND LOW SALT DIET'),(828,'recommendations','AVOID LOUD NOISES'),(829,'history','SADAS '),(830,'chief_complaint','LOREM IPSUM DOLOR SIT AMET, CONSECTETUR ADIPISCING ELIT. '),(831,'chief_complaint','SED DO EIUSMOD TEMPOR INCIDIDUNT UT LABORE ET DOLORE MAGNA ALIQUA. '),(832,'chief_complaint','UT ENIM AD MINIM VENIAM, QUIS NOSTRUD EXERCITATION ULLAMCO LABORIS '),(833,'chief_complaint','NISI UT ALIQUIP EX EA COMMODO CONSEQUAT. DUIS AUTE IRURE DOLOR IN '),(834,'chief_complaint','REPREHENDERIT IN VOLUPTATE VELIT ESSE CILLUM DOLORE EU FUGIAT NULLA PARIATUR. '),(835,'chief_complaint','EXCEPTEUR SINT OCCAECAT CUPIDATAT NON PROIDENT, SUNT IN CULPA QUI OFFICIA '),(836,'chief_complaint','DESERUNT MOLLIT ANIM ID EST LABORUM. '),(844,'ear_exam','SWELLING RIGHT EAR CANA, SWOLLEN EAR CANAL AS, MODERATE DISCHARGE AD, WITH INTACT TYMPANIC MEMBRANES, SWOLLEN EXTERNAL CANAL AD, SWOLLEN EAR CANAL AS, MODERATE DISCHARGE AD, WITH INTACT TYMPANIC MEMBRANES '),(845,'past_medical_history','W'),(849,'ear_exam','SWOLLEN EAR CANAL AS, MODERATE DISCHARGE AD, WITH INTACT TYMPANIC MEMBRANES'),(853,'chief_complaint','WHY DO WE USE IT? '),(854,'history','WHAT IS LOREM IPSUM? '),(855,'diagnosis','S/P FESS BILATERAL (8-14-25) MIHMCI, S/P FESS (2016), S/P REVISION MASTOIDECTOMY (2019), S/P FESS BILATERAL (8-14-25) MIHMCI, S/P FESS (2016) '),(856,'recommendations','SAL SPRAY, SAL SPRAY, SERRAPEPTASE 1 TAB TID, SAL SPRAY, SERRAPEPTASE 1 TAB TID '),(862,'ear_exam','SWELLING RIGHT EAR CANA, SWOLLEN EAR CANAL AS, MODERATE DISCHARGE AD, WITH INTACT TYMPANIC MEMBRANES, SWOLLEN EXTERNAL CANAL AD, SWOLLEN EAR CANAL AS, MODERATE DISCHARGE AD, WITH INTACT TYMPANIC MEMBRANES'),(863,'diagnosis','S/P REVISION MASTOIDECTOMY (2019)'),(864,'recommendations','SAL SPRAY, SAL SPRAY, SERRAPEPTASE 1 TAB TID'),(865,'chief_complaint','LACUS CONSECTETUR. NAM EU MOLESTIE EX. SUSPENDISSE INTERDUM, QUAM NON PORTA CONDIMENTUM, DIAM ANTE VESTIBULUM RISUS, '),(866,'history','DONEC VELIT PURUS, PLACERAT VEL ULLAMCORPER ET, CONGUE ET PURUS. NULLA TINCIDUNT ULTRICIES ORCI. UT SUSCIPIT LACUS ET MAURIS ELEIFEND, A PULVINAR '),(867,'ear_exam','SWELLING RIGHT EAR CANA, SWOLLEN EAR CANAL AS, MODERATE DISCHARGE AD, WITH INTACT TYMPANIC MEMBRANES, SWOLLEN EXTERNAL CANAL AD, SWOLLEN EAR CANAL AS, MODERATE DISCHARGE AD, WITH INTACT TYMPANIC MEMBRANES, SWELLING RIGHT EAR CANAL, SWOLLEN EAR CANAL ASS '),(868,'diagnosis','S/P MASTOIDECTOMY AS (2005), S/P FESS BILATERAL (8-14-25) MIHMCI, S/P FESS (2016), S/P REVISION MASTOIDECTOMY (2019), S/P FESS BILATERAL (8-14-25) MIHMCI, S/P FESS (2016) '),(869,'recommendations','SAL SPRAY, SERRAPEPTASE 1 TAB TID, SAL SPRAY, SAL SPRAY, SERRAPEPTASE 1 TAB TID, SAL SPRAY, SERRAPEPTASE 1 TAB TID, SERRAPEPTASE 1 TAB TID '),(873,'ear_exam','SWOLLEN EAR CANAL ASS'),(874,'diagnosis','S/P MASTOIDECTOMY AS (2005)'),(875,'diagnosis','S/P FESS BILATERAL (8-14-25) MIHMCI, S/P FESS (2016), S/P REVISION MASTOIDECTOMY (2019), S/P FESS BILATERAL (8-14-25) MIHMCI, S/P FESS (2016)'),(876,'recommendations','SAL SPRAY, SAL SPRAY, SERRAPEPTASE 1 TAB TID, SAL SPRAY, SERRAPEPTASE 1 TAB TID'),(877,'history','10/23/2025 1:15 PM	CONSUMO, JOVIEWIN  C.	EAR PAIN AND TINNITUS	3 DAYS NOTED EAR PAIN ASSOCIATED WITH VERTIGO '),(878,'history','5 DAYS PTC- CONSULT DONE AND GIVEN ANTI-VERTIGO BTAHISTINE 16MG X 5 DAYS	INTACT TM - AU, DRY SCALY EAR WAX AU				MENIERE\'S DISEASE	LOW SALT DIET, PTABS			29	BASCOS '),(879,'diagnosis','S/P FESS (2016), S/P FESS BILATERAL (8-14-25) MIHMCI, S/P FESS (2016), S/P REVISION MASTOIDECTOMY (2019) '),(880,'recommendations','SAL SPRAY, SAL SPRAY, SERRAPEPTASE 1 TAB TID, SAL SPRAY, SAL SPRAY, SERRAPEPTASE 1 TAB TID, SAL SPRAY, SERRAPEPTASE 1 TAB TID, SERRAPEPTASE 1 TAB TID '),(881,'notes','removal of ear wick '),(883,'allergies','SEAFOOD'),(884,'allergies','ANTIBIOTIC'),(885,'family_history','DIABETIAS'),(886,'family_history','POKPOK'),(887,'personal_social_history','TRAVEL IN CEBU'),(888,'personal_social_history','SMOKING'),(889,'personal_social_history','DRINGKING'),(890,'recommendations','SAL SPRAY, SERRAPEPTASE 1 TAB TID, SAL SPRAY, SAL SPRAY, SERRAPEPTASE 1 TAB TID, SAL SPRAY, SERRAPEPTASE 1 TAB TID, SERRAPEPTASE 1 TAB TID'),(891,'history','RECENTS '),(892,'ear_exam','EARS '),(893,'nose_exam','NOSE '),(894,'throat_exam','THROAT '),(895,'diagnosis','DIAGNOSIS '),(896,'recommendations','RECOMENDATIONS '),(897,'notes','notes '),(899,'allergies','ALLERGIES'),(900,'family_history','FAMILT'),(901,'personal_social_history','SOCIAL'),(902,'ear_exam','EARS'),(903,'nose_exam','NOSE'),(904,'throat_exam','THROAT'),(905,'others_exam','OTHERS'),(906,'diagnosis','DIAGNOSIS'),(907,'recommendations','RECOMENDATIONS'),(908,'chief_complaint','LOREM IPSUM IS SIMPLY DUMMY TEXT OF THE PRINTING AND TYPESETTING INDUSTRY. LOREM IPSUM HAS BEEN THE INDUSTRY\'S STANDARD DUMMY TEXT EVER SINCE THE 1500S, WHEN AN UNKNOWN PRINTER TOOK A GALLEY OF TYPE AND SCRAMBLED IT TO MAKE A TYPE SPECIMEN BOOK. '),(909,'history','IT IS A LONG ESTABLISHED FACT THAT A READER WILL BE DISTRACTED BY THE READABLE CONTENT OF A PAGE WHEN LOOKING AT ITS LAYOUT. '),(910,'ear_exam','SWELLING RIGHT EAR CANA, SWOLLEN EAR CANAL AS, MODERATE DISCHARGE AD, WITH INTACT TYMPANIC MEMBRANES, SWOLLEN EXTERNAL CANAL AD, SWOLLEN EAR CANAL AS, MODERATE DISCHARGE AD, WITH INTACT TYMPANIC MEMBRANES, SWELLING RIGHT EAR CANAL, SWOLLEN EAR CANAL ASS'),(911,'diagnosis','S/P FESS (2016), S/P FESS BILATERAL (8-14-25) MIHMCI, S/P FESS (2016), S/P REVISION MASTOIDECTOMY (2019)');
/*!40000 ALTER TABLE `autocomplete_entries` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_autocomplete_entries_no_duplicates` BEFORE INSERT ON `autocomplete_entries` FOR EACH ROW BEGIN
    -- Check for existing entry with same column_name and value (case-insensitive)
    IF EXISTS (
        SELECT 1
        FROM autocomplete_entries
        WHERE LOWER(column_name) = LOWER(NEW.column_name)
          AND LOWER(value) = LOWER(NEW.value)
        LIMIT 1
    ) THEN
        -- Prevent insert silently by setting NEW.id to NULL (auto_increment field)
        SET NEW.id = NULL;
    END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `billing`
--

DROP TABLE IF EXISTS `billing`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `billing` (
  `billing_id` int NOT NULL AUTO_INCREMENT,
  `consultation_id` int DEFAULT NULL,
  `fee` decimal(10,2) NOT NULL,
  `discount_percent` int DEFAULT '0',
  `discount_amount` decimal(10,2) DEFAULT '0.00',
  `total_amount` decimal(10,2) NOT NULL,
  `note` text,
  `payment_status` varchar(40) DEFAULT 'UNPAID',
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
  `updated_at` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `amount_paid` decimal(10,2) DEFAULT '0.00',
  `balance` decimal(10,2) DEFAULT NULL,
  `patient_id` int DEFAULT NULL,
  PRIMARY KEY (`billing_id`),
  KEY `consultation_id` (`consultation_id`),
  CONSTRAINT `billing_ibfk_1` FOREIGN KEY (`consultation_id`) REFERENCES `consultation` (`consultation_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=124 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `billing`
--

LOCK TABLES `billing` WRITE;
/*!40000 ALTER TABLE `billing` DISABLE KEYS */;
INSERT INTO `billing` VALUES (89,247,800.00,0,0.00,800.00,'','FULLY PAID','2025-10-20 12:54:10','2025-10-20 12:59:29',800.00,0.00,2073),(90,249,1000.00,100,1000.00,0.00,'FIRST FOLLOW-UP','FULLY PAID','2025-10-20 13:09:58','2025-10-20 13:09:58',0.00,0.00,2075),(91,250,1000.00,0,0.00,1000.00,'ear cleaning','FULLY PAID','2025-10-20 13:15:33','2025-10-20 13:59:04',1000.00,0.00,2077),(92,251,800.00,100,800.00,0.00,'','FULLY PAID','2025-10-20 13:33:21','2025-10-20 13:33:21',0.00,0.00,2076),(93,252,1000.00,100,1000.00,0.00,'AVEGA','FULLY PAID','2025-10-20 13:42:13','2025-10-20 13:42:13',0.00,0.00,2074),(94,253,800.00,0,0.00,800.00,'','FULLY PAID','2025-10-20 13:47:47','2025-10-20 13:56:59',800.00,0.00,2078),(97,258,1000.00,20,200.00,800.00,'senior citizen','FULLY PAID','2025-10-21 14:17:57','2025-10-21 14:19:51',800.00,0.00,2056),(98,259,1000.00,0,0.00,1000.00,'earcleaning','FULLY PAID','2025-10-21 14:28:05','2025-10-21 14:38:41',1000.00,0.00,2081),(99,260,1000.00,100,1000.00,0.00,'intellicare','FULLY PAID','2025-10-21 14:43:08','2025-10-21 14:43:08',0.00,0.00,2082),(100,261,1000.00,0,0.00,1000.00,'ear cleaning','FULLY PAID','2025-10-21 14:54:27','2025-10-21 14:56:56',1000.00,0.00,2084),(101,262,1000.00,0,0.00,1000.00,'ear cleaning and ear wick','FULLY PAID','2025-10-21 15:04:04','2025-10-21 15:05:32',1000.00,0.00,2085),(106,267,800.00,0,0.00,800.00,'','FULLY PAID','2025-10-23 11:30:36','2025-10-23 11:40:33',800.00,0.00,2086),(107,268,800.00,0,0.00,800.00,'','FULLY PAID','2025-10-23 11:50:18','2025-10-23 11:52:48',800.00,0.00,2087),(108,269,1000.00,0,0.00,1000.00,'EAR CLEANING','FULLY PAID','2025-10-23 12:07:01','2025-10-23 12:09:48',1000.00,0.00,2088),(109,270,1000.00,0,0.00,1000.00,'EAR CLEANING','FULLY PAID','2025-10-23 12:19:18','2025-10-23 12:23:12',1000.00,0.00,2090),(110,271,800.00,0,0.00,800.00,'','FULLY PAID','2025-10-23 13:08:47','2025-10-23 13:10:49',800.00,0.00,2091),(111,272,1000.00,0,0.00,1000.00,'','FULLY PAID','2025-10-23 13:19:41','2025-10-23 13:26:33',1000.00,0.00,2092),(112,273,1000.00,0,0.00,1000.00,'','FULLY PAID','2025-10-23 13:34:28','2025-10-23 13:42:18',1000.00,0.00,2089),(113,274,800.00,100,800.00,0.00,'AVEGA','FULLY PAID','2025-10-23 13:58:22','2025-10-23 13:58:22',0.00,0.00,2074),(116,278,800.00,0,0.00,800.00,'','UNPAID','2025-10-26 20:03:10','2025-10-26 20:03:10',0.00,NULL,2072),(117,279,700.00,0,0.00,700.00,'','UNPAID','2025-10-26 22:35:50','2025-10-26 22:35:50',0.00,NULL,2072),(119,281,800.00,0,0.00,800.00,'','UNPAID','2025-10-26 23:19:30','2025-10-26 23:19:30',0.00,NULL,2008),(120,282,1000.00,0,0.00,1000.00,'','UNPAID','2025-10-26 23:20:58','2025-10-26 23:20:58',0.00,NULL,2008),(121,283,1000.00,0,0.00,1000.00,'','UNPAID','2025-10-26 23:22:45','2025-10-26 23:22:45',0.00,NULL,2035);
/*!40000 ALTER TABLE `billing` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `billing_before_insert` BEFORE INSERT ON `billing` FOR EACH ROW BEGIN
    -- If total_amount is 0 or discount_percent is 100, mark as fully paid
    IF NEW.total_amount = 0.00 OR NEW.discount_percent = 100 THEN
        SET NEW.payment_status = 'FULLY PAID';
        SET NEW.balance = 0.00;
        SET NEW.amount_paid = NEW.total_amount;
    END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `billing_payments`
--

DROP TABLE IF EXISTS `billing_payments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `billing_payments` (
  `payment_id` int NOT NULL AUTO_INCREMENT,
  `billing_id` int NOT NULL,
  `payment_date` datetime DEFAULT CURRENT_TIMESTAMP,
  `amount` decimal(10,2) NOT NULL,
  `note` varchar(255) DEFAULT NULL,
  `balance` decimal(10,2) DEFAULT NULL,
  `change_due` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`payment_id`),
  KEY `billing_id` (`billing_id`),
  CONSTRAINT `billing_payments_ibfk_1` FOREIGN KEY (`billing_id`) REFERENCES `billing` (`billing_id`)
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `billing_payments`
--

LOCK TABLES `billing_payments` WRITE;
/*!40000 ALTER TABLE `billing_payments` DISABLE KEYS */;
/*!40000 ALTER TABLE `billing_payments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `billing_with_patient`
--

DROP TABLE IF EXISTS `billing_with_patient`;
/*!50001 DROP VIEW IF EXISTS `billing_with_patient`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `billing_with_patient` AS SELECT 
 1 AS `billing_id`,
 1 AS `consultation_id`,
 1 AS `fee`,
 1 AS `discount_percent`,
 1 AS `discount_amount`,
 1 AS `total_amount`,
 1 AS `note`,
 1 AS `payment_status`,
 1 AS `created_at`,
 1 AS `updated_at`,
 1 AS `amount_paid`,
 1 AS `balance`,
 1 AS `patient_id`,
 1 AS `patient_name`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `consultation`
--

DROP TABLE IF EXISTS `consultation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `consultation` (
  `consultation_id` int NOT NULL AUTO_INCREMENT,
  `patient_id` int NOT NULL,
  `doctor_name` varchar(100) DEFAULT NULL,
  `consultation_date` datetime DEFAULT CURRENT_TIMESTAMP,
  `chief_complaint` text,
  `history` text,
  `ear_exam` text,
  `nose_exam` text,
  `throat_exam` text,
  `diagnosis` text,
  `recommendations` text,
  `notes` text,
  `follow_up_date` date DEFAULT NULL,
  `follow_up_notes` text,
  `age` int DEFAULT NULL,
  `neck_exam` text,
  `doctor_id` int DEFAULT NULL,
  `others_exam` text,
  PRIMARY KEY (`consultation_id`),
  KEY `patient_id` (`patient_id`),
  CONSTRAINT `consultation_ibfk_1` FOREIGN KEY (`patient_id`) REFERENCES `patients` (`patient_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=286 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `consultation`
--

LOCK TABLES `consultation` WRITE;
/*!40000 ALTER TABLE `consultation` DISABLE KEYS */;
INSERT INTO `consultation` VALUES (247,2073,'2','2025-10-20 12:50:19','HEARING LOSS','1 MONTH PTC- NOTED ON AND OFF HEARING LOSS','INTACT TM - AU','','','IMPACTED CERUMEN AU','PROPER EAR CLEANING','','2025-10-27','',76,NULL,NULL,NULL),(249,2075,'2','2025-10-20 13:05:07','FOLLOW-UP CHECKUP','ADMITTED AT METRO ILOILO FOR TONSILLITIS\nEPISTAXIS NOTED NIGHT PTC','','ABRASION RIGHT SEPTUM','','RHINOSINUSITIS WITH POST NASAL DRIP, ACUTE TONSILLOPHARYNGITIS, RHINOSINUSITIS WITH POST NASAL DRIP, INACTIVE EPISTAXIS, EPISTAXIS INACTIVE','','',NULL,'',25,NULL,NULL,NULL),(250,2077,'2','2025-10-20 13:15:17','EAR IRRITATION','','INTACT TM - AU','','','IMPACTED CERUMEN AU, MILD OTOMYCOSIS AS','PROPER EAR CLEANING','',NULL,'',73,NULL,NULL,NULL),(251,2076,'2','2025-10-20 13:33:11','NASAL DEFORMITY','3 DAYS PTC- FALL','','NASAL DEFORMITY WITH SUTURED LACERATED WOUND','','NASAL BONE FRACTURE SECONDARY TO TRAUMA','FOR CLOSED REDUCTION OF NBF, FOR CP CLEARANCE PRIOR TO PROCEDURE','',NULL,'',89,NULL,NULL,NULL),(252,2074,'2','2025-10-20 13:42:01','HEARING LOSS','1 DAY PTC, NOED HEARING LOSS ASSOCIATED WITH HEADACHE','INTACT TM - AU','','','MENIERE\'S DISEASE','LOW SALT DIET, VERTIGO EXERCISES, PTABS','',NULL,'',44,NULL,NULL,NULL),(253,2078,'2','2025-10-20 13:47:38','EAR PAIN','1 WEEK PTC- NOTED EAR PAIN','INTACT TM - AU','','','NEURALGIA','PROPER EAR CLEANING','',NULL,'',41,NULL,NULL,NULL),(258,2056,'2','2025-10-21 14:17:38','FOLLOW-UP','1 WEEK PTC- REFERRED FOR HEARING LOSS AS','FUNGAL ELEMENTS, AS','','','OTITIS MEDIA WITH OTOMYCOSIS, AS','KEEP LEFT EAR DRY','',NULL,'',65,NULL,NULL,NULL),(259,2081,'2','2025-10-21 14:26:08','EAR CHARGES','5 DAYS PTC, NOTED EAR DISCHARGE BILATERAL','YELLOWISH FOUL-SMELLING DISCHARGE, AS, TYMPANIC MEMBRANE PERFORATION 40% AD','','','OTITIS MEDIA, AU','KEEP BOTH EARS DRY','',NULL,'',53,NULL,NULL,NULL),(260,2082,'2','2025-10-21 14:38:47','SWELLING LEFT EAR','1 WEEK PTC NOTED RIGHT EAR PAIN\n2 DAYS PTC LEFT EAR PAIN NOTED','SWOLLEN EAR CANAL AS, MODERATE DISCHARGE AD, WITH INTACT TYMPANIC MEMBRANES','','','OTITIS EXTERNA, AU','KEEP BOTH EARS DRY, FOLLOW UP AFTER 3 DAYS FOR REMOVAL OF EAR WICK','',NULL,'',35,NULL,NULL,NULL),(261,2084,'2','2025-10-21 14:52:05','EAR FULLNES','2 DAYS EAR ITCHINESS WITH DIFFICULTY IN HEARING','BILATERAL YELLOWISH DISCHARGE, FUNGAL ELEMENTS, AU','','','OTITIS EXTERNA WITH OTOMYCOSIS, AU','KEEP BOTH EARS DRY','',NULL,'',23,NULL,NULL,NULL),(262,2085,'2','2025-10-21 15:01:08','EAR PAIN AU','3 DAYS PTC, NOTED EAR PAIN','BILATERAL SWOLLEN EAR CANAL','','','OTITIS EXTERNA, AU','KEEP BOTH EARS DRY','for removal of ear wick',NULL,'for removal of ear wick',24,NULL,NULL,NULL),(267,2086,'2','2025-10-23 11:27:28','THROAT DISCOMFORT','3 WEEKS PTC NOTED THROAT DISCOMFORT','','','NON HYPEREMIC TONSILS','LARYNGOPHARYNGEAL REFLUX','FOR FLEXIBLE LARYNGOSCOPY IF WITH PHILHEALTH, THYROID ULTRASOUND','follow with result of laboratory','2025-10-30','follow with result of laboratory',53,NULL,NULL,''),(268,2087,'2','2025-10-23 11:42:39','HYPERTROPHIC TONSILS','NOTED ENLARGED TONSILS SINCE CHILDHOOD \nNO RECURRENT INFECTION\nNO OSA SYMPTOMS AS CLAIMED','','','GRADE 3 TONSILS- BILATERAL, NO TONSILLOLITH, NO HYPEREMIA','TONSILLAR HYPERTROPHY- BILATERAL','OBSERVE FOR OSA SYMPTOMS, OBSERVE FOR RECURRENT INFECTION, FIT TO WORK','',NULL,'',28,NULL,NULL,''),(269,2088,'2','2025-10-23 12:03:31','HEARING LOSS','1 WEEK PTC, NOTED HEARING LOSS \n3 DAYS NOTED EAR DISCOMFORT','INFLAMED EAR CANAL AD, FUNGAL ELEMENTS, AU','','','OTITIS EXTERNA, AU, OTOMYCOSIS, AU','PROPER EAR CLEANING, KEEP BOTH EARS DRY','',NULL,'',48,NULL,NULL,''),(270,2090,'2','2025-10-23 12:17:21','EAR DISCHARGE','1 MONTH TREATED FOR FUNGAL INFECTION\nGIVEN ANTIBIOTICS 2 COURSES - UNRECALLED','YELLOWISH DISCHARGE AS, TYMPANIC MEMBRANE PERFORATION 30%','','','RECURRENT OTITIS MEDIA AS','KEEP LEFT EAR DRY','',NULL,'',58,NULL,NULL,''),(271,2091,'2','2025-10-23 13:03:44','OTORRHAGIA','2 DAYS NOTED EAR BLEEDING','INTACT TM - AU, ABRASION LEFT EAR CANAL','','','ABRASION LEFT EAR CNAL','KEEP LEFT EAR DRY','',NULL,'',6,NULL,NULL,''),(272,2092,'2','2025-10-23 13:15:34','EAR PAIN AND TINNITUS','3 DAYS NOTED EAR PAIN ASSOCIATED WITH VERTIGO\n5 DAYS PTC- CONSULT DONE AND GIVEN ANTI-VERTIGO BTAHISTINE 16MG X 5 DAYS','INTACT TM - AU, DRY SCALY EAR WAX AU','','','MENIERE\'S DISEASE','LOW SALT DIET, PTABS','',NULL,'',29,NULL,NULL,''),(273,2089,'2','2025-10-23 13:27:58','NASAL CONGESTION','1 YEAR PTC NOTED ON AND OFF NASAL CONGESTION','','HYPERTROPHIC TURBINATES, NO POLYP','','ALLERGIC RHINITIS','ALLERGEN AVOIDANCE','',NULL,'',28,NULL,NULL,''),(274,2074,'2','2025-10-23 13:58:07','FOLLOW-UP CHECK UP- WITH RESULT OF PTABS','CONSULTED 1 WEEK PTC FOR HEADACHE AND VERTIGO','INTACT TM - AU','','','MENIERE\'S DISEASE, NOISE-INDUCED HEARING LOSS - LEFT EAR','LOW SALT DIET, CONTINUE VERTIGO EXERCISES AND LOW SALT DIET, AVOID LOUD NOISES','','0205-10-26','',44,NULL,NULL,''),(278,2072,'2','2025-10-26 20:03:04','WHY DO WE USE IT?\nIT IS A LONG ESTABLISHED FACT THAT A READER WILL BE DISTRACTED BY THE READABLE CONTENT OF A PAGE WHEN LOOKING AT ITS LAYOUT. THE POINT OF USING LOREM IPSUM IS THAT IT HAS A MORE-OR-LESS NORMAL DISTRIBUTION OF LETTERS, AS OPPOSED TO USING \'CONTENT HERE, CONTENT HERE\', MAKING IT LOOK LIKE READABLE ENGLISH. MANY DESKTOP PUBLISHING PACKAGES AND WEB PAGE EDITORS NOW USE LOREM IPSUM AS THEIR DEFAULT MODEL TEXT, AND A SEARCH FOR \'LOREM IPSUM\' WILL UNCOVER MANY WEB SITES STILL IN THEIR INFANCY. VARIOUS VERSIONS HAVE EVOLVED OVER THE YEARS, SOMETIMES BY ACCIDENT, SOMETIMES ON PURPOSE (INJECTED HUMOUR AND THE LIKE).','WHAT IS LOREM IPSUM?\nLOREM IPSUM IS SIMPLY DUMMY TEXT OF THE PRINTING AND TYPESETTING INDUSTRY. LOREM IPSUM HAS BEEN THE INDUSTRY\'S STANDARD DUMMY TEXT EVER SINCE THE 1500S, WHEN AN UNKNOWN PRINTER TOOK A GALLEY OF TYPE AND SCRAMBLED IT TO MAKE A TYPE SPECIMEN BOOK. IT HAS SURVIVED NOT ONLY FIVE CENTURIES, BUT ALSO THE LEAP INTO ELECTRONIC TYPESETTING, REMAINING ESSENTIALLY UNCHANGED. IT WAS POPULARISED IN THE 1960S WITH THE RELEASE OF LETRASET SHEETS CONTAINING LOREM IPSUM PASSAGES, AND MORE RECENTLY WITH DESKTOP PUBLISHING SOFTWARE LIKE ALDUS PAGEMAKER INCLUDING VERSIONS OF LOREM IPSUM.','SWELLING RIGHT EAR CANA, SWOLLEN EAR CANAL AS, MODERATE DISCHARGE AD, WITH INTACT TYMPANIC MEMBRANES, SWOLLEN EXTERNAL CANAL AD, SWOLLEN EAR CANAL AS, MODERATE DISCHARGE AD, WITH INTACT TYMPANIC MEMBRANES, SWOLLEN EAR CANAL AS, MODERATE DISCHARGE AD, WITH INTACT TYMPANIC MEMBRANES','','','S/P FESS BILATERAL (8-14-25) MIHMCI, S/P FESS (2016), S/P REVISION MASTOIDECTOMY (2019), S/P FESS BILATERAL (8-14-25) MIHMCI, S/P FESS (2016)','SAL SPRAY, SAL SPRAY, SERRAPEPTASE 1 TAB TID, SAL SPRAY, SERRAPEPTASE 1 TAB TID','Why do we use it?\nIt is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using \'Content here, content here\', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for \'lorem ipsum\' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).','2025-10-26','Why do we use it?\nIt is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using \'Content here, content here\', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for \'lorem ipsum\' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).',23,NULL,NULL,''),(279,2072,'2','2025-10-26 22:35:40','LACUS CONSECTETUR. NAM EU MOLESTIE EX. SUSPENDISSE INTERDUM, QUAM NON PORTA CONDIMENTUM, DIAM ANTE VESTIBULUM RISUS,','DONEC VELIT PURUS, PLACERAT VEL ULLAMCORPER ET, CONGUE ET PURUS. NULLA TINCIDUNT ULTRICIES ORCI. UT SUSCIPIT LACUS ET MAURIS ELEIFEND, A PULVINAR','SWELLING RIGHT EAR CANA, SWOLLEN EAR CANAL AS, MODERATE DISCHARGE AD, WITH INTACT TYMPANIC MEMBRANES, SWOLLEN EXTERNAL CANAL AD, SWOLLEN EAR CANAL AS, MODERATE DISCHARGE AD, WITH INTACT TYMPANIC MEMBRANES, SWELLING RIGHT EAR CANAL, SWOLLEN EAR CANAL ASS','','','S/P MASTOIDECTOMY AS (2005), S/P FESS BILATERAL (8-14-25) MIHMCI, S/P FESS (2016), S/P REVISION MASTOIDECTOMY (2019), S/P FESS BILATERAL (8-14-25) MIHMCI, S/P FESS (2016)','SAL SPRAY, SERRAPEPTASE 1 TAB TID, SAL SPRAY, SAL SPRAY, SERRAPEPTASE 1 TAB TID, SAL SPRAY, SERRAPEPTASE 1 TAB TID, SERRAPEPTASE 1 TAB TID','',NULL,'',23,NULL,NULL,''),(281,2008,'2','2025-10-26 23:19:27','','','BILATERAL SWOLLEN EAR CANAL','','','','','',NULL,'',21,NULL,NULL,''),(282,2008,'2','2025-10-26 23:20:55','','','BILATERAL SWOLLEN EAR CANAL','','','','','',NULL,'',21,NULL,NULL,''),(283,2035,'2','2025-10-26 23:22:37','','','BILATERAL SWOLLEN EAR CANAL','','','','','',NULL,'',31,NULL,NULL,'');
/*!40000 ALTER TABLE `consultation` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_update_autocomplete` AFTER INSERT ON `consultation` FOR EACH ROW BEGIN
    DECLARE line TEXT;
    DECLARE pos INT;
    DECLARE next_pos INT;
    DECLARE col_value TEXT;

    -- ===================== HELPER BLOCK =====================
    -- Processes each column with bullet points and user errors

    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
    BEGIN
        -- just continue on errors (safety)
    END;

    -- ----------------- FUNCTION TO PROCESS A COLUMN -----------------
    -- We inline it for each column
    SET col_value = NEW.chief_complaint;
    IF col_value IS NOT NULL AND TRIM(col_value) <> '' THEN
        SET pos = 1;
        WHILE pos <= CHAR_LENGTH(col_value) DO
            SET next_pos = LOCATE('\n', col_value, pos);
            IF next_pos = 0 THEN
                SET line = SUBSTRING(col_value, pos);
                SET pos = CHAR_LENGTH(col_value) + 1;
            ELSE
                SET line = SUBSTRING(col_value, pos, next_pos - pos);
                SET pos = next_pos + 1;
            END IF;

            -- Clean line: remove leading bullet and spaces
            SET line = TRIM(line);
            IF LEFT(line,2) = 'â¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            -- Ignore invalid lines (empty or only bullet)
            IF line <> '' AND line <> 'â¢' THEN
                -- Only insert if it does not exist yet
                IF NOT EXISTS (
                    SELECT 1 FROM ent_clinic_db.autocomplete_entries
                    WHERE column_name = 'chief_complaint'
                      AND value = CONCAT(line,' ')
                ) THEN
                    INSERT INTO ent_clinic_db.autocomplete_entries(column_name, value)
                    VALUES ('chief_complaint', CONCAT(line,' '));
                END IF;
            END IF;
        END WHILE;
    END IF;

    -- ----------------- Repeat for other columns -----------------
    SET col_value = NEW.history;
    IF col_value IS NOT NULL AND TRIM(col_value) <> '' THEN
        SET pos = 1;
        WHILE pos <= CHAR_LENGTH(col_value) DO
            SET next_pos = LOCATE('\n', col_value, pos);
            IF next_pos = 0 THEN
                SET line = SUBSTRING(col_value, pos);
                SET pos = CHAR_LENGTH(col_value) + 1;
            ELSE
                SET line = SUBSTRING(col_value, pos, next_pos - pos);
                SET pos = next_pos + 1;
            END IF;

            SET line = TRIM(line);
            IF LEFT(line,2) = 'â¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> 'â¢' THEN
                IF NOT EXISTS (
                    SELECT 1 FROM ent_clinic_db.autocomplete_entries
                    WHERE column_name = 'history'
                      AND value = CONCAT(line,' ')
                ) THEN
                    INSERT INTO ent_clinic_db.autocomplete_entries(column_name, value)
                    VALUES ('history', CONCAT(line,' '));
                END IF;
            END IF;
        END WHILE;
    END IF;

    -- ----------------- Repeat for ear_exam -----------------
    SET col_value = NEW.ear_exam;
    IF col_value IS NOT NULL AND TRIM(col_value) <> '' THEN
        SET pos = 1;
        WHILE pos <= CHAR_LENGTH(col_value) DO
            SET next_pos = LOCATE('\n', col_value, pos);
            IF next_pos = 0 THEN
                SET line = SUBSTRING(col_value, pos);
                SET pos = CHAR_LENGTH(col_value) + 1;
            ELSE
                SET line = SUBSTRING(col_value, pos, next_pos - pos);
                SET pos = next_pos + 1;
            END IF;

            SET line = TRIM(line);
            IF LEFT(line,2) = 'â¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> 'â¢' THEN
                IF NOT EXISTS (
                    SELECT 1 FROM ent_clinic_db.autocomplete_entries
                    WHERE column_name = 'ear_exam'
                      AND value = CONCAT(line,' ')
                ) THEN
                    INSERT INTO ent_clinic_db.autocomplete_entries(column_name, value)
                    VALUES ('ear_exam', CONCAT(line,' '));
                END IF;
            END IF;
        END WHILE;
    END IF;

    -- ----------------- Repeat for nose_exam -----------------
    SET col_value = NEW.nose_exam;
    IF col_value IS NOT NULL AND TRIM(col_value) <> '' THEN
        SET pos = 1;
        WHILE pos <= CHAR_LENGTH(col_value) DO
            SET next_pos = LOCATE('\n', col_value, pos);
            IF next_pos = 0 THEN
                SET line = SUBSTRING(col_value, pos);
                SET pos = CHAR_LENGTH(col_value) + 1;
            ELSE
                SET line = SUBSTRING(col_value, pos, next_pos - pos);
                SET pos = next_pos + 1;
            END IF;

            SET line = TRIM(line);
            IF LEFT(line,2) = 'â¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> 'â¢' THEN
                IF NOT EXISTS (
                    SELECT 1 FROM ent_clinic_db.autocomplete_entries
                    WHERE column_name = 'nose_exam'
                      AND value = CONCAT(line,' ')
                ) THEN
                    INSERT INTO ent_clinic_db.autocomplete_entries(column_name, value)
                    VALUES ('nose_exam', CONCAT(line,' '));
                END IF;
            END IF;
        END WHILE;
    END IF;

    -- ----------------- Repeat for throat_exam -----------------
    SET col_value = NEW.throat_exam;
    IF col_value IS NOT NULL AND TRIM(col_value) <> '' THEN
        SET pos = 1;
        WHILE pos <= CHAR_LENGTH(col_value) DO
            SET next_pos = LOCATE('\n', col_value, pos);
            IF next_pos = 0 THEN
                SET line = SUBSTRING(col_value, pos);
                SET pos = CHAR_LENGTH(col_value) + 1;
            ELSE
                SET line = SUBSTRING(col_value, pos, next_pos - pos);
                SET pos = next_pos + 1;
            END IF;

            SET line = TRIM(line);
            IF LEFT(line,2) = 'â¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> 'â¢' THEN
                IF NOT EXISTS (
                    SELECT 1 FROM ent_clinic_db.autocomplete_entries
                    WHERE column_name = 'throat_exam'
                      AND value = CONCAT(line,' ')
                ) THEN
                    INSERT INTO ent_clinic_db.autocomplete_entries(column_name, value)
                    VALUES ('throat_exam', CONCAT(line,' '));
                END IF;
            END IF;
        END WHILE;
    END IF;

    -- ----------------- diagnosis -----------------
    SET col_value = NEW.diagnosis;
    IF col_value IS NOT NULL AND TRIM(col_value) <> '' THEN
        SET pos = 1;
        WHILE pos <= CHAR_LENGTH(col_value) DO
            SET next_pos = LOCATE('\n', col_value, pos);
            IF next_pos = 0 THEN
                SET line = SUBSTRING(col_value, pos);
                SET pos = CHAR_LENGTH(col_value) + 1;
            ELSE
                SET line = SUBSTRING(col_value, pos, next_pos - pos);
                SET pos = next_pos + 1;
            END IF;

            SET line = TRIM(line);
            IF LEFT(line,2) = 'â¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> 'â¢' THEN
                IF NOT EXISTS (
                    SELECT 1 FROM ent_clinic_db.autocomplete_entries
                    WHERE column_name = 'diagnosis'
                      AND value = CONCAT(line,' ')
                ) THEN
                    INSERT INTO ent_clinic_db.autocomplete_entries(column_name, value)
                    VALUES ('diagnosis', CONCAT(line,' '));
                END IF;
            END IF;
        END WHILE;
    END IF;

    -- ----------------- recommendations -----------------
    SET col_value = NEW.recommendations;
    IF col_value IS NOT NULL AND TRIM(col_value) <> '' THEN
        SET pos = 1;
        WHILE pos <= CHAR_LENGTH(col_value) DO
            SET next_pos = LOCATE('\n', col_value, pos);
            IF next_pos = 0 THEN
                SET line = SUBSTRING(col_value, pos);
                SET pos = CHAR_LENGTH(col_value) + 1;
            ELSE
                SET line = SUBSTRING(col_value, pos, next_pos - pos);
                SET pos = next_pos + 1;
            END IF;

            SET line = TRIM(line);
            IF LEFT(line,2) = 'â¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> 'â¢' THEN
                IF NOT EXISTS (
                    SELECT 1 FROM ent_clinic_db.autocomplete_entries
                    WHERE column_name = 'recommendations'
                      AND value = CONCAT(line,' ')
                ) THEN
                    INSERT INTO ent_clinic_db.autocomplete_entries(column_name, value)
                    VALUES ('recommendations', CONCAT(line,' '));
                END IF;
            END IF;
        END WHILE;
    END IF;

    -- ----------------- notes -----------------
    SET col_value = NEW.notes;
    IF col_value IS NOT NULL AND TRIM(col_value) <> '' THEN
        SET pos = 1;
        WHILE pos <= CHAR_LENGTH(col_value) DO
            SET next_pos = LOCATE('\n', col_value, pos);
            IF next_pos = 0 THEN
                SET line = SUBSTRING(col_value, pos);
                SET pos = CHAR_LENGTH(col_value) + 1;
            ELSE
                SET line = SUBSTRING(col_value, pos, next_pos - pos);
                SET pos = next_pos + 1;
            END IF;

            SET line = TRIM(line);
            IF LEFT(line,2) = 'â¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> 'â¢' THEN
                IF NOT EXISTS (
                    SELECT 1 FROM ent_clinic_db.autocomplete_entries
                    WHERE column_name = 'notes'
                      AND value = CONCAT(line,' ')
                ) THEN
                    INSERT INTO ent_clinic_db.autocomplete_entries(column_name, value)
                    VALUES ('notes', CONCAT(line,' '));
                END IF;
            END IF;
        END WHILE;
    END IF;

    -- ----------------- follow_up_notes -----------------
    SET col_value = NEW.follow_up_notes;
    IF col_value IS NOT NULL AND TRIM(col_value) <> '' THEN
        SET pos = 1;
        WHILE pos <= CHAR_LENGTH(col_value) DO
            SET next_pos = LOCATE('\n', col_value, pos);
            IF next_pos = 0 THEN
                SET line = SUBSTRING(col_value, pos);
                SET pos = CHAR_LENGTH(col_value) + 1;
            ELSE
                SET line = SUBSTRING(col_value, pos, next_pos - pos);
                SET pos = next_pos + 1;
            END IF;

            SET line = TRIM(line);
            IF LEFT(line,2) = 'â¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> 'â¢' THEN
                IF NOT EXISTS (
                    SELECT 1 FROM ent_clinic_db.autocomplete_entries
                    WHERE column_name = 'follow_up_notes'
                      AND value = CONCAT(line,' ')
                ) THEN
                    INSERT INTO ent_clinic_db.autocomplete_entries(column_name, value)
                    VALUES ('follow_up_notes', CONCAT(line,' '));
                END IF;
            END IF;
        END WHILE;
    END IF;

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `after_consultation_insert` AFTER INSERT ON `consultation` FOR EACH ROW BEGIN
    -- Insert the health_record snapshot into health_record_history
    INSERT INTO `health_record_history` (
        consultation_id,
        patient_id,
        past_medical_history,
        family_history,
        personal_social_history,
        bp,
        temperature,
        pr,
        rr,
        ht,
        wt,
        general_appearance,
        skin,
        head_and_face,
        eyes,
        neck,
        chest_lungs,
        heart,
        abdomen,
        extremities,
        neurologic,
        created_at,
        updated_at,
        allergies
    )
    SELECT
        NEW.consultation_id,
        hr.patient_id,
        hr.past_medical_history,
        hr.family_history,
        hr.personal_social_history,
        hr.bp,
        hr.temperature,
        hr.pr,
        hr.rr,
        hr.ht,
        hr.wt,
        hr.general_appearance,
        hr.skin,
        hr.head_and_face,
        hr.eyes,
        hr.neck,
        hr.chest_lungs,
        hr.heart,
        hr.abdomen,
        hr.extremities,
        hr.neurologic,
        hr.created_at,
        hr.updated_at,
        hr.allergies
    FROM `health_record` AS hr
    WHERE hr.patient_id = NEW.patient_id;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `dispense_prescription`
--

DROP TABLE IF EXISTS `dispense_prescription`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dispense_prescription` (
  `dispense_id` int NOT NULL AUTO_INCREMENT,
  `prescription_id` int NOT NULL,
  `patient_id` int NOT NULL,
  `item_id` int NOT NULL,
  `quantity` int NOT NULL,
  `invoice_item_id` int NOT NULL,
  `dispensed_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `note` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`dispense_id`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dispense_prescription`
--

LOCK TABLES `dispense_prescription` WRITE;
/*!40000 ALTER TABLE `dispense_prescription` DISABLE KEYS */;
INSERT INTO `dispense_prescription` VALUES (3,84,2072,18,2,101,'2025-10-18 13:41:14',NULL),(4,85,2072,10,6,102,'2025-10-18 13:41:14',NULL),(5,86,2072,16,1,103,'2025-10-18 13:41:14',NULL),(6,88,2072,22,1,105,'2025-10-18 13:51:27',NULL),(7,89,2072,12,14,106,'2025-10-18 13:51:27',NULL),(8,93,2073,19,1,107,'2025-10-20 13:01:12',NULL),(9,94,2075,23,21,108,'2025-10-20 13:17:40',NULL),(10,96,2080,13,7,114,'2025-10-21 14:26:39',NULL),(11,97,2080,10,6,115,'2025-10-21 14:26:39',NULL),(12,95,2080,22,1,116,'2025-10-21 14:26:39',NULL),(13,100,2081,22,1,117,'2025-10-21 14:36:45',NULL),(14,101,2081,13,14,118,'2025-10-21 14:36:45',NULL),(15,102,2081,26,10,119,'2025-10-21 14:36:45',NULL),(16,103,2082,22,1,120,'2025-10-21 14:46:19',NULL),(17,106,2084,15,1,121,'2025-10-21 14:58:47',NULL),(18,108,2085,13,10,122,'2025-10-21 15:12:10',NULL),(19,109,2085,15,1,123,'2025-10-21 15:12:10',NULL),(20,110,2085,10,1,124,'2025-10-21 15:12:11',NULL),(21,116,2086,12,14,125,'2025-10-23 11:41:33',NULL),(22,115,2086,14,1,126,'2025-10-23 11:41:33',NULL),(23,120,2088,27,14,127,'2025-10-23 12:10:04',NULL),(24,121,2088,15,1,128,'2025-10-23 12:10:04',NULL),(25,125,2091,19,1,129,'2025-10-23 13:11:12',NULL),(26,123,2090,15,1,130,'2025-10-23 13:12:06',NULL),(27,124,2090,26,10,131,'2025-10-23 13:12:06',NULL),(28,122,2090,13,14,132,'2025-10-23 13:12:06',NULL),(29,127,2092,16,30,133,'2025-10-23 13:27:01',NULL),(30,126,2092,19,1,134,'2025-10-23 13:27:02',NULL),(31,128,2089,23,21,135,'2025-10-23 13:41:26',NULL),(32,129,2089,26,10,136,'2025-10-23 13:41:26',NULL);
/*!40000 ALTER TABLE `dispense_prescription` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `general_pe`
--

DROP TABLE IF EXISTS `general_pe`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `general_pe` (
  `id` int NOT NULL AUTO_INCREMENT,
  `pe_name` varchar(255) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `general_pe`
--

LOCK TABLES `general_pe` WRITE;
/*!40000 ALTER TABLE `general_pe` DISABLE KEYS */;
/*!40000 ALTER TABLE `general_pe` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `health_record`
--

DROP TABLE IF EXISTS `health_record`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `health_record` (
  `health_record_id` int NOT NULL AUTO_INCREMENT,
  `patient_id` int DEFAULT NULL,
  `past_medical_history` text,
  `family_history` text,
  `personal_social_history` text,
  `bp` varchar(20) DEFAULT NULL,
  `temperature` varchar(20) DEFAULT NULL,
  `pr` varchar(20) DEFAULT NULL,
  `rr` varchar(20) DEFAULT NULL,
  `ht` varchar(20) DEFAULT NULL,
  `wt` varchar(20) DEFAULT NULL,
  `general_appearance` varchar(255) DEFAULT NULL,
  `skin` varchar(255) DEFAULT NULL,
  `head_and_face` varchar(255) DEFAULT NULL,
  `eyes` varchar(255) DEFAULT NULL,
  `neck` varchar(255) DEFAULT NULL,
  `chest_lungs` varchar(255) DEFAULT NULL,
  `heart` varchar(255) DEFAULT NULL,
  `abdomen` varchar(255) DEFAULT NULL,
  `extremities` varchar(255) DEFAULT NULL,
  `neurologic` varchar(255) DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `consultation_id` int DEFAULT NULL,
  `allergies` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`health_record_id`),
  KEY `patient_id` (`patient_id`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `health_record`
--

LOCK TABLES `health_record` WRITE;
/*!40000 ALTER TABLE `health_record` DISABLE KEYS */;
INSERT INTO `health_record` VALUES (1,2,'NONE','NONE','NONE','',NULL,NULL,NULL,NULL,NULL,'NORMAL','NORMAL','NORMAL','NORMAL','NORMAL','NORMAL','NORMAL','NORMAL','NORMAL','NORMAL','2025-10-10 02:09:21','2025-10-10 02:09:21',NULL,NULL),(2,2044,'azithromycin- 5 days','allergic rhinitis','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-10 04:57:09','2025-10-10 04:57:09',NULL,NULL),(3,2046,'cough x 1 week','','','',NULL,NULL,NULL,NULL,NULL,'NORMAL','','','','','','','','','','2025-10-10 05:28:24','2025-10-10 05:28:24',NULL,NULL),(4,2049,'meniere\'s disease 2023','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-13 03:18:08','2025-10-13 03:18:08',NULL,NULL),(5,2047,'','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-13 03:33:40','2025-10-13 03:33:40',NULL,NULL),(6,2050,'ON AND OFF NASAL BLEEDING','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-13 03:42:00','2025-10-18 03:17:04',NULL,NULL),(7,2051,'ON AND OFF THROAT DISCOMFORT FOR 3 YEARS','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-13 03:51:51','2025-10-13 03:51:51',NULL,NULL),(8,2013,'','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-13 04:11:00','2025-10-18 03:49:00',NULL,NULL),(9,2019,'GIVEN EAR DROPS X 1 WEEK','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-13 04:18:47','2025-10-13 04:18:47',NULL,NULL),(10,2052,'ON AND OFF ITCHINESS EARS','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-13 04:31:42','2025-10-13 04:31:42',NULL,NULL),(11,2040,'','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-13 04:37:07','2025-10-13 04:37:07',NULL,NULL),(12,2053,'','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-13 05:31:01','2025-10-13 05:31:01',NULL,NULL),(13,2054,'','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-13 06:11:42','2025-10-13 06:11:42',NULL,NULL),(14,2055,'','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-13 06:24:07','2025-10-13 06:24:07',NULL,NULL),(15,2056,'','','','','','','','','','','','','','','','','','','','2025-10-14 03:33:00','2025-10-21 06:17:38',NULL,''),(16,2027,'S/P FESS - 2016','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-14 04:06:28','2025-10-14 04:06:28',NULL,NULL),(17,2057,'','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-14 04:14:58','2025-10-14 04:14:58',NULL,NULL),(18,2058,'','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-14 04:24:48','2025-10-14 04:24:48',NULL,NULL),(19,2059,'20 YEARS PTC, NOTED RIGHT PAROTID MAS, 2015, EXCISION DONE, 10 MONTHS PTC, NOTED SUDDEN INCREASE IN SIZE','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-16 04:16:15','2025-10-16 04:16:15',NULL,NULL),(20,2060,'GIVEN ANTIBIOTICS X 2 WEEKS','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-16 04:25:02','2025-10-16 04:25:02',NULL,NULL),(21,2061,'','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-16 04:40:42','2025-10-16 04:40:42',NULL,NULL),(22,2062,'','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-16 04:46:31','2025-10-16 04:46:31',NULL,NULL),(23,2063,'','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-16 05:46:02','2025-10-16 05:46:02',NULL,NULL),(24,2064,'','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-16 05:57:44','2025-10-16 05:57:44',NULL,NULL),(25,2066,'','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-16 06:13:45','2025-10-16 06:13:45',NULL,NULL),(26,2067,'','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-17 03:42:09','2025-10-17 03:42:09',NULL,NULL),(27,2070,'','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-18 03:27:49','2025-10-18 03:27:49',NULL,NULL),(28,2071,'','goiter- sister and niece','','',NULL,NULL,NULL,NULL,NULL,'','','anterior neck mass - thyroid','','','','','','','','2025-10-18 03:42:43','2025-10-18 03:42:43',NULL,NULL),(29,2072,'NONE, PAST MEDICAL HISTORY, S/P FESS - 2016, W','FAMILT','SOCIAL','12/80','35.7','12','12','168.00','75.00','gen','skin','head','eyes','nech','chest','heart','abdomen','extremities','neuro','2025-10-18 05:27:09','2025-10-27 23:20:33',NULL,'ALLERGIES'),(30,2073,'','','','','','','','','','','','','','','','','','','','2025-10-20 04:50:19','2025-10-20 04:50:19',NULL,''),(31,2074,'HYPERTENSION - ON LOSARTAN 50MG, DIABETIC - METFORMIN 500 MG, GLICLASIDE 80 MG, HYERCHOLESTEROLEMIA - ATORVASTATIN 10 MG, ON PHAREX B COMPLEX','','WORKS IN COAL FIRE POWER PLANT, EXPOSD TO LOUD NOISE','','','','','','','','','','','','','','','','','2025-10-20 05:02:39','2025-10-23 05:58:06',NULL,'CO-TRIMOXAZOLE'),(32,2075,'','','','','','','','','','','','','','','','','','','','2025-10-20 05:05:06','2025-10-20 05:05:06',NULL,''),(33,2077,'NOTED DRY SKIN AND FLAKES, LEFT EAR','','','','','','','','','','','','','','','','','','','2025-10-20 05:15:16','2025-10-20 05:15:16',NULL,''),(34,2076,'3 DAYS PTC FALL, CONSULT AT MIHMCI','','','','','','','','','','','','','','','','','','','2025-10-20 05:33:10','2025-10-20 05:33:10',NULL,''),(35,2078,'NO PREVIOUS EAR INFECTIONS','','','','','','','','','','','','','','','','','','','2025-10-20 05:47:38','2025-10-20 05:47:38',NULL,''),(36,2008,'','','','','','','','','','','','','','','','','','','','2025-10-21 05:08:03','2025-10-26 15:20:54',NULL,''),(37,2079,'UNREMARKABLE','','','','','','','','','','','','','non palpable neckmass','','','','','','2025-10-21 06:02:57','2025-10-21 06:02:57',NULL,''),(38,2080,'','','','','','','','','','','','','','','','','','','','2025-10-21 06:08:57','2025-10-21 06:08:57',NULL,''),(39,2081,'ON AND OFF EAR DISCHARGE SINCE 2017','','','','','','','','','','','','','','','','','','','2025-10-21 06:26:08','2025-10-21 06:26:08',NULL,''),(40,2082,'','','','','','','','','','','','','','','','','','','','2025-10-21 06:38:47','2025-10-21 06:38:47',NULL,''),(41,2084,'','','','','','','','','','','','','','','','','','','','2025-10-21 06:52:05','2025-10-21 06:52:05',NULL,''),(42,2085,'','','','','','','','','','','','','','','','','','','','2025-10-21 07:01:08','2025-10-21 07:01:08',NULL,''),(43,2086,'NOTED THYROID NODULES 10 YEARS PTC','','','','','','','','','','','','','','','','','','','2025-10-23 03:27:27','2025-10-23 03:27:27',NULL,'CLINDAMYCIN'),(44,2087,'','','','','','','','','','','','','','','','','','','','2025-10-23 03:42:38','2025-10-23 03:42:38',NULL,''),(45,2088,'','','','','','','','','','','','','','','','','','','','2025-10-23 04:03:31','2025-10-23 04:03:31',NULL,'CLOTRIMOXAZOLE'),(46,2090,'3 YEARS NOTED FUNGAL INFECTION','','','','','','','','','','','','','','','','','','','2025-10-23 04:17:21','2025-10-23 04:17:21',NULL,''),(47,2091,'','','','','','','','','','','','','','','','','','','','2025-10-23 05:03:44','2025-10-23 05:03:44',NULL,''),(48,2092,'','','','','','','','','','','','','','','','','','','','2025-10-23 05:15:33','2025-10-23 05:15:33',NULL,''),(49,2089,'','','','','','','','','','','','','','','','','','','','2025-10-23 05:27:58','2025-10-23 05:27:58',NULL,''),(50,2010,'','','','','','','','','','','','','','','','','','','','2025-10-24 02:32:31','2025-10-24 02:32:31',NULL,''),(51,2020,'S/P FESS - 2016, AZITHROMYCIN- 5 DAYS, S/P FESS - 2016','DIABETIAS, POKPOK','TRAVEL IN CEBU, SMOKING, DRINGKING','12/60','120','20','142','212','211','NORMAL','','anterior neck mass - thyroid','NORMAL','non palpable neckmass','NORMAL','NORMAL','NORMAL','NORMAL','NORMAL','2025-10-26 14:47:24','2025-10-26 14:47:24',NULL,'SEAFOOD, ANTIBIOTIC'),(52,2035,'','','','','','','','','','','','','','','','','','','','2025-10-26 15:22:36','2025-10-26 15:22:36',NULL,'');
/*!40000 ALTER TABLE `health_record` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `health_record_history`
--

DROP TABLE IF EXISTS `health_record_history`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `health_record_history` (
  `history_id` int NOT NULL AUTO_INCREMENT,
  `consultation_id` int NOT NULL,
  `patient_id` int NOT NULL,
  `past_medical_history` text,
  `family_history` text,
  `personal_social_history` text,
  `bp` varchar(50) DEFAULT NULL,
  `temperature` varchar(10) DEFAULT NULL,
  `pr` varchar(10) DEFAULT NULL,
  `rr` varchar(10) DEFAULT NULL,
  `ht` varchar(10) DEFAULT NULL,
  `wt` varchar(10) DEFAULT NULL,
  `general_appearance` text,
  `skin` text,
  `head_and_face` text,
  `eyes` text,
  `neck` text,
  `chest_lungs` text,
  `heart` text,
  `abdomen` text,
  `extremities` text,
  `neurologic` text,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `allergies` text,
  `recorded_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`history_id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `health_record_history`
--

LOCK TABLES `health_record_history` WRITE;
/*!40000 ALTER TABLE `health_record_history` DISABLE KEYS */;
INSERT INTO `health_record_history` VALUES (7,266,2072,'NONE, PAST MEDICAL HISTORY','NONE','NONE','12/80','35.7','12','12','168.00','75.00','NORMAL','NORMAL','anterior neck mass - thyroid','NORMAL','NORMAL','NORMAL','NORMAL','NORMAL','','','2025-10-18 13:27:09','2025-10-23 10:00:03','NONE','2025-10-23 02:25:39'),(8,274,2074,'','','','','','','','','','','','','','','','','','','','2025-10-20 13:02:39','2025-10-20 13:42:00','','2025-10-23 05:58:06'),(9,276,2072,'NONE, PAST MEDICAL HISTORY','NONE','NONE','12/80','35.7','12','12','168.00','75.00','NORMAL','NORMAL','Thyroid','NORMAL','Anterior Neck Mass -','NORMAL','NORMAL','NORMAL','','','2025-10-18 13:27:09','2025-10-23 10:25:39','NONE','2025-10-24 10:20:04'),(10,277,2072,'NONE, PAST MEDICAL HISTORY, S/P FESS - 2016, W','NONE','NONE','12/80','35.7','12','12','168.00','75.00','NORMAL','NORMAL','Thyroid','NORMAL','Anterior Neck Mass -','NORMAL','NORMAL','NORMAL','','','2025-10-18 13:27:09','2025-10-24 18:20:04','NONE','2025-10-24 10:45:19'),(11,278,2072,'NONE, PAST MEDICAL HISTORY, S/P FESS - 2016, W','NONE','NONE','12/80','35.7','12','12','168.00','75.00','NORMAL','NORMAL','Thyroid','NORMAL','Anterior Neck Mass -','NORMAL','NORMAL','NORMAL','','','2025-10-18 13:27:09','2025-10-24 18:45:19','NONE','2025-10-26 12:03:03'),(12,279,2072,'NONE, PAST MEDICAL HISTORY, S/P FESS - 2016, W','NONE','NONE','12/80','35.7','12','12','168.00','75.00','NORMAL','NORMAL','Thyroid','NORMAL','Anterior Neck Mass -','NORMAL','NORMAL','NORMAL','','','2025-10-18 13:27:09','2025-10-26 20:03:03','NONE','2025-10-26 14:35:39'),(13,281,2008,'','','','','','','','','','','','','','','','','','','','2025-10-21 13:08:03','2025-10-21 13:08:03','','2025-10-26 15:19:26'),(14,282,2008,'','','','','','','','','','','','','','','','','','','','2025-10-21 13:08:03','2025-10-26 23:19:27','','2025-10-26 15:20:54'),(15,284,2072,'NONE, PAST MEDICAL HISTORY, S/P FESS - 2016, W','NONE','NONE','12/80','35.7','12','12','168.00','75.00','NORMAL','NORMAL','Thyroid','NORMAL','Anterior Neck Mass -','NORMAL','NORMAL','NORMAL','','','2025-10-18 13:27:09','2025-10-26 22:35:39','NONE','2025-10-27 22:48:29'),(16,285,2072,'NONE, PAST MEDICAL HISTORY, S/P FESS - 2016, W','FAMILT','SOCIAL','12/80','35.7','12','12','168.00','75.00','gen','skin','head','eyes','nech','chest','heart','abdomen','extremities','neuro','2025-10-18 13:27:09','2025-10-28 06:48:29','ALLERGIES','2025-10-27 23:20:33');
/*!40000 ALTER TABLE `health_record_history` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `invoice_items`
--

DROP TABLE IF EXISTS `invoice_items`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `invoice_items` (
  `invoice_item_id` int NOT NULL AUTO_INCREMENT,
  `invoice_id` int DEFAULT NULL,
  `item_id` int DEFAULT NULL,
  `quantity` int DEFAULT NULL,
  `unit_price` decimal(10,2) DEFAULT NULL,
  `total_price` decimal(10,2) DEFAULT NULL,
  `prescription_id` int DEFAULT NULL,
  PRIMARY KEY (`invoice_item_id`),
  KEY `invoice_id` (`invoice_id`),
  CONSTRAINT `invoice_items_ibfk_1` FOREIGN KEY (`invoice_id`) REFERENCES `invoices` (`invoice_id`)
) ENGINE=InnoDB AUTO_INCREMENT=139 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `invoice_items`
--

LOCK TABLES `invoice_items` WRITE;
/*!40000 ALTER TABLE `invoice_items` DISABLE KEYS */;
INSERT INTO `invoice_items` VALUES (101,214,18,2,450.00,900.00,84),(102,214,10,6,35.00,210.00,85),(103,214,16,1,40.00,40.00,86),(104,215,27,14,55.00,770.00,NULL),(105,216,22,1,550.00,550.00,88),(106,216,12,10,65.00,650.00,89),(107,217,19,1,550.00,550.00,93),(108,218,23,21,25.00,525.00,94),(109,219,15,1,400.00,400.00,NULL),(110,219,16,10,40.00,400.00,NULL),(111,220,16,5,40.00,200.00,NULL),(112,221,17,1,450.00,450.00,NULL),(113,222,19,1,550.00,550.00,NULL),(114,223,13,7,75.00,525.00,96),(115,223,10,6,35.00,210.00,97),(116,223,22,1,550.00,550.00,95),(117,224,22,1,550.00,550.00,100),(118,224,13,14,75.00,1050.00,101),(119,224,26,10,35.00,350.00,102),(120,225,22,1,550.00,550.00,103),(121,226,15,1,400.00,400.00,106),(122,227,13,10,75.00,750.00,108),(123,227,15,1,400.00,400.00,109),(124,227,10,1,35.00,35.00,110),(125,228,12,14,65.00,910.00,116),(126,228,14,1,550.00,550.00,115),(127,229,27,14,55.00,770.00,120),(128,229,15,1,400.00,400.00,121),(129,230,19,1,550.00,550.00,125),(130,231,15,3,400.00,1200.00,123),(131,231,26,10,35.00,350.00,124),(132,231,13,14,75.00,1050.00,122),(133,232,16,5,40.00,200.00,127),(134,232,19,1,550.00,550.00,126),(135,233,23,21,25.00,525.00,128),(136,233,26,10,35.00,350.00,129),(137,234,13,2,75.00,150.00,NULL),(138,235,9,1,350.00,350.00,NULL);
/*!40000 ALTER TABLE `invoice_items` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `after_invoice_item_insert` AFTER INSERT ON `invoice_items` FOR EACH ROW BEGIN
    -- Only proceed if the inserted row has a prescription_id
    IF NEW.prescription_id IS NOT NULL THEN
        -- Insert prescription data into dispense_prescription
        INSERT INTO dispense_prescription (prescription_id, patient_id, item_id, quantity, invoice_item_id)
        SELECT p.prescription_id, p.patient_id, p.item_id, p.quantity, NEW.invoice_item_id
        FROM prescription p
        WHERE p.prescription_id = NEW.prescription_id;

        -- Only delete if the insert affected 1 row
        IF ROW_COUNT() = 1 THEN
            DELETE FROM prescription WHERE prescription_id = NEW.prescription_id;
        END IF;
    END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `invoices`
--

DROP TABLE IF EXISTS `invoices`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `invoices` (
  `invoice_id` int NOT NULL AUTO_INCREMENT,
  `customer_name` varchar(255) DEFAULT NULL,
  `invoice_date` datetime DEFAULT CURRENT_TIMESTAMP,
  `subtotal` decimal(10,2) DEFAULT NULL,
  `discount_amount` decimal(10,2) DEFAULT NULL,
  `net_total` decimal(10,2) DEFAULT NULL,
  `amount_received` decimal(10,2) DEFAULT '0.00',
  `change_due` decimal(10,2) DEFAULT '0.00',
  `invoice_type` varchar(20) DEFAULT NULL,
  `note` varchar(100) DEFAULT NULL,
  `discount_percent` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`invoice_id`)
) ENGINE=InnoDB AUTO_INCREMENT=236 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `invoices`
--

LOCK TABLES `invoices` WRITE;
/*!40000 ALTER TABLE `invoices` DISABLE KEYS */;
INSERT INTO `invoices` VALUES (214,'Duzy D Buzz. Jr.','2025-10-18 13:41:14',1150.00,115.00,1035.00,2000.00,965.00,'ITEMS','senior citizen','10'),(215,'Walk-in','2025-10-18 13:46:52',770.00,0.00,770.00,1000.00,230.00,'ITEMS','','0'),(216,'Duzy D Buzz. Jr.','2025-10-18 13:51:27',1200.00,0.00,1200.00,1500.00,300.00,'ITEMS','','0'),(217,'Esquera, Raymunda E.','2025-10-20 13:01:12',550.00,0.00,550.00,550.00,0.00,'ITEMS','','0'),(218,'Sondia, Dwight Derek S.','2025-10-20 13:17:40',525.00,0.00,525.00,525.00,0.00,'ITEMS','','0'),(219,'Walk-in','2025-10-20 13:24:08',800.00,0.00,800.00,1000.00,200.00,'ITEMS','','0'),(220,'Walk-in','2025-10-20 13:47:10',200.00,0.00,200.00,200.00,0.00,'ITEMS','','0'),(221,'Walk-in','2025-10-20 13:52:41',450.00,0.00,450.00,450.00,0.00,'ITEMS','','0'),(222,'Walk-in','2025-10-20 13:53:45',550.00,0.00,550.00,550.00,0.00,'ITEMS','','0'),(223,'Luceno, Leona M.','2025-10-21 14:26:39',1285.00,0.00,1285.00,1285.00,0.00,'ITEMS','','0'),(224,'Cuben, Renato  P.','2025-10-21 14:36:45',1950.00,0.00,1950.00,1950.00,0.00,'ITEMS','','0'),(225,'Jardeloza, Ma. Wella Lou  A.','2025-10-21 14:46:18',550.00,0.00,550.00,550.00,0.00,'ITEMS','','0'),(226,'Aris, Mayrel  P.','2025-10-21 14:58:47',400.00,0.00,400.00,400.00,0.00,'ITEMS','','0'),(227,'Narte, Krisxan  B.','2025-10-21 15:12:10',1185.00,0.00,1185.00,1185.00,0.00,'ITEMS','','0'),(228,'Legaspi, Rubina  P.','2025-10-23 11:41:33',1460.00,0.00,1460.00,1460.00,0.00,'ITEMS','','0'),(229,'Sison, Alona  M.','2025-10-23 12:10:04',1170.00,0.00,1170.00,1170.00,0.00,'ITEMS','','0'),(230,'Aguihap, Kyross Daniel  G.','2025-10-23 13:11:12',550.00,0.00,550.00,550.00,0.00,'ITEMS','','0'),(231,'Stuertz, Rosalla  C.','2025-10-23 13:12:06',2600.00,0.00,2600.00,2600.00,0.00,'ITEMS','','0'),(232,'Consumo, Joviewin  C.','2025-10-23 13:27:01',750.00,0.00,750.00,750.00,0.00,'ITEMS','','0'),(233,'Esgrina, Beljohn  G.','2025-10-23 13:41:26',875.00,0.00,875.00,875.00,0.00,'ITEMS','','0'),(234,'Walk-in','2025-10-28 06:29:19',150.00,0.00,150.00,200.00,50.00,'ITEMS','','0'),(235,'Walk-in','2025-10-28 06:32:28',350.00,0.00,350.00,500.00,150.00,'ITEMS','','0');
/*!40000 ALTER TABLE `invoices` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `issued_medical_certificate`
--

DROP TABLE IF EXISTS `issued_medical_certificate`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `issued_medical_certificate` (
  `medical_certificate_id` int NOT NULL AUTO_INCREMENT,
  `consultation_id` int NOT NULL,
  PRIMARY KEY (`medical_certificate_id`),
  UNIQUE KEY `uq_consultation` (`consultation_id`),
  CONSTRAINT `issued_medical_certificate_ibfk_1` FOREIGN KEY (`consultation_id`) REFERENCES `consultation` (`consultation_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `issued_medical_certificate`
--

LOCK TABLES `issued_medical_certificate` WRITE;
/*!40000 ALTER TABLE `issued_medical_certificate` DISABLE KEYS */;
/*!40000 ALTER TABLE `issued_medical_certificate` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `items`
--

DROP TABLE IF EXISTS `items`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `items` (
  `item_id` int NOT NULL AUTO_INCREMENT,
  `generic_name` varchar(100) DEFAULT NULL,
  `brand_name` varchar(100) DEFAULT NULL,
  `strength` varchar(50) DEFAULT NULL,
  `dosage` varchar(100) DEFAULT NULL,
  `category` varchar(100) DEFAULT NULL,
  `quantity` int DEFAULT '0',
  `cost_price` decimal(10,2) DEFAULT '0.00',
  `selling_price` decimal(10,2) DEFAULT '0.00',
  `description` text,
  `status` enum('active','disabled') NOT NULL DEFAULT 'active',
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
  `updated_at` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`item_id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `items`
--

LOCK TABLES `items` WRITE;
/*!40000 ALTER TABLE `items` DISABLE KEYS */;
INSERT INTO `items` VALUES (9,'Mupirocin','Mupiderm','20mg/g','Ointment','Antibacterial',74,0.00,350.00,'','active','2025-10-18 12:56:59','2025-10-28 06:32:28'),(10,'Celecoxib','Recosan','200mg','Capsule','Pain reliever',173,0.00,35.00,'','active','2025-10-18 12:59:37','2025-10-21 15:12:11'),(11,'Ascorbic acid+ zinc+vit d3','Augmenz plus','500mg/ 10mg/1000iu','Capsule','Fsupplement',164,0.00,25.00,'','active','2025-10-18 13:03:53','2025-10-24 10:36:40'),(12,'Esomeprazole','Peprazom','40mg','Capsule','Anti acid',560,0.00,65.00,'','active','2025-10-18 13:05:39','2025-10-23 11:41:33'),(13,'Ciprofloxacin','Cifrextab','750mg','Tablets','Antibacterial',243,0.00,75.00,'','active','2025-10-18 13:07:21','2025-10-28 06:29:19'),(14,'Chlorhexidine','Essprin','20ml','Bottle','Oral spray',22,0.00,550.00,'','active','2025-10-18 13:13:04','2025-10-23 11:41:33'),(15,'Ofloxacin, beclomethasone, clotrimazole & lidocane','Otobiotic plus','.3%','Eardrops','Antibacterial',83,0.00,400.00,'','active','2025-10-18 13:17:38','2025-10-23 13:12:06'),(16,'Alpha lipoic acid, ginkgo biloba','Otoshield','30mg','Capsule','Fsupplement',850,0.00,40.00,'','active','2025-10-18 13:20:00','2025-10-23 13:27:01'),(17,'Isotonic','Snizel','.9%','Bottle','Nasal spray',9,0.00,450.00,'','active','2025-10-18 13:22:38','2025-10-20 14:18:41'),(18,'Hypertonic','Snizel','2.3%','Bottle','Nasal spray',1,0.00,450.00,'','active','2025-10-18 13:24:18','2025-10-20 14:38:12'),(19,'Fluocinilonr, polymyxin,neomycin','Aplosyn','250mcg','Bottle','Eardrops',1,0.00,550.00,'','active','2025-10-18 13:27:18','2025-10-23 13:27:02'),(20,'Betamethasone, gentamicin, clotrimazole','Bethamistine','640mcg','Cream','Topical',27,0.00,780.00,'','active','2025-10-18 13:31:47','2025-10-20 14:05:03'),(21,'Dimethicone','Scarlite','15mg','Tube','Scar gel',12,0.00,1000.00,'','active','2025-10-18 13:35:21','2025-10-20 14:42:39'),(22,'Ofloxacin, beclomethasone, clotrimazole & lidocane','Otoqure plus','10ml','Bottle','Eardrops',52,0.00,550.00,'','active','2025-10-18 13:37:03','2025-10-21 14:46:24'),(23,'Serrapeptase','Chatase','10mg','Tablets','Fsupplement',345,0.00,25.00,'','active','2025-10-18 13:38:40','2025-10-23 13:41:26'),(24,'Acetylcysteine','Pneumotyl','600mg','Tablets','Mucolytic',50,0.00,35.00,'','active','2025-10-18 13:40:40','2025-10-20 12:17:27'),(25,'Clindamycin','Peldacyn','300mg','Capsule','Antibacterial',200,0.00,45.00,'','active','2025-10-18 13:41:55','2025-10-20 14:06:58'),(26,'Montelukast+levocetirizine','Stelix','1omg/5mg','Tablets','Antihistamine',297,0.00,35.00,'','active','2025-10-18 13:43:26','2025-10-23 13:41:26'),(27,'Coamoxiclav','Natravox','625mg','Tablets','Antibacterial',266,0.00,55.00,'','active','2025-10-18 13:44:51','2025-10-23 12:10:04'),(28,'Thiamine, pyridoxine, cyanocobalamin','Neurocare','300mg','Capsule','Fsupplement',750,0.00,25.00,NULL,'active','2025-10-21 10:31:10','2025-10-21 10:33:03'),(29,'Sodium chloride','Flo sinus care kit','1.544g','Bottle','Nasal spray',12,380.00,550.00,NULL,'active','2025-10-21 12:35:18','2025-10-21 15:29:01'),(30,'Sodium chloride','Flo sinus care refill sachet','1.544g','Sachet','Nasal spray',200,10.40,20.00,NULL,'active','2025-10-21 12:40:23','2025-10-21 15:29:54');
/*!40000 ALTER TABLE `items` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lab_requests`
--

DROP TABLE IF EXISTS `lab_requests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `lab_requests` (
  `request_id` int NOT NULL AUTO_INCREMENT,
  `patient_id` int NOT NULL,
  `test_ids` json NOT NULL,
  `request_date` date NOT NULL,
  `consultation_id` int DEFAULT NULL,
  PRIMARY KEY (`request_id`),
  KEY `patient_id` (`patient_id`),
  KEY `fk_labrequests_consultation` (`consultation_id`),
  CONSTRAINT `fk_labrequests_consultation` FOREIGN KEY (`consultation_id`) REFERENCES `consultation` (`consultation_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `lab_requests_ibfk_1` FOREIGN KEY (`patient_id`) REFERENCES `patients` (`patient_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lab_requests`
--

LOCK TABLES `lab_requests` WRITE;
/*!40000 ALTER TABLE `lab_requests` DISABLE KEYS */;
INSERT INTO `lab_requests` VALUES (35,2086,'[31, 30]','2025-10-23',267),(37,2086,'[30, 31]','2025-10-23',267),(38,2089,'[32]','2025-10-23',273);
/*!40000 ALTER TABLE `lab_requests` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lab_results`
--

DROP TABLE IF EXISTS `lab_results`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `lab_results` (
  `result_id` int NOT NULL AUTO_INCREMENT,
  `consultation_id` int NOT NULL,
  `test_name` varchar(255) NOT NULL,
  `result_text` text,
  `result_file` varchar(255) DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`result_id`),
  KEY `consultation_id` (`consultation_id`),
  CONSTRAINT `lab_results_ibfk_1` FOREIGN KEY (`consultation_id`) REFERENCES `consultation` (`consultation_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lab_results`
--

LOCK TABLES `lab_results` WRITE;
/*!40000 ALTER TABLE `lab_results` DISABLE KEYS */;
/*!40000 ALTER TABLE `lab_results` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lab_tests`
--

DROP TABLE IF EXISTS `lab_tests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `lab_tests` (
  `id` int NOT NULL AUTO_INCREMENT,
  `category` varchar(100) NOT NULL,
  `test_name` varchar(200) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lab_tests`
--

LOCK TABLES `lab_tests` WRITE;
/*!40000 ALTER TABLE `lab_tests` DISABLE KEYS */;
INSERT INTO `lab_tests` VALUES (28,'Blood Test','CBC'),(29,'Blood Test','White Blood Test'),(30,'thyroid function test','T3, T4, TSH'),(31,'Ultrasound','Thyroid'),(32,'X-RAY','PNS WATER\'S VIEW UPRIGHT');
/*!40000 ALTER TABLE `lab_tests` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `other_items`
--

DROP TABLE IF EXISTS `other_items`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `other_items` (
  `item_id` int NOT NULL AUTO_INCREMENT,
  `generic_name` varchar(100) DEFAULT NULL,
  `brand_name` varchar(100) DEFAULT NULL,
  `strength` varchar(50) DEFAULT NULL,
  `dosage` varchar(100) DEFAULT NULL,
  `description` text,
  `category` varchar(100) DEFAULT NULL,
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
  `updated_at` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`item_id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `other_items`
--

LOCK TABLES `other_items` WRITE;
/*!40000 ALTER TABLE `other_items` DISABLE KEYS */;
INSERT INTO `other_items` VALUES (5,'Clarithromycin','Klear','500mg','Tablet','','Antibacterial','2025-10-18 13:30:09','2025-10-18 13:30:09'),(6,'Radix gentianae','Sinupret forte','','Dragee','','Mucolytic','2025-10-20 13:06:24','2025-10-20 13:06:24'),(7,'CEFALEXIN','CEFALIN','250 MG','SYRUP','BOTTLE','Antibacterial','2025-10-23 13:06:02','2025-10-23 13:06:02'),(8,'BETAHISTINE','SERC','24MG','Tablet','TABLET','ANTI- VERTIGO','2025-10-23 13:16:41','2025-10-23 13:16:41'),(9,'MOMETASONE FUROATE','NASONEX','50 MCG/DOSE','140 ACTUATIONS','BOTTLE','NASAL STEROID','2025-10-23 13:32:38','2025-10-23 13:32:38');
/*!40000 ALTER TABLE `other_items` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patient_documents`
--

DROP TABLE IF EXISTS `patient_documents`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patient_documents` (
  `id` int NOT NULL AUTO_INCREMENT,
  `patient_id` varchar(20) NOT NULL,
  `image_path` varchar(255) NOT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patient_documents`
--

LOCK TABLES `patient_documents` WRITE;
/*!40000 ALTER TABLE `patient_documents` DISABLE KEYS */;
INSERT INTO `patient_documents` VALUES (1,'2091','C:\\Users\\wenwe\\Downloads\\511196925_1521276215894979_5986668024209737069_n.jpg','2025-10-25 18:07:46'),(4,'2072','//SERVER/Shared/2072/ScannedDocuments/queen bday.jpg','2025-10-26 16:04:48'),(5,'2072','//SERVER/Shared/2072/ScannedDocuments/Jonathan-Rey-Devices-Printers-Printer-Brother-HL-2240.256.png','2025-10-26 16:04:48'),(6,'2067','//SERVER/Shared/2067/ScannedDocuments/Jonathan-Rey-Devices-Printers-Printer-Brother-HL-2240.256.png','2025-10-26 16:29:23'),(7,'2033','//SERVER/Shared/2033/ScannedDocuments/queen bday.jpg','2025-10-26 16:30:16'),(8,'2013','//SERVER/Shared/2013/ScannedDocuments/068ce57f-3b1f-41df-869d-dcc6a13bae33.jpg','2025-10-26 16:30:28'),(9,'2028','//SERVER/Shared/2028/ScannedDocuments/aa9d075a1d744e619b72b0dfa6be19fa.jpg','2025-10-27 01:19:52'),(10,'2049','//SERVER/Shared/2049/ScannedDocuments/aa9d075a1d744e619b72b0dfa6be19fa.jpg','2025-10-27 01:20:02'),(11,'2049','//SERVER/Shared/2049/ScannedDocuments/143490d4f127469ab3ce5d8d1927e979.png','2025-10-27 01:20:02'),(12,'2084','//SERVER/Shared/2084/ScannedDocuments/143490d4f127469ab3ce5d8d1927e979.png','2025-10-27 01:20:07'),(13,'2084','//SERVER/Shared/2084/ScannedDocuments/8ed6eb4d5ff94846b94743ad3e89f34f.jpg','2025-10-27 01:20:07'),(14,'2057','//SERVER/Shared/ScannedDocuments/2057/ScannedDocuments/Jonathan-Rey-Devices-Printers-Printer-Brother-HL-2240.256.png','2025-10-28 00:20:57'),(15,'2061','//SERVER/Shared/ScannedDocuments/2061/ScannedDocuments/Jonathan-Rey-Devices-Printers-Printer-Brother-HL-2240.256.png','2025-10-28 00:25:24'),(16,'2067','//SERVER/Shared/ScannedDocuments/2067/ScannedDocuments/adasd.jpg','2025-10-28 00:27:02'),(17,'2050','//SERVER/Shared/ScannedDocuments/2050/ScannedDocuments/queen bday.jpg','2025-10-28 00:27:26'),(18,'2050','//SERVER/Shared/ScannedDocuments/2050/ScannedDocuments/Jonathan-Rey-Devices-Printers-Printer-Brother-HL-2240.256.png','2025-10-28 00:27:26'),(19,'2018','//SERVER/Shared/ScannedDocuments/2018/ScannedDocuments/queen bday.jpg','2025-10-28 00:28:51'),(20,'2022','//SERVER/Shared/ScannedDocuments/2022/ScannedDocuments/adasd.jpg','2025-10-28 00:29:11');
/*!40000 ALTER TABLE `patient_documents` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patients`
--

DROP TABLE IF EXISTS `patients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patients` (
  `patient_id` int NOT NULL AUTO_INCREMENT,
  `full_name` varchar(100) NOT NULL,
  `address` varchar(255) DEFAULT NULL,
  `birth_date` date DEFAULT NULL,
  `age` int DEFAULT NULL,
  `sex` enum('M','F') DEFAULT NULL,
  `civil_status` varchar(20) DEFAULT NULL,
  `patient_contact_number` varchar(11) DEFAULT NULL,
  `emergency_name` varchar(150) DEFAULT NULL,
  `emergency_contact_number` varchar(11) DEFAULT NULL,
  `emergency_relationship` varchar(50) DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `photo` longblob,
  `referred_by` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`patient_id`),
  KEY `idx_patients_full_name` (`full_name`)
) ENGINE=InnoDB AUTO_INCREMENT=2096 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patients`
--

LOCK TABLES `patients` WRITE;
/*!40000 ALTER TABLE `patients` DISABLE KEYS */;
INSERT INTO `patients` VALUES (2008,'Tolentino, Aaron Joshua M.','Calinog Iloilo','2004-06-25',21,'M','Single','09670277266','Arlene M Untaran. ','09670277266','Mother','2025-10-03 03:39:35',NULL,NULL),(2009,'Villarma, Genefer G.','Tagbak Jaro','1973-09-10',52,'F','Single','09308736313','','','','2025-10-03 03:52:51',NULL,NULL),(2011,'Monares, Ma. Jazel C.','Pandac Pavia','1978-06-06',47,'F','Single','09940143448',NULL,'','','2025-10-03 04:37:34',NULL,NULL),(2012,'Velando, Lawrence Michael A.','Guadalupe Janiuay Iloilo','1995-11-24',29,'M','Single','09625393368','','','','2025-10-03 04:52:47',NULL,NULL),(2013,'Cachuela, Grace C.','45 R Dicen St Lapaz','1977-01-28',48,'F','Single','09216099508','','','','2025-10-03 05:26:47',NULL,NULL),(2014,'Pendon, Mary Annie B.','Bliss Caingin La Paz','2017-04-10',8,'F','Single','09106044529','Chona B Pendon. ','09106044529','Mother','2025-10-03 06:00:11',NULL,NULL),(2015,'Toong, Robert James T.','Tigbauan Iloilo','1988-02-07',37,'M','Single','09692445818',NULL,'','','2025-10-04 01:45:43',NULL,NULL),(2016,'Singh, Gurpreet D.','Mandurriao','1985-08-22',40,'M','Married','09158542669','','','','2025-10-04 02:09:32',NULL,NULL),(2017,'Jadan, Susan G.','Tacas Jaro','1959-11-21',65,'F','Married','09469474508','','','','2025-10-04 02:23:48',NULL,NULL),(2018,'Cabaya, Rex Jr. V.','San Isidro, Jaro','2007-03-17',18,'M','Single','09122757879','','','','2025-10-06 01:52:50',NULL,NULL),(2019,'Jarbadan, Juvan Jr. A.','Dumangas','2021-01-20',4,'M','Single','09429198954','','','','2025-10-06 04:32:45',NULL,NULL),(2020,'De Los Santos, Arjoe M.','Jaro','1998-03-10',27,'M','Married','09562937351','','','','2025-10-06 04:34:39',NULL,NULL),(2021,'Lubis, Judy N.','Pototan','1964-01-10',61,'F','Married','09500334401','','','','2025-10-06 04:38:19',NULL,NULL),(2022,'Cabuhay, John Vincent A.','Capiz','1988-04-28',37,'M','Single','09943283948','','','','2025-10-06 04:40:43',NULL,NULL),(2023,'Brillantes, Kristel Arianne S.','Btac. Nuevo','2007-07-20',18,'F','Single','09930908561','','','','2025-10-06 04:42:35',NULL,NULL),(2024,'Tobongbanua, Edzey P.','Pavia','2018-07-03',7,'M','Single','09673747124','','','','2025-10-06 04:44:26',NULL,NULL),(2025,'Co, Catherine B.','San Juaquin','1970-02-23',55,'F','Single','09262465898','','','','2025-10-06 04:46:21',NULL,NULL),(2026,'De Chavez, Emee D.','Passi City','1982-07-02',43,'F','Married','09398920923','','','','2025-10-06 06:10:27',NULL,NULL),(2027,'Cagampang, Quirico C.','Leon Iloilo','1973-08-24',52,'M','Married','09089575612','','','','2025-10-07 02:18:04',NULL,NULL),(2028,'Canas, Corazon C.','Maribong, Lambunao','1959-11-05',65,'F','Single','09956435605','','','','2025-10-07 02:42:33',NULL,NULL),(2029,'Salado, Leanne Ma. Abigail P.','Buenavista, Guimaras','2000-12-20',24,'F','Single','09691454698',NULL,'','','2025-10-07 02:57:09',NULL,NULL),(2030,'Infante, Dolores S.','Jaro','1934-03-25',91,'F','Widowed','09278170053','','','','2025-10-07 04:18:31',NULL,NULL),(2032,'Zamudio, Larry G.','Molo, Iloilo City','1974-09-06',51,'M','Married','09466110131','','','','2025-10-07 05:13:18',NULL,NULL),(2033,'Alcantara, Ramil P.','Pavia','1969-02-27',56,'M','Married','09082770229','','','','2025-10-09 01:00:29',NULL,NULL),(2034,'Alanan, Thaniel O.','Lemery','1970-07-20',55,'M','Married','09625297499','','','','2025-10-09 01:30:52',NULL,NULL),(2035,'Nawanao, Cherrylyn A.','Lambunao','1994-07-22',31,'F','Single','09957342064','','','','2025-10-09 01:40:13',NULL,NULL),(2036,'Unternahrer, Johnny Mark A.','Molo, Iloilo City','1985-06-25',40,'M','Single','09628168203','','','','2025-10-09 02:49:47',NULL,NULL),(2037,'Dieminger, Karl Heinz W.','Jaro Iloilo City','1943-10-21',81,'M','Widowed','09307790431','','','','2025-10-09 03:34:41',NULL,NULL),(2038,'Quiling, Joevy C.','Balabag, Pavia','1975-04-21',50,'M','Married','09481625400','','','','2025-10-09 04:15:42',NULL,NULL),(2039,'Fagarita, Rhian Dhenniel S.','Sto. Nino, San Miguel, Iloilo','2011-11-02',13,'F','Single','09631271970','Rhea S Fagarita. ','09631271970','Mother','2025-10-09 04:36:33',NULL,NULL),(2040,'Seruelo, Maricris A.','Democracia, Jaro','1972-10-27',52,'F','Married','09300433372','','','','2025-10-09 05:01:30',NULL,NULL),(2041,'Suarez, Evelyn F.','Leganes, Iloilo','1964-01-21',61,'F','Single','09128763028','','','','2025-10-09 05:04:24',NULL,NULL),(2042,'Lorilla, Roi Yisrael P.','Jaro Iloilo City','2016-08-02',9,'M','Single','09603354986','','','','2025-10-09 05:08:20',NULL,NULL),(2043,'Yap, Rayner A.','Jaro Iloilo City','1976-10-01',49,'M','Married','09209611346','','','','2025-10-09 05:12:40',NULL,NULL),(2044,'Hiponia, Rhoan I.','Pavia, Iloilo','1989-12-18',35,'M','Married','09171793577','','','','2025-10-10 03:21:12',NULL,NULL),(2046,'Rosas, Leonora A.','Jaro Iloilo City','1976-05-28',49,'F','Married','09177960194','','','','2025-10-10 03:53:36',NULL,NULL),(2047,'Javier, Gilda C.','Jayme St. Jaro','1967-10-10',58,'F','Married','09109181848','','','','2025-10-13 01:58:14',NULL,NULL),(2049,'Banares, Karl Benjie D.','Arevalo, Iloilo City','1974-10-15',50,'M','Married','09778577200','','','','2025-10-13 02:33:14',NULL,NULL),(2050,'Cardinal, Kate Chriezl M.','Imbang, Passi','2013-09-13',12,'F','Single','09993798018','','','','2025-10-13 02:39:22',NULL,NULL),(2051,'Calaman, John Andy P.','Pavia, Iloilo','1990-01-08',35,'M','Married','09260678877','','','','2025-10-13 03:05:15',NULL,NULL),(2052,'Gambalan, Elynor Grace B.','La Paz','1984-01-24',41,'F','Married','09177220625','','','','2025-10-13 03:56:11',NULL,NULL),(2053,'Cantero, Monje Val C.','Leon Iloilo','1989-02-14',36,'M','Single','09352521784','','','','2025-10-13 05:10:38',NULL,NULL),(2054,'Celmar, Elmer Jr. A.','Dingle, Iloilo','2005-03-24',20,'M','Single','09099871932','','','','2025-10-13 05:58:39',NULL,NULL),(2055,'Kumar, Pardeep Jr. D.','Jaro Iloilo City','1988-05-08',37,'M','Single','09301274555','','','','2025-10-13 06:01:25',NULL,NULL),(2056,'Lagamon, Ronnie A.','Anilao, Iloilo','1960-04-03',65,'M','Single','09393245168','','','','2025-10-14 02:43:22',NULL,NULL),(2057,'Balagosa, John Irwin F.','Benedicto, Jaro','1995-06-08',30,'M','Single','09157576732','','','','2025-10-14 03:43:28',NULL,NULL),(2058,'Garcia, Aniela C.','La Paz','2008-12-05',16,'F','Single','09778038810','Evita C Garcia. ','','Mother','2025-10-14 04:03:20',NULL,NULL),(2059,'Bernal, Arnie A.','Cawayan, Carles','1973-06-17',52,'F','Married','09707148138','','','','2025-10-16 02:24:21',NULL,NULL),(2060,'Sibonga, Angelo L.','Cabatuan','2009-12-09',15,'M','Single','09917243969','Jasmine L Sibonga. ','09924123884','Mother','2025-10-16 02:37:12',NULL,NULL),(2061,'Castelo, John Martin D.','Tabuc Suba, Jaro','1996-10-02',29,'M','Single','09176277720','','','','2025-10-16 03:01:32',NULL,NULL),(2062,'Castelo, Joanne Marie D.','Tabuc Suba, Jaro','1959-10-21',65,'F','Married','09176277720','','','','2025-10-16 03:03:44',NULL,NULL),(2063,'Tayona, Jyra S.','Tacas Jaro','1991-03-01',34,'F','Single','09205073730','','','','2025-10-16 05:08:17',NULL,NULL),(2064,'Siason, Pedro D.','Commission Civil, Jaro','1947-12-23',77,'M','Married','09209454565','','','','2025-10-16 05:29:00',NULL,NULL),(2065,'Pongan, Jann Christian P.','Jaro Iloilo City','2005-01-24',20,'M','Single','09776697152','','','','2025-10-16 05:33:18',NULL,NULL),(2066,'Fabiana, Ma. Rena B.','Jaro Iloilo City','1970-09-29',55,'F','Married','09468611193','','','','2025-10-16 05:43:40',NULL,NULL),(2067,'Aguilano, Glenn B.','Binalbagan, Negros Occ','1989-03-03',36,'M','Married','09479718460','','','','2025-10-17 02:20:52',NULL,NULL),(2068,'Jimenez, Diego M.','Mandurriao','1951-09-24',74,'M','Married','09125470449','','','','2025-10-17 03:14:42',NULL,NULL),(2069,'Castronuevo, Antonio C.','Benedicto, Jaro','1957-06-13',68,'M','Married','09489785513','','','','2025-10-17 03:19:48',NULL,NULL),(2070,'Cardinal, Liezel M.','Passi City','1975-10-08',50,'F','Married','09993798018','','','','2025-10-18 03:03:22',NULL,NULL),(2071,'Arroyo, Tessie M.','Dumarao, Capiz','1956-07-06',69,'F','Married','09993798018','','','','2025-10-18 03:12:51',NULL,NULL),(2072,'Buzz, Duzy Jr. G.','Buntatala Jaro, Iloilo City','2002-04-03',23,'M','Single','09511365191','','','','2025-10-18 05:24:56',_binary '����\0JFIF\0\0`\0`\0\0��\0C\0		\n\r\Z\Z $.\' \",#(7),01444\'9=82<.342��\0C			\r\r2!!22222222222222222222222222222222222222222222222222��\0�Y\"\0��\0\0\0\0\0\0\0\0\0\0\0	\n��\0�\0\0\0}\0!1AQa\"q2���#B��R��$3br�	\n\Z%&\'()*456789:CDEFGHIJSTUVWXYZcdefghijstuvwxyz���������������������������������������������������������������������������\0\0\0\0\0\0\0\0	\n��\0�\0\0w\0!1AQaq\"2�B����	#3R�br�\n$4�%�\Z&\'()*56789:CDEFGHIJSTUVWXYZcdefghijstuvwxyz��������������������������������������������������������������������������\0\0\0?\0��(��AEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEī���¢�&�.!�G�%Ft��\"��	o\\��&��vW.pA�^����ꪰ]����\0�\rXꨢ�\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��4����J�#A���\0�3���▾y��ĩ��r+M:VM:	;y���W�hW�PЬ��ͅX��@���\n(��Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@`x��i����\0���~�����ǡ��$~hc��G!k�e\'�����Yf=+��9�[��v��4	�á�ɭ8�I�܌���5��\\�w��$l25��sȶC���)���)��G*z\Z\0��+>�U��%g[�U���A{P�E\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE2Y��wH�G�\09�Q�\n�2I�^�3��jS�\'K�+Ĳ/Y�ҺOx����K;\"�BN����W�O	yX���@��Mʒ8<���.>����J����a	��s�=r$����-��P>���E(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0+ɾ.L���<�+�k�~#��k���Y�(\Z<��lf���GL��͘`H�T��`��(3�v�q�5�hw`Ȟ���,��hi{�h��\0��>B}*ϟ,,<�I���\n=��Wf}����3Z\rZ�K\Z��\Z�Kؘ�s�\\��7=Ei� u�����2��N����BFpƁh�9ǽ.�n٠QI���d~T\0�QE\0QE\0T77v�P��S�j2ZF\0W�|e𶓘�%�S�c�_���@�oc��II�GTQݎ|�|n�&��-6��MR0X1��\'��\Z�/�MwXg}GU����+Hv������F���}Q}��i���f���³��?������5��i��T�a��)s�Q�,_<#<�������/�3[���ܕ]�!a�@I��M� ����JdI-��[y%�A��b�?G0�/3��+�M#���2��W�X�8��y�~�����F���V=oI��kF�O�S�3Üt�n������ �ꐙy�̧c��k��@QE\0QE\0QE�\0QM�?\\Ry��EFd=��U�Y\n����\0\\gT�\0���jvЮCo>�Y��d��@�@�K{��۠��<��e$�r�L��T��P�XJ���f|܎�h��L%$��k��Xᐑ�]�\0�$U��旮�V�zP}ܦYT������k]BB��ZbKv�[� �Ϩ(��kc�袊	\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n�O�[ķ={]x/�$�\0����\rl��f+����,c�V:N.�Jz�w���(�mx>��+|N8�jk�rֶt�Idlc4��HR6\'��.�ݎz\nv����4ۏ�VP9��$95��e�����sW��\\t�f�K���93T\"��@�~O�Ӄd�j�d��jDbW��\0ZX\'�~���Q���QJO�����}qJ>�O��F�ųO���Gș˿�G&�_Ŀ\Z��L5���u�O=�ҷN�����ړi9l{f���V�}V�+q��	˷|�My��:\\�����1�i�cע�é�+ʝn/�\r������&r��&��g���T�3h�Kq5McTצ�k뛶��裁U�����(��#��8?�J�b��4F|v���~f;v\\e�}3Z	�N;\n����#��\0�������Ҙ0{\n��3@A��Q�[	��\0�S;i��cޢx\'߭h�-ШϡY��q�@����U�*����A�\Z�1�pv�Ƒ��(��PAy\rЊ�O�~,���S{�e<�w�����d��y��ޙ�4��{��៎\Z�R�Y������;�\'���Ǯ:רC4W,�ȲF�!��\Z��[`rG~����\Zk�\r�V�nY�r7��KF�=��y=?Z�#)S�}qEq\n�����5���=L(�i)�\'���C�\0�]�Q�V\Z���i����9���8��\0z���\n��G���*L��\Z\0���U\\�\"�Hx��T���ei?.{�	[�jԌ7j��c�p�m�Y�$�!�u\"���:S����g�\0�?����W�ܒ���s]ߌ� <s\\++��\08�Fk.�ȯR�\"<�F�=c�o��,G+]���^�Q�*�=��(����(\0��(\0��(\0��(\0��(\0��(\0��(\0��(\0��(\0��(\0��(\0��(\0���5�?�k��üU�]��z\Z�g+t�UW�<��̸5��ZQ�ڱ.,\\K����9��7Z��%\"bCW=k��H�0�$y���܆uҟ3�� �>�pL�v����\0�3TF�*���c�e�#3�z\ZيP6�X`�ԫy�\\��4y�lԊ��?֜�c��\'�|_�xGN7Z��W�P//)����76g�8#�Y]R4�fc�+��q�c6��}�3��Ɵ��\0x���=�w�����3�M39[X�\r����\\�0d�*�F�V>���V�{�F�[��:�#d�=���pZ��=��\0sڴ#���T���n�0=j�vĎ1�SWa�f\\���t�Q����W�M )����=��*��*�0=��\n���� �tN�;��(�qX���O�h��\"��)��#�jUX��y�]�0�(7Lș����\'��<o���j�P\r{���\0ߺ��1�\n�d�j،��l��ӆ��=1\"�@�m|�S���U��[p#�?�i\0�e8�Dz���N�Q�Z\0Ɩ�:����\\G$kꧦkjHБ��g�*���+����sFgg�ګ�\rh�gi\0s���\0���\0��1�A���G\"�^���j��;�@�f�*�$l�\"���e=�5����ub��)-5�v��2zo���z��x���4�L���׶�����vs�=���$���pi���_,�O�zׂ/Z�n,[��H�a��>�ך�\'��/Ҽ[�%̈́���m��H��ץZw0�\\M�9�R0;G�ӿ�q��NB���I�\Z��f���r)�E@уg�3� na�Y��6�Z3�\0泤Q�}�Jgǎ�cOR�^�U�Xֆ�wI��Z�W��w\\�<�x,��1��\0�q*��z����˶@Q{�($�S�H��5<WO�m��%�����	����|�w�n\0��\nG��EQE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0��&�o��3�Hk�k�<S0o_�\0�S@�ŏ2\'�g�D�bGܸ �T�2ĸ=�ŵ����� �_5�Ɲ��yw9�f��e������*�\r5��Yb�1ӽW��\0��zd\n�-��2��Tnl��9<P46ز��ՆV<Uk[���k\"v�2�S��j7sTV\';F��m��|%�\0��o��z��}�1Z�\"Ǎ� Y�:�*��e������?�|����\"�e��Y�k�~����E=�֭�M}{3M4��v�#��K�����f�ΘC��F��+J��s8�M��,w5jăQ���RX�ƨ9���z�,�з�\0�N������!W��v݋��䟠��n\n�Q�\"A$�\\u��J|p*��l���:$�c���/�\'�)���w0���9ZH�f�O	����F�H���rj��sb1�F�@����	��{ƪv�r!�Lpۗ���*�	t��Ga��:Pf�3��@R�YȎ�(��sZ�^tan��2�+�0P2���w:?���R}�#p�<~���lԧ�aq��͍R�nM�ޝ�\0Rh�����[��/��0��8�vR���k��,}\rEp��e��\"�h��36ݸa��U����`���VûL���c�T%e�1ʳ\'L��ǒԐrW���T���\0����c�y%��z���Ts��rN8��4��i\"d��U�3��iέ~\\�z��r/FE*�Yj->��\0A�a�4����#�0����;�VS9*z��x���ݏ��\Z��\'��\0��^2��4ʶ��#2ۓ�����*����+��k�y!������كC�V�\Z��?-�XL��-�uN���J�j�;�N���j��=�ę�V�a�d�v�8�Y��,d�Nz�u͸rh\Z2\'�iǽ^�e�\0JS��R�\"Q�<�R�L#�H\Z�>1�����y��2퍐�k��܎#e��y����	�4ͽ$�:��<W7��k���30�z�Ƿ��Bh\Z=Ҋ(����(\0��(\0��(\0��(\0��(\0��(\0��(\0��(\0��(\0��(\0��(\0��(C_9x������bC�������_;�o�5��YO�kc\n{�@9���d����fT�:UXc*�v��GCg2�хn��cp�2�����ȕX1��exn@#�f����܇=5V�@�,�U4{�`Y�%i�h{��u�H�,��sZ9�X�ҕlg�8�*9�C��}M���m���]�Ҿb��w�\\_\\�\ZC�#N�>��5��LE���r�\\\\��?\"�\'��\ryTE��j$��er�i�����g\'�K*��U���=���T�ۙ\0pkZ�%;B�=�=�Ϸ�GoT�O�J�}?Ƶ-�E܎B�>iO�b�TB�|�1���E$k4��P[��taV���Eފ���PT��<�|�\0�Q�M\0g��f*L���婾���0�Oe֔3�[m˄8�9���v�@x�\08�\nQA��՘��X63��L(��V>�y*��F�H��H�g��.n���@��20�m�����W�еF�%u��Z�H��H�7��ҬEi�ܐ�1E>��i��\0�?x�\"Y\Z�-��j����Lcpq�5$%�luF*sulqs��%�Jab�����&)��Ui\"�1����Gڵ�;�f�R�ة��P��կ�љzo��-��{v.�J78�l���&	��^զ�,��Xܘ���f�M$�H����:P\"��)\"�=��*t�i����b� �*ђa�XK��\"�U2)����׊\0�H>M�M�+>��*��2�p:}kME?�\'\r8o�$�$�R ��#�9٢ۜ�j͞O�V��j�H�S�}����hF@ uO��1��9�(Xq������5R���@w\0ۺL\0���U;�{;���&dua$R��V�,{ԧ5�������\0�>�!3识~=���[��ꖃm�c�qÏc���\\�w�_#�g��}��Lj�fA�q��?������\rGO�����<bDe<FkD�i+2��W&��,�y��W̏�eL�d=��9۰~Ӵ�ՋL��8�����U�17,��\\�Cg7�h��AZ洍9.n�w\\�o��\'�C\nk�[����?uqL���`�`�c]_�7�\\(�\0�&��v��rν�������l��\0�@��tQE�Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@�t�+�uh�+���׹7�o�x�;��.A�P5�_�q֠�Ҽ���zU��� �sSi��f��=�4ȇN*��x���H����{t���0�VZ�6�\n�P2�H���Wq���Iml�\Z�:sY7���<w\r2ݣ6w֚\\�V�B(,~�5V�܄ֱ>\"�>��=FE���[�\'�;N=�$�����:��\'�<K{�HIY�.���8Q�RY��ϸ���ζ]�(�Þ+j�3��+#�+Q��vv���}}{�Go�>����V���TQ�>�z�T��>w̄�BSWb�3���#�\0}�Cm�,b��ˏ�j�v��B\0u\0���@�#�7w8�\'��\0\ZF��p:��ҽ��)s�|��j͝�����\"1�A��b�\r��U\0ć��jس���pX��Ҙ%���m���8�W��cW���9��(���r>��8���ǔy�/�>�+Ni�,�S!��d�UI��LF\n)�9cHi>�	��U�ʣ��\0�4��d�6�L1��N��\"g\r\"�$>��֜Zm�]H�{/oƁhd(9�͑S�Կo��R�>���5�-�8BA����	�<c�1h�3X29{I1�v���1/�;��zA*�*�q�ɋ����\n��Ԕ�#�k�\r������Ͷ��n���~�]帷����L�?Wn4yl��24~�<\ZH�!1�G���\Z�3��3fkg�H?Q6٘G:�V�͂.-X,���?Z����]�\'��P#=��T�m��\\RN�\'��	��A�¬|ҫZM�ʧ�8��UʙC �s��\0=h�q�������Z�aʑ�>��bE���r|�=jϚ/�\'��X��4�͖ r��O�Wt��iH	~?���oP������#�f�gq#���Gz׻Q�n�V=ԁ6��y��ff�`�(q�;���|Po�1&�;�����>S}���~�װ�+��\'J�~�M��kn��W��0�\08�	���j�g5�����1\"�n�/�ֶ.���|����1G?{&wcRY��\"�� *-G\n�p1R�#�@R)���㩛��pW߻���\03�����jm�ݓ\\7��9;��zS%���r�k��,���l�\0�#^w0/ t5���&\'�y\ZC]O�袊d�Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@�q����x#�5�\0a�}�\0q��󧉐\r~��$�h\Z�lӤ�Xc�ǚ��%�&\n�\0f���v�YGADX�?8�f�rʋ	V�Z؊�����\n���7@y�-���$w��g>��j;��ܶ1�A�ίvr?����f�\0�{0�<zW��o��i�6�	�%�i��*�6���f\r�J�ϋ���񽽈�4vV�?��r@���t��q���Tc��Һ;~L{r}?���(��D`z�\0�]]�|��y���~u��^�M�	^s���?Rdؿx���=M:�R�y_�V5}#X�,1��O�4�Y��4/.q�m_v�&�Ĺ\\6�=��\0�T��<��;�m���({����^{�\r���-���!��w�r�n���\'�NX�idEg4�\0�-��WtD��0��G����64��X� \" ���.C=��(̑tR:�����u����߼�z���cA#�*�����j�]���@�ns՚�E�Hؿ�#���W�Qª]B��\ra]^ɪ0Uc�?�\Z �E��R8TCj�����tv��?4�8���p+B�K�qԞտ�����z�F�wQ����O0<Ẍ��t\Z��#�o6�\"�F�e*Kȡ��Ց}�H��H?��i٢y�-�Q��lc`3�\'�kRҥIwE�6��x5<֗V,^��;���S�k�1X�c���Ct4�f�Fm��v�Zݕd��h�k�>n��G�-ݔ3\r�?��\0\n���E2����z����\ZL�r�g��}*mJ�yk}j@e����K�������KN�<Oo1���?����q��j���4|�jo*��/�O�����-��ͳp�s�@j �]ٶF2@?�c>�.0Fɗr�\ZΑ<��n�؊�vg��L|�>ҫI	3H�Ny����(���Њ�4_\'z~��I2�	� �mbxo����F�<�󌃞��v�Փ�#WC~�L#�����i��<w:\n���F�Y}�G�\Zf�a~ą���F#�AZ\'�K\'m���uț󦉖��a���8e?\Z��)`\0�*������^H����{֝�� +C���vي��F�fS�So�)36�:��h�猯Z���`˪]�3����F��|�����S�U��[rW��Rh\0`��⥴\'P;�y�v�\'��\0�\\m��l��uve�<Oq�\0\\�\0�s��(QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0��OҾ}�mK���OO0��M�OҼ7�m���rz�悖�H,bh�\r���r�G�mQLmEW;[�>�t��߂(�m]�|�Һ+K9�6`N���b{�\\�����k�����Z��pJqU云[5���۳���<���\0�z�k�]���ڭǭ�G�\0|��Z�aGjXp@ɯ�uI|�j��t��G�ؚ�\Z��消�e��#��s��c�\"�l�|���J�<1��|��h�C]�����f.��+3����K08�76G�\Z���8 �)���\0۾2�`n�:�I�9�ךE����19*�ڬ��6��l�\'�Z����H9���������O?�\n�ۚ\0u��HN[��\0&��-e����w�W�_�Uk2%����\0���i-�׏y/\n\"xǭ�44K$�43	�?��˽n��yP;F\0�s7�ݿ�,�yy��j$r1��@�wb�R�ig��/���+X��G��\n�Lv�#�*��:}N8��T\"����n��ވ��1�#Qɪ��Q�.���+2��[�|�S�Ogd.��O�~�(%��\0m�[%\\�j�u���dܹ����v$2����2G�FS��c�~V����WS�5��h�+��v��\0ר�5�o�8V�Ǯ۸À%�S��qq�Ŷ��>�f ������6i=�u!���ÿ���j0Y�+2��a�s����Ƈ?�0�,��î�q@���{I���ZH�uBz�/�엩p��~T��i*-Ż���ڙ+�3p9b3�\"�_���uos�;��j�����S���@�\0��0}��\0?�W���LI:�D��Z\0cD����,�p��Z�a��\'�-�jҼa�����I��T儵��g�z�ށ����,��|Ī�bc� \\��+E�)�{lq�Y�+夊̇r�{P-�.������\0�z����Mu�c��y��+���E�S ���p[�VJ\r�7�h޾-�UH�R513菃�\"O�!�-ʿA������艂�^��.��kȉ�c� �]��*����+��%R��Y���L���Ǹ�!��V�]�Y^�~jbg7�^��M �\0v�kε\rFYcR��\'5�����s4 p�9�:��b���C#�%$�ץ�m�$�?��א,��\n�߀����?�M\"�>���)�QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0!WϾ9�[\\n�⾃���fF��T�0>�y����5-�ܑ����쯆\ZX��6}h��}��\r���3r��1\'�y��w��3^��$K�\"c�y���o��[�LN�=�Y����0Ȣ�cbۺ��\0�{WM3×�����T��q��M|�(\"�	�	5��=��G�y�!>ہ?ʼ3Q�0���#���Q\'��Ih٫�Y|�A׏�,����\0]w�ي��,{~�י�s��,_���#�<W���)�� Pq�\Z�Th��@}������m.z��})֮S�9�F}*B�!��w�m��zC&]��\09�<��H�m.g<�1PO~��{��#b��M9x�m�~f�޾��,I��`�S��O͎��\0֬�f#���%�G�Go\\�S9�D6�}}j֙��K���\0�@-��L���8�G�\0]A���\\n�,~Lx댱��۵<*oT;FO���0n�6#�UN2}h\r�w��%�<�[��V�����*]�7�\0\\�ϩ��X%U�#ʟm�[f�/�(���\0�KB�5з��\\��O����kZ؅}�\r�잕U5�b@�[wv,?¢��C#W|u �\0����g#^T��F��*�[��T\0:�b�ˮNY���P�\\�@��W#?{�\0�O�0T��ȭd��,�	\rְ��x����A��@�z����V�Ji�U�ú;F=�*WEr��쮬�i1*�yX�?Z����LUq0���W-�}E�K	�b:g��VM���r��u��~e��K� ��[��3#l���)�o�MFKf8�A��S�A��[�T�p��ze�ۻ�dNH�#��	-���	\"<$� Z��B�\\ے6��3�֣��M7(>e?6�N����dI�ހ!?��e���F~�?�G�2�́�\r�����������x�x�\0B8\0�M��c@��/-�H\'+�?�z˸�3�n�*�j��e�u$�����X󑒌T��l�&`�3,���\n=OJ�b��^a�z֖�({���$���R?,8�&�=L���\" �O�v�;�X�6�B:]�J�;��z��$χu��{�\0�k]��?�I�����\r��_i�^q�\0~ֻ똉�5kc�9���I�:f���%�=r���t��:֕�Ae��9�K<��.�̫�\ry��\"�^��-:9cTUÙ>b;��MV���\0\n��(����f�5�,\0���.0�\ryU�e��sҽ��=��C�U �p(Ш��QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0�x�CMQ!��ewW[U����HI�#�#���/|;3��#bC`+��t��2\\K/�+��u{��[V�6�=Eo階��hV-^�!yc�E<j�H]���I��%8�텭��\n�4��?� !X�=��O�N�t��/�������F[Q�P/���r�,W�[;zV��yv\\HὫ*����m���G7�_��>HT��q�k�o�Z[����1�란W�|@�7��t�e?��\0*�U�@�]�Y�s����2�Ӑ	V�u>��n�ĺe�)�\"V��^i���ʫ�I�#3x~);�Q��T�jB��� q�U�2��m�9>��6�P�+\ZgӃ�R+���b�fb3�ҁ�%�����.r}��\0���}j!ԅ��P�n�Eb�ʗʴf�Yj��F�s���R/n��L�Ӵٍ�Kj�v5%�67�z�����CO��[ȅū~���(K���edRF$7��o^�4�r<g�BA�s2���І��:�TSP���Yr7�Q��\n�;�;�-����|����5n�B��1�3ߎk���k(�g��\'p�\0h�w\Z.��Ī���J�і�k�$qi1Fp�CU��f�\ZeGl`V�����<zR�\"(+��4�U��y[c8Y����O t��sh�@1�֌��)�S�I#V\n;U(��ʴ�R�� L�P@*\rBKX�s(J���5���-��޸]cU{�0B��R9��\0^���T�\'�,	#��T�3�}�:V��\"�X��N�:���jK��B�D�gq��5Z�RmF�a������Sk�3�oL�>�;n�a��j�����9@ă���X��,l�>f#��(��o�.}��Jbf\\0������H����?�g��5�b�4ˉ�&q��\0�Q����@����Q�ED�W8㚅w1P$���O�.͏f��DUPNwF�}(�3��!�1>�\Zɼ�ʶ�v8�֤�^���2AX>)��i2��$k��;��,��Yn_�Z6@�/&��=�R1�TVU˵��|��LB�FJn�,kj���I!l�B���&ȡ�1��H��8���(�;��LiG���ĺ�����ːs��^�,��0s^M�@:>�tG�\"��7�^��0������ ���P��zֶ�8��$��sח�3	�z���Es(�D��h%�񆤖�����կ�\Z{���Em���j�q}����ˆ�=����Hf��V�����4�s9�3�O<-}��Ŭ�#la���x��.�0�<�0*������U��ҭm�7G�r�(�AEPEPEPEPEPEPEPEPEPEPEPEPEPF��#T��Eݢ���$f�|1��s�q����k^�\0��>���[�d\rX�%�ݾD`{(5�k��\0`�w�&<�����&�9$^���}�N��}k�-/L��~� \ZE-�{�x��?}e+|�B�^��o4׶�%3�z��#D0xn\rx���g⋻}�G���;�*foGq���{Gf�1�Q]V��\"��H$v��\0�Vf���.�l��\\�V�A��ɋ9�sPl�m?ﮮ6*�\0v��\0��Ǎ>��<�8���UY\"/ko\0�&�Bs�+M�H5h\Z�0�7A�(�7X��͝�}����b�&n6��j��5���dYpC7V)����yW��H?�F��T��Y�<����P��U�.��9�\0��f�����7%�?#��+��|Y�^ZKkm�K!~��+�Զ~2��H��k���\0̙9Ǩ�f��5?��/m�(z�޲���/)�˷��o�6_\\��b����a׷ӭ?N�W�m�$��Y�2;+{�2-��.�1v0�ִmw�\\���|�nv�#WSu��\0ii���c�����^9��솝��ɗ��PZ�fz��lЇ,zt�J��3�����\0.�#��>�u�]&�d���?u��)Y��N填,k���;�8\"����������\0J��A/�4`�Jج���.��I6)�ڝ�pz�o�9�i��N��n��T�M��wV��d�����m�cU��5�:�ך�ыOB��h)]�6*\\_M�k�Vl3cw��t7�d�mnC�S�����V\Zϥ�J�n5&��SN��{�Y�$�wOO�2l�,Z�f=]c��ҷEQ��X`���#�,{A�=k����<��Ȃ#<]��ֺ{?h�i�MF���q��\0פRj�f����ȋ;�����G{\0�Ђ7���UN���T�Y|�/���\rrx\ZȪ0\'#�\0��׈cӠ$t*A�MEsƥ	����j��o3�K#q���nOwK�i`��M�2 �o�<~����eΉm&NY�?���Q��������g�k��f%��d|р*b{%��cؽO�XѭZB>c�ҪGK*�9=u�m���y\'�2{P%��@��8+����������X���q��[\Z;�l�|\r�Q~��\rFSus�v���\r�z���G�#���.��\0�-w��Ao~��|\"�H���f��F?���+��\n�\nF����r�Bi��U�h.�ȳ�e��	*֨� P�.y$V��2\'����i��6�\0ᦏ�F\r��FLrbp����?+)YuW��/�\0^�Eއ�m@������>�?@+���\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0(��\0��x���<Is�\"�������Id`:�L�nG5]��Ҽ��>��ˆ�-¤���O�^�g����\0�\\��(��uX�\r�����_�a�:Mhii���w�b�<��溛[p�L��������=����2�=N�ֻ��H��)���aԜf�:�\n)ӡ�X$�����k�M�66��,��}�Q\\�\0�uˍF�,�C��en�KE���O?�M#9N�ȹ�7��:�������\0\Z�K���*��D���Oj�M�I�#�H�X.z��V��_X�F2=��4��%6���\0�OZ�`�F�e_����k��	��6ќ$y���U�V+�-�q�ygzqHfF$�N:t��\0\Zι�d��]۟�y ta����\0��n�68_hª�1��T���#����ph��5��+����,0&N��Z7�-��GulJN��\ny����ʻ����k�y�ӯT�_�kb�� eS ����}�\"���wQ,~M���~��u)x����Q�Wi��!3\"ʸ�}\rwZV����*�:�),}�:�-;t�]Ib�����\0��y�W7�\"�9/cZ���K1p�m�n�pO�Z�+�bk�y%���~H�d��;\n�w�|B�0��E��8�rK�j�آ�4R����ߠ�ﴗ��f7>Q=��Ҩ�\\�E��Df[�Yfm�	�������f\\\\Icxd Ky\' >r�ս)w�Z�f{�P��\n�e�i\ZY�s���#]\r��\Zd���;�[Y�\n�F��jy`��&f�>����Iʱ��A �Kz���{FQH*_��*nZEh�q�۰���W?�J���]KM�JXj��ă���i ���]�|ĦQ�VVٴ�8�=��(���Z����R����_�m]Ԡ��\0���P���3+�@7,�psW��n��[���R�}��a�w;[��.�)���8��)�<��p��)u�tCQ����\"�,.��?��h�&`\Zb[�?���sz]��\\F���.?\Z�u�[]֑��U�;k_��v��\r}\Z\"� ��f�Z���2]������B�V��sq��N_�1�X3�����\\��j�<Q�a����}��u�+2���ʫ*�Jg���DI��\0���x/L�d1��+ȧ�v?��K��_Zt�E\n��*��337SV`S��,W ����xi\0�bn�k����Mz7����m���\n[\Z�QE2�(��(��(��(��(��(��(��(��(��(��(��(��(��(��(��(��(������\'R�z5yψ0���Ɠ*\"XȡU�	�#��-��[��������Gc����O�E6W8�Y>9��Ef�832FO�<�ZWzs�;w[sr����#��]�xr�S�yM��8�= y[\0B��w�+��t�E#�!XYw2����y�ƍm��s��9�i4�ͧ�C(\"L���t�!����(\0�T�j�çˎ)\'�i��e1;Cj��b�Rs�f��GE���e���������W;�Yo�p����֤)s��v�����>顄|��B_逝��;�u<Tf�>�5�����	��n��I�i\n�T!��b�\'�Ž�\\�J	^٠f��%�Hx��gd���?/�X� ���E�/�ϼ�s��߭o$�[;�%�O9wj}�-���\0�ɷS��Im�nc4\r��#�{�]��Q^X-�J��̑��W\r��yf�}�ݸc�S��8��iYj�o��@��<��f\\��ʋF��ĥc-�7�Gu��]���!]3!F�����ι}>0��H�ж}��\0����aA�76>����Y�MU��p�9\0��<�ٝ����X�M��Mt~12XMq	��sX,$��F�99?��Mޥ{�y.��\r���N	\'�;W\"+k{9N��CLދ�(�������l5Fߗ�L���m���S�T1$�ʡo~��M\nZ�kS\Z+2��ե�d��\0�Z�=�]�4�H�8�<�q#۽Z��Ke��ēI�e�_$(�M��hW���*ׂ�\'��X�|���ƪ������Yw�����*�\r�!ʩ��A�;�&����8�XX^Ժo�-a�%���b��g#8��B���1Xc�u~�	�����7�u�(kQD��쏗��`=+���Y#m� t\Z����,0gʅ��5]#V�N�~4Ź/�u	��K[�� �g�_��]ny7!X#C�շ��6�F����z՝N�lĖi���8�%�2-58b�E�Q�\0�+����?��Z�A,�Y>�:���-��=T���rY9�O���fL�����LW)I&Vi�ca}���\0�z�����k�V���M��\0�g�KP��ʿ(�װ|�|�\'Q�$F�QѕGQ��?\n��9�J��yy>��s	��+~�1R+&�B;\Z�$s�͍���F�n�n?ٮ2�μb�]�A�\0��)=\r\n(��!EPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEP^q�)}~���G�F��7�.�ME���h����OL�s���R	��-�PF6���>�ݔ䉔� ұJV<^,@��@����ƽO����q���#?��$��ͦ+?,�)�������+�������j�ƀ�q��*k��s��\n3]��5Uԃg$�c\\DC�u�r��zU-���4�s7*��\0�WI��k��e0̛�\'���i8��>vJ�楊C������d\r��=�1�2��C�Q���y�5�\\��i��������Hc���b�9�CP����I�n#��d(�(�v&.��v#��S�0�l��m��������W7�Lr-���Eu\Z��48X�S�a�\0އ5�:Z^�0@�\r��A���63��\"S�[����7�g�u6�m�@W��1�]rG��V,���Xtƌ��΍&z���2i�6v�J,4�_�}��5�:����/��i\0u�����&O��b��C[:D�af@;�nO�}K�8�}B��u�2/̧�bi�\'�D �	R��R�2][1u]�q�O�����K�o-���L��V֢k�F8C�f�\'���ѣ�;�M-�_-ӡF=�S�8�_;���9�j��崽\n���@�~���%�N�����.���-`�.`͸�C+�x��´�K�i\Z?��c�T�<�?b�$c�\0�a.���Ş�-����\ZԵ�g���Wm�]�X\'��\0f���Z&���3�ı���O\n?ϭ:tanU�v�\0�:3��X�m\"��1;������,�z�c���<灜*C3�M���tuS]e��=�sjH�\"H�I��+Y�G\n��pq�d�o����Ö\Zd\\I>֔�@(\Z�S��&������,\n��+��֍�[kH���\nα����9��2z����� ��!����P��Wےy�\0�]����Kb��b?\n����zRT�Rjl_6�8�D$�i��Yr�����~ �ʌ�#�=k�ֈ]W��� �Ok`o��2��SD�8�rH�_Tx3G�\0�w��m��!/?�ܷ�Mx��/˭k�Mq�5�	��aʧ�?�z�������޶)�ۘ�Y�.|��CW�dP	��\Z���(݂��C9����5��F\Z,o��h�\ZDژ�ƅQA!EPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEP\\^�pc�n\Z�+εY���Npq�\n��bm^����[۫}�4����y��%^ܴ֞dKtZ�����{���&��C}q$���1Ƅ�{��]�������,EI$u v�������>�\"%=v�ps]�����6����v����Z�ځ����\0\n��i���K����ɬ΄M⏵:3\\2��@�5�[]왔�:�+-��e�p1^_���ޒx\0գ�s��O6�h�����\Z��Wֱ�AVS�s��z�Q&�4m��.�tu��^.���\nVΓ�Z����0**gޫ_�%���Tِ?�Y�wv�T� y\"�\"�Kv�Y_�,�H\\�!�q��}((�q�&�ꄒ�,*��_Jɟ]��Q\n��ۢ�y��=�*Fy�L���~��ַ4�l嶘�:�+��ð���\"���k��0$ ��\0J����k��.2��Wc��Ⳮ-�� s��A�ա�ȶZb���\'�X��(\Z��:zɪ���˖�/�Һ�o$!P�#n\'=9��X���G\"8�@g=���ts5�w���7��\\�h/Q���Wc��;q\\l5��2d�~H���]�/�7�π��=����:����S�����4���v\Z��e�O�2��*h�n4�֤�	#[e�8ES��q�;Nd��k)��|�Ҳ�H�%���˔����R�X�o�fG7��Z�T��ϼ����e�j���>��=��O]�02\Z�>��\\~\"�ጺ��p�@.�q�d�iZk B>�L�>b��H�J�L�1��( v渷��YT����kOM�����v ͜z~�GG�<�\'�^?ƭjW�]ȉ;�А���Ⲭd�P�U���6\0�Rk��q��I*��F�He���T�����\n���̨��uQ�D9�\\�Rh1K��vO\0�)�}OP�� ��r��*���5�|�J^~\\�����K���US+��\\}��K�l�v���I�z5|��1��D?�sQ��5��j�:�=�&�p�\"f�ŕ	�N*��^���GJ�Cg��0�tG�1Z�h��0/�M�6_v\'������,���>��/s�evb���z��\Z���?��ƛ+���ہ����=>���Y��@�f��\0��j�d�a��^wN})��v	^���г��o���{�ǜWe��\0�\"����hQEQE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0!!T��W��M޿y.2���zM�+9r0>��(mZ�F142��$�5���$�B�Rr%������Y�W0��D�n�,9�k�~(i�4;]Id�-�����1eo^Bs�@os�f�gL]�mY�P@?�����?�(p2��?J��!%��Ǔ���]\r����ZEc��RQx@�m�ėS��\'�q�\'��ɐ�!��U�Z�3��wc�s��a}`��В,��!?�	�%��;�!�O�I�v�ϒ������c�i��d����\rWȹqEF9��[\r<#r��_���3�Ml|�4������K\Z[q��N�\\/͸�5�����ĭ�����O��\0\n�@���{da�f�d5x�@�6�ai	��ס�G�泲�V�V��bc���|;�XJ+tܽ��+OZ���H��o,Jqǹ�G5�5t��<�Y��KGd�-�� u�pWR����a���{�]��\n�Ek#�Ě�oc�u��J¥��J�C:=4��xZ�F��	bTg8�Y�x�~�R;H��	Xջ�j�� ���䷆5,7g�Ͼk.�G�����Uʹr*�g���ֵ[��~5�n�f���ʫ�,ZqqՒP���k�+{o>C�@T��GjtW1ꖷs\"��� �B�)�����O�[���ٍt�nX\r�J�\0G8��Q�_�<~\\�µ-�cc�\00��$R���\r63=��e�Q��Q\\�^��˘��F��ҽ�J{�f������w�Z�\r��f0.Xg*0is\rS����R:��j�\0>iuo���������X���N��^��ԃ!�IQE�8������KIb�ٚ)�c�p��V�Χy}o\n^��#���5�[�դ#7l9�8�_�$��ͽ_8N��=i��-�ɺ�.RX���yl}��o\r����;�8db��A����Ät�O����6���p9�&�Q��Bᤊ6o������bq�B݁���z�O3�px�@�E`�JD�(?�@ّ�F�C��\n����pfHf�6�,v�+N唍�6��R��4�pN��S&ڊ48]ē��p5z�ȼ�cZ�(� x�z�Y�Rd2�3�rs^����\0�/�4- �����3�`{\nq�3�GA!�q��!J�9��GU�aQHwųon\rY��jM�����ͻC�9��\\N����&��\\y�8��P��7��QE�\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n*���e��e����<�¹��V�25������~���\r}kRSs�x�DC.{f�bY��f5�m�˗�o������1AH��\r/�{×:q`E2{09��kI��o��h�#p����\0\n��ذ�j��\0�Z	.���7#��[�TI\Zӕ��K����w��j�$��/�>���W-���S�<�]N��i#�H�ċ��z��ح������f�L~�\":���:N�n��������U\\��z��\0�Ƿ�nȦ͢��é�E^�G)y�Ait�`��r���RY���r��������<��6`� ;�h v�i��z�,~32ĆH�H��\0U�?��񤓆�x?0��z�&x#��%0JF1��V���%���D�YD�I�$V�c�@֮���]Γ�F�&��<0�������4g挆�3\\U��6�k7L\ZH�	�[o{�xz�,����)X�+�qw�o<E\Z�$�k��=�Ū�PD�h����Q���#S����dk��YO/�3�~�ks	;#м=�iWI.m��q;��8ʎ���\nɾЦYfY$���yWH�G�szU��\Z�&՛n�s]_ۯ�ԣ���,�	�G�H����V���ص���I� ����]U��^��V�e�C�=�WK�xZ��~1�I��`�,��ơ���(�k���uOxz�T�W�X-�qQ.9��#�ӳ���z����_G�tR��R��:ի.B�\0���*��%���,�e0�O�O]�<��gI\'��0�?��3��a6�Lm�n?��V���\nV�\0b��k����*�\0����J���hv�f[���:�Ԛ&��mĳ^�Ā|�S�k��\\Hng���6�^k�6� 6�ڌ��v�^O�t��|1��� \';��E9ɛz������F=3�W5}���Y��<��*X��������1�s��meo�Y<�!�wf?ҙ���-biX�c��g�\\��)<�݁­mZ��k�����v�8T.����=�?Ɓ�e�3\"���=��-��,����WKy9�ehђ�\0`H����?ﷹ����0h����튫`�K�=	�qVu	�L�(%�n���~0�0iа3�$�\0u}�\0�Rԇ��Ϋ��\0������Ғ��ʶ\'�l|��?:�I��ҡ�����>>5���_�ެ43�d��Z%c�N��i��IU�5��P���E��v�/m�A<����4�Q�#�m>��R��q�QEi��\"����>���\r\'�v�0���#�^��Jߠ���(\0��(\0��(\0��(\0��(\0��(\0��(\0��(\0��(\0��(\0��(\0��(\0��(\0��(\0��(���I=��<a�Y�Z[]W�;Z���V���F��k���-|��Jw19=(���_�ծ\ZK��F�]�[����U���A�Q�߇:δ��X͕�%�~c�Z�o�7J�� [�e�������@���&��Q00�pY��Y�FE!�.Aꑌ���\0�]e�m��gl\n�û��Ou�Y���g=�4)W\\u��@��u���=d�M���o�Y�F�I�U�=��5��*�A���OK��,d���I�q��?Q�k�u������K�W���2[?��_o�:�Q�uS���(I<p^+��M��O@\r;G�};R6wL�6?�rx���/�-�!ڍ��~�f���\r­��_�g6C�j\rluڦ��Q�+c��J�gѥM��\0<��\Zۆ�P��6�y��O5�j�]9���A��P}���gXn\"�s�#�5��0Ӯ⻴�z_�8��8c�_.�%p*¹�g�r[�D塗�^{�)�	����;�hm�,�����[z�mb�*6#��s����Ĳ�������ք�j��툟�P+��u�����5܁��}<�\rܲ�G�8�f�{�P?Z���i������\0�Xm(��ax��3������Lե��߽b-2{����P�2�>���|�ܕ]OP�;�~��{C�a�m:��w�%�x���׃[,�R�t�u=B�{q%+���.�..)j7�:���G�\Z��2�K�� ̑��-���x�\0f�<�x�ki?�<J�qL�\Z�#�3��;V%��!Λ���mp>��0���5����]��I\'���xe��^�M5rZ�ZA�맇[6�߻�����u��&�f�\r�U���R�GG���v��M��\0\08���xv^�pH�~��kby�>�,%�|���{�Xp����� �$�+���=Q��x����\\嶧l4���l� �}��X��uo\Z+ۥ���7�«�x��e�r@^M`^�oo��q���\n���槲�eK湹Cn�2�z���S�\Z�̪���\'\0�\0WEo��sl�^L���W!���b�����{{h��!T �1H�v$���������#>�CZT��X ?���F�]L,m���;������!��6\r;�\0��ҁ�^�տ��/-��W+w�B����=�+��E�\"@Ω<��k/D�o�g�}�O���qu����8�j)5�^��1֚�6x�ӛ���A�5�f��X�K�O����X�$�4�B������$��{�{�ӭR��9�0��)��f����l��I#�5=�6�+]�$t<��\Z]凝\'����S��]5�ǉ��nIB\r^��\rCI�-�<�Y��I��{\Z��\0���V.^����w�>��x״mG�W�X�P�`�_�Ph�K�o�\"��p�J���J�����׺,�|��ېS\'�Mze\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0QE\0x�<\'�������oelϐ���v��W�xd$�\'ۯG>lÅ>�ڻ�(w\0\0\0vQE\n(��\n(��\n(��9ms����Hg���]�mnv1>���^M��WR��R�U���[���ѵ�d�=	�k�*��K�_���?R����,v3.�dlnR?*d�iNvz�yN��)�vL��l�����a�����RLq\"�����Z�Σw���@.�{��+�0�I=�k�Z鶹R�U-�H���Jͦ���Ob��ɭ ��)�� `�����������{��\0�b��.��ZuO%�v�xƲ��z��O��\Z���%b{=JB\0�dc�&�dCpX����+�ѵ�B��1��h�mNp������Ɓt8o\Z�I.��_���clv��\0�\\ឤZ�ilWQ��壁�+4y��c�y&�a%��\0�*���N�;/|a�o��#���.��D�M�F0��~\"k�>Kk-D��Ry�8�\0���	�둎�Z6`p@��[���^Z�@^�ɦ�1���Ԭ��rP6F7g���w⯋|5f�vz��e��΢@��g��b����A�mF�>���,$��N�؆��L����\Zꖺ=��\"�IVIBB����^�V���\Z��\r�$����Y��?�-\Z��N�(T�_�j�t�U�A#����\'�wgLa�L{��S��7?��Cˁo%2G�A�]���\"�^�M��`�s�+����r�F��ӓB��V���cK(�o!�>o�֞���:��]���OA]���V����]�\'��Л��g�(Ԩ�p9jM\"ԝ�CM𽽝�y�HGV��V�����5��\0�|όP�NY�yv����1j>(Ԇ����h����z��B���-5c�b��?��dn��e�kv�j#R���V8�瑏n;��O�S,~�4���pA�%��Dz\0?�5���3�������jsmo{#C�	$���U�̝X��7���Z���y�4�2?�~\\\n�m?M��m���;xp��Z��+��ESQE\0QE\0QE\0V/�|1���L{-B�$�|�}A��(���g�>9{i�i��Ъ\\ �}F}\r{P\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n(��\n�x3Q�ķ\Z��j�Y�(yR\"7G(�q�p?k���ʌ�]��Z��u��~T�4��H�@�H�4��gSm���v\r�}]$qʻdEu=�dV���k�7�tbLm��8�<�����ļikq.3!;Pz]���p�9�?O�\0Ut��9�g�,/�䁛r��\"X{dZ2xKWt ]���<�_�\0���w4U!�y�d�\0g�JYnY�;|��z����tQ�@9��w0|?���Ũ���8�M��}������jm}-�[��C�R�PsE�w�%\\�-2�yRK+?t(=+N�4�AE\\\0i��WV�noٲ6K�#��50����;��}������3Q}ql����~n�J��	�*�i��� v�S�k+J�������v#�|��1\'Ԝ⻽;H�m�!�]�X)�(\0�\0�T;����0o&7Z��B�T\r��������>/獱��c�~�Q��l-��y���S�~���M��d�=��L�{IE��F�S��Y����&�^p��ve�$t��u�jZޞm���C�?�G��&�n�[��;�f�[?�*�Y��D�|�h���sD�;�\0G�_ֵt���g\'�Fy�~r�^�g��V�mt�h�cn;h!$�q�ԪG �v���|C�#��c$v����R�rA��s^��\rYx[G���r�zi�ޕ����*�H�sr\n(��EPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPEPR���s4I��?Θ�h���aЈ�\"�Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@Q@��',NULL),(2073,'Esquera, Raymunda E.','Mandurriao','1949-01-23',76,'M','Widowed','09053359540','','','','2025-10-20 02:31:57',NULL,NULL),(2074,'Aligonero, Larryboy D.','La Paz','1981-04-13',44,'M','Married','09511771244','','','','2025-10-20 02:42:27',NULL,NULL),(2075,'Sondia, Dwight Derek S.','Bacolod ','1999-11-30',25,'M','Single','09565314879','','','','2025-10-20 03:12:38',NULL,''),(2076,'Torres, Felix O.','Jaro Iloilo City','1935-11-20',89,'M','Married','09276477560','','','','2025-10-20 03:25:09',NULL,''),(2077,'Garbino, Salvacion C.','Hechanova, Jaro Iloilo','1952-02-22',73,'F','Married','09222079350','','','','2025-10-20 03:27:01',NULL,''),(2078,'Espada, GenelynT.','San. Miguel, Iloilo','1984-08-14',41,'F','Married','09638925771','','','','2025-10-20 04:00:28',NULL,''),(2079,'Lupera, Joenery C.','Balabago, Jaro Iloilo City','1982-03-03',43,'M','Married','09182101120','','','','2025-10-21 00:47:13',NULL,'Supercare'),(2080,'Luceno, Leona M.','Leganes, Iloilo','1986-09-10',39,'F','Married','09177177239','','','','2025-10-21 01:53:58',NULL,''),(2081,'Cuben, Renato  P.','San. Miguel, Iloilo','1972-05-17',53,'M','Married','09212824545','','','','2025-10-21 03:00:13',NULL,''),(2082,'Jardeloza, Ma. Wella Lou  A.','Landheights Ville, Jaro','1990-06-28',35,'F','Single','09209727009','','','','2025-10-21 03:49:17',NULL,'Intellicare'),(2083,'De La Fuente, Pearly  G.','Carles, Iloilo','1978-07-18',47,'F','Married','09517043785','','','','2025-10-21 04:03:50',NULL,''),(2084,'Aris, Mayrel  P.','Lj Ledesma, Jaro','2002-05-10',23,'F','Single','09660538306','','','','2025-10-21 04:05:36',NULL,''),(2085,'Narte, Krisxan  B.','La Paz','2001-09-16',24,'F','Single','09055197085','','','','2025-10-21 06:24:03',NULL,''),(2086,'Legaspi, Rubina  P.','Concepcion, Iloilo','1971-12-08',53,'F','Married','09218837702','','','','2025-10-23 01:00:46',NULL,''),(2087,'Epistola, Jose Kenneth  R.','Kabankalan, Neg Occ.','1996-11-03',28,'M','Single','09618242810','','','','2025-10-23 01:07:36',NULL,'Supercare'),(2088,'Sison, Alona  M.','San. Miguel, Iloilo','1977-10-16',48,'F','Married','09984069524','','','','2025-10-23 01:22:06',NULL,''),(2089,'Esgrina, Beljohn  G.','Igbaras, Iloilo','1996-12-25',28,'M','Single','09273962860','','','','2025-10-23 02:35:59',NULL,''),(2090,'Stuertz, Rosalla  C.','Mandurriao','1967-06-11',58,'F','Widowed','09171244680','','','','2025-10-23 03:05:23',NULL,''),(2091,'Aguihap, Kyross Daniel  G.','Jaro Iloilo City','2018-11-11',6,'M','Single','09952319571','','','','2025-10-23 03:56:34',NULL,''),(2092,'Consumo, Joviewin  C.','Baldoza, La Paz','1995-11-21',29,'F','Single','09108504468','','','','2025-10-23 04:46:31',NULL,''),(2093,'Diosana, Honey Pearl  A.','Lapuz, Iloilo','1992-04-24',33,'F','Single','09514120091','','','','2025-10-24 02:32:46',NULL,''),(2094,'Casuyo, Erlinda  S.','Duenas, Iloilo','1957-09-09',68,'F','Single','09104357859','','','','2025-10-24 02:35:20',NULL,'');
/*!40000 ALTER TABLE `patients` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_patients_update` BEFORE UPDATE ON `patients` FOR EACH ROW BEGIN
    -- Only update age if birth_date is changed
    IF NEW.birth_date <> OLD.birth_date THEN
        IF NEW.birth_date IS NOT NULL THEN
            SET NEW.age = TIMESTAMPDIFF(YEAR, NEW.birth_date, CURDATE());
        ELSE
            SET NEW.age = NULL;
        END IF;
    END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `before_patient_delete` BEFORE DELETE ON `patients` FOR EACH ROW BEGIN
    INSERT INTO patients_backup 
    (
        patient_id,
        full_name,
        address,
        birth_date,
        age,
        sex,
        civil_status,
        patient_contact_number,
        emergency_name,
        emergency_contact_number,
        emergency_relationship,
        photo
    )
    VALUES
    (
        OLD.patient_id,
        OLD.full_name,
        OLD.address,
        OLD.birth_date,
        OLD.age,
        OLD.sex,
        OLD.civil_status,
        OLD.patient_contact_number,
        OLD.emergency_name,
        OLD.emergency_contact_number,
        OLD.emergency_relationship,
        OLD.photo
    );
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `patients_backup`
--

DROP TABLE IF EXISTS `patients_backup`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patients_backup` (
  `backup_id` int NOT NULL AUTO_INCREMENT,
  `patient_id` int DEFAULT NULL,
  `full_name` varchar(255) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  `birth_date` date DEFAULT NULL,
  `age` int DEFAULT NULL,
  `sex` varchar(50) DEFAULT NULL,
  `civil_status` varchar(50) DEFAULT NULL,
  `patient_contact_number` varchar(20) DEFAULT NULL,
  `emergency_name` varchar(255) DEFAULT NULL,
  `emergency_contact_number` varchar(20) DEFAULT NULL,
  `emergency_relationship` varchar(50) DEFAULT NULL,
  `photo` longblob,
  `deleted_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`backup_id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patients_backup`
--

LOCK TABLES `patients_backup` WRITE;
/*!40000 ALTER TABLE `patients_backup` DISABLE KEYS */;
INSERT INTO `patients_backup` VALUES (4,2031,'Apura, Ariella A.','Anilao','2022-07-09',3,'F','Single','09308879061','  . ','','',NULL,'2025-11-14 14:23:31'),(5,2010,'Piansay, Edgar Jr. V.','Lopez, Jaena, Norte','1972-03-26',53,'M','Single','09177962152','  . ','','',NULL,'2025-11-14 14:23:33'),(7,2095,'Asdas, Asdasd  Jr. ','Asdasd','2002-03-02',23,'M','Single','09511365191',',   ','','',NULL,'2025-11-14 14:54:57');
/*!40000 ALTER TABLE `patients_backup` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prescription`
--

DROP TABLE IF EXISTS `prescription`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prescription` (
  `prescription_id` int NOT NULL AUTO_INCREMENT,
  `patient_id` int NOT NULL,
  `item_id` int NOT NULL,
  `quantity` int NOT NULL DEFAULT '1',
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `sig` text,
  `consultation_id` int DEFAULT NULL,
  PRIMARY KEY (`prescription_id`),
  KEY `patient_id` (`patient_id`),
  CONSTRAINT `prescription_ibfk_1` FOREIGN KEY (`patient_id`) REFERENCES `patients` (`patient_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=135 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prescription`
--

LOCK TABLES `prescription` WRITE;
/*!40000 ALTER TABLE `prescription` DISABLE KEYS */;
INSERT INTO `prescription` VALUES (104,2082,27,14,'2025-10-21 14:40:02','1 tablet 2 x a day x 1 week',260),(105,2082,10,6,'2025-10-21 14:40:02','1 tablet 2 x a day as need for pain',260),(107,2084,27,14,'2025-10-21 14:53:13','1 tablet 2x a day 1 week',261),(117,2087,14,1,'2025-10-23 11:43:58','GARGLE 15 CC 2 X A DAY AFTER MEALS',268),(118,2087,11,30,'2025-10-23 11:43:58','1 CAPSULE DAILY',268),(119,2087,23,21,'2025-10-23 11:43:58','1 TABLET 3 X A DAY 30 MINS BEFORE MEALS',268),(130,2008,30,1,'2025-10-25 06:44:43','s',102),(131,2008,16,1,'2025-10-25 06:44:43','sss',102),(132,2008,14,1,'2025-10-25 06:44:43','GARGLE 15 CC 2 X A DAY AFTER MEALS',102),(133,2072,16,1,'2025-10-28 07:20:45','sss2 PUFFS TO EACH NOSE 2 X DAILY (AM AND PM) TO CONSUME 1 BOTTLE',285),(134,2072,9,1,'2025-10-28 07:20:45','2 PUFFS TO EACH NOSE 2 X DAILY (AM AND PM) TO CONSUME 1 BOTTLE',285);
/*!40000 ALTER TABLE `prescription` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prescription_other`
--

DROP TABLE IF EXISTS `prescription_other`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prescription_other` (
  `id` int NOT NULL AUTO_INCREMENT,
  `patient_id` int NOT NULL,
  `consultation_id` int NOT NULL,
  `item_id` int NOT NULL,
  `quantity` int NOT NULL DEFAULT '1',
  `sig` text,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `fk_prescription_other_patient` (`patient_id`),
  KEY `fk_prescription_other_consultation` (`consultation_id`),
  CONSTRAINT `fk_prescription_other_consultation` FOREIGN KEY (`consultation_id`) REFERENCES `consultation` (`consultation_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_prescription_other_patient` FOREIGN KEY (`patient_id`) REFERENCES `patients` (`patient_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prescription_other`
--

LOCK TABLES `prescription_other` WRITE;
/*!40000 ALTER TABLE `prescription_other` DISABLE KEYS */;
INSERT INTO `prescription_other` VALUES (21,2075,249,6,21,'1 tablet 3 x a day x 1 week after meals','2025-10-20 05:07:18'),(23,2091,271,7,1,'6 ML 3 X A DAY X 1 WEEK','2025-10-23 05:07:12'),(24,2092,272,8,1,'1 TABLET 2 X ADAY X 2 WEEKS','2025-10-23 05:18:08'),(25,2089,273,9,1,'2 PUFFS TO EACH NOSE 2 X DAILY (AM AND PM) TO CONSUME 1 BOTTLE','2025-10-23 05:33:58'),(29,2008,282,8,1,'1 TABLET 2 X ADAY X 2 WEEKS','2025-10-27 21:49:47');
/*!40000 ALTER TABLE `prescription_other` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `queue`
--

DROP TABLE IF EXISTS `queue`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `queue` (
  `queue_id` int NOT NULL AUTO_INCREMENT,
  `patient_id` int NOT NULL,
  `queue_number` int NOT NULL,
  `status` enum('Examining','Waiting','Done','Skipped','Cancelled') DEFAULT 'Waiting',
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
  `called_at` datetime DEFAULT NULL,
  `finished_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`queue_id`),
  KEY `patient_id` (`patient_id`),
  CONSTRAINT `queue_ibfk_1` FOREIGN KEY (`patient_id`) REFERENCES `patients` (`patient_id`)
) ENGINE=InnoDB AUTO_INCREMENT=80 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `queue`
--

LOCK TABLES `queue` WRITE;
/*!40000 ALTER TABLE `queue` DISABLE KEYS */;
INSERT INTO `queue` VALUES (43,2073,1,'Done','2025-10-20 10:32:32','2025-10-20 14:19:02','2025-10-20 14:19:54','2025-10-28 12:17:38'),(44,2074,2,'Done','2025-10-20 10:43:03','2025-10-20 14:19:10','2025-10-20 14:19:58','2025-10-28 12:17:38'),(47,2075,3,'Done','2025-10-20 11:13:16','2025-10-20 14:19:18','2025-10-20 14:19:55','2025-10-28 12:17:38'),(48,2076,4,'Done','2025-10-20 11:27:29','2025-10-20 14:18:59','2025-10-20 14:20:00','2025-10-28 12:17:38'),(49,2077,5,'Done','2025-10-20 11:27:53','2025-10-20 14:19:30','2025-10-20 14:20:02','2025-10-28 12:17:38'),(50,2078,6,'Done','2025-10-20 12:00:56','2025-10-20 14:19:38','2025-10-20 14:20:07','2025-10-28 12:17:38'),(51,2079,1,'Done','2025-10-21 08:47:47','2025-10-21 13:56:48','2025-10-21 14:02:57','2025-10-28 12:17:38'),(52,2080,2,'Done','2025-10-21 09:54:25','2025-10-21 14:03:20','2025-10-21 14:07:40','2025-10-28 12:17:38'),(53,2056,3,'Done','2025-10-21 10:58:17','2025-10-21 14:11:28','2025-10-21 14:24:57','2025-10-28 12:17:38'),(54,2081,4,'Done','2025-10-21 11:00:41','2025-10-21 14:28:29','2025-10-21 14:30:54','2025-10-28 12:17:38'),(55,2082,5,'Done','2025-10-21 11:52:12','2025-10-21 14:32:18','2025-10-21 14:44:17','2025-10-28 12:17:38'),(56,2083,6,'Skipped','2025-10-21 12:08:35',NULL,NULL,'2025-10-28 12:17:38'),(57,2084,7,'Done','2025-10-21 12:08:51','2025-10-21 14:43:43','2025-10-21 14:59:33','2025-10-28 12:17:38'),(58,2085,8,'Examining','2025-10-21 14:24:43','2025-10-21 15:39:00','2025-10-21 15:04:39','2025-10-28 12:17:38'),(59,2009,9,'Skipped','2025-10-21 15:14:28',NULL,NULL,'2025-10-28 12:17:38'),(60,2086,1,'Done','2025-10-23 09:03:22','2025-10-23 11:32:27',NULL,'2025-10-28 12:17:38'),(61,2087,2,'Done','2025-10-23 09:13:24','2025-10-23 11:38:51',NULL,'2025-10-28 12:17:38'),(62,2088,3,'Done','2025-10-23 09:30:16','2025-10-23 11:50:24',NULL,'2025-10-28 12:17:38'),(63,2089,4,'Done','2025-10-23 10:39:41','2025-10-23 13:24:33',NULL,'2025-10-28 12:17:38'),(64,2090,5,'Done','2025-10-23 11:07:53','2025-10-23 12:07:41',NULL,'2025-10-28 12:17:38'),(65,2074,6,'Done','2025-10-23 11:26:10','2025-10-23 13:51:57',NULL,'2025-10-28 12:17:38'),(66,2091,7,'Done','2025-10-23 11:59:53','2025-10-23 13:00:12',NULL,'2025-10-28 12:17:38'),(67,2092,8,'Done','2025-10-23 12:50:02','2025-10-23 13:09:51',NULL,'2025-10-28 12:17:38'),(68,2094,1,'Waiting','2025-10-24 10:36:02',NULL,NULL,'2025-10-28 12:17:38'),(69,2093,2,'Waiting','2025-10-24 10:36:48',NULL,NULL,'2025-10-28 12:17:38'),(70,2076,3,'Waiting','2025-10-24 10:54:48',NULL,NULL,'2025-10-28 12:17:38'),(71,2013,4,'Examining','2025-10-24 10:58:18','2025-10-24 18:50:45',NULL,'2025-10-28 12:17:38'),(72,2008,4,'Waiting','2025-10-28 06:36:21',NULL,NULL,'2025-10-28 12:18:40'),(73,2012,43,'Waiting','2025-10-28 06:36:22',NULL,'2025-10-28 12:17:48','2025-10-28 12:18:36'),(74,2019,3,'Waiting','2025-10-28 06:36:22',NULL,NULL,'2025-10-28 12:18:38'),(75,2033,2,'Waiting','2025-10-28 06:45:32',NULL,NULL,'2025-10-28 12:18:34'),(76,2008,1,'Done','2025-11-15 12:15:23','2025-11-15 12:15:41','2025-11-15 12:15:52','2025-11-15 12:15:52'),(77,2016,2,'Waiting','2025-11-15 12:15:26',NULL,NULL,'2025-11-15 12:15:26'),(78,2022,3,'Waiting','2025-11-15 12:15:26',NULL,NULL,'2025-11-15 12:15:26'),(79,2027,4,'Waiting','2025-11-15 12:15:27',NULL,NULL,'2025-11-15 12:15:27');
/*!40000 ALTER TABLE `queue` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales`
--

DROP TABLE IF EXISTS `sales`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sales` (
  `sale_id` int NOT NULL AUTO_INCREMENT,
  `item_id` int NOT NULL,
  `quantity` int NOT NULL,
  `unit_price` decimal(10,2) NOT NULL,
  `discount_amount` decimal(10,2) DEFAULT '0.00',
  `tax_amount` decimal(10,2) DEFAULT '0.00',
  `total_price` decimal(10,2) NOT NULL,
  `sale_date` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`sale_id`)
) ENGINE=InnoDB AUTO_INCREMENT=265 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales`
--

LOCK TABLES `sales` WRITE;
/*!40000 ALTER TABLE `sales` DISABLE KEYS */;
/*!40000 ALTER TABLE `sales` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stock_movements`
--

DROP TABLE IF EXISTS `stock_movements`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stock_movements` (
  `movement_id` int NOT NULL AUTO_INCREMENT,
  `item_id` int NOT NULL,
  `movement_type` varchar(40) NOT NULL,
  `quantity` int NOT NULL,
  `movement_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `expiration_date` datetime DEFAULT NULL,
  `unit_price` decimal(10,2) DEFAULT NULL,
  `user_id` int DEFAULT NULL,
  PRIMARY KEY (`movement_id`)
) ENGINE=InnoDB AUTO_INCREMENT=353 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stock_movements`
--

LOCK TABLES `stock_movements` WRITE;
/*!40000 ALTER TABLE `stock_movements` DISABLE KEYS */;
INSERT INTO `stock_movements` VALUES (231,2,'IN',120,'2025-10-16 23:50:54','2025-10-17 07:50:47',2.50,NULL),(233,1,'IN',120,'2025-10-17 00:41:24','2025-10-17 08:41:06',5.00,NULL),(237,3,'IN',200,'2025-10-17 01:09:49','2025-10-17 09:06:52',0.00,NULL),(238,4,'OUT',1,'2025-10-17 06:07:25',NULL,0.00,NULL),(239,5,'OUT',2,'2025-10-17 06:07:25',NULL,0.00,NULL),(240,6,'OUT',1,'2025-10-17 06:07:25',NULL,0.00,NULL),(241,3,'OUT',3,'2025-10-17 06:07:25',NULL,0.00,NULL),(242,4,'OUT',1,'2025-10-17 06:08:55',NULL,0.00,NULL),(243,6,'OUT',1,'2025-10-17 06:08:55',NULL,0.00,NULL),(244,6,'OUT',1,'2025-10-17 06:10:52',NULL,0.00,NULL),(245,1,'OUT',1,'2025-10-17 06:10:52',NULL,5.00,NULL),(246,3,'OUT',1,'2025-10-17 06:11:09',NULL,0.00,NULL),(247,4,'OUT',1,'2025-10-17 06:12:01',NULL,0.00,NULL),(248,4,'OUT',1,'2025-10-17 06:12:24',NULL,0.00,NULL),(249,3,'OUT',1,'2025-10-17 06:25:03',NULL,0.00,NULL),(250,1,'OUT',1,'2025-10-17 06:25:03',NULL,5.00,NULL),(251,7,'OUT',1,'2025-10-17 06:25:35',NULL,0.00,NULL),(252,3,'OUT',1,'2025-10-17 06:37:19',NULL,0.00,NULL),(253,1,'OUT',1,'2025-10-17 06:37:19',NULL,5.00,NULL),(254,6,'OUT',1,'2025-10-17 06:37:19',NULL,0.00,NULL),(255,6,'OUT',1,'2025-10-17 07:17:03',NULL,0.00,NULL),(256,7,'OUT',1,'2025-10-17 07:17:03',NULL,0.00,NULL),(257,1,'OUT',4,'2025-10-17 07:17:03',NULL,5.00,NULL),(258,3,'OUT',1,'2025-10-17 07:36:41',NULL,0.00,NULL),(259,1,'OUT',2,'2025-10-17 07:36:41',NULL,5.00,NULL),(260,5,'OUT',1,'2025-10-17 07:55:45',NULL,0.00,NULL),(261,5,'OUT',1,'2025-10-17 07:55:47',NULL,0.00,NULL),(262,1,'OUT',1,'2025-10-17 23:22:23',NULL,5.00,NULL),(263,1,'OUT',2,'2025-10-17 23:37:07',NULL,5.00,NULL),(264,6,'OUT',1,'2025-10-17 23:42:52',NULL,0.00,NULL),(265,6,'OUT',1,'2025-10-17 23:42:54',NULL,0.00,NULL),(266,6,'OUT',1,'2025-10-17 23:42:55',NULL,0.00,NULL),(267,6,'OUT',1,'2025-10-17 23:42:56',NULL,0.00,NULL),(268,6,'OUT',1,'2025-10-17 23:42:56',NULL,0.00,NULL),(269,6,'OUT',1,'2025-10-17 23:42:57',NULL,0.00,NULL),(270,6,'OUT',1,'2025-10-17 23:42:57',NULL,0.00,NULL),(271,6,'OUT',1,'2025-10-17 23:42:58',NULL,0.00,NULL),(272,6,'OUT',1,'2025-10-17 23:42:58',NULL,0.00,NULL),(273,3,'OUT',1,'2025-10-17 23:45:50',NULL,0.00,NULL),(274,7,'OUT',1,'2025-10-17 23:45:50',NULL,0.00,NULL),(275,1,'OUT',2,'2025-10-17 23:45:50',NULL,5.00,NULL),(276,1,'OUT',1,'2025-10-17 23:47:39',NULL,5.00,NULL),(277,7,'OUT',1,'2025-10-17 23:47:39',NULL,0.00,NULL),(278,1,'OUT',1,'2025-10-17 23:51:02',NULL,5.00,NULL),(279,1,'OUT',2,'2025-10-17 23:51:44',NULL,5.00,NULL),(280,1,'OUT',1,'2025-10-17 23:56:33',NULL,5.00,NULL),(281,1,'OUT',1,'2025-10-17 23:58:51',NULL,5.00,NULL),(282,1,'OUT',1,'2025-10-18 00:00:18',NULL,5.00,NULL),(283,1,'OUT',2,'2025-10-18 00:02:07',NULL,5.00,NULL),(284,1,'OUT',1,'2025-10-18 00:02:28',NULL,5.00,NULL),(285,1,'OUT',1,'2025-10-18 00:18:35',NULL,5.00,NULL),(286,1,'OUT',1,'2025-10-18 00:19:14',NULL,5.00,NULL),(287,4,'IN',4,'2025-10-18 00:21:56','2025-10-18 08:21:28',300.00,NULL),(288,4,'IN',4,'2025-10-18 00:22:03','2025-10-18 08:21:28',300.00,NULL),(289,6,'OUT',1,'2025-10-18 00:39:29',NULL,0.00,NULL),(290,7,'OUT',2,'2025-10-18 00:40:15',NULL,0.00,NULL),(291,50,'IN',465,'2025-10-18 04:28:09','2027-02-27 12:06:30',0.00,NULL),(298,19,'IN',5,'2025-10-20 03:44:48','2026-09-30 11:43:14',550.00,NULL),(299,16,'IN',870,'2025-10-20 03:47:36','2026-12-31 11:43:14',40.00,NULL),(300,27,'IN',280,'2025-10-20 03:49:41','2028-04-30 11:43:14',55.00,NULL),(301,15,'IN',90,'2025-10-20 03:51:07','2028-02-28 11:43:14',400.00,NULL),(302,22,'IN',55,'2025-10-20 03:53:54','2028-02-28 11:43:14',550.00,NULL),(303,24,'IN',50,'2025-10-20 04:17:27','2026-09-30 12:16:33',35.00,NULL),(304,26,'IN',327,'2025-10-20 04:19:24','2027-04-30 12:16:33',35.00,NULL),(305,19,'OUT',1,'2025-10-20 05:01:12',NULL,550.00,NULL),(306,23,'OUT',21,'2025-10-20 05:17:40',NULL,25.00,NULL),(307,15,'OUT',1,'2025-10-20 05:24:08',NULL,400.00,NULL),(308,16,'OUT',10,'2025-10-20 05:24:09',NULL,40.00,NULL),(309,16,'OUT',5,'2025-10-20 05:47:11',NULL,40.00,NULL),(310,17,'OUT',1,'2025-10-20 05:52:41',NULL,450.00,NULL),(311,19,'OUT',1,'2025-10-20 05:53:45',NULL,550.00,NULL),(312,20,'IN',27,'2025-10-20 06:05:03','2027-02-27 14:01:40',780.00,NULL),(313,25,'IN',200,'2025-10-20 06:06:58','2027-05-30 14:01:40',45.00,NULL),(314,13,'IN',290,'2025-10-20 06:12:19','2026-05-26 14:01:40',75.00,NULL),(315,12,'IN',574,'2025-10-20 06:16:14','2026-07-30 14:01:40',65.00,NULL),(316,17,'IN',10,'2025-10-20 06:18:41','2027-06-30 14:01:40',450.00,NULL),(317,14,'IN',23,'2025-10-20 06:20:20','2027-04-30 14:01:40',550.00,NULL),(318,10,'IN',180,'2025-10-20 06:23:02','2027-06-30 14:01:40',35.00,NULL),(319,18,'IN',1,'2025-10-20 06:38:12','2027-02-28 14:01:40',450.00,NULL),(320,11,'IN',165,'2025-10-20 06:40:32','2026-04-30 14:01:40',25.00,NULL),(321,21,'IN',12,'2025-10-20 06:42:39','2027-06-30 14:01:40',1000.00,NULL),(322,9,'IN',75,'2025-10-20 06:44:33','2027-03-30 14:01:40',350.00,NULL),(323,28,'IN',750,'2025-10-21 02:33:03','2026-08-31 10:26:22',25.00,NULL),(324,13,'OUT',7,'2025-10-21 06:26:39',NULL,75.00,NULL),(325,10,'OUT',6,'2025-10-21 06:26:39',NULL,35.00,NULL),(326,22,'OUT',1,'2025-10-21 06:26:39',NULL,550.00,NULL),(327,22,'OUT',1,'2025-10-21 06:36:45',NULL,550.00,NULL),(328,13,'OUT',14,'2025-10-21 06:36:45',NULL,75.00,NULL),(329,26,'OUT',10,'2025-10-21 06:36:45',NULL,35.00,NULL),(330,22,'OUT',1,'2025-10-21 06:46:24',NULL,550.00,NULL),(331,15,'OUT',1,'2025-10-21 06:58:47',NULL,400.00,NULL),(332,13,'OUT',10,'2025-10-21 07:12:10',NULL,75.00,NULL),(333,15,'OUT',1,'2025-10-21 07:12:10',NULL,400.00,NULL),(334,10,'OUT',1,'2025-10-21 07:12:11',NULL,35.00,NULL),(335,23,'IN',387,'2025-10-21 07:26:15','2027-08-01 15:23:35',25.00,NULL),(336,29,'IN',12,'2025-10-21 07:29:01','2030-06-30 15:23:35',550.00,NULL),(337,30,'IN',200,'2025-10-21 07:29:54','2030-06-30 15:23:35',20.00,NULL),(338,12,'OUT',14,'2025-10-23 03:41:33',NULL,65.00,NULL),(339,14,'OUT',1,'2025-10-23 03:41:33',NULL,550.00,NULL),(340,27,'OUT',14,'2025-10-23 04:10:04',NULL,55.00,NULL),(341,15,'OUT',1,'2025-10-23 04:10:04',NULL,400.00,NULL),(342,19,'OUT',1,'2025-10-23 05:11:12',NULL,550.00,NULL),(343,15,'OUT',3,'2025-10-23 05:12:06',NULL,400.00,NULL),(344,26,'OUT',10,'2025-10-23 05:12:06',NULL,35.00,NULL),(345,13,'OUT',14,'2025-10-23 05:12:06',NULL,75.00,NULL),(346,16,'OUT',5,'2025-10-23 05:27:01',NULL,40.00,NULL),(347,19,'OUT',1,'2025-10-23 05:27:02',NULL,550.00,NULL),(348,23,'OUT',21,'2025-10-23 05:41:26',NULL,25.00,NULL),(349,26,'OUT',10,'2025-10-23 05:41:26',NULL,35.00,NULL),(351,13,'OUT',2,'2025-10-27 22:29:19',NULL,75.00,NULL),(352,9,'OUT',1,'2025-10-27 22:32:28',NULL,350.00,NULL);
/*!40000 ALTER TABLE `stock_movements` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_stock_movements_insert` AFTER INSERT ON `stock_movements` FOR EACH ROW BEGIN
    -- 1. Adjust stock quantity based on movement type
    IF NEW.movement_type = 'IN' THEN
        -- Incoming stock increases quantity
        UPDATE items
        SET quantity = quantity + NEW.quantity
        WHERE item_id = NEW.item_id;
    ELSEIF NEW.movement_type = 'OUT' OR NEW.movement_type = 'WRITE-OFF' THEN
        -- Outgoing or written-off items decrease stock
        UPDATE items
        SET quantity = quantity - NEW.quantity
        WHERE item_id = NEW.item_id;
    END IF;

    -- 2. Insert into history log
    INSERT INTO stock_movements_history (
        movement_id, item_id, movement_type, quantity, expiration_date, 
        action_type, old_quantity, new_quantity
    )
    VALUES (
        NEW.movement_id, NEW.item_id, NEW.movement_type, NEW.quantity, NEW.expiration_date, 
        'INSERT', NULL, NEW.quantity
    );
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_stock_movements_after_delete` AFTER DELETE ON `stock_movements` FOR EACH ROW BEGIN
    -- 1. Insert into history log
    INSERT INTO stock_movements_history (
        movement_id,
        item_id,
        movement_type,
        quantity,
        expiration_date,
        action_type,
        old_quantity,
        new_quantity
    )
    VALUES (
        OLD.movement_id,
        OLD.item_id,
        OLD.movement_type,
        OLD.quantity,
        OLD.expiration_date,
        'DELETE',
        OLD.quantity,
        NULL
    );

    -- 2. Adjust stock quantity
    IF OLD.movement_type = 'IN' THEN
        UPDATE items
        SET quantity = quantity - OLD.quantity
        WHERE item_id = OLD.item_id;
    ELSEIF OLD.movement_type = 'OUT' THEN
        UPDATE items
        SET quantity = quantity + OLD.quantity
        WHERE item_id = OLD.item_id;
    END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `stock_movements_history`
--

DROP TABLE IF EXISTS `stock_movements_history`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stock_movements_history` (
  `history_id` int NOT NULL AUTO_INCREMENT,
  `movement_id` int NOT NULL,
  `item_id` int NOT NULL,
  `movement_type` varchar(40) NOT NULL,
  `quantity` int NOT NULL,
  `expiration_date` date DEFAULT NULL,
  `action_type` enum('INSERT','UPDATE','DELETE') NOT NULL,
  `action_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `old_quantity` int DEFAULT NULL,
  `new_quantity` int DEFAULT NULL,
  PRIMARY KEY (`history_id`)
) ENGINE=InnoDB AUTO_INCREMENT=571 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stock_movements_history`
--

LOCK TABLES `stock_movements_history` WRITE;
/*!40000 ALTER TABLE `stock_movements_history` DISABLE KEYS */;
INSERT INTO `stock_movements_history` VALUES (498,291,50,'IN',465,'2027-02-27','INSERT','2025-10-18 04:28:09',NULL,465),(499,292,18,'OUT',2,NULL,'INSERT','2025-10-18 05:41:14',NULL,2),(500,293,10,'OUT',6,NULL,'INSERT','2025-10-18 05:41:14',NULL,6),(501,294,16,'OUT',1,NULL,'INSERT','2025-10-18 05:41:14',NULL,1),(502,295,27,'OUT',14,NULL,'INSERT','2025-10-18 05:46:52',NULL,14),(503,296,22,'OUT',1,NULL,'INSERT','2025-10-18 05:51:27',NULL,1),(504,297,12,'OUT',10,NULL,'INSERT','2025-10-18 05:51:27',NULL,10),(505,297,12,'OUT',10,NULL,'DELETE','2025-10-18 05:56:53',10,NULL),(506,295,27,'OUT',14,NULL,'DELETE','2025-10-18 05:57:34',14,NULL),(507,294,16,'OUT',1,NULL,'DELETE','2025-10-18 05:58:00',1,NULL),(508,296,22,'OUT',1,NULL,'DELETE','2025-10-18 05:58:18',1,NULL),(509,292,18,'OUT',2,NULL,'DELETE','2025-10-18 05:58:33',2,NULL),(510,293,10,'OUT',6,NULL,'DELETE','2025-10-18 05:58:48',6,NULL),(511,298,19,'IN',5,'2026-09-30','INSERT','2025-10-20 03:44:48',NULL,5),(512,299,16,'IN',870,'2026-12-31','INSERT','2025-10-20 03:47:36',NULL,870),(513,300,27,'IN',280,'2028-04-30','INSERT','2025-10-20 03:49:41',NULL,280),(514,301,15,'IN',90,'2028-02-28','INSERT','2025-10-20 03:51:07',NULL,90),(515,302,22,'IN',55,'2028-02-28','INSERT','2025-10-20 03:53:54',NULL,55),(516,303,24,'IN',50,'2026-09-30','INSERT','2025-10-20 04:17:27',NULL,50),(517,304,26,'IN',327,'2027-04-30','INSERT','2025-10-20 04:19:24',NULL,327),(518,305,19,'OUT',1,NULL,'INSERT','2025-10-20 05:01:12',NULL,1),(519,306,23,'OUT',21,NULL,'INSERT','2025-10-20 05:17:40',NULL,21),(520,307,15,'OUT',1,NULL,'INSERT','2025-10-20 05:24:08',NULL,1),(521,308,16,'OUT',10,NULL,'INSERT','2025-10-20 05:24:09',NULL,10),(522,309,16,'OUT',5,NULL,'INSERT','2025-10-20 05:47:11',NULL,5),(523,310,17,'OUT',1,NULL,'INSERT','2025-10-20 05:52:41',NULL,1),(524,311,19,'OUT',1,NULL,'INSERT','2025-10-20 05:53:45',NULL,1),(525,312,20,'IN',27,'2027-02-27','INSERT','2025-10-20 06:05:03',NULL,27),(526,313,25,'IN',200,'2027-05-30','INSERT','2025-10-20 06:06:58',NULL,200),(527,314,13,'IN',290,'2026-05-26','INSERT','2025-10-20 06:12:19',NULL,290),(528,315,12,'IN',574,'2026-07-30','INSERT','2025-10-20 06:16:14',NULL,574),(529,316,17,'IN',10,'2027-06-30','INSERT','2025-10-20 06:18:41',NULL,10),(530,317,14,'IN',23,'2027-04-30','INSERT','2025-10-20 06:20:20',NULL,23),(531,318,10,'IN',180,'2027-06-30','INSERT','2025-10-20 06:23:02',NULL,180),(532,319,18,'IN',1,'2027-02-28','INSERT','2025-10-20 06:38:12',NULL,1),(533,320,11,'IN',165,'2026-04-30','INSERT','2025-10-20 06:40:32',NULL,165),(534,321,21,'IN',12,'2027-06-30','INSERT','2025-10-20 06:42:39',NULL,12),(535,322,9,'IN',75,'2027-03-30','INSERT','2025-10-20 06:44:33',NULL,75),(536,323,28,'IN',750,'2026-08-31','INSERT','2025-10-21 02:33:03',NULL,750),(537,232,2,'WRITE-OFF',20,NULL,'DELETE','2025-10-21 05:44:42',20,NULL),(538,234,1,'WRITE-OFF',1,NULL,'DELETE','2025-10-21 05:44:42',1,NULL),(539,235,1,'WRITE-OFF',19,NULL,'DELETE','2025-10-21 05:44:42',19,NULL),(540,236,1,'WRITE-OFF',20,NULL,'DELETE','2025-10-21 05:44:42',20,NULL),(541,324,13,'OUT',7,NULL,'INSERT','2025-10-21 06:26:39',NULL,7),(542,325,10,'OUT',6,NULL,'INSERT','2025-10-21 06:26:39',NULL,6),(543,326,22,'OUT',1,NULL,'INSERT','2025-10-21 06:26:39',NULL,1),(544,327,22,'OUT',1,NULL,'INSERT','2025-10-21 06:36:45',NULL,1),(545,328,13,'OUT',14,NULL,'INSERT','2025-10-21 06:36:45',NULL,14),(546,329,26,'OUT',10,NULL,'INSERT','2025-10-21 06:36:45',NULL,10),(547,330,22,'OUT',1,NULL,'INSERT','2025-10-21 06:46:24',NULL,1),(548,331,15,'OUT',1,NULL,'INSERT','2025-10-21 06:58:47',NULL,1),(549,332,13,'OUT',10,NULL,'INSERT','2025-10-21 07:12:10',NULL,10),(550,333,15,'OUT',1,NULL,'INSERT','2025-10-21 07:12:10',NULL,1),(551,334,10,'OUT',1,NULL,'INSERT','2025-10-21 07:12:11',NULL,1),(552,335,23,'IN',387,'2027-08-01','INSERT','2025-10-21 07:26:15',NULL,387),(553,336,29,'IN',12,'2030-06-30','INSERT','2025-10-21 07:29:01',NULL,12),(554,337,30,'IN',200,'2030-06-30','INSERT','2025-10-21 07:29:54',NULL,200),(555,338,12,'OUT',14,NULL,'INSERT','2025-10-23 03:41:33',NULL,14),(556,339,14,'OUT',1,NULL,'INSERT','2025-10-23 03:41:33',NULL,1),(557,340,27,'OUT',14,NULL,'INSERT','2025-10-23 04:10:04',NULL,14),(558,341,15,'OUT',1,NULL,'INSERT','2025-10-23 04:10:04',NULL,1),(559,342,19,'OUT',1,NULL,'INSERT','2025-10-23 05:11:12',NULL,1),(560,343,15,'OUT',3,NULL,'INSERT','2025-10-23 05:12:06',NULL,3),(561,344,26,'OUT',10,NULL,'INSERT','2025-10-23 05:12:06',NULL,10),(562,345,13,'OUT',14,NULL,'INSERT','2025-10-23 05:12:06',NULL,14),(563,346,16,'OUT',5,NULL,'INSERT','2025-10-23 05:27:01',NULL,5),(564,347,19,'OUT',1,NULL,'INSERT','2025-10-23 05:27:02',NULL,1),(565,348,23,'OUT',21,NULL,'INSERT','2025-10-23 05:41:26',NULL,21),(566,349,26,'OUT',10,NULL,'INSERT','2025-10-23 05:41:26',NULL,10),(567,350,11,'WRITE-OFF',1,NULL,'INSERT','2025-10-24 02:36:40',NULL,1),(568,350,11,'WRITE-OFF',1,NULL,'DELETE','2025-10-24 02:36:51',1,NULL),(569,351,13,'OUT',2,NULL,'INSERT','2025-10-27 22:29:19',NULL,2),(570,352,9,'OUT',1,NULL,'INSERT','2025-10-27 22:32:28',NULL,1);
/*!40000 ALTER TABLE `stock_movements_history` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stock_movements_log`
--

DROP TABLE IF EXISTS `stock_movements_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stock_movements_log` (
  `log_id` int NOT NULL AUTO_INCREMENT,
  `movement_id` int NOT NULL,
  `item_id` int DEFAULT NULL,
  `movement_type` varchar(50) DEFAULT NULL,
  `quantity` int DEFAULT NULL,
  `movement_date` date DEFAULT NULL,
  `expiration_date` date DEFAULT NULL,
  `unit_price` decimal(10,2) DEFAULT NULL,
  `deleted_by_user_id` int NOT NULL,
  `deleted_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`log_id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stock_movements_log`
--

LOCK TABLES `stock_movements_log` WRITE;
/*!40000 ALTER TABLE `stock_movements_log` DISABLE KEYS */;
INSERT INTO `stock_movements_log` VALUES (7,350,11,'WRITE-OFF',1,'2025-10-24',NULL,NULL,2,'2025-10-24 02:36:51');
/*!40000 ALTER TABLE `stock_movements_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `system_settings`
--

DROP TABLE IF EXISTS `system_settings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `system_settings` (
  `setting_id` int NOT NULL AUTO_INCREMENT,
  `setting_key` varchar(100) DEFAULT NULL,
  `setting_value` text,
  `description` text,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`setting_id`),
  UNIQUE KEY `setting_key` (`setting_key`)
) ENGINE=InnoDB AUTO_INCREMENT=92 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `system_settings`
--

LOCK TABLES `system_settings` WRITE;
/*!40000 ALTER TABLE `system_settings` DISABLE KEYS */;
INSERT INTO `system_settings` VALUES (21,'default_currency','PHP','Default currency of the system','2025-09-12 17:11:00','2025-09-12 17:11:00'),(22,'currency_symbol','P','Currency symbol for displaying prices','2025-09-12 17:11:00','2025-10-18 04:26:37'),(23,'invoice_prefix','INV','Prefix used when generating invoice numbers','2025-09-12 17:11:00','2025-09-12 17:11:00'),(52,'allow_negative_stock','0',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(53,'low_stock_threshold','10',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(54,'clinic_name','[CLINIC NAME]',NULL,'2025-09-24 08:17:22','2025-11-14 17:38:21'),(55,'clinic_address','Iloilo Mission Hospital, Jaro, Iloilo City',NULL,'2025-09-24 08:17:22','2025-11-14 17:38:21'),(56,'clinic_tel','000-000',NULL,'2025-09-24 08:17:22','2025-11-14 17:38:21'),(57,'clinic_mobile','0900-0000000',NULL,'2025-09-24 08:17:22','2025-11-14 17:38:21'),(58,'clinic_hours','Monday, Tuesday, Thursday, Friday, Saturday, 11:00 AM - 2:00 PM',NULL,'2025-09-24 08:17:22','2025-10-23 01:22:06'),(59,'clinic_affiliations','St. Paul\'s Hospital, Iloilo Doctor\'s Hospital,  Western Visayas Medical Center, West Visayas State Uni. Med Center, Medicus Ambulatory, Metro Iloilo Hospital & Med. Center Inc.',NULL,'2025-09-24 08:17:22','2025-11-14 17:38:21'),(60,'report_header','ENT CLINIC ',NULL,'2025-09-24 08:17:22','2025-10-01 12:03:16'),(61,'report_footer','','MA. CANDIE PEARL O. BASCOS-VILLENA, MD. FPSO-HNS','2025-09-24 08:17:22','2025-10-08 22:41:18'),(62,'date_format','yyyy-MM-dd',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(63,'time_format','hh:mm tt',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(64,'printer_name	','XP-58',NULL,'2025-09-24 08:17:22','2025-10-08 13:47:14'),(65,'markup_percentage','50',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(66,'clinic_subtitle','Fellow, Phil. Society of Otolaryngology, Head & Neck Surgery',NULL,'2025-09-24 08:28:15','2025-09-24 08:53:37'),(67,'clinic_email','clinicemail@yahoo.com',NULL,'2025-09-24 08:42:52','2025-11-14 17:38:21'),(68,'license_number','00000','LIC. NO. 99566','2025-09-25 06:27:50','2025-11-14 17:38:21'),(82,'ptr','00000',NULL,'2025-10-08 13:47:14','2025-11-14 17:38:21'),(83,'stwo','00000',NULL,'2025-10-08 13:47:14','2025-11-14 17:38:21'),(84,'land_mark','(Land Mark)',NULL,'2025-10-17 03:21:44','2025-11-14 17:39:23'),(85,'printer_name','XP-58 (copy 1)',NULL,'2025-10-17 06:09:04','2025-10-17 07:17:12'),(88,'base_path','\\\\SERVER\\Shared\\ScannedDocuments',NULL,'2025-10-24 15:42:38','2025-10-28 00:16:49'),(90,'receptionist_ip','192.168.1.25',NULL,'2025-10-27 21:29:37','2025-10-27 21:42:43');
/*!40000 ALTER TABLE `system_settings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL,
  `password` varchar(255) NOT NULL,
  `full_name` varchar(100) NOT NULL,
  `role` varchar(45) NOT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'a','a','Receptionist','Receptionist'),(2,'d','d','BASCOS','Doctor'),(3,'admin','admin','Admin','Admin'),(4,'janet','janet','Janet','Receptionist');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `v_billing_with_patient_report`
--

DROP TABLE IF EXISTS `v_billing_with_patient_report`;
/*!50001 DROP VIEW IF EXISTS `v_billing_with_patient_report`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_billing_with_patient_report` AS SELECT 
 1 AS `Billing_ID`,
 1 AS `Consultation_ID`,
 1 AS `Patient_ID`,
 1 AS `Patient_Name`,
 1 AS `Fee`,
 1 AS `Discount_Percent`,
 1 AS `Discount_Amount`,
 1 AS `Total_Amount`,
 1 AS `Amount_Paid`,
 1 AS `Balance`,
 1 AS `Payment_Status`,
 1 AS `Note`,
 1 AS `Date_Billed`,
 1 AS `Updated_At`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_consultation_details`
--

DROP TABLE IF EXISTS `v_consultation_details`;
/*!50001 DROP VIEW IF EXISTS `v_consultation_details`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_consultation_details` AS SELECT 
 1 AS `consultation_id`,
 1 AS `patient_id`,
 1 AS `full_name`,
 1 AS `address`,
 1 AS `age`,
 1 AS `sex`,
 1 AS `civil_status`,
 1 AS `referred_by`,
 1 AS `consultation_date`,
 1 AS `chief_complaint`,
 1 AS `history`,
 1 AS `ear_exam`,
 1 AS `nose_exam`,
 1 AS `throat_exam`,
 1 AS `others_exam`,
 1 AS `diagnosis`,
 1 AS `recommendations`,
 1 AS `notes`,
 1 AS `follow_up_date`,
 1 AS `follow_up_notes`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_detailed_dispensing_report`
--

DROP TABLE IF EXISTS `v_detailed_dispensing_report`;
/*!50001 DROP VIEW IF EXISTS `v_detailed_dispensing_report`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_detailed_dispensing_report` AS SELECT 
 1 AS `Invoice_ID`,
 1 AS `Invoice_Date`,
 1 AS `Customer_Name`,
 1 AS `Prescription_ID`,
 1 AS `Item_ID`,
 1 AS `Generic_Name`,
 1 AS `Brand_Name`,
 1 AS `Strength`,
 1 AS `Dosage`,
 1 AS `Category`,
 1 AS `Quantity`,
 1 AS `Cost_Price`,
 1 AS `Unit_Price`,
 1 AS `Total`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_low_stock_report`
--

DROP TABLE IF EXISTS `v_low_stock_report`;
/*!50001 DROP VIEW IF EXISTS `v_low_stock_report`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_low_stock_report` AS SELECT 
 1 AS `Item_ID`,
 1 AS `Generic_Name`,
 1 AS `Brand_Name`,
 1 AS `Strength`,
 1 AS `Dosage`,
 1 AS `Category`,
 1 AS `Current_Stock`,
 1 AS `Cost_Price`,
 1 AS `Selling_Price`,
 1 AS `Description`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_sig_suggestions`
--

DROP TABLE IF EXISTS `v_sig_suggestions`;
/*!50001 DROP VIEW IF EXISTS `v_sig_suggestions`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_sig_suggestions` AS SELECT 
 1 AS `item_id`,
 1 AS `sig`,
 1 AS `last_used`,
 1 AS `use_count`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_stock_near_expiry_report`
--

DROP TABLE IF EXISTS `v_stock_near_expiry_report`;
/*!50001 DROP VIEW IF EXISTS `v_stock_near_expiry_report`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_stock_near_expiry_report` AS SELECT 
 1 AS `Movement_ID`,
 1 AS `Item_ID`,
 1 AS `Generic_Name`,
 1 AS `Brand_Name`,
 1 AS `Strength`,
 1 AS `Dosage`,
 1 AS `Movement_Type`,
 1 AS `Quantity`,
 1 AS `Movement_Date`,
 1 AS `Expiration_Date`,
 1 AS `User_ID`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_stock_on_hand_report`
--

DROP TABLE IF EXISTS `v_stock_on_hand_report`;
/*!50001 DROP VIEW IF EXISTS `v_stock_on_hand_report`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_stock_on_hand_report` AS SELECT 
 1 AS `Item_ID`,
 1 AS `Generic_Name`,
 1 AS `Brand_Name`,
 1 AS `Strength`,
 1 AS `Dosage`,
 1 AS `Category`,
 1 AS `Current_Stock`,
 1 AS `Cost_Price`,
 1 AS `Selling_Price`,
 1 AS `Status`,
 1 AS `Description`,
 1 AS `Created_At`,
 1 AS `Updated_At`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_write_off_report`
--

DROP TABLE IF EXISTS `v_write_off_report`;
/*!50001 DROP VIEW IF EXISTS `v_write_off_report`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_write_off_report` AS SELECT 
 1 AS `Write_Off_ID`,
 1 AS `Item_ID`,
 1 AS `Generic_Name`,
 1 AS `Brand_Name`,
 1 AS `Strength`,
 1 AS `Dosage`,
 1 AS `Quantity`,
 1 AS `Reason`,
 1 AS `Expiration_Date`,
 1 AS `Created_At`,
 1 AS `Updated_At`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_patients_with_documents`
--

DROP TABLE IF EXISTS `view_patients_with_documents`;
/*!50001 DROP VIEW IF EXISTS `view_patients_with_documents`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `view_patients_with_documents` AS SELECT 
 1 AS `patient_id`,
 1 AS `full_name`,
 1 AS `has_documents`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_queue_with_patients`
--

DROP TABLE IF EXISTS `view_queue_with_patients`;
/*!50001 DROP VIEW IF EXISTS `view_queue_with_patients`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `view_queue_with_patients` AS SELECT 
 1 AS `Queue_ID`,
 1 AS `Patient_ID`,
 1 AS `Patient_Name`,
 1 AS `Address`,
 1 AS `Age`,
 1 AS `Sex`,
 1 AS `Civil_Status`,
 1 AS `Patient_Contact_Number`,
 1 AS `Emergency_Contact_Number`,
 1 AS `Queue_Number`,
 1 AS `Status`,
 1 AS `Queued_At`,
 1 AS `Finished_Time`,
 1 AS `Referred_By`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_admit_orders_with_patient`
--

DROP TABLE IF EXISTS `vw_admit_orders_with_patient`;
/*!50001 DROP VIEW IF EXISTS `vw_admit_orders_with_patient`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_admit_orders_with_patient` AS SELECT 
 1 AS `admit_order_id`,
 1 AS `patient_id`,
 1 AS `patient_name`,
 1 AS `patient_address`,
 1 AS `patient_age`,
 1 AS `patient_gender`,
 1 AS `admit_date`,
 1 AS `special_orders`,
 1 AS `created_at`,
 1 AS `updated_at`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `write_off_movements`
--

DROP TABLE IF EXISTS `write_off_movements`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `write_off_movements` (
  `write_off_id` int NOT NULL AUTO_INCREMENT,
  `item_id` int NOT NULL,
  `quantity` int NOT NULL,
  `reason` varchar(255) NOT NULL,
  `expiration_date` date DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`write_off_id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `write_off_movements`
--

LOCK TABLES `write_off_movements` WRITE;
/*!40000 ALTER TABLE `write_off_movements` DISABLE KEYS */;
INSERT INTO `write_off_movements` VALUES (6,11,1,'DAMAGE',NULL,'2025-10-24 02:36:40','2025-10-24 02:36:40');
/*!40000 ALTER TABLE `write_off_movements` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'ent_clinic_db'
--

--
-- Dumping routines for database 'ent_clinic_db'
--
/*!50003 DROP PROCEDURE IF EXISTS `add_billing_payment` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `add_billing_payment`(
    IN p_billing_id INT,
    IN p_amount DECIMAL(10,2),
    IN p_note VARCHAR(255)
)
BEGIN
    DECLARE v_total_paid DECIMAL(10,2) DEFAULT 0.00;
    DECLARE v_total_amount DECIMAL(10,2) DEFAULT 0.00;
    DECLARE v_balance DECIMAL(10,2) DEFAULT 0.00;
    DECLARE v_status VARCHAR(20) DEFAULT 'UNPAID';
    DECLARE v_change_due DECIMAL(10,2) DEFAULT 0.00;

    -- Handle any SQL exceptions by rolling back
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Error in add_billing_payment. Transaction rolled back.';
    END;

    -- Validate payment amount
    IF p_amount IS NULL OR p_amount <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Payment amount must be greater than 0';
    END IF;

    START TRANSACTION;

    -- Get billing total (lock row to avoid race conditions)
    SELECT total_amount
    INTO v_total_amount
    FROM ent_clinic_db.billing
    WHERE billing_id = p_billing_id
    FOR UPDATE;

    IF v_total_amount IS NULL THEN
        ROLLBACK;
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Billing record not found';
    END IF;

    -- Get total paid so far
    SELECT COALESCE(SUM(amount), 0.00)
    INTO v_total_paid
    FROM ent_clinic_db.billing_payments
    WHERE billing_id = p_billing_id;

    -- Calculate current balance before new payment
    SET v_balance = v_total_amount - v_total_paid;

    -- Calculate change_due for this payment
    IF p_amount > v_balance THEN
        SET v_change_due = p_amount - v_balance; -- excess payment
        SET v_balance = 0.00; -- after this payment, no balance left
    ELSE
        SET v_change_due = 0.00;
        SET v_balance = v_balance - p_amount;
    END IF;

    -- Insert into billing_payments (store balance & change_due here)
    INSERT INTO ent_clinic_db.billing_payments (
        billing_id,
        payment_date,
        amount,
        note,
        balance,
        change_due
    )
    VALUES (
        p_billing_id,
        NOW(),
        p_amount,
        p_note,
        v_balance,
        v_change_due
    );

    -- Update total paid (exclude change_due, since that's returned to patient)
    SET v_total_paid = v_total_paid + p_amount - v_change_due;

    -- Decide payment status
    IF v_total_paid = 0 THEN
        SET v_status = 'UNPAID';
    ELSEIF v_total_paid >= v_total_amount THEN
        SET v_status = 'FULLY PAID';
    ELSE
        SET v_status = 'PARTIALLY PAID';
    END IF;

    -- Update billing table
    UPDATE ent_clinic_db.billing
    SET amount_paid = v_total_paid,
        balance = v_balance,
        payment_status = v_status,
        updated_at = NOW()
    WHERE billing_id = p_billing_id;

    COMMIT;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteStockMovement` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteStockMovement`(
    IN p_movement_id INT,
    IN p_user_id INT
)
BEGIN
    DECLARE v_item_id INT;
    DECLARE v_movement_type VARCHAR(50);
    DECLARE v_quantity INT;
    DECLARE v_movement_date DATE;
    DECLARE v_expiration_date DATE;
    DECLARE v_unit_price DECIMAL(10,2);

    -- Step 1: Fetch the movement details
    SELECT item_id, movement_type, quantity, movement_date, expiration_date, unit_price
    INTO v_item_id, v_movement_type, v_quantity, v_movement_date, v_expiration_date, v_unit_price
    FROM stock_movements
    WHERE movement_id = p_movement_id;

    -- Step 2: Insert into the log table
    INSERT INTO stock_movements_log(
        movement_id,
        item_id,
        movement_type,
        quantity,
        movement_date,
        expiration_date,
        unit_price,
        deleted_by_user_id
    )
    VALUES (
        p_movement_id,
        v_item_id,
        v_movement_type,
        v_quantity,
        v_movement_date,
        v_expiration_date,
        v_unit_price,
        p_user_id
    );

    -- Step 3: Delete from main table
    DELETE FROM stock_movements
    WHERE movement_id = p_movement_id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `update_patient_age` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `update_patient_age`(IN patientId INT)
BEGIN
    UPDATE patients
    SET age = TIMESTAMPDIFF(YEAR, birthdate, CURDATE())
    WHERE id = patientId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Final view structure for view `billing_with_patient`
--

/*!50001 DROP VIEW IF EXISTS `billing_with_patient`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `billing_with_patient` AS select `b`.`billing_id` AS `billing_id`,`b`.`consultation_id` AS `consultation_id`,`b`.`fee` AS `fee`,`b`.`discount_percent` AS `discount_percent`,`b`.`discount_amount` AS `discount_amount`,`b`.`total_amount` AS `total_amount`,`b`.`note` AS `note`,`b`.`payment_status` AS `payment_status`,`b`.`created_at` AS `created_at`,`b`.`updated_at` AS `updated_at`,`b`.`amount_paid` AS `amount_paid`,`b`.`balance` AS `balance`,`b`.`patient_id` AS `patient_id`,`p`.`full_name` AS `patient_name` from (`billing` `b` join `patients` `p` on((`b`.`patient_id` = `p`.`patient_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_billing_with_patient_report`
--

/*!50001 DROP VIEW IF EXISTS `v_billing_with_patient_report`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_billing_with_patient_report` AS select `billing_with_patient`.`billing_id` AS `Billing_ID`,`billing_with_patient`.`consultation_id` AS `Consultation_ID`,`billing_with_patient`.`patient_id` AS `Patient_ID`,`billing_with_patient`.`patient_name` AS `Patient_Name`,`billing_with_patient`.`fee` AS `Fee`,`billing_with_patient`.`discount_percent` AS `Discount_Percent`,`billing_with_patient`.`discount_amount` AS `Discount_Amount`,`billing_with_patient`.`total_amount` AS `Total_Amount`,`billing_with_patient`.`amount_paid` AS `Amount_Paid`,`billing_with_patient`.`balance` AS `Balance`,`billing_with_patient`.`payment_status` AS `Payment_Status`,`billing_with_patient`.`note` AS `Note`,`billing_with_patient`.`created_at` AS `Date_Billed`,`billing_with_patient`.`updated_at` AS `Updated_At` from `billing_with_patient` order by `billing_with_patient`.`created_at`,`billing_with_patient`.`billing_id` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_consultation_details`
--

/*!50001 DROP VIEW IF EXISTS `v_consultation_details`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_consultation_details` AS select `c`.`consultation_id` AS `consultation_id`,`c`.`patient_id` AS `patient_id`,`p`.`full_name` AS `full_name`,`p`.`address` AS `address`,`c`.`age` AS `age`,`p`.`sex` AS `sex`,`p`.`civil_status` AS `civil_status`,`p`.`referred_by` AS `referred_by`,`c`.`consultation_date` AS `consultation_date`,`c`.`chief_complaint` AS `chief_complaint`,`c`.`history` AS `history`,`c`.`ear_exam` AS `ear_exam`,`c`.`nose_exam` AS `nose_exam`,`c`.`throat_exam` AS `throat_exam`,`c`.`others_exam` AS `others_exam`,`c`.`diagnosis` AS `diagnosis`,`c`.`recommendations` AS `recommendations`,`c`.`notes` AS `notes`,`c`.`follow_up_date` AS `follow_up_date`,`c`.`follow_up_notes` AS `follow_up_notes` from (`consultation` `c` left join `patients` `p` on((`p`.`patient_id` = `c`.`patient_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_detailed_dispensing_report`
--

/*!50001 DROP VIEW IF EXISTS `v_detailed_dispensing_report`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_detailed_dispensing_report` AS select `i`.`invoice_id` AS `Invoice_ID`,`i`.`invoice_date` AS `Invoice_Date`,`i`.`customer_name` AS `Customer_Name`,`ii`.`prescription_id` AS `Prescription_ID`,`it`.`item_id` AS `Item_ID`,`it`.`generic_name` AS `Generic_Name`,`it`.`brand_name` AS `Brand_Name`,`it`.`strength` AS `Strength`,`it`.`dosage` AS `Dosage`,`it`.`category` AS `Category`,`ii`.`quantity` AS `Quantity`,`it`.`cost_price` AS `Cost_Price`,`ii`.`unit_price` AS `Unit_Price`,`ii`.`total_price` AS `Total` from ((`invoice_items` `ii` join `invoices` `i` on((`ii`.`invoice_id` = `i`.`invoice_id`))) join `items` `it` on((`ii`.`item_id` = `it`.`item_id`))) order by `i`.`invoice_date`,`i`.`invoice_id`,`it`.`generic_name` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_low_stock_report`
--

/*!50001 DROP VIEW IF EXISTS `v_low_stock_report`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_low_stock_report` AS select `items`.`item_id` AS `Item_ID`,`items`.`generic_name` AS `Generic_Name`,`items`.`brand_name` AS `Brand_Name`,`items`.`strength` AS `Strength`,`items`.`dosage` AS `Dosage`,`items`.`category` AS `Category`,`items`.`quantity` AS `Current_Stock`,`items`.`cost_price` AS `Cost_Price`,`items`.`selling_price` AS `Selling_Price`,`items`.`description` AS `Description` from `items` where (`items`.`quantity` <= 10) order by `items`.`quantity`,`items`.`generic_name` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_sig_suggestions`
--

/*!50001 DROP VIEW IF EXISTS `v_sig_suggestions`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_sig_suggestions` AS select `all_prescriptions`.`item_id` AS `item_id`,`all_prescriptions`.`sig` AS `sig`,max(`all_prescriptions`.`created_at`) AS `last_used`,count(0) AS `use_count` from (select `prescription`.`item_id` AS `item_id`,`prescription`.`sig` AS `sig`,`prescription`.`created_at` AS `created_at` from `prescription` where ((`prescription`.`sig` is not null) and (`prescription`.`sig` <> '')) union all select `prescription_other`.`item_id` AS `item_id`,`prescription_other`.`sig` AS `sig`,`prescription_other`.`created_at` AS `created_at` from `prescription_other` where ((`prescription_other`.`sig` is not null) and (`prescription_other`.`sig` <> ''))) `all_prescriptions` group by `all_prescriptions`.`item_id`,`all_prescriptions`.`sig` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_stock_near_expiry_report`
--

/*!50001 DROP VIEW IF EXISTS `v_stock_near_expiry_report`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_stock_near_expiry_report` AS select `sm`.`movement_id` AS `Movement_ID`,`sm`.`item_id` AS `Item_ID`,`it`.`generic_name` AS `Generic_Name`,`it`.`brand_name` AS `Brand_Name`,`it`.`strength` AS `Strength`,`it`.`dosage` AS `Dosage`,`sm`.`movement_type` AS `Movement_Type`,`sm`.`quantity` AS `Quantity`,`sm`.`movement_date` AS `Movement_Date`,`sm`.`expiration_date` AS `Expiration_Date`,`sm`.`user_id` AS `User_ID` from (`stock_movements` `sm` join `items` `it` on((`sm`.`item_id` = `it`.`item_id`))) where ((`sm`.`expiration_date` is not null) and (`sm`.`expiration_date` <= (curdate() + interval 30 day))) order by `sm`.`expiration_date`,`sm`.`movement_date`,`sm`.`item_id` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_stock_on_hand_report`
--

/*!50001 DROP VIEW IF EXISTS `v_stock_on_hand_report`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_stock_on_hand_report` AS select `items`.`item_id` AS `Item_ID`,`items`.`generic_name` AS `Generic_Name`,`items`.`brand_name` AS `Brand_Name`,`items`.`strength` AS `Strength`,`items`.`dosage` AS `Dosage`,`items`.`category` AS `Category`,`items`.`quantity` AS `Current_Stock`,`items`.`cost_price` AS `Cost_Price`,`items`.`selling_price` AS `Selling_Price`,`items`.`status` AS `Status`,`items`.`description` AS `Description`,`items`.`created_at` AS `Created_At`,`items`.`updated_at` AS `Updated_At` from `items` order by `items`.`generic_name`,`items`.`brand_name` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_write_off_report`
--

/*!50001 DROP VIEW IF EXISTS `v_write_off_report`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_write_off_report` AS select `w`.`write_off_id` AS `Write_Off_ID`,`w`.`item_id` AS `Item_ID`,`it`.`generic_name` AS `Generic_Name`,`it`.`brand_name` AS `Brand_Name`,`it`.`strength` AS `Strength`,`it`.`dosage` AS `Dosage`,`w`.`quantity` AS `Quantity`,`w`.`reason` AS `Reason`,`w`.`expiration_date` AS `Expiration_Date`,`w`.`created_at` AS `Created_At`,`w`.`updated_at` AS `Updated_At` from (`write_off_movements` `w` join `items` `it` on((`w`.`item_id` = `it`.`item_id`))) order by `w`.`created_at` desc */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_patients_with_documents`
--

/*!50001 DROP VIEW IF EXISTS `view_patients_with_documents`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `view_patients_with_documents` AS select `p`.`patient_id` AS `patient_id`,`p`.`full_name` AS `full_name`,(case when (count(`d`.`id`) > 0) then true else false end) AS `has_documents` from (`patients` `p` left join `patient_documents` `d` on((`p`.`patient_id` = `d`.`patient_id`))) group by `p`.`patient_id`,`p`.`full_name` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_queue_with_patients`
--

/*!50001 DROP VIEW IF EXISTS `view_queue_with_patients`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `view_queue_with_patients` AS select `q`.`queue_id` AS `Queue_ID`,`q`.`patient_id` AS `Patient_ID`,`p`.`full_name` AS `Patient_Name`,`p`.`address` AS `Address`,`p`.`age` AS `Age`,`p`.`sex` AS `Sex`,`p`.`civil_status` AS `Civil_Status`,`p`.`patient_contact_number` AS `Patient_Contact_Number`,`p`.`emergency_contact_number` AS `Emergency_Contact_Number`,`q`.`queue_number` AS `Queue_Number`,`q`.`status` AS `Status`,`q`.`created_at` AS `Queued_At`,`q`.`finished_at` AS `Finished_Time`,`p`.`referred_by` AS `Referred_By` from (`queue` `q` join `patients` `p` on((`q`.`patient_id` = `p`.`patient_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_admit_orders_with_patient`
--

/*!50001 DROP VIEW IF EXISTS `vw_admit_orders_with_patient`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_admit_orders_with_patient` AS select `ao`.`admit_order_id` AS `admit_order_id`,`ao`.`patient_id` AS `patient_id`,`p`.`full_name` AS `patient_name`,`p`.`address` AS `patient_address`,timestampdiff(YEAR,`p`.`birth_date`,curdate()) AS `patient_age`,`p`.`sex` AS `patient_gender`,`ao`.`admit_date` AS `admit_date`,`ao`.`special_orders` AS `special_orders`,`ao`.`created_at` AS `created_at`,`ao`.`updated_at` AS `updated_at` from (`admit_orders` `ao` join `patients` `p` on((`ao`.`patient_id` = `p`.`patient_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-11-15 12:20:15
