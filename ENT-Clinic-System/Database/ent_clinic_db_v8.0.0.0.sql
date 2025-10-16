CREATE DATABASE  IF NOT EXISTS `ent_clinic_db` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `ent_clinic_db`;
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
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `appointments`
--

LOCK TABLES `appointments` WRITE;
/*!40000 ALTER TABLE `appointments` DISABLE KEYS */;
INSERT INTO `appointments` VALUES (1,2,'2025-10-18','hera'),(2,NULL,'2025-10-17','Setup Clinic'),(3,NULL,'2025-10-17','hahahah'),(4,990,'2025-10-25','ear checkup follow up'),(5,1003,'2025-10-23',NULL),(6,1003,'2025-10-18',NULL),(7,1003,'2025-10-16','OBSERVE IMPACTED SERUMEN IN 1 WEEK');
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
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `attachments`
--

LOCK TABLES `attachments` WRITE;
/*!40000 ALTER TABLE `attachments` DISABLE KEYS */;
INSERT INTO `attachments` VALUES (33,142,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-09\\Images\\image_20251009_115701_115732469.png','Image','General','2025-10-09 11:57:32',''),(34,142,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-09\\Images\\image_20251009_115702_115732557.png','Image','General','2025-10-09 11:57:32',''),(35,142,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-09\\Images\\image_20251009_115703_115732641.png','Image','General','2025-10-09 11:57:35',''),(36,142,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-09\\Images\\image_20251009_115704_115735086.png','Image','General','2025-10-09 11:57:35',''),(37,143,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-09\\Images\\image_20251009_120106_120127182.png','Image','General','2025-10-09 12:01:29',''),(38,143,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-09\\Images\\image_20251009_120108_120129610.png','Image','Ears','2025-10-09 12:01:29','asdasd'),(39,143,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-09\\Images\\image_20251009_120109_120129705.png','Image','General','2025-10-09 12:01:29','');
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
) ENGINE=InnoDB AUTO_INCREMENT=292 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `autocomplete_entries`
--

LOCK TABLES `autocomplete_entries` WRITE;
/*!40000 ALTER TABLE `autocomplete_entries` DISABLE KEYS */;
INSERT INTO `autocomplete_entries` VALUES (21,'chief_complaint','EAR DISCHARGE '),(22,'history','NOTED EAR INFECTION 3 YEARS PTC '),(23,'ear_exam','TM PERFORATION AD 70% '),(24,'diagnosis','OTITIS MEDIA, AS '),(25,'recommendations','KEEP RIGHT EAR DRY '),(26,'chief_complaint','HEARING LOSS LEFT EAR '),(27,'history','ON AND OFF HEARING LOSS '),(28,'ear_exam','IMPACTED CERUMEN, AS '),(29,'diagnosis','IMPACTED CERUMEN '),(30,'recommendations','PROPER EAR CLEANING '),(31,'history','FOLLOW UP '),(32,'ear_exam','PERFORATED TM PERFORATION AS, 40% '),(33,'ear_exam','WITH MINIMAL DISCHARGE '),(34,'diagnosis','IMPACTED CERUMEN WITH TYMPANIC MEMBRANE PERFORATION, AS '),(35,'recommendations','KEEP LEFT EAR DRY '),(36,'chief_complaint','EAR FULLNESS AS '),(37,'history','3 DAYS ITCHINESS WITH EAR FULLNESS '),(38,'ear_exam','FUNGAL ELEMENTS AS '),(39,'diagnosis','OTOMYCOSIS, AS '),(40,'chief_complaint','EAR CHECK UP '),(41,'history','ITCHINESS X 5 DAYS '),(42,'ear_exam','YELLOWISH DISCHARGE AS, WITH FUNGAL ELEMENTS '),(43,'notes','EAR CLEANING '),(44,'chief_complaint','EAR IRRITATION '),(45,'history','1 MONTH ON AND OFF EAR IRRITATION '),(46,'history','NO HEARING LOSS '),(47,'ear_exam','DRY EARS '),(48,'ear_exam','MILD ITCHINESS '),(49,'diagnosis','MILD OTOMYCOSIS, AS '),(243,'chief_complaint','EAR CHECK UP'),(244,'chief_complaint','EAR DISCHARGE'),(245,'history','3 DAYS ITCHINESS WITH EAR FULLNESS'),(246,'history','ITCHINESS X 5 DAYS'),(247,'history','NO HEARING LOSS'),(248,'chief_complaint','EAR FULLNESS AS'),(249,'history','FOLLOW UP'),(250,'chief_complaint','EAR IRRITATION'),(251,'past_medical_history','SURGERY'),(252,'ear_exam','FUNGAL ELEMENTS AS'),(253,'diagnosis','IMPACTED CERUMEN WITH TYMPANIC MEMBRANE PERFORATION, AS'),(254,'recommendations','KEEP LEFT EAR DRY'),(255,'recommendations','PROPER EAR CLEANING'),(256,'procedures','EAR CLEANING'),(257,'ear_exam','MILD ITCHINESS'),(258,'history','1 MONTH ON AND OFF EAR IRRITATION'),(259,'diagnosis','IMPACTED CERUMEN'),(260,'past_medical_history','TUMOR'),(261,'nose_exam','SINUS'),(262,'throat_exam','BIGOL'),(263,'recommendations','KEEP RIGHT EAR DRY'),(264,'chief_complaint','HEARING LOSS LEFT EAR'),(265,'notes','hera '),(267,'ear_exam','DRY EARS'),(268,'notes','ear checkup follow up '),(270,'history','NOTED EAR INFECTION 3 YEARS PTC'),(271,'ear_exam','IMPACTED CERUMEN, AS'),(272,'history','3 DAYS ITCHINESS WITH EAR FULLNESS, SURGERY '),(273,'ear_exam','PERFORATED TM PERFORATION AS, 40%'),(274,'history','3 DAYS ITCHINESS WITH EAR FULLNESS, SURGERY'),(275,'diagnosis','MILD OTOMYCOSIS, AS'),(276,'recommendations','WASH HANDS DAILY'),(277,'chief_complaint','EAR DISCHARGE, EAR FULLNESS AS, EAR IRRITATION '),(278,'history','3 DAYS ITCHINESS WITH EAR FULLNESS, SURGERY, 1 MONTH ON AND OFF EAR IRRITATION, SURGERY '),(279,'ear_exam','DRY EARS, PERFORATED TM PERFORATION AS, 40%, MILD ITCHINESS '),(280,'nose_exam','SINUS '),(281,'throat_exam','BIGOL '),(282,'recommendations','KEEP LEFT EAR DRY, PROPER EAR CLEANING, PROPER EAR CLEANING, WASH HANDS DAILY '),(283,'chief_complaint','EAR DISCHARGE, EAR FULLNESS AS, EAR IRRITATION'),(284,'ear_exam','DRY EARS, PERFORATED TM PERFORATION AS, 40%, MILD ITCHINESS'),(285,'diagnosis','OTITIS MEDIA, AS'),(286,'chief_complaint','EAR DISCHARGE, EAR DISCHARGE, EAR FULLNESS AS, EAR IRRITATION '),(287,'ear_exam','DRY EARS, DRY EARS, PERFORATED TM PERFORATION AS, 40%, MILD ITCHINESS '),(288,'diagnosis','IMPACTED CERUMEN, IMPACTED CERUMEN WITH TYMPANIC MEMBRANE PERFORATION, AS, OTITIS MEDIA, AS '),(289,'notes','OBSERVE IMPACTED SERUMEN IN 1 WEEK '),(291,'history',', SURGERY ');
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
  `consultation_id` int NOT NULL,
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
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `billing`
--

LOCK TABLES `billing` WRITE;
/*!40000 ALTER TABLE `billing` DISABLE KEYS */;
INSERT INTO `billing` VALUES (44,141,700.00,20,140.00,560.00,'','UNPAID','2025-10-09 06:44:12','2025-10-09 06:44:12',0.00,NULL,2),(45,144,700.00,20,140.00,560.00,'SENIOR CITIZEN','UNPAID','2025-10-09 14:17:20','2025-10-09 14:17:20',0.00,NULL,990),(46,145,700.00,20,140.00,560.00,'sd','UNPAID','2025-10-09 14:23:41','2025-10-09 14:23:41',0.00,NULL,1002),(47,146,700.00,0,0.00,700.00,'asdasd','UNPAID','2025-10-09 14:26:28','2025-10-09 14:26:28',0.00,NULL,1003),(48,147,700.00,0,0.00,700.00,'asdasd','UNPAID','2025-10-09 14:32:01','2025-10-09 14:32:01',0.00,NULL,1003),(49,148,700.00,20,140.00,560.00,'SENIOR CITIZEN','UNPAID','2025-10-09 14:43:11','2025-10-09 14:43:11',0.00,NULL,1003),(50,149,700.00,0,0.00,700.00,'asdasd','UNPAID','2025-10-09 15:02:35','2025-10-09 15:02:35',0.00,NULL,1003);
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
-- Temporary view structure for view `billing_overview`
--

DROP TABLE IF EXISTS `billing_overview`;
/*!50001 DROP VIEW IF EXISTS `billing_overview`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `billing_overview` AS SELECT 
 1 AS `billing_id`,
 1 AS `consultation_id`,
 1 AS `patient_id`,
 1 AS `patient_name`,
 1 AS `fee`,
 1 AS `discount_percent`,
 1 AS `discount_amount`,
 1 AS `total_amount`,
 1 AS `amount_paid`,
 1 AS `balance`,
 1 AS `payment_status`,
 1 AS `created_at`,
 1 AS `updated_at`,
 1 AS `note`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `billing_payment_history`
--

DROP TABLE IF EXISTS `billing_payment_history`;
/*!50001 DROP VIEW IF EXISTS `billing_payment_history`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `billing_payment_history` AS SELECT 
 1 AS `payment_id`,
 1 AS `billing_id`,
 1 AS `patient_name`,
 1 AS `payment_date`,
 1 AS `amount`,
 1 AS `balance`,
 1 AS `change_due`,
 1 AS `note`*/;
SET character_set_client = @saved_cs_client;

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
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `billing_payments`
--

LOCK TABLES `billing_payments` WRITE;
/*!40000 ALTER TABLE `billing_payments` DISABLE KEYS */;
/*!40000 ALTER TABLE `billing_payments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `billing_report`
--

DROP TABLE IF EXISTS `billing_report`;
/*!50001 DROP VIEW IF EXISTS `billing_report`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `billing_report` AS SELECT 
 1 AS `billing_id`,
 1 AS `consultation_id`,
 1 AS `patient_id`,
 1 AS `patient_name`,
 1 AS `fee`,
 1 AS `discount_percent`,
 1 AS `discount_amount`,
 1 AS `total_amount`,
 1 AS `amount_paid`,
 1 AS `billing_balance`,
 1 AS `payment_status`,
 1 AS `billing_note`,
 1 AS `created_at`,
 1 AS `updated_at`,
 1 AS `payment_id`,
 1 AS `payment_date`,
 1 AS `payment_amount`,
 1 AS `payment_balance`,
 1 AS `change_due`,
 1 AS `payment_note`*/;
SET character_set_client = @saved_cs_client;

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
  PRIMARY KEY (`consultation_id`),
  KEY `patient_id` (`patient_id`),
  CONSTRAINT `consultation_ibfk_1` FOREIGN KEY (`patient_id`) REFERENCES `patients` (`patient_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=150 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `consultation`
--

LOCK TABLES `consultation` WRITE;
/*!40000 ALTER TABLE `consultation` DISABLE KEYS */;
INSERT INTO `consultation` VALUES (141,2,'Dr. Receptionistssss','2025-10-09 06:43:30','','','','','','','','',NULL,'',18,''),(142,2,'Dr. Receptionistssss','2025-10-09 11:57:32','','','','','','','','',NULL,'',18,''),(143,2,'Dr. Receptionistssss','2025-10-09 12:01:27','','','','','','','','',NULL,'',18,''),(144,990,'Dr. Receptionistssss','2025-10-09 14:17:01','','','','','','','','ear checkup follow up','2025-10-25','ear checkup follow up',8,''),(145,1002,'Dr. Receptionistssss','2025-10-09 14:23:30','','','','','','','','',NULL,'',5,''),(146,1003,'Dr. Receptionistssss','2025-10-09 14:26:22','EAR CHECK UP','3 DAYS ITCHINESS WITH EAR FULLNESS, SURGERY','DRY EARS','','','IMPACTED CERUMEN','KEEP RIGHT EAR DRY','','2025-10-23','',1,NULL),(147,1003,'Dr. Receptionistssss','2025-10-09 14:31:52','EAR DISCHARGE, EAR FULLNESS AS, EAR IRRITATION','3 DAYS ITCHINESS WITH EAR FULLNESS, SURGERY, 1 MONTH ON AND OFF EAR IRRITATION, SURGERY','DRY EARS, PERFORATED TM PERFORATION AS, 40%, MILD ITCHINESS','SINUS','BIGOL','MILD OTOMYCOSIS, AS','KEEP LEFT EAR DRY, PROPER EAR CLEANING, PROPER EAR CLEANING, WASH HANDS DAILY','','2025-10-18','',1,NULL),(148,1003,'Dr. Receptionistssss','2025-10-09 14:41:57','EAR DISCHARGE, EAR DISCHARGE, EAR FULLNESS AS, EAR IRRITATION','3 DAYS ITCHINESS WITH EAR FULLNESS, SURGERY','DRY EARS, DRY EARS, PERFORATED TM PERFORATION AS, 40%, MILD ITCHINESS','','','IMPACTED CERUMEN, IMPACTED CERUMEN WITH TYMPANIC MEMBRANE PERFORATION, AS, OTITIS MEDIA, AS','PROPER EAR CLEANING','OBSERVE IMPACTED SERUMEN IN 1 WEEK','2025-10-16','OBSERVE IMPACTED SERUMEN IN 1 WEEK',1,NULL),(149,1003,'Dr. Receptionistssss','2025-10-09 14:55:26','',', SURGERY','','','BIGOL','','','',NULL,'',1,NULL);
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
            IF LEFT(line,2) = 'â€¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            -- Ignore invalid lines (empty or only bullet)
            IF line <> '' AND line <> 'â€¢' THEN
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
            IF LEFT(line,2) = 'â€¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> 'â€¢' THEN
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
            IF LEFT(line,2) = 'â€¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> 'â€¢' THEN
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
            IF LEFT(line,2) = 'â€¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> 'â€¢' THEN
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
            IF LEFT(line,2) = 'â€¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> 'â€¢' THEN
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
            IF LEFT(line,2) = 'â€¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> 'â€¢' THEN
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
            IF LEFT(line,2) = 'â€¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> 'â€¢' THEN
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
            IF LEFT(line,2) = 'â€¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> 'â€¢' THEN
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
            IF LEFT(line,2) = 'â€¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> 'â€¢' THEN
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

--
-- Temporary view structure for view `consultation_detail`
--

DROP TABLE IF EXISTS `consultation_detail`;
/*!50001 DROP VIEW IF EXISTS `consultation_detail`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `consultation_detail` AS SELECT 
 1 AS `consultation_id`,
 1 AS `patient_name`,
 1 AS `consultation_date`,
 1 AS `doctor_name`,
 1 AS `chief_complaint`,
 1 AS `history`,
 1 AS `ear_exam`,
 1 AS `nose_exam`,
 1 AS `throat_exam`,
 1 AS `diagnosis`,
 1 AS `recommendations`,
 1 AS `notes`,
 1 AS `follow_up_date`,
 1 AS `follow_up_notes`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `dispense_history`
--

DROP TABLE IF EXISTS `dispense_history`;
/*!50001 DROP VIEW IF EXISTS `dispense_history`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `dispense_history` AS SELECT 
 1 AS `dispense_id`,
 1 AS `patient_id`,
 1 AS `patient_name`,
 1 AS `item_id`,
 1 AS `item_name`,
 1 AS `description`,
 1 AS `category`,
 1 AS `quantity`,
 1 AS `invoice_item_id`,
 1 AS `dispensed_at`,
 1 AS `note`*/;
SET character_set_client = @saved_cs_client;

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
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dispense_prescription`
--

LOCK TABLES `dispense_prescription` WRITE;
/*!40000 ALTER TABLE `dispense_prescription` DISABLE KEYS */;
INSERT INTO `dispense_prescription` VALUES (9,28,2,30,1,11,'2025-10-01 21:25:24','hihi'),(10,30,2,33,2,12,'2025-10-01 21:25:24','huhu'),(11,32,2,34,1,13,'2025-10-01 21:25:24','asdasd'),(12,34,2,34,1,14,'2025-10-01 21:25:24','asdas'),(13,35,2,30,1,15,'2025-10-01 21:25:24','asdas'),(14,36,2,34,5,16,'2025-10-01 21:25:24','drink this if you have alergy'),(15,37,2,35,1,17,'2025-10-01 21:25:24','asd'),(16,38,2,33,1,18,'2025-10-01 21:25:24','asd'),(17,39,2,34,1,19,'2025-10-01 21:25:24','wow');
/*!40000 ALTER TABLE `dispense_prescription` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `expired_items`
--

DROP TABLE IF EXISTS `expired_items`;
/*!50001 DROP VIEW IF EXISTS `expired_items`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `expired_items` AS SELECT 
 1 AS `movement_id`,
 1 AS `item_name`,
 1 AS `category`,
 1 AS `description`,
 1 AS `quantity`,
 1 AS `expiration_date`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `expiry_report`
--

DROP TABLE IF EXISTS `expiry_report`;
/*!50001 DROP VIEW IF EXISTS `expiry_report`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `expiry_report` AS SELECT 
 1 AS `movement_id`,
 1 AS `expiration_date`,
 1 AS `item_name`,
 1 AS `description`,
 1 AS `category`,
 1 AS `quantity`,
 1 AS `note`*/;
SET character_set_client = @saved_cs_client;

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
INSERT INTO `general_pe` VALUES (1,'General Apperance'),(2,'Skin'),(3,'Head and Neck'),(4,'Lungs'),(5,'Heart'),(6,'Breats'),(7,'Abdomen'),(8,'Anus and Rectum'),(9,'Genitals'),(10,'Extremities'),(11,'Neuro'),(12,'Remarks'),(13,'Recommendations');
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
  `personal_history` text,
  `bp` varchar(20) DEFAULT NULL,
  `temperature` decimal(4,1) DEFAULT NULL,
  `pr` int DEFAULT NULL,
  `rr` int DEFAULT NULL,
  `ht` decimal(5,2) DEFAULT NULL,
  `wt` decimal(5,2) DEFAULT NULL,
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
  PRIMARY KEY (`health_record_id`),
  KEY `patient_id` (`patient_id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `health_record`
--

LOCK TABLES `health_record` WRITE;
/*!40000 ALTER TABLE `health_record` DISABLE KEYS */;
INSERT INTO `health_record` VALUES (1,2,'SURGERY, TUMOR','NONE','NONE','120/60',36.6,12,124,21.00,123.00,'NORMAL','NORMAL','NORMAL','NORMAL','NORMAL','NORMAL','NORMAL','NORMAL','NORMAL','NORMAL','2025-10-06 21:28:20','2025-10-09 04:01:29'),(2,1005,'','','','',NULL,NULL,NULL,NULL,NULL,'UGLY','','','','','','','','','','2025-10-07 11:09:09','2025-10-07 11:45:50'),(3,1003,'SURGERY','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-07 11:18:33','2025-10-09 06:55:25'),(4,1002,'TUMOR','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-07 11:30:34','2025-10-09 06:23:29'),(5,1006,'','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-07 11:34:50','2025-10-07 11:35:24'),(6,1004,'','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-07 11:46:32','2025-10-07 11:46:57'),(7,1000,'','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-07 12:06:32','2025-10-07 12:06:32'),(8,990,'SURGERY','','','',NULL,NULL,NULL,NULL,NULL,'','','','','','','','','','','2025-10-09 06:17:01','2025-10-09 06:17:01');
/*!40000 ALTER TABLE `health_record` ENABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=72 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `invoice_items`
--

LOCK TABLES `invoice_items` WRITE;
/*!40000 ALTER TABLE `invoice_items` DISABLE KEYS */;
INSERT INTO `invoice_items` VALUES (1,105,34,1,6.61,6.61,23),(2,105,32,2,12.00,24.00,24),(3,105,33,3,12.31,36.93,25),(4,106,34,1,6.61,6.61,NULL),(5,106,30,1,12.00,12.00,NULL),(6,107,30,1,12.00,12.00,31),(7,107,31,2,15.00,30.00,27),(8,107,31,2,15.00,30.00,33),(9,107,32,2,12.00,24.00,29),(10,107,34,2,6.61,13.22,26),(11,108,30,1,12.00,12.00,28),(12,108,33,2,12.31,24.62,30),(13,108,34,1,6.61,6.61,32),(14,108,34,1,6.61,6.61,34),(15,108,30,1,12.00,12.00,35),(16,108,34,5,6.61,33.05,36),(17,108,35,1,150.00,150.00,37),(18,108,33,1,12.31,12.31,38),(19,108,34,1,6.61,6.61,39),(20,109,33,1,12.31,12.31,NULL),(21,109,34,1,6.61,6.61,NULL),(22,110,34,1,6.61,6.61,NULL),(23,110,35,2,150.00,300.00,NULL),(24,111,35,1,150.00,150.00,NULL),(25,112,30,1,12.00,12.00,NULL),(26,113,35,1,150.00,150.00,NULL),(27,114,34,1,6.61,6.61,NULL),(28,115,31,2,15.00,30.00,NULL),(29,116,34,1,6.61,6.61,NULL),(30,116,35,1,150.00,150.00,NULL),(31,117,34,1,6.61,6.61,NULL),(32,117,35,1,150.00,150.00,NULL),(33,118,34,2,6.61,13.22,NULL),(34,119,30,1,12.00,12.00,NULL),(35,119,34,1,6.61,6.61,NULL),(36,120,35,1,150.00,150.00,NULL),(37,120,34,1,6.61,6.61,NULL),(38,121,34,1,6.61,6.61,NULL),(39,121,35,1,150.00,150.00,NULL),(40,122,34,1,6.61,6.61,NULL),(41,122,31,1,15.00,15.00,NULL),(42,123,34,1,6.61,6.61,NULL),(43,123,35,1,150.00,150.00,NULL),(44,124,31,1,15.00,15.00,NULL),(45,124,34,1,6.61,6.61,NULL),(46,125,34,1,6.61,6.61,NULL),(47,126,30,1,12.00,12.00,NULL),(48,126,34,1,6.61,6.61,NULL),(49,126,35,10,150.00,1500.00,NULL),(50,127,35,11,150.00,1650.00,NULL),(51,128,34,10,6.61,66.10,NULL),(52,129,32,1,12.00,12.00,NULL),(53,130,31,1,15.00,15.00,NULL),(54,130,34,1,6.61,6.61,NULL),(55,131,30,1,12.00,12.00,NULL),(56,131,35,1,150.00,150.00,NULL),(57,132,34,1,6.61,6.61,NULL),(58,133,32,1,12.00,12.00,NULL),(59,133,30,1,12.00,12.00,NULL),(60,134,34,1,6.61,6.61,NULL),(61,134,35,1,150.00,150.00,NULL),(62,135,32,1,12.00,12.00,NULL),(63,135,30,1,12.00,12.00,NULL),(64,136,33,1,12.31,12.31,NULL),(65,136,32,1,12.00,12.00,NULL),(66,136,31,1,15.00,15.00,NULL),(67,137,30,1,12.00,12.00,NULL),(68,137,32,1,12.00,12.00,NULL),(69,138,34,1,6.61,6.61,NULL),(70,139,31,1,15.00,15.00,NULL),(71,140,33,1,12.31,12.31,NULL);
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
        INSERT INTO dispense_prescription (prescription_id, patient_id, item_id, quantity, invoice_item_id, note)
        SELECT p.prescription_id, p.patient_id, p.item_id, p.quantity, NEW.invoice_item_id, p.note
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
) ENGINE=InnoDB AUTO_INCREMENT=141 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `invoices`
--

LOCK TABLES `invoices` WRITE;
/*!40000 ALTER TABLE `invoices` DISABLE KEYS */;
INSERT INTO `invoices` VALUES (105,'Duzzy D. Buzz Jr.','2025-09-28 00:58:29',67.54,6.75,60.79,100.00,39.21,'ITEMS','Initial invoice!!!','10'),(106,'Walk-in','2025-09-28 02:34:42',18.61,1.86,16.75,20.00,3.25,'ITEMS','','10'),(107,'Duzzy D. Buzz Jr.','2025-09-30 18:11:31',109.22,10.92,98.30,100.00,1.70,'ITEMS','senior citizen','10'),(108,'Duzzy D. Buzz Jr.','2025-10-01 21:25:24',263.81,0.00,263.81,300.00,36.19,'ITEMS','wow','0'),(109,'Walk-in','2025-10-01 21:26:23',18.92,0.00,18.92,20.00,1.08,'ITEMS','qw','0'),(110,'Walk-in','2025-10-08 15:43:46',306.61,0.00,306.61,500.00,193.39,'ITEMS','Initial invoice!!!','0'),(111,'Walk-in','2025-10-08 16:29:47',150.00,0.00,150.00,200.00,50.00,'ITEMS','','0'),(112,'Walk-in','2025-10-08 16:33:24',12.00,0.00,12.00,20.00,8.00,'ITEMS','','0'),(113,'Walk-in','2025-10-08 16:34:28',150.00,0.00,150.00,200.00,50.00,'ITEMS','','0'),(114,'Walk-in','2025-10-08 16:36:00',6.61,0.00,6.61,20.00,13.39,'ITEMS','','0'),(115,'Walk-in','2025-10-08 16:37:23',30.00,0.00,30.00,30.00,0.00,'ITEMS','','0'),(116,'Walk-in','2025-10-08 16:43:39',156.61,0.00,156.61,200.00,43.39,'ITEMS','','0'),(117,'Walk-in','2025-10-08 16:56:49',156.61,0.00,156.61,2000.00,1843.39,'ITEMS','','0'),(118,'Walk-in','2025-10-08 16:57:37',13.22,0.00,13.22,20.00,6.78,'ITEMS','','0'),(119,'Walk-in','2025-10-08 17:18:38',18.61,0.00,18.61,20.00,1.39,'ITEMS','','0'),(120,'Walk-in','2025-10-08 17:27:53',156.61,0.00,156.61,200.00,43.39,'ITEMS','','0'),(121,'Walk-in','2025-10-08 17:41:16',156.61,15.66,140.95,150.00,9.05,'ITEMS','','10'),(122,'Walk-in','2025-10-08 17:41:45',21.61,0.00,21.61,40.00,18.39,'ITEMS','','0'),(123,'Walk-in','2025-10-08 17:46:02',156.61,0.00,156.61,200.00,43.39,'ITEMS','','0'),(124,'Walk-in','2025-10-08 17:49:26',21.61,0.00,21.61,30.00,8.39,'ITEMS','','0'),(125,'Walk-in','2025-10-08 17:57:34',6.61,0.00,6.61,100.00,93.39,'ITEMS','','0'),(126,'Walk-in','2025-10-08 18:26:22',1518.61,0.00,1518.61,2000.00,481.39,'ITEMS','','0'),(127,'Walk-in','2025-10-08 18:27:14',1650.00,0.00,1650.00,2000.00,350.00,'ITEMS','','0'),(128,'Walk-in','2025-10-08 18:27:29',66.10,0.00,66.10,100.00,33.90,'ITEMS','','0'),(129,'Walk-in','2025-10-08 18:29:56',12.00,0.00,12.00,100.00,88.00,'ITEMS','','0'),(130,'Walk-in','2025-10-08 18:30:12',21.61,0.00,21.61,100.00,78.39,'ITEMS','','0'),(131,'Walk-in','2025-10-08 18:37:25',162.00,0.00,162.00,200.00,38.00,'ITEMS','','0'),(132,'Walk-in','2025-10-08 19:00:59',6.61,0.00,6.61,10.00,3.39,'ITEMS','','0'),(133,'Walk-in','2025-10-08 19:05:41',24.00,0.00,24.00,100.00,76.00,'ITEMS','','0'),(134,'Walk-in','2025-10-08 19:07:29',156.61,0.00,156.61,200.00,43.39,'ITEMS','','0'),(135,'Walk-in','2025-10-08 20:02:29',24.00,0.00,24.00,30.00,6.00,'ITEMS','','0'),(136,'Walk-in','2025-10-08 20:05:09',39.31,0.00,39.31,40.00,0.69,'ITEMS','','0'),(137,'Walk-in','2025-10-08 20:07:44',24.00,0.00,24.00,100.00,76.00,'ITEMS','','0'),(138,'Walk-in','2025-10-08 20:07:59',6.61,0.00,6.61,10.00,3.39,'ITEMS','','0'),(139,'Walk-in','2025-10-08 21:05:59',15.00,1.50,13.50,20.00,6.50,'ITEMS','','10'),(140,'Walk-in','2025-10-08 21:12:59',12.31,0.00,12.31,20.00,7.69,'ITEMS','','0');
/*!40000 ALTER TABLE `invoices` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `items`
--

DROP TABLE IF EXISTS `items`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `items` (
  `item_id` int NOT NULL AUTO_INCREMENT,
  `item_name` varchar(255) NOT NULL,
  `description` varchar(100) DEFAULT NULL,
  `category` varchar(100) NOT NULL,
  `cost_price` decimal(10,2) NOT NULL,
  `selling_price` decimal(10,2) NOT NULL,
  `stock_quantity` int DEFAULT '0',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`item_id`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `items`
--

LOCK TABLES `items` WRITE;
/*!40000 ALTER TABLE `items` DISABLE KEYS */;
INSERT INTO `items` VALUES (30,'Paracetamol','100Mg','Medicine',10.00,12.00,259,'2025-09-27 02:44:51','2025-10-08 12:07:44'),(31,'Paracetamol','200Mg','Medicine',12.00,15.00,-11,'2025-09-27 04:09:11','2025-10-08 13:06:00'),(32,'Paracetamol','300Mg','Medicine',12.51,12.00,-7,'2025-09-27 04:11:29','2025-10-08 12:07:44'),(33,'Paracetamol','400Mg','Medicine',12.51,12.31,-6,'2025-09-27 04:11:42','2025-10-08 13:12:59'),(34,'Citirizine','200Mg','Medicine',5.00,6.61,81,'2025-09-27 04:22:34','2025-10-08 12:07:59'),(35,'Ear Buds','Small','Supplies',120.00,150.00,87,'2025-10-01 11:43:50','2025-10-08 11:07:29');
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
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lab_requests`
--

LOCK TABLES `lab_requests` WRITE;
/*!40000 ALTER TABLE `lab_requests` DISABLE KEYS */;
INSERT INTO `lab_requests` VALUES (17,2,'[1, 6]','2025-10-09',141),(18,2,'[16]','2025-10-09',143),(19,2,'[17, 20]','2025-10-09',143),(20,2,'[20, 16]','2025-10-09',143),(21,2,'[20, 16]','2025-10-09',143),(22,990,'[16, 19]','2025-10-09',144),(23,1003,'[16, 19]','2025-10-09',147),(24,1003,'[22, 23, 21]','2025-10-09',148);
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
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lab_results`
--

LOCK TABLES `lab_results` WRITE;
/*!40000 ALTER TABLE `lab_results` DISABLE KEYS */;
INSERT INTO `lab_results` VALUES (17,141,'Anti-HAV (IgM)','zip','D:\\ENT_CLINIC_Attachments\\2\\141\\cd396e57-85a0-4c38-8e01-cfa7d9dc9821.rar','2025-10-09 03:31:12','2025-10-09 03:31:12');
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
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lab_tests`
--

LOCK TABLES `lab_tests` WRITE;
/*!40000 ALTER TABLE `lab_tests` DISABLE KEYS */;
INSERT INTO `lab_tests` VALUES (1,'Hematology','Complete Blood Count (CBC)'),(2,'Hematology','Hemoglobin (Hgb)'),(3,'Hematology','Hematocrit (Hct)'),(4,'Hematology','Platelet Count'),(5,'Hematology','White Blood Cell Count (WBC)'),(6,'Clinical Chemistry','Fasting Blood Sugar (FBS)'),(7,'Clinical Chemistry','Blood Uric Acid'),(8,'Clinical Chemistry','Blood Urea Nitrogen (BUN)'),(9,'Clinical Chemistry','Serum Creatinine'),(10,'Clinical Chemistry','SGPT (ALT)'),(11,'Clinical Chemistry','SGOT (AST)'),(12,'Urinalysis','Routine Urinalysis'),(13,'Urinalysis','Pregnancy Test (Urine hCG)'),(14,'Fecalysis','Routine Fecalysis'),(15,'Fecalysis','Occult Blood Test'),(16,'Serology / Immunology','HBsAg (Screening for Hepatitis B)'),(17,'Serology / Immunology','Anti-HAV (IgM)'),(18,'Serology / Immunology','Dengue NS1 / IgG / IgM'),(19,'Serology / Immunology','HIV Test'),(20,'Serology / Immunology','CRP (C-Reactive Protein)'),(21,'Microbiology','Throat Swab Culture and Sensitivity'),(22,'Microbiology','Ear Discharge Culture and Sensitivity'),(23,'Microbiology','Nasal Swab Culture and Sensitivity'),(24,'Imaging / Diagnostics','X-Ray (Sinuses)'),(25,'Imaging / Diagnostics','X-Ray (Chest PA)'),(26,'Imaging / Diagnostics','Pure Tone Audiometry'),(27,'Imaging / Diagnostics','Tympanometry');
/*!40000 ALTER TABLE `lab_tests` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `low_stock_report`
--

DROP TABLE IF EXISTS `low_stock_report`;
/*!50001 DROP VIEW IF EXISTS `low_stock_report`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `low_stock_report` AS SELECT 
 1 AS `item_id`,
 1 AS `item_name`,
 1 AS `category`,
 1 AS `stock_quantity`,
 1 AS `cost_price`,
 1 AS `selling_price`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `other_items`
--

DROP TABLE IF EXISTS `other_items`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `other_items` (
  `item_id` int NOT NULL AUTO_INCREMENT,
  `item_name` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `description` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `category` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`item_id`)
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `other_items`
--

LOCK TABLES `other_items` WRITE;
/*!40000 ALTER TABLE `other_items` DISABLE KEYS */;
INSERT INTO `other_items` VALUES (35,'Ear Buds','Small','Supplies');
/*!40000 ALTER TABLE `other_items` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `patient_lab_requests`
--

DROP TABLE IF EXISTS `patient_lab_requests`;
/*!50001 DROP VIEW IF EXISTS `patient_lab_requests`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `patient_lab_requests` AS SELECT 
 1 AS `request_id`,
 1 AS `patient_name`,
 1 AS `request_date`,
 1 AS `test_ids`,
 1 AS `consultation_date`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `patient_summary`
--

DROP TABLE IF EXISTS `patient_summary`;
/*!50001 DROP VIEW IF EXISTS `patient_summary`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `patient_summary` AS SELECT 
 1 AS `patient_id`,
 1 AS `full_name`,
 1 AS `birth_date`,
 1 AS `age`,
 1 AS `sex`,
 1 AS `civil_status`,
 1 AS `patient_contact_number`,
 1 AS `total_consultations`,
 1 AS `last_consultation`*/;
SET character_set_client = @saved_cs_client;

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
  `birth_date` date NOT NULL,
  `age` int NOT NULL,
  `sex` enum('M','F') NOT NULL,
  `civil_status` varchar(20) DEFAULT NULL,
  `chronic_diseases` varchar(500) DEFAULT NULL,
  `current_medications` varchar(500) DEFAULT NULL,
  `previous_surgeries` varchar(500) DEFAULT NULL,
  `allergies` varchar(500) DEFAULT NULL,
  `smoking_history` varchar(100) DEFAULT NULL,
  `alcohol_history` varchar(100) DEFAULT NULL,
  `ear_history` varchar(500) DEFAULT NULL,
  `nose_history` varchar(500) DEFAULT NULL,
  `throat_history` varchar(500) DEFAULT NULL,
  `family_history` varchar(500) DEFAULT NULL,
  `blood_type` varchar(5) DEFAULT NULL,
  `insurance_info` varchar(255) DEFAULT NULL,
  `patient_contact_number` varchar(11) NOT NULL,
  `emergency_name` varchar(150) DEFAULT NULL,
  `emergency_contact_number` varchar(11) DEFAULT NULL,
  `emergency_relationship` varchar(50) DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `photo` longblob,
  PRIMARY KEY (`patient_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2008 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patients`
--

LOCK TABLES `patients` WRITE;
/*!40000 ALTER TABLE `patients` DISABLE KEYS */;
INSERT INTO `patients` VALUES (2,'Xuzzy D. Buzz Jr.','Buntatala Jaro Iloilo City','2007-03-08',18,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09511365191','Marry F. Buzz','09511365191','Spause','2025-09-07 18:15:54',NULL),(990,'Joshuah Suffieldsss','PO Box 100','2017-06-17',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Charmian Feavyour','09171234567','Child','2025-09-07 22:42:42',NULL),(1000,'Winne Earingey','Suite 200','2017-04-20',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Pierson Stainson','09171234567','Spouse','2025-09-07 22:42:12',NULL),(1002,'Corette Coppin','Suite 11','2020-09-12',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lennie Ormshaw','09215678901','Sibling','2025-09-07 22:43:57',NULL),(1003,'Arri Caldera','Tagbac Jaro Iloilo City','2024-08-31',1,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Hazlett O\'Hearn','09171234567','Friend','2025-09-07 22:43:57',NULL),(1004,'Edgardo Ham','Apt 376','2023-05-05',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','John Lindfors','09171234567','Friend','2025-09-07 22:43:57',NULL),(1005,'Yetta Wrathmall','3rd Floor','2021-07-09',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Brett Trevett','09348901234','Child','2025-09-07 22:43:57',NULL),(1006,'Morgun Yakovliv','PO Box 2849','2019-10-22',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Brand Potbury','09348901234','Father','2025-09-07 22:43:57',NULL),(1007,'Shanie Thomazet','PO Box 72324','2015-07-17',10,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Minette Simenel','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1008,'Emily Hankinson','PO Box 68345','2014-11-11',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ingeborg Paraman','09215678901','Sibling','2025-09-07 22:43:57',NULL),(1009,'Eileen Kleinstub','Room 1136','2018-11-22',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Andres Dhenin','09348901234','Friend','2025-09-07 22:43:57',NULL),(1010,'Valerye Jodrellec','Room 1429','2017-07-30',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Payton Borres','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1011,'Lisha Kenelin','Room 881','2017-03-10',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Bartholemy Hubatsch','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1012,'Tory Tharme','Suite 80','2014-10-30',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Hermon Twohig','09215678901','Sibling','2025-09-07 22:43:57',NULL),(1013,'Daniele Bethune','Suite 15','2025-08-19',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Walsh Wilbud','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1014,'Valentin Espie','Room 1847','2018-12-06',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Laverne Cribbins','09348901234','Friend','2025-09-07 22:43:57',NULL),(1015,'Ford Pachmann','1st Floor','2017-04-07',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Isa Kesley','09171234567','Mother','2025-09-07 22:43:57',NULL),(1016,'Xaviera Marc','20th Floor','2015-04-15',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rica Prantl','09348901234','Mother','2025-09-07 22:43:57',NULL),(1017,'Sande Donovan','Apt 999','2024-10-10',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Korella Saleway','09348901234','Child','2025-09-07 22:43:57',NULL),(1018,'Carissa Astman','Apt 980','2017-12-15',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Eliot Domek','09171234567','Friend','2025-09-07 22:43:57',NULL),(1019,'Claudius Clacson','PO Box 2360','2023-02-04',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Collen Bartoli','09215678901','Child','2025-09-07 22:43:57',NULL),(1020,'Harriette Cyples','Suite 18','2025-03-11',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gusti Kidde','09348901234','Child','2025-09-07 22:43:57',NULL),(1021,'Fidole Staresmeare','Room 1943','2019-02-26',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jimmie Billin','09215678901','Sibling','2025-09-07 22:43:57',NULL),(1022,'Torie Wilshaw','3rd Floor','2024-05-28',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rayshell Hucker','09215678901','Father','2025-09-07 22:43:57',NULL),(1023,'Michaeline Haryngton','12th Floor','2019-02-20',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ellis Burling','09348901234','Mother','2025-09-07 22:43:57',NULL),(1024,'Elwira Lehrahan','Suite 15','2022-09-25',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Albert Berford','09171234567','Mother','2025-09-07 22:43:57',NULL),(1025,'Persis Tassell','Apt 775','2020-12-22',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Carole Diego','09348901234','Father','2025-09-07 22:43:57',NULL),(1026,'Daffy Coleford','2nd Floor','2024-12-26',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Morlee Petrushka','09348901234','Father','2025-09-07 22:43:57',NULL),(1027,'Rois Beadham','PO Box 18764','2025-08-20',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Magdaia Jouhandeau','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1028,'Billie Killcross','PO Box 20543','2025-07-31',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Delilah Hurll','09348901234','Mother','2025-09-07 22:43:57',NULL),(1029,'Quentin Bansal','PO Box 84175','2014-12-30',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Darwin Conws','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1030,'Gardy Macauley','Suite 69','2020-01-24',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Sydel Gueny','09171234567','Friend','2025-09-07 22:43:57',NULL),(1031,'Josie Lishmund','Suite 3','2022-03-23',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gussie Gresty','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1032,'Kamilah La Padula','Suite 59','2022-05-23',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sergent Castelin','09215678901','Mother','2025-09-07 22:43:57',NULL),(1033,'Ashlen O\'Fergus','Suite 5','2023-09-04',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jemimah Padell','09171234567','Father','2025-09-07 22:43:57',NULL),(1034,'Sarina Castiglioni','Room 81','2020-11-03',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ermanno Eynald','09348901234','Mother','2025-09-07 22:43:57',NULL),(1035,'Arabel Dumper','13th Floor','2024-11-30',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Justine Lennon','09348901234','Father','2025-09-07 22:43:57',NULL),(1036,'Agnese Farrington','Apt 1880','2016-09-27',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Stanley Fiske','09171234567','Father','2025-09-07 22:43:57',NULL),(1037,'Bertrand Kincla','Apt 1009','2020-11-14',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kara Abrahamsohn','09171234567','Mother','2025-09-07 22:43:57',NULL),(1038,'Joyan Crank','Suite 3','2023-10-16',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kinny Kellard','09348901234','Father','2025-09-07 22:43:57',NULL),(1039,'Thebault Cooksley','18th Floor','2016-02-28',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alejoa Gianetti','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1040,'Julieta Laurant','Suite 9','2015-05-29',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Sarene Ellis','09171234567','Child','2025-09-07 22:43:57',NULL),(1041,'Melvyn Alsopp','Apt 1469','2021-01-13',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Moses De Santos','09215678901','Mother','2025-09-07 22:43:57',NULL),(1042,'Haydon Brownill','6th Floor','2020-10-08',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dickie Yantsev','09348901234','Friend','2025-09-07 22:43:57',NULL),(1043,'Vicki Shemelt','Room 1368','2015-10-15',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Derward Kneale','09171234567','Mother','2025-09-07 22:43:57',NULL),(1044,'Tobin Muddimer','Room 1369','2023-08-14',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Zorana Lindelof','09171234567','Friend','2025-09-07 22:43:57',NULL),(1045,'Pierette Gregol','13th Floor','2016-12-20',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ericka Oakly','09171234567','Friend','2025-09-07 22:43:57',NULL),(1046,'Edithe Tinkler','PO Box 54597','2017-01-26',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Shay Wharfe','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1047,'Berni Banting','PO Box 99889','2019-07-13',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Arie Templeman','09348901234','Child','2025-09-07 22:43:57',NULL),(1048,'Robb Angerstein','PO Box 40523','2020-06-21',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alaine Ebbage','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1049,'Hayley Flinn','7th Floor','2014-10-23',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Florrie Burkett','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1050,'Genvieve Dollin','Apt 129','2015-07-11',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Mauricio Brute','09171234567','Friend','2025-09-07 22:43:57',NULL),(1051,'Faydra Kingston','Apt 987','2023-04-09',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Petey Woodthorpe','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1052,'Grissel Wahlberg','Apt 1310','2016-09-13',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Man Weeke','09215678901','Child','2025-09-07 22:43:57',NULL),(1053,'Brina Ballantine','Suite 99','2022-04-12',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Corella Tythe','09215678901','Friend','2025-09-07 22:43:57',NULL),(1054,'Rockey Hundley','Room 1002','2014-12-10',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Coriss Cudihy','09215678901','Father','2025-09-07 22:43:57',NULL),(1055,'Inesita Wasiel','Apt 1359','2017-06-06',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Rosalynd Inseal','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1056,'Shelba Gegg','Suite 89','2018-01-15',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lita Levis','09348901234','Father','2025-09-07 22:43:57',NULL),(1057,'Briant Leggitt','Apt 138','2020-08-25',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Annnora Hailes','09171234567','Child','2025-09-07 22:43:57',NULL),(1058,'Karl Denisyev','10th Floor','2016-08-29',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Min Brunskill','09215678901','Sibling','2025-09-07 22:43:57',NULL),(1059,'Laurent Coulson','PO Box 39175','2023-11-24',1,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Madelon Piercey','09348901234','Friend','2025-09-07 22:43:57',NULL),(1060,'Abbie Redit','PO Box 44177','2021-04-07',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Fawnia Andreix','09348901234','Mother','2025-09-07 22:43:57',NULL),(1061,'Libbie Anstead','PO Box 72483','2025-01-05',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Nora Ovens','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1062,'Renard Mulles','PO Box 51361','2020-12-01',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Bendicty Lammie','09348901234','Mother','2025-09-07 22:43:57',NULL),(1063,'Imogen Bickerdike','Room 897','2015-03-30',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Grethel Pedrozzi','09348901234','Mother','2025-09-07 22:43:57',NULL),(1064,'Giacopo Beddon','Room 866','2020-02-28',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Tripp Wrightham','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1065,'Lilian Klimontovich','PO Box 50430','2016-08-25',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lynn Gilstin','09348901234','Mother','2025-09-07 22:43:57',NULL),(1066,'Rancell Shelsher','Suite 34','2016-05-07',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Adaline Squibbs','09171234567','Father','2025-09-07 22:43:57',NULL),(1067,'Cybill Ebbotts','5th Floor','2014-10-16',10,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rosmunda Entres','09348901234','Father','2025-09-07 22:43:57',NULL),(1068,'Stepha Peacham','Room 1420','2019-09-19',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Judy Clipston','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1069,'Mona Middle','14th Floor','2023-10-28',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jeanna Houtbie','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1070,'Jervis Dimmick','Room 1813','2021-02-12',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Locke Schukraft','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1071,'Tabb Rame','PO Box 34268','2022-03-19',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Yoshiko Collyns','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1072,'Dannye Escudier','14th Floor','2025-07-07',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Aymer Del Castello','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1073,'Emlen Cunniff','Room 1361','2020-11-01',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Steffen Marielle','09215678901','Mother','2025-09-07 22:43:57',NULL),(1074,'Isidore O\'Lunney','Room 696','2020-12-08',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Genny Riccardini','09215678901','Mother','2025-09-07 22:43:57',NULL),(1075,'Trefor Riddich','Suite 13','2020-11-25',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Francisco Guiot','09348901234','Mother','2025-09-07 22:43:57',NULL),(1076,'Mareah Dunsmuir','20th Floor','2022-03-12',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Barry Harbidge','09171234567','Father','2025-09-07 22:43:57',NULL),(1077,'Maris Pirouet','Apt 1288','2023-12-06',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Abra Sawnwy','09171234567','Mother','2025-09-07 22:43:57',NULL),(1078,'Karrah Faircliffe','PO Box 16121','2018-07-15',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Loella Moyle','09215678901','Child','2025-09-07 22:43:57',NULL),(1079,'Silvana Punshon','PO Box 39910','2020-06-17',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Nora Loughton','09215678901','Friend','2025-09-07 22:43:57',NULL),(1080,'Raleigh Polo','Room 1070','2022-02-27',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Athena Fairey','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1081,'Quintin Tague','PO Box 95438','2023-05-13',2,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ketty Bassindale','09215678901','Father','2025-09-07 22:43:57',NULL),(1082,'Delly Gotliffe','2nd Floor','2021-04-03',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jessie Shackleford','09171234567','Father','2025-09-07 22:43:57',NULL),(1083,'Katha Rigardeau','PO Box 46174','2021-02-17',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Friedrick Blackaby','09215678901','Child','2025-09-07 22:43:57',NULL),(1084,'Artemas Brannon','PO Box 81730','2022-05-04',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Glen Burtenshaw','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1085,'Corina Colt','Apt 1270','2021-04-17',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Brand Peaker','09171234567','Child','2025-09-07 22:43:57',NULL),(1086,'Ambrose Mussettini','Apt 1701','2021-09-18',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Latashia Cleynaert','09171234567','Child','2025-09-07 22:43:57',NULL),(1087,'Virgina Hablet','Suite 42','2021-11-06',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Athena Drewson','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1088,'Israel Lowndsbrough','PO Box 36917','2021-07-03',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Franni Watts','09215678901','Father','2025-09-07 22:43:57',NULL),(1089,'Maisey Persent','Room 1774','2017-12-02',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Frederic Spittall','09215678901','Father','2025-09-07 22:43:57',NULL),(1090,'Bernelle Mohring','PO Box 76725','2020-07-18',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Georgianne Checcucci','09215678901','Father','2025-09-07 22:43:57',NULL),(1091,'Lucais Maxworthy','Suite 49','2020-10-03',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Elvyn Chastney','09215678901','Child','2025-09-07 22:43:57',NULL),(1092,'Katherina Dowson','Room 1007','2016-08-25',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Red Fancet','09215678901','Father','2025-09-07 22:43:57',NULL),(1093,'Sayer Scarre','Suite 26','2025-05-04',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dorey Varnam','09348901234','Friend','2025-09-07 22:43:57',NULL),(1094,'Datha Rakestraw','Suite 49','2019-08-08',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Bryant Kwiek','09215678901','Father','2025-09-07 22:43:57',NULL),(1095,'Raphael Akram','Suite 53','2024-11-27',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Robert Vowell','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1096,'Cornell Mayow','Room 1606','2025-09-02',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Barn Sheraton','09348901234','Friend','2025-09-07 22:43:57',NULL),(1097,'Harmony Jowers','Room 1759','2020-09-02',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Olwen Rohlfing','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1098,'Gregor Boatman','Apt 841','2016-03-23',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Tish Crawley','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1099,'Edd Carnoghan','PO Box 5278','2015-05-14',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Giavani Willoughey','09348901234','Mother','2025-09-07 22:43:57',NULL),(1100,'Darby Grosvener','5th Floor','2016-09-23',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Cathrine Heatley','09171234567','Father','2025-09-07 22:43:57',NULL),(1101,'Olenolin Grafton','PO Box 67682','2021-03-27',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Nelie McGettigan','09171234567','Child','2025-09-07 22:43:57',NULL),(1102,'Waverly Temblett','Suite 85','2021-12-14',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Cordell Sonschein','09215678901','Friend','2025-09-07 22:43:57',NULL),(1103,'Leoine Wylam','Apt 695','2024-04-21',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gino Sainz','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1104,'Raviv Caitlin','PO Box 52071','2016-06-27',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jaye Benasik','09348901234','Friend','2025-09-07 22:43:57',NULL),(1105,'Richard Kemmis','14th Floor','2023-10-20',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Juliane Bretherick','09348901234','Mother','2025-09-07 22:43:57',NULL),(1106,'Deane Dillway','Room 1777','2015-06-24',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Tyrus Simonds','09215678901','Friend','2025-09-07 22:43:57',NULL),(1107,'Novelia McArtan','2nd Floor','2023-11-24',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Faunie Warfield','09171234567','Friend','2025-09-07 22:43:57',NULL),(1108,'Ronny McDugal','Room 1703','2023-12-01',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Pammi Bonsul','09215678901','Father','2025-09-07 22:43:57',NULL),(1109,'Abner Drinkel','Apt 1602','2021-06-04',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Berti Smart','09171234567','Child','2025-09-07 22:43:57',NULL),(1110,'Robinett Paunton','Room 1915','2017-08-26',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Trumann Thying','09348901234','Child','2025-09-07 22:43:57',NULL),(1111,'Bealle Biss','PO Box 65759','2016-07-19',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tanitansy Titheridge','09171234567','Father','2025-09-07 22:43:57',NULL),(1112,'Anet Cahey','4th Floor','2019-01-16',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Bondy Broader','09171234567','Father','2025-09-07 22:43:57',NULL),(1113,'Ree McGillacoell','Room 275','2025-04-14',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Katleen Wilshin','09171234567','Mother','2025-09-07 22:43:57',NULL),(1114,'Rice Blazdell','Room 693','2015-09-07',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Blanche Domokos','09348901234','Friend','2025-09-07 22:43:57',NULL),(1115,'Belia Fawke','PO Box 40083','2019-05-16',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Elizabeth Ellacott','09171234567','Mother','2025-09-07 22:43:57',NULL),(1116,'Ritchie Pearton','PO Box 3842','2021-06-12',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ethel Feragh','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1117,'Morris Fritschmann','10th Floor','2020-03-09',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jaquenetta Meacher','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1118,'Joye Warkup','Room 1443','2016-10-15',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rafe Gibbons','09171234567','Mother','2025-09-07 22:43:57',NULL),(1119,'Cull Gilstoun','8th Floor','2019-08-17',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lief Adderley','09348901234','Father','2025-09-07 22:43:57',NULL),(1120,'Sallie Olle','Room 1332','2018-01-15',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jaye Kinge','09171234567','Child','2025-09-07 22:43:57',NULL),(1121,'Kary Paschek','Room 150','2021-01-31',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Sonja Kennermann','09215678901','Friend','2025-09-07 22:43:57',NULL),(1122,'Gabriel Pitchers','Room 334','2016-05-05',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jammal Scimonelli','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1123,'Leeland Clacson','Apt 855','2024-09-03',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Philip Sineath','09215678901','Friend','2025-09-07 22:43:57',NULL),(1124,'Leslie Edison','PO Box 21944','2017-09-06',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Roseann Elcomb','09215678901','Friend','2025-09-07 22:43:57',NULL),(1125,'Roberta McMackin','Suite 47','2016-08-02',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lothaire Verecker','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1126,'Carissa Harbor','Apt 1272','2025-07-19',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Brade Staden','09171234567','Father','2025-09-07 22:43:57',NULL),(1127,'Barret Goulthorp','Apt 322','2017-01-29',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Toma Titmus','09348901234','Child','2025-09-07 22:43:57',NULL),(1128,'Fairleigh Buss','Room 908','2017-02-21',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Rubetta Illiston','09348901234','Friend','2025-09-07 22:43:57',NULL),(1129,'Darrin Parnby','PO Box 42033','2025-01-10',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Charlotta Vosper','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1130,'Vikky Shee','11th Floor','2015-08-30',10,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Salmon Von Helmholtz','09171234567','Friend','2025-09-07 22:43:57',NULL),(1131,'Gavrielle Agent','Room 1826','2021-07-11',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Mill Dudley','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1132,'Marten Falkner','Room 363','2023-10-04',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Berk Dulling','09215678901','Father','2025-09-07 22:43:57',NULL),(1133,'Ripley Crapper','PO Box 25859','2021-02-02',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Rosaline Cominello','09171234567','Mother','2025-09-07 22:43:57',NULL),(1134,'Dredi Apark','PO Box 1701','2021-07-04',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Juditha Jimeno','09171234567','Friend','2025-09-07 22:43:57',NULL),(1135,'Kamila Rathe','Suite 90','2024-07-30',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Wylma McCallion','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1136,'Nickolaus Van Leijs','PO Box 19123','2025-08-25',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jaquenetta Sings','09348901234','Father','2025-09-07 22:43:57',NULL),(1137,'Carita Andriveaux','19th Floor','2018-02-13',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Bald Bremmer','09215678901','Father','2025-09-07 22:43:57',NULL),(1138,'Reinald Klehn','PO Box 24669','2016-07-08',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tierney Cornfield','09171234567','Father','2025-09-07 22:43:57',NULL),(1139,'Hayden Ciobotaru','Room 393','2016-08-21',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Garik Soutar','09215678901','Father','2025-09-07 22:43:57',NULL),(1140,'Jamie Haken','Apt 601','2015-12-03',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Mark Hiddersley','09215678901','Child','2025-09-07 22:43:57',NULL),(1141,'Jayme Elleray','9th Floor','2021-01-28',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lazarus Ferrario','09348901234','Friend','2025-09-07 22:43:57',NULL),(1142,'Zarah Jouanny','Suite 70','2025-08-09',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Linda Kemish','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1143,'Hazel Janz','20th Floor','2023-06-07',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Etheline Jaram','09171234567','Father','2025-09-07 22:43:57',NULL),(1144,'Magda Mallett','Suite 53','2016-01-07',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Verina Cartwight','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1145,'Cilka Amerighi','Room 427','2015-05-27',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Babbie McLachlan','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1146,'Hube Tearle','PO Box 17504','2021-08-26',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lanita Oxton','09171234567','Child','2025-09-07 22:43:57',NULL),(1147,'Lurline Howgill','18th Floor','2017-01-25',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Pauli Kwiek','09171234567','Mother','2025-09-07 22:43:57',NULL),(1148,'Andy Byrde','Apt 1988','2017-11-25',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Greta Maplethorpe','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1149,'Evelin Shorter','Room 1320','2016-11-13',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Archibaldo Spragge','09348901234','Friend','2025-09-07 22:43:57',NULL),(1150,'Rickey Alexandrou','Suite 45','2022-04-19',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jerrie Peascod','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1151,'Elspeth Massingberd','Room 1805','2025-01-07',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Giffy Pollins','09215678901','Friend','2025-09-07 22:43:57',NULL),(1152,'Cammie Tattershall','PO Box 32065','2015-02-05',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Cello Hasson','09171234567','Friend','2025-09-07 22:43:57',NULL),(1153,'Fredrika Sheering','Suite 56','2014-12-16',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Bendite Beaven','09171234567','Mother','2025-09-07 22:43:57',NULL),(1154,'Liva Barthelmes','PO Box 29692','2020-05-27',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Dell Batchan','09171234567','Father','2025-09-07 22:43:57',NULL),(1155,'Cyrus Edelman','Apt 647','2015-09-08',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hillary McOmish','09215678901','Friend','2025-09-07 22:43:57',NULL),(1156,'Aurore Kelleher','Room 610','2014-09-25',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Blakeley Taw','09171234567','Mother','2025-09-07 22:43:57',NULL),(1157,'Cass McGriffin','PO Box 38858','2017-12-06',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Adriaens Sidebottom','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1158,'Ara Cearley','16th Floor','2015-04-08',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Zackariah Thickins','09215678901','Father','2025-09-07 22:43:57',NULL),(1159,'Celestyna Sinisbury','Room 607','2023-12-12',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Helen-elizabeth Brugsma','09215678901','Sibling','2025-09-07 22:43:57',NULL),(1160,'Spense Estoile','PO Box 69075','2018-02-07',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hort Sinderson','09215678901','Mother','2025-09-07 22:43:57',NULL),(1161,'Aldus Connors','4th Floor','2020-06-06',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Hastie Chennells','09215678901','Mother','2025-09-07 22:43:57',NULL),(1162,'Rolland Kornilyev','Suite 56','2016-10-23',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Danell Garett','09348901234','Child','2025-09-07 22:43:57',NULL),(1163,'Gilli Edler','16th Floor','2016-08-03',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lindsy Carriage','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1164,'Ferrel Kerridge','Room 1117','2024-06-21',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Germain Haw','09348901234','Friend','2025-09-07 22:43:57',NULL),(1165,'Fanya Nowaczyk','3rd Floor','2017-06-28',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Giffard Bouttell','09171234567','Father','2025-09-07 22:43:57',NULL),(1166,'Ibbie Dumbrill','5th Floor','2017-08-17',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Henriette McCorley','09215678901','Child','2025-09-07 22:43:57',NULL),(1167,'Penelope Djordjevic','Suite 85','2020-09-27',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kizzee Edmunds','09348901234','Mother','2025-09-07 22:43:57',NULL),(1168,'Mala Foskett','Apt 1589','2020-10-20',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Arlen Sollom','09215678901','Mother','2025-09-07 22:43:57',NULL),(1169,'Florencia Dugan','20th Floor','2018-05-28',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Koral Leathart','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1170,'Ollie Tripean','Apt 1927','2023-10-17',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Calla Pessel','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1171,'Dorothea De Atta','Suite 88','2023-11-25',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Nikaniki Bakhrushkin','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1172,'Berk Capron','Room 1234','2018-02-13',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lethia Ericssen','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1173,'Dulcie Stanmer','10th Floor','2016-06-27',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Dagmar Shillabear','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1174,'Marty Withers','4th Floor','2019-08-12',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Philippa Bartleman','09171234567','Father','2025-09-07 22:43:57',NULL),(1175,'Mirabella Nys','Apt 917','2017-06-03',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Englebert Siburn','09215678901','Sibling','2025-09-07 22:43:57',NULL),(1176,'Christabel Simonato','Apt 292','2022-04-24',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kristina Capitano','09215678901','Friend','2025-09-07 22:43:57',NULL),(1177,'Poul McLaine','Apt 505','2022-11-07',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Danette Clac','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1178,'Rodina Ambrogioni','Apt 1545','2025-08-23',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Randene McIlwraith','09171234567','Mother','2025-09-07 22:43:57',NULL),(1179,'Kaila Aurelius','PO Box 39768','2023-05-18',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kin Habbal','09215678901','Mother','2025-09-07 22:43:57',NULL),(1180,'Mariana Pearn','PO Box 85350','2025-07-21',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Melisent Rushe','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1181,'Dianemarie Redborn','Apt 3','2019-04-12',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Norri Farris','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1182,'Coletta Dumbar','16th Floor','2020-12-26',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Padgett Dibdale','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1183,'Micky Roche','PO Box 72402','2022-05-06',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Edyth Pughe','09348901234','Mother','2025-09-07 22:43:57',NULL),(1184,'Alina Simonetto','4th Floor','2020-01-27',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Erwin Allison','09215678901','Child','2025-09-07 22:43:57',NULL),(1185,'Irma Lippett','16th Floor','2021-06-18',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lou Daveley','09215678901','Friend','2025-09-07 22:43:57',NULL),(1186,'Ernaline Muehler','Suite 52','2023-09-07',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Dee Gwatkins','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1187,'Chuck Fockes','Apt 106','2022-12-20',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alta Adamides','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1188,'Aurelie Devigne','PO Box 40477','2021-10-16',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Chariot Quainton','09215678901','Friend','2025-09-07 22:43:57',NULL),(1189,'Malvin Vassbender','PO Box 46739','2014-11-29',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Francis Kopps','09348901234','Father','2025-09-07 22:43:57',NULL),(1190,'Iris Menpes','Suite 2','2024-12-29',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Clifford Maliffe','09215678901','Child','2025-09-07 22:43:57',NULL),(1191,'Jada Philimore','Apt 164','2025-04-19',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Frankie Duncan','09348901234','Father','2025-09-07 22:43:57',NULL),(1192,'Caye Abyss','Suite 71','2018-12-15',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Marie-jeanne Backe','09215678901','Friend','2025-09-07 22:43:57',NULL),(1193,'Geri Cowgill','Apt 309','2016-10-16',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jefferey Geroldi','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1194,'Jsandye Warricker','Suite 4','2018-01-27',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Doralynne Kyffin','09171234567','Friend','2025-09-07 22:43:57',NULL),(1195,'Dukie Licciardiello','Room 1954','2019-07-07',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Willow Strettell','09171234567','Friend','2025-09-07 22:43:57',NULL),(1196,'Cary Crack','Apt 1258','2018-12-02',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lavena Terron','09215678901','Child','2025-09-07 22:43:57',NULL),(1197,'Berri Pestricke','Room 660','2015-10-10',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Wandie Formby','09348901234','Child','2025-09-07 22:43:57',NULL),(1198,'Hilde Angier','Apt 707','2016-09-20',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Loutitia Riccio','09171234567','Child','2025-09-07 22:43:57',NULL),(1199,'Shandie Carter','Room 1533','2023-03-10',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Corrie Gonnard','09348901234','Child','2025-09-07 22:43:57',NULL),(1200,'Mela Mollatt','5th Floor','2016-02-07',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kaja Sindle','09171234567','Child','2025-09-07 22:43:57',NULL),(1201,'Rochette Brunsden','Room 488','2019-11-15',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Sheffield Longo','09348901234','Mother','2025-09-07 22:43:57',NULL),(1202,'Tristan Stegel','2nd Floor','2022-10-29',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Fabe Tarbert','09348901234','Father','2025-09-07 22:43:57',NULL),(1203,'Darbie Gynn','Apt 1064','2018-07-15',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ravi Farthin','09348901234','Father','2025-09-07 22:43:57',NULL),(1204,'Lennard Monahan','Room 430','2021-05-15',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Craggie Paz','09171234567','Child','2025-09-07 22:43:57',NULL),(1205,'Suzette Ventum','PO Box 52576','2019-07-01',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Tybalt Dowson','09215678901','Father','2025-09-07 22:43:57',NULL),(1206,'Noe Grahl','Suite 78','2019-10-29',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Olivette Bende','09171234567','Child','2025-09-07 22:43:57',NULL),(1207,'Winifred Bednall','1st Floor','2023-10-17',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Larine Farrey','09171234567','Child','2025-09-07 22:43:57',NULL),(1208,'Inglebert Duggan','Suite 54','2024-03-20',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lilla Lamp','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1209,'Rock Borman','Room 378','2024-03-23',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Keeley Parish','09215678901','Sibling','2025-09-07 22:43:57',NULL),(1210,'Darla Tretter','PO Box 12945','2020-02-11',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ellynn Dugald','09348901234','Child','2025-09-07 22:43:57',NULL),(1211,'Corrine Guidotti','15th Floor','2017-03-16',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Nellie Mollin','09171234567','Child','2025-09-07 22:43:57',NULL),(1212,'Elli Dyzart','Apt 1517','2021-09-10',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Neala Hackworthy','09348901234','Mother','2025-09-07 22:43:57',NULL),(1213,'Vanni Penswick','PO Box 14877','2021-01-09',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Leroi O\'Flaverty','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1214,'Court Dominicacci','Room 871','2021-04-18',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sarajane Skitterel','09348901234','Mother','2025-09-07 22:43:57',NULL),(1215,'Marj Filewood','18th Floor','2022-10-16',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kailey Drabble','09348901234','Friend','2025-09-07 22:43:57',NULL),(1216,'Mirella Whyte','Apt 1574','2024-12-18',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tucky Malone','09171234567','Child','2025-09-07 22:43:57',NULL),(1217,'Noellyn Beaze','PO Box 94009','2023-01-30',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lindsay Rispen','09348901234','Mother','2025-09-07 22:43:57',NULL),(1218,'Roxanne Twidale','Apt 900','2014-12-26',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Shea Jerosch','09215678901','Father','2025-09-07 22:43:57',NULL),(1219,'Valina Dansey','Apt 734','2023-06-20',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Burl Swinford','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1220,'Helen-elizabeth Shortin','Apt 1017','2020-01-30',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Stacy Drains','09215678901','Friend','2025-09-07 22:43:57',NULL),(1221,'Jae Dutton','Suite 6','2017-11-17',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Merna Mewrcik','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1222,'Padraig Checkley','PO Box 28302','2017-03-11',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Mickey Heggie','09171234567','Father','2025-09-07 22:43:57',NULL),(1223,'Maurits Burnsides','PO Box 6343','2015-11-18',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Elia Gellion','09215678901','Friend','2025-09-07 22:43:57',NULL),(1224,'Tedman Confort','PO Box 80548','2020-08-30',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Nat O\'Doghesty','09215678901','Mother','2025-09-07 22:43:57',NULL),(1225,'Blair Coast','Apt 1889','2020-12-14',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Salaidh Penhale','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1226,'Curtice Longmore','8th Floor','2014-09-18',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Benni Syphus','09215678901','Father','2025-09-07 22:43:57',NULL),(1227,'Tamarra Mallam','Apt 726','2023-07-25',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Myranda Sandal','09215678901','Mother','2025-09-07 22:43:57',NULL),(1228,'Piper Sides','Room 1695','2024-08-12',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dena Tremoille','09215678901','Father','2025-09-07 22:43:57',NULL),(1229,'Colan Legges','Suite 45','2018-03-08',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Orella Niblock','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1230,'Emilio Jenicke','PO Box 72205','2020-10-18',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cathe Wolland','09215678901','Friend','2025-09-07 22:43:57',NULL),(1231,'Aloisia Nicholes','3rd Floor','2023-10-20',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Val Treverton','09348901234','Friend','2025-09-07 22:43:57',NULL),(1232,'Dilly Dutton','PO Box 98664','2022-05-13',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Torrey Banbrook','09171234567','Friend','2025-09-07 22:43:57',NULL),(1233,'Queenie O\'Neal','Suite 25','2023-05-23',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Juliane Sandercock','09348901234','Father','2025-09-07 22:43:57',NULL),(1234,'Josephine Davion','Apt 145','2024-05-26',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Clementina Lamyman','09215678901','Child','2025-09-07 22:43:57',NULL),(1235,'Roarke Cheng','Suite 2','2015-01-07',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Leopold Bates','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1236,'Benedetta Dwyer','2nd Floor','2025-08-24',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Talyah Vernall','09215678901','Father','2025-09-07 22:43:57',NULL),(1237,'Josi Rableau','Apt 164','2016-12-10',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Rochester Leroux','09215678901','Father','2025-09-07 22:43:57',NULL),(1238,'Moses Elsdon','PO Box 60139','2023-05-25',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Tedman Kyngdon','09348901234','Child','2025-09-07 22:43:57',NULL),(1239,'Cecil Kubal','7th Floor','2023-10-18',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Nikolaus Hannis','09348901234','Father','2025-09-07 22:43:57',NULL),(1240,'Willamina Rabbage','Room 1638','2015-10-31',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Anastassia Noteyoung','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1241,'Wilhelmine Greneham','Room 509','2019-03-24',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Brandise Goodyer','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1242,'Maurise Virgoe','Room 1030','2018-02-05',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Maddy Ludgrove','09215678901','Sibling','2025-09-07 22:43:57',NULL),(1243,'Consalve Huggen','PO Box 72979','2024-11-14',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Fayina Burghill','09171234567','Child','2025-09-07 22:43:57',NULL),(1244,'Lynette Stutely','Suite 52','2018-12-02',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Bradan Budgen','09171234567','Child','2025-09-07 22:43:57',NULL),(1245,'Cindee Stiegers','Apt 1779','2022-07-18',3,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Darrel Dumini','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1246,'Marian Readshall','PO Box 50801','2024-02-16',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cody Ullrich','09171234567','Child','2025-09-07 22:43:58',NULL),(1247,'Ode Hardwich','5th Floor','2017-06-12',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Roman Eliasson','09348901234','Mother','2025-09-07 22:43:58',NULL),(1248,'Evanne Asbery','Apt 583','2023-11-20',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Mortimer Alessandone','09215678901','Friend','2025-09-07 22:43:58',NULL),(1249,'Clerc Troppmann','15th Floor','2018-11-16',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sula Cadman','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1250,'Yolanthe Mansour','Apt 31','2015-01-05',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Elane Adrien','09215678901','Father','2025-09-07 22:43:58',NULL),(1251,'Jone Morrott','Room 806','2021-01-30',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Maye Mac Geaney','09215678901','Mother','2025-09-07 22:43:58',NULL),(1252,'Vinnie Groucutt','20th Floor','2016-07-02',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Carmela Rylett','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1253,'Skip McDonogh','PO Box 18997','2016-04-23',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Horacio MacPike','09171234567','Mother','2025-09-07 22:43:58',NULL),(1254,'Thaxter Laming','PO Box 41004','2019-01-24',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Saxon McCullock','09215678901','Friend','2025-09-07 22:43:58',NULL),(1255,'Domeniga La Croce','7th Floor','2014-12-05',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tammie Frazer','09215678901','Child','2025-09-07 22:43:58',NULL),(1256,'Halsey Tolumello','Apt 1136','2022-01-21',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ivory Thireau','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1257,'Conny Frenchum','Room 833','2025-05-05',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cleveland Littrick','09171234567','Child','2025-09-07 22:43:58',NULL),(1258,'Doralynne Frame','Apt 1985','2019-11-17',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Constantine Learmont','09215678901','Friend','2025-09-07 22:43:58',NULL),(1259,'Aida Arpino','Room 1401','2019-05-15',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Agnesse Spittle','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1260,'Humberto Skamell','Room 1225','2022-08-20',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Davey Beeke','09171234567','Friend','2025-09-07 22:43:58',NULL),(1261,'Emmalynn Sreenan','Suite 69','2024-08-01',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ibby Jarman','09171234567','Mother','2025-09-07 22:43:58',NULL),(1262,'Wandis Blaxall','Suite 74','2019-08-19',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Mirella Steanyng','09171234567','Mother','2025-09-07 22:43:58',NULL),(1263,'Hedi Liff','Apt 448','2016-01-16',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ursola Luddy','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1264,'Benton Benoiton','Room 422','2022-07-22',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Florencia Furst','09348901234','Father','2025-09-07 22:43:58',NULL),(1265,'Eloise Meynell','Suite 21','2017-06-29',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Terrijo Southers','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1266,'Edy Macia','Room 1347','2024-09-07',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Remington Meneer','09215678901','Father','2025-09-07 22:43:58',NULL),(1267,'Lefty Heber','2nd Floor','2018-06-23',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ginny Anton','09348901234','Friend','2025-09-07 22:43:58',NULL),(1268,'Mick Gentil','Suite 53','2018-10-20',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ottilie Malter','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1269,'Nadean Birnie','Room 1443','2019-05-14',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Pyotr Jinkins','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1270,'Keely Fasson','Apt 243','2025-03-16',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Warren Kempshall','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1271,'Kay Fleeman','Apt 1268','2017-12-05',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Blondy Belhome','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1272,'Ginni Kingswell','11th Floor','2021-02-17',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Liza Kilkenny','09215678901','Mother','2025-09-07 22:43:58',NULL),(1273,'Randie Domerq','5th Floor','2014-09-20',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Priscella Mersh','09215678901','Friend','2025-09-07 22:43:58',NULL),(1274,'Butch Dollen','PO Box 93786','2022-01-03',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ailene McKean','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1275,'Helga Broinlich','13th Floor','2021-09-02',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lilith Langstaff','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1276,'Trever Shuttlewood','Apt 1182','2025-09-02',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Toby Kleeman','09348901234','Mother','2025-09-07 22:43:58',NULL),(1277,'Pavla Davern','PO Box 53034','2017-01-14',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Thorstein Fillon','09171234567','Friend','2025-09-07 22:43:58',NULL),(1278,'Andeee Nerger','PO Box 60591','2018-01-28',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Farrand Shearme','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1279,'Magdaia Lebbern','Room 3','2015-04-04',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Pavel Muldowney','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1280,'Maddi Dewis','Apt 1687','2018-03-05',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ambrose Collibear','09348901234','Friend','2025-09-07 22:43:58',NULL),(1281,'Sybilla Simioni','Apt 1406','2015-07-29',10,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Betti Langer','09348901234','Father','2025-09-07 22:43:58',NULL),(1282,'Loree Yitzhakov','Apt 1338','2020-03-18',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Bridgette Crasford','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1283,'Bobette Hurcombe','PO Box 9259','2023-08-18',2,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sallyanne Union','09171234567','Friend','2025-09-07 22:43:58',NULL),(1284,'Niels Helling','Suite 49','2021-02-09',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Alejandrina Bruyntjes','09215678901','Friend','2025-09-07 22:43:58',NULL),(1285,'Ramona Spragg','6th Floor','2025-05-31',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Camila Tregear','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1286,'Laurena Kellock','Suite 79','2024-06-07',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Costa Warin','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1287,'Chloris Blagburn','PO Box 53460','2021-12-22',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cheri Phateplace','09171234567','Mother','2025-09-07 22:43:58',NULL),(1288,'Stacy Waind','Suite 13','2022-05-08',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Matthus Simonetti','09215678901','Friend','2025-09-07 22:43:58',NULL),(1289,'Leora Vasyutichev','Apt 1223','2024-05-12',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Mersey Folder','09215678901','Friend','2025-09-07 22:43:58',NULL),(1290,'Arliene Pinar','Suite 11','2019-03-07',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Patrizia Reddel','09171234567','Father','2025-09-07 22:43:58',NULL),(1291,'Aggi Lethley','PO Box 96507','2016-02-04',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Had Ridpath','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1292,'Dorene Gellier','Suite 2','2024-06-20',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Theodosia Haffard','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1293,'Saw Pointin','Suite 8','2021-12-15',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Salomone Snuggs','09348901234','Child','2025-09-07 22:43:58',NULL),(1294,'Bea Gealle','Suite 57','2021-01-07',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Meredeth Haddeston','09171234567','Child','2025-09-07 22:43:58',NULL),(1295,'Dottie Jehaes','PO Box 79868','2018-10-07',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tabbi Sharrem','09215678901','Father','2025-09-07 22:43:58',NULL),(1296,'Brocky King','Apt 1273','2016-08-03',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lenka Chamberlayne','09348901234','Mother','2025-09-07 22:43:58',NULL),(1297,'Barry Cloney','Room 1949','2024-08-14',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Pip Lawden','09171234567','Child','2025-09-07 22:43:58',NULL),(1298,'Celestia Drillingcourt','PO Box 97130','2018-12-28',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Sianna Iacovolo','09215678901','Father','2025-09-07 22:43:58',NULL),(1299,'Gardener Franckton','17th Floor','2019-07-30',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Devy Grover','09348901234','Child','2025-09-07 22:43:58',NULL),(1300,'Justina Dalley','Apt 1001','2017-12-17',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Alfred Sloley','09215678901','Child','2025-09-07 22:43:58',NULL),(1301,'Orelee Kaesmakers','Room 1354','2015-08-11',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Dane Martinec','09348901234','Child','2025-09-07 22:43:58',NULL),(1302,'Rosene People','Room 1850','2022-02-02',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Reinaldo Arnoud','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1303,'Elizabeth Coot','2nd Floor','2020-07-15',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Pierson Womersley','09348901234','Child','2025-09-07 22:43:58',NULL),(1304,'Raimondo Hanham','Room 1367','2025-05-18',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Barbabas Hambly','09348901234','Friend','2025-09-07 22:43:58',NULL),(1305,'Corenda McMichan','PO Box 10926','2022-05-09',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Demetria Dober','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1306,'Modesty Darrington','Room 1008','2023-03-25',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Halette Botterill','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1307,'Angie Ruppel','Suite 63','2023-06-28',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Sada Ivanets','09171234567','Father','2025-09-07 22:43:58',NULL),(1308,'Carrie Searchfield','Apt 1721','2016-07-13',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Raychel Switsur','09171234567','Child','2025-09-07 22:43:58',NULL),(1309,'Ricky Boorman','Apt 1741','2014-10-11',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jereme Bulfoy','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1310,'Pietro Wong','Suite 17','2021-02-07',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Iorgos Malloch','09348901234','Mother','2025-09-07 22:43:58',NULL),(1311,'Tamarra Culshaw','14th Floor','2023-08-04',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rosalyn Jakes','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1312,'Livvy Birkin','6th Floor','2015-04-26',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Essie Davidovich','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1313,'Blondy Tynewell','Suite 34','2022-01-12',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Opalina Forrestor','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1314,'Lilly Siggin','1st Floor','2020-04-10',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Virge French','09215678901','Friend','2025-09-07 22:43:58',NULL),(1315,'Edeline Kinchlea','13th Floor','2016-11-19',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Tobit Gozzard','09348901234','Father','2025-09-07 22:43:58',NULL),(1316,'Isa Kayne','Suite 49','2024-02-14',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Nikita Brevetor','09215678901','Mother','2025-09-07 22:43:58',NULL),(1317,'Odessa Nisbith','Suite 51','2025-08-05',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rouvin Hulks','09171234567','Father','2025-09-07 22:43:58',NULL),(1318,'Junette Beacom','Room 1967','2016-01-06',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ingeberg Donlon','09171234567','Father','2025-09-07 22:43:58',NULL),(1319,'Ardenia Lunbech','Suite 15','2025-01-15',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Sharona O\'Griffin','09215678901','Child','2025-09-07 22:43:58',NULL),(1320,'Dot Brian','PO Box 20810','2024-05-29',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kris Bartolomucci','09348901234','Friend','2025-09-07 22:43:58',NULL),(1321,'Jemmie Ganniclifft','Room 1872','2015-11-25',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Christyna Chamberlen','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1322,'Tana Degoy','16th Floor','2016-08-28',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Erick Jane','09348901234','Friend','2025-09-07 22:43:58',NULL),(1323,'Francklin Golde','Apt 210','2016-08-25',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Catlin Hakonsson','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1324,'Dasha Fluck','Suite 71','2016-07-31',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Wald Gunningham','09171234567','Friend','2025-09-07 22:43:58',NULL),(1325,'Findlay De Simone','Suite 25','2018-10-27',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Madonna Terney','09171234567','Child','2025-09-07 22:43:58',NULL),(1326,'Delores Toplis','11th Floor','2016-02-20',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ruthanne Dimmock','09348901234','Friend','2025-09-07 22:43:58',NULL),(1327,'Blakeley Dalgardno','Apt 1499','2025-01-27',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ardelis Storrs','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1328,'Jennifer Kelson','Room 420','2015-04-21',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ree McClay','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1329,'Sadella Tonry','15th Floor','2020-02-18',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Odell Minihan','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1330,'Rubie Gingles','PO Box 73867','2018-12-12',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Nalani Credland','09215678901','Friend','2025-09-07 22:43:58',NULL),(1331,'Cheslie Fero','Apt 265','2016-06-06',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Leonardo Matura','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1332,'Adrianna Beldham','Apt 313','2019-10-14',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Andi Jirusek','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1333,'Sydel Luthwood','PO Box 42754','2023-05-13',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Linda Foston','09171234567','Friend','2025-09-07 22:43:58',NULL),(1334,'Daveta Woolam','2nd Floor','2017-08-18',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Fax Gillespie','09171234567','Friend','2025-09-07 22:43:58',NULL),(1335,'Kellen Revely','19th Floor','2016-11-18',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Megen Outhwaite','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1336,'Ursa Kloisner','PO Box 33056','2016-03-17',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Clarabelle Colthurst','09215678901','Child','2025-09-07 22:43:58',NULL),(1337,'Diane Rolf','Suite 59','2016-09-26',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Laetitia Coviello','09215678901','Mother','2025-09-07 22:43:58',NULL),(1338,'Lina Likly','Apt 225','2023-12-29',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kelbee Livingston','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1339,'Melania Crowcher','Room 1446','2015-10-25',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Robb Bromby','09348901234','Father','2025-09-07 22:43:58',NULL),(1340,'Darin Antonelli','Apt 324','2022-03-01',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lucian Boobyer','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1341,'Phylys Grange','Room 1389','2022-01-07',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Julieta Goddard','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1342,'Rickie Lilford','5th Floor','2023-02-17',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Adelind Landrick','09171234567','Father','2025-09-07 22:43:58',NULL),(1343,'Kerri Scay','PO Box 46650','2017-04-13',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Archaimbaud Peerless','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1344,'La verne Benfell','PO Box 34737','2020-05-25',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Irita Hanes','09215678901','Mother','2025-09-07 22:43:58',NULL),(1345,'Roi Biss','Apt 170','2025-03-02',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lorenzo Treske','09215678901','Child','2025-09-07 22:43:58',NULL),(1346,'Addy Heffernan','PO Box 5806','2015-10-03',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Krysta Jandera','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1347,'Marla Andino','Apt 602','2015-10-11',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Camila Ridgers','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1348,'Kirby Dymond','14th Floor','2022-12-09',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Bella Goulthorp','09215678901','Friend','2025-09-07 22:43:58',NULL),(1349,'Doretta Delgadillo','1st Floor','2016-06-14',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Tatiania De Lorenzo','09215678901','Mother','2025-09-07 22:43:58',NULL),(1350,'Gwenni Nelson','Room 1492','2016-03-11',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Elston Becconsall','09171234567','Friend','2025-09-07 22:43:58',NULL),(1351,'Adams Tate','16th Floor','2020-09-23',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Maryjo Corradetti','09348901234','Child','2025-09-07 22:43:58',NULL),(1352,'Steffi Sandhill','Suite 55','2025-04-23',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','August Guillain','09215678901','Child','2025-09-07 22:43:58',NULL),(1353,'Avigdor Edeler','13th Floor','2019-01-25',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Clemmy Grimwade','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1354,'Marie-ann Paffett','Apt 1565','2018-03-24',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Reed Urry','09348901234','Mother','2025-09-07 22:43:58',NULL),(1355,'Galvan Josland','Suite 46','2023-05-15',2,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Harrie Le Friec','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1356,'Karly Balharrie','Apt 733','2017-06-02',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Philippine Kassel','09215678901','Child','2025-09-07 22:43:58',NULL),(1357,'Daryl Garber','PO Box 97991','2020-12-05',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Eldredge Ghiroldi','09171234567','Friend','2025-09-07 22:43:58',NULL),(1358,'Chrissie Piccop','Suite 47','2021-07-26',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Edan Woolbrook','09348901234','Mother','2025-09-07 22:43:58',NULL),(1359,'Vita Jenkyn','Room 671','2017-01-27',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Freddie Loads','09171234567','Child','2025-09-07 22:43:58',NULL),(1360,'Norbie Acreman','PO Box 89754','2024-05-06',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Mill Haston','09171234567','Child','2025-09-07 22:43:58',NULL),(1361,'Thomasine Verryan','Room 1694','2024-08-11',1,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Florina Thornber','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1362,'Nerita Goard','Suite 25','2023-09-23',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Gaven Doret','09348901234','Child','2025-09-07 22:43:58',NULL),(1363,'Judon Conroy','PO Box 67730','2015-11-30',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kathleen Bachura','09215678901','Child','2025-09-07 22:43:58',NULL),(1364,'Madalena Sibery','13th Floor','2025-02-07',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Cozmo Gilbey','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1365,'Ron Restill','Apt 1866','2024-05-22',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Gray Kringe','09171234567','Mother','2025-09-07 22:43:58',NULL),(1366,'Francesca McGuiness','Apt 530','2024-02-20',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Hendrick Hurdiss','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1367,'Avie Minett','Room 491','2018-10-16',6,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kaye Rivilis','09171234567','Mother','2025-09-07 22:43:58',NULL),(1368,'Alex Crowcher','PO Box 53800','2016-12-05',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lin Eves','09348901234','Mother','2025-09-07 22:43:58',NULL),(1369,'Sibeal Samett','Suite 67','2022-08-11',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Antonius Juschke','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1370,'Michelina Baggelley','Apt 1388','2016-07-25',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Isak Guinn','09171234567','Child','2025-09-07 22:43:58',NULL),(1371,'Viviana Stennes','Suite 98','2017-12-12',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ardath Dunnet','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1372,'Gretchen Allridge','Suite 24','2016-10-22',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Wyn Howitt','09171234567','Child','2025-09-07 22:43:58',NULL),(1373,'Leda Nannizzi','Suite 19','2014-11-14',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hildagard Manuel','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1374,'Alec Lamkin','Suite 71','2021-06-19',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ardra Jerdein','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1375,'Salaidh Alvares','Apt 505','2022-09-18',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jeremiah Fluger','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1376,'Kaila Beumant','PO Box 71263','2015-07-11',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Brendon Lecordier','09171234567','Mother','2025-09-07 22:43:58',NULL),(1377,'Had Wilmot','PO Box 67262','2014-12-19',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Bari Bowcock','09171234567','Father','2025-09-07 22:43:58',NULL),(1378,'Loella Dukesbury','13th Floor','2015-11-01',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Tremain Ditter','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1379,'Amara Andrew','Room 1609','2015-06-01',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Chad Kayley','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1380,'Trevar Ahern','PO Box 92981','2022-04-08',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Hesther Addison','09215678901','Mother','2025-09-07 22:43:58',NULL),(1381,'Danny Dyson','Room 345','2016-07-28',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Karlene Juliano','09215678901','Friend','2025-09-07 22:43:58',NULL),(1382,'Devon Fardon','PO Box 94966','2019-04-26',6,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kattie Ould','09348901234','Father','2025-09-07 22:43:58',NULL),(1383,'Sarette Rubenczyk','11th Floor','2018-07-25',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Marty De La Salle','09215678901','Father','2025-09-07 22:43:58',NULL),(1384,'Foss Bizzey','PO Box 53681','2017-12-07',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Clementina L\'Archer','09215678901','Friend','2025-09-07 22:43:58',NULL),(1385,'Juditha Doubleday','PO Box 62635','2016-08-26',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Berty Heard','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1386,'Regine McCaughan','Room 17','2019-07-31',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gui Loton','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1387,'Imelda Ollenbuttel','9th Floor','2016-08-01',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cooper Bartak','09215678901','Father','2025-09-07 22:43:58',NULL),(1388,'Wynn Oglesbee','PO Box 57222','2025-03-21',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jill Toghill','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1389,'Marlie Fielders','Apt 359','2017-04-12',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Derron Baile','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1390,'Lucais Handlin','PO Box 35118','2024-05-01',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kyle Ludovici','09215678901','Father','2025-09-07 22:43:58',NULL),(1391,'Cordy Pellman','Suite 36','2018-10-05',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rosalynd Gurnell','09215678901','Mother','2025-09-07 22:43:58',NULL),(1392,'Bernie Gonnel','Apt 1835','2019-05-08',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Adriane Teek','09171234567','Friend','2025-09-07 22:43:58',NULL),(1393,'Shanna Klaessen','6th Floor','2015-06-20',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Maryrose Gullam','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1394,'Tabby Kippins','PO Box 48347','2020-06-30',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Genny Wheeliker','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1395,'Lazare Ricardo','8th Floor','2021-09-30',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gardner Slyme','09348901234','Mother','2025-09-07 22:43:58',NULL),(1396,'Barclay Keunemann','20th Floor','2021-01-18',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Asher Shiels','09348901234','Mother','2025-09-07 22:43:58',NULL),(1397,'Vere Warmisham','Apt 1445','2024-08-19',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Alta Husband','09348901234','Father','2025-09-07 22:43:58',NULL),(1398,'Callie Plewman','Apt 40','2023-11-25',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Natalina Caine','09348901234','Friend','2025-09-07 22:43:58',NULL),(1399,'Holmes Godspeede','12th Floor','2017-08-12',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Griffith Greetland','09215678901','Mother','2025-09-07 22:43:58',NULL),(1400,'Kit Inchbald','14th Floor','2024-07-22',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Yule Collingworth','09171234567','Child','2025-09-07 22:43:58',NULL),(1401,'Veronica Swithenby','Room 1280','2017-07-17',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Eliza Albisser','09171234567','Child','2025-09-07 22:43:58',NULL),(1402,'Alvie Dumbrall','Suite 21','2014-10-08',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Trever Coles','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1403,'Jedidiah Edworthie','Suite 12','2017-11-24',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dani Miguet','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1404,'Katti Musterd','Room 1655','2015-12-09',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Denny Bercher','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1405,'Kermy Penley','19th Floor','2020-05-18',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Bennie O\'Neil','09215678901','Father','2025-09-07 22:43:58',NULL),(1406,'Jessica Jendrich','Suite 89','2015-05-05',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Hillier Unger','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1407,'Michaelina Zamora','16th Floor','2021-11-18',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Austin Chomicki','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1408,'Parry Tretwell','4th Floor','2016-08-31',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Annabal Slayford','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1409,'Egan Bradock','Apt 1409','2022-05-16',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Amalie O\'Teague','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1410,'Dela Bromige','Room 1892','2021-06-15',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lorie Jockle','09215678901','Father','2025-09-07 22:43:58',NULL),(1411,'Jacob Tubble','PO Box 60555','2024-02-27',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Brandy Snell','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1412,'Ivonne Gregoletti','Suite 20','2025-01-10',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Alex Vanin','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1413,'Dun Impy','Apt 554','2015-04-12',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kristyn Kersaw','09171234567','Mother','2025-09-07 22:43:58',NULL),(1414,'Muire Milkeham','Suite 82','2017-08-15',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rice Kipping','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1415,'Alexandros Sell','Room 601','2015-06-15',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kiele Seavers','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1416,'Corny Flaxman','Apt 1554','2020-10-13',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Alexa Menichi','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1417,'Arlena Beyne','19th Floor','2021-06-01',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alfonso Meatyard','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1418,'Dedra Harradine','PO Box 82610','2022-01-01',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Junette Gladhill','09215678901','Friend','2025-09-07 22:43:58',NULL),(1419,'Worth Hartford','Apt 748','2023-11-08',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sunny Foat','09215678901','Mother','2025-09-07 22:43:58',NULL),(1420,'Reidar Pinner','Suite 95','2021-02-22',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Alano Yegorovnin','09171234567','Mother','2025-09-07 22:43:58',NULL),(1421,'Agathe McAneny','Suite 41','2025-03-23',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Dahlia Bramstom','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1422,'Benedicto Michallat','Suite 95','2015-03-05',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Carmita Tomney','09348901234','Mother','2025-09-07 22:43:58',NULL),(1423,'Harman Lutzmann','Apt 1239','2020-12-21',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Bealle Swynley','09171234567','Father','2025-09-07 22:43:58',NULL),(1424,'Brier Van der Kruijs','5th Floor','2017-10-01',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Inglebert Gimblett','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1425,'Sunny Rafe','PO Box 71926','2021-04-27',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gerick Balderson','09348901234','Friend','2025-09-07 22:43:58',NULL),(1426,'Stefanie Merigon','14th Floor','2021-11-10',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Errol Caulcott','09171234567','Father','2025-09-07 22:43:58',NULL),(1427,'Caleb Grece','Room 1782','2021-01-22',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alayne Casone','09215678901','Friend','2025-09-07 22:43:58',NULL),(1428,'Gerard McIllroy','Room 874','2018-09-23',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Julita Dunaway','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1429,'Idette Mosedall','Apt 702','2016-02-24',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Micheil Styant','09348901234','Child','2025-09-07 22:43:58',NULL),(1430,'Ingmar Keddey','Apt 619','2015-01-18',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lauren Taile','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1431,'Orren Dallosso','Apt 1912','2019-02-07',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Aurora Kibbel','09348901234','Mother','2025-09-07 22:43:58',NULL),(1432,'Perrine Eaglesham','PO Box 13466','2021-10-20',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ardisj Crotty','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1433,'Trev Atteridge','Room 597','2025-02-08',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Briana Sainsberry','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1434,'Adi Coggin','Apt 1076','2021-08-27',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dodie Fencott','09215678901','Child','2025-09-07 22:43:58',NULL),(1435,'Tammie Marguerite','4th Floor','2024-04-13',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Robbi Kinnane','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1436,'Meir Stitch','16th Floor','2022-12-31',2,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Biddie De Pietri','09171234567','Mother','2025-09-07 22:43:58',NULL),(1437,'Wilburt Dunkley','Room 970','2021-06-15',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Trevar Starford','09171234567','Child','2025-09-07 22:43:58',NULL),(1438,'Beatriz Bessent','PO Box 46280','2025-07-07',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alphard Geggie','09171234567','Father','2025-09-07 22:43:58',NULL),(1439,'Sibilla Peplay','Room 402','2020-02-01',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Zaccaria Feathers','09348901234','Mother','2025-09-07 22:43:58',NULL),(1440,'Emelyne Sandey','Apt 1668','2025-08-15',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gannie Larsen','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1441,'Lou Luckwell','Suite 58','2019-09-09',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Nisse Iverson','09215678901','Mother','2025-09-07 22:43:58',NULL),(1442,'Geoff Benedicte','Room 613','2015-09-15',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Any Bein','09348901234','Friend','2025-09-07 22:43:58',NULL),(1443,'Aylmer McCallister','PO Box 9348','2017-05-19',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Briney Rickesies','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1444,'Torry Rimington','Room 320','2020-11-06',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Germain Tong','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1445,'Freddy Salman','PO Box 82278','2021-12-02',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Josy Stapele','09215678901','Child','2025-09-07 22:43:58',NULL),(1446,'Dimitri Greed','Apt 99','2024-12-31',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Zabrina Handrok','09348901234','Child','2025-09-07 22:43:58',NULL),(1447,'Eustace Vurley','15th Floor','2020-06-15',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Megen Babalola','09215678901','Mother','2025-09-07 22:43:58',NULL),(1448,'Any Cicchetto','PO Box 55751','2018-10-28',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Sinclair Unitt','09348901234','Mother','2025-09-07 22:43:58',NULL),(1449,'Christal Kadd','PO Box 13083','2018-10-11',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Brita Corness','09215678901','Child','2025-09-07 22:43:58',NULL),(1450,'Frankie Olufsen','Suite 54','2024-09-13',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Roderick Tolcher','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1451,'Jeddy Whyley','PO Box 19912','2018-05-30',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Anthiathia Fitzgerald','09171234567','Mother','2025-09-07 22:43:58',NULL),(1452,'Natal Hoofe','Apt 305','2017-04-09',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Valaria Oleszczak','09215678901','Mother','2025-09-07 22:43:58',NULL),(1453,'Holli Stoner','Apt 906','2018-02-15',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Giulia Braz','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1454,'Agosto Dametti','PO Box 24798','2018-10-25',6,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cathe Hardiman','09171234567','Father','2025-09-07 22:43:58',NULL),(1455,'Drew Donaldson','Room 767','2018-07-23',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kiel Lopez','09348901234','Child','2025-09-07 22:43:58',NULL),(1456,'Gwyn Misson','Suite 94','2021-12-21',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Gustavus Hawton','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1457,'Damara Youles','11th Floor','2018-10-05',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Klara Gard','09348901234','Father','2025-09-07 22:43:58',NULL),(1458,'Stepha Powling','Apt 918','2023-05-02',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lelia Mifflin','09215678901','Mother','2025-09-07 22:43:58',NULL),(1459,'Doti Scrancher','Apt 1670','2024-04-18',1,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cordy Brower','09215678901','Child','2025-09-07 22:43:58',NULL),(1460,'Matty Hencke','Suite 44','2020-02-15',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Timmy Sherrin','09171234567','Friend','2025-09-07 22:43:58',NULL),(1461,'Emilee Aloshikin','PO Box 49084','2015-03-23',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Briant Quenby','09215678901','Friend','2025-09-07 22:43:58',NULL),(1462,'Marcia Trippack','PO Box 89190','2022-02-22',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Opaline Widmoor','09171234567','Child','2025-09-07 22:43:58',NULL),(1463,'Adrianna Jeves','7th Floor','2019-10-29',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Timmy Jahnel','09171234567','Child','2025-09-07 22:43:58',NULL),(1464,'Tallia Ross','11th Floor','2023-07-02',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Dunn Cornford','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1465,'Lana Maplethorpe','Suite 56','2019-11-26',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Gordon Mawby','09215678901','Father','2025-09-07 22:43:58',NULL),(1466,'Darrel Keigher','Apt 433','2024-01-21',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alane Dunguy','09171234567','Friend','2025-09-07 22:43:58',NULL),(1467,'Melissa Bachmann','Suite 75','2016-03-03',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kevan Buer','09348901234','Father','2025-09-07 22:43:58',NULL),(1468,'Mallory Normavell','PO Box 44789','2020-06-30',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Elvira Jago','09348901234','Child','2025-09-07 22:43:58',NULL),(1469,'Brianne Murton','19th Floor','2015-12-30',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Dori Cayle','09171234567','Friend','2025-09-07 22:43:58',NULL),(1470,'Garald Jakubovicz','Suite 6','2018-03-30',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Matthew Duddan','09171234567','Father','2025-09-07 22:43:58',NULL),(1471,'Illa Slatter','Apt 510','2019-06-18',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Toma Burnet','09215678901','Mother','2025-09-07 22:43:58',NULL),(1472,'Tilda Le Barr','19th Floor','2021-05-30',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Aloisia Ledbury','09171234567','Mother','2025-09-07 22:43:58',NULL),(1473,'Gus Staries','Room 904','2017-12-18',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kip Commusso','09171234567','Mother','2025-09-07 22:43:58',NULL),(1474,'Brandea Ors','Room 560','2020-05-04',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Charmian Zavattiero','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1475,'Ketty Skirven','Room 357','2019-11-27',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Marla Romanet','09348901234','Child','2025-09-07 22:43:58',NULL),(1476,'Eleanore Pechet','Room 1495','2018-03-01',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Dasha Climpson','09348901234','Father','2025-09-07 22:43:58',NULL),(1477,'Lilas Magrane','Apt 863','2022-11-10',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Merlina Cases','09215678901','Mother','2025-09-07 22:43:58',NULL),(1478,'Elwood Cursey','PO Box 64337','2015-08-22',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Gaven Vasilchikov','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1479,'Alejoa Lenox','16th Floor','2021-05-03',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Mireielle Tippler','09215678901','Friend','2025-09-07 22:43:58',NULL),(1480,'Dulcie Botwright','Suite 14','2018-12-18',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Trixy Boler','09215678901','Father','2025-09-07 22:43:58',NULL),(1481,'Julee Scotchmoor','Apt 733','2015-11-23',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rhett Bedinn','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1482,'Pietrek Glasscoe','14th Floor','2022-10-25',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Cathleen Battram','09215678901','Father','2025-09-07 22:43:58',NULL),(1483,'Danice Caulkett','Suite 83','2018-06-08',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Liane Parsley','09348901234','Father','2025-09-07 22:43:58',NULL),(1484,'Philippe Redhead','Room 1073','2018-08-13',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Karney Fewkes','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1485,'Celina Dayer','Apt 239','2018-12-08',6,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Honoria Roddie','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1486,'Timoteo Normanville','Room 650','2021-04-21',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Corissa Satterfitt','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1487,'Wendy Kincla','PO Box 2429','2024-10-13',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Francisco Gildersleeve','09215678901','Mother','2025-09-07 22:43:58',NULL),(1488,'Maressa Wallen','Room 1490','2018-04-08',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Dom Dummigan','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1489,'Misha Piwall','17th Floor','2025-05-28',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Nellie Uwins','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1490,'Marlo Ivakhno','PO Box 35606','2024-06-27',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jesselyn Tivenan','09348901234','Mother','2025-09-07 22:43:58',NULL),(1491,'Aurelea Jandak','Room 1679','2020-06-16',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Horatio Tomczykowski','09215678901','Friend','2025-09-07 22:43:58',NULL),(1492,'Nydia Ewin','Room 1742','2019-12-21',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Clemence Basson','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1493,'Donetta Kenward','8th Floor','2023-07-09',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Trudi McFfaden','09348901234','Mother','2025-09-07 22:43:58',NULL),(1494,'Rivkah Evert','Apt 460','2016-01-31',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Marylynne Carsey','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1495,'Miles Kitto','Apt 1102','2021-09-21',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Timoteo Quinney','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1496,'Netta Kirkhouse','Room 1244','2023-02-02',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Maryann Barwise','09215678901','Father','2025-09-07 22:43:58',NULL),(1497,'Leila Whewill','Suite 25','2023-10-18',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Tine Bolland','09171234567','Child','2025-09-07 22:43:58',NULL),(1498,'Oswald Gronous','14th Floor','2018-04-06',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Fields Lathbury','09171234567','Friend','2025-09-07 22:43:58',NULL),(1499,'Ferrell Belcham','19th Floor','2019-07-28',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Scott Jeanequin','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1500,'Wylie Heber','Room 823','2020-04-16',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ingram Maplethorpe','09171234567','Child','2025-09-07 22:43:58',NULL),(1501,'Arie Liddington','PO Box 78922','2021-05-26',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dionysus Hadkins','09348901234','Child','2025-09-07 22:43:58',NULL),(1502,'Bradney Wandrich','Room 662','2016-10-23',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dirk Verrills','09348901234','Mother','2025-09-07 22:43:58',NULL),(1503,'Dari Frew','PO Box 32767','2024-08-19',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Franciskus Kaplin','09348901234','Father','2025-09-07 22:43:58',NULL),(1504,'Fawnia Winspeare','Apt 491','2021-04-25',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Nancee Bisp','09348901234','Child','2025-09-07 22:43:58',NULL),(1505,'Symon Chsteney','18th Floor','2023-01-27',2,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Anny Daugherty','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1506,'Janene Bottoner','17th Floor','2018-11-02',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Joya Nowakowski','09348901234','Friend','2025-09-07 22:43:58',NULL),(1507,'Had Iron','Suite 78','2018-11-11',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Chlo Pellett','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1508,'Eleen Snap','Room 903','2019-06-24',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Brinn Behr','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1509,'Arnoldo Redsull','Suite 49','2024-01-01',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Karlan Swannick','09171234567','Father','2025-09-07 22:43:58',NULL),(1510,'Erin Copsey','Suite 100','2022-09-03',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Bambi Farebrother','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1511,'Garald Swatradge','Apt 1167','2019-06-27',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Roosevelt Ginity','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1512,'Melodie Laycock','Room 1303','2016-02-20',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Granthem Staden','09215678901','Mother','2025-09-07 22:43:58',NULL),(1513,'Raoul Oxe','PO Box 26911','2016-11-26',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jeffie Rudloff','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1514,'Corene Gumn','PO Box 58750','2019-12-23',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Melva Henri','09171234567','Father','2025-09-07 22:43:58',NULL),(1515,'Rafferty Sherrard','PO Box 74838','2019-04-04',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Grantley MacLise','09348901234','Friend','2025-09-07 22:43:58',NULL),(1516,'Ginny Haversum','Room 1296','2017-09-28',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Valentina Jayme','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1517,'Valentino McMorland','Suite 22','2016-02-29',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Reidar Beahan','09171234567','Child','2025-09-07 22:43:58',NULL),(1518,'Minetta Gorler','Suite 46','2017-03-15',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Pattie Menelaws','09348901234','Mother','2025-09-07 22:43:58',NULL),(1519,'Jonis Pyle','PO Box 54363','2023-07-01',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Leonidas Ridley','09171234567','Child','2025-09-07 22:43:58',NULL),(1520,'Betty Heyfield','PO Box 57254','2017-10-06',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Petr MacAllister','09171234567','Mother','2025-09-07 22:43:58',NULL),(1521,'Damian Luby','Suite 88','2018-12-04',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Crin Lidgley','09215678901','Mother','2025-09-07 22:43:58',NULL),(1522,'Gal Kennifick','PO Box 73082','2018-01-16',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Caria Domini','09215678901','Mother','2025-09-07 22:43:58',NULL),(1523,'Currey McCarrell','PO Box 30595','2014-11-24',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Bourke Sawley','09348901234','Mother','2025-09-07 22:43:58',NULL),(1524,'Chancey Brantzen','Room 1622','2019-01-28',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Russell Fairbrother','09171234567','Mother','2025-09-07 22:43:58',NULL),(1525,'Claretta Tighe','Suite 40','2023-01-27',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Gerrie Hemshall','09215678901','Mother','2025-09-07 22:43:58',NULL),(1526,'Thorin Snowdon','PO Box 89673','2021-04-21',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Aundrea Prue','09215678901','Mother','2025-09-07 22:43:58',NULL),(1527,'Zach Bithany','7th Floor','2015-04-05',10,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Al Pearch','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1528,'Hanna Basden','PO Box 46880','2020-08-21',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Corey Grattan','09171234567','Child','2025-09-07 22:43:58',NULL),(1529,'Laney Ragg','Apt 1647','2016-04-27',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kendre Rama','09215678901','Father','2025-09-07 22:43:58',NULL),(1530,'Brittany Buzzing','Room 1168','2017-09-13',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Umberto Larrosa','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1531,'Sisely Pointer','Room 180','2021-08-19',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jobie McLinden','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1532,'Cristal Cookman','10th Floor','2022-07-13',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Sanders Masseo','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1533,'Torie McCollum','17th Floor','2015-07-19',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Dael Hasley','09348901234','Child','2025-09-07 22:43:58',NULL),(1534,'Tami Smeeton','Room 1421','2020-11-13',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Laurianne Rabjohn','09348901234','Mother','2025-09-07 22:43:58',NULL),(1535,'Charmian Astman','13th Floor','2017-12-31',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Fanny Berkowitz','09171234567','Friend','2025-09-07 22:43:58',NULL),(1536,'Tabbitha Dietz','1st Floor','2016-04-26',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gerrard Cornelius','09171234567','Friend','2025-09-07 22:43:58',NULL),(1537,'Broderick Danielot','20th Floor','2016-01-15',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Marcelo Compson','09348901234','Friend','2025-09-07 22:43:58',NULL),(1538,'Parrnell Leidl','Room 1513','2020-04-03',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Bruce Antwis','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1539,'Jerrome Sauvain','PO Box 91480','2017-02-16',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Joseph O\'Mohun','09348901234','Father','2025-09-07 22:43:58',NULL),(1540,'Kaleb Pell','Suite 56','2021-10-14',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Bell Terbeck','09171234567','Child','2025-09-07 22:43:58',NULL),(1541,'Syd Caswall','Apt 310','2020-02-11',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sarene Charlewood','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1542,'Deloria Gooble','Suite 89','2022-07-21',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Valentia Boother','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1543,'Jemima Littrick','Suite 51','2020-08-28',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Falito Craney','09348901234','Father','2025-09-07 22:43:58',NULL),(1544,'Laurie Shippey','PO Box 17927','2022-03-27',3,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Farris Orr','09348901234','Child','2025-09-07 22:43:58',NULL),(1545,'Tim Gonzalo','Apt 1278','2020-03-03',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Crysta Beardow','09348901234','Friend','2025-09-07 22:43:58',NULL),(1546,'Dina Norville','Apt 1951','2018-02-11',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Prent Vayro','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1547,'Hilario Jarvie','17th Floor','2019-11-06',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jennica Butchart','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1548,'Lothario Sudran','Suite 85','2022-05-08',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Alissa Watting','09215678901','Father','2025-09-07 22:43:58',NULL),(1549,'Angelo Wagner','Room 814','2021-09-11',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Dorthy Keitch','09348901234','Friend','2025-09-07 22:43:58',NULL),(1550,'Quent Twoohy','Suite 5','2024-06-19',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jorgan Stonier','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1551,'Murial Doxsey','14th Floor','2022-06-10',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Stanleigh Dundridge','09171234567','Mother','2025-09-07 22:43:58',NULL),(1552,'Etan Attride','7th Floor','2015-05-26',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Clifford Asty','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1553,'Devlin Worcs','Apt 1881','2016-06-01',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ofelia Garrigan','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1554,'Rosabella Emilien','Room 1741','2025-07-29',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Sorcha Ayer','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1555,'Sybil Swinley','Apt 1545','2024-12-07',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Joella Gumm','09348901234','Child','2025-09-07 22:43:58',NULL),(1556,'Benson Brooksbie','1st Floor','2016-10-28',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Hortensia Conaghy','09348901234','Father','2025-09-07 22:43:58',NULL),(1557,'Levon Trenholm','4th Floor','2018-09-08',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Bertie Scoyne','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1558,'Ludovico Moffat','Room 232','2015-11-04',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Marietta Hounsom','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1559,'Alexandros Willshear','Suite 95','2020-02-20',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lorettalorna Threadgill','09171234567','Mother','2025-09-07 22:43:58',NULL),(1560,'Purcell Grayland','PO Box 39604','2017-06-21',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Carmelia Rivelon','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1561,'Bone Tanswell','Apt 173','2018-08-16',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Allister Morrant','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1562,'Ashlen De Cleyne','Suite 47','2024-05-16',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Allan Decruse','09171234567','Father','2025-09-07 22:43:58',NULL),(1563,'Kath Jeffrey','PO Box 72080','2017-10-04',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ruddie Vasyukov','09215678901','Mother','2025-09-07 22:43:58',NULL),(1564,'Nicky Fewkes','Suite 79','2022-11-21',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Burt Meader','09348901234','Friend','2025-09-07 22:43:58',NULL),(1565,'Pauli Vallintine','Suite 82','2024-10-20',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Charmion Bessey','09348901234','Friend','2025-09-07 22:43:58',NULL),(1566,'Lindy Tebald','20th Floor','2022-09-02',3,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Emmit Kellog','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1567,'Alyda Wastling','Suite 83','2021-10-19',3,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jenda Pagden','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1568,'Andreana Braisby','Apt 1797','2021-03-28',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Pepi Brose','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1569,'Birk Abade','Apt 1141','2023-04-22',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Cary Moralis','09348901234','Friend','2025-09-07 22:43:58',NULL),(1570,'Crissy Strathearn','Room 1968','2020-11-25',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Marianne Ida','09348901234','Father','2025-09-07 22:43:58',NULL),(1571,'Harley Le Surf','Apt 296','2025-02-05',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Torin Wakenshaw','09215678901','Friend','2025-09-07 22:43:58',NULL),(1572,'Iain Chilles','Suite 13','2017-07-09',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Alane Aleksandrev','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1573,'Siffre Glidden','Room 1419','2020-03-22',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Leda Rosendahl','09171234567','Friend','2025-09-07 22:43:58',NULL),(1574,'Grover Rembrandt','Room 335','2016-12-27',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lebbie Eunson','09348901234','Father','2025-09-07 22:43:58',NULL),(1575,'Alistair Hunday','Apt 598','2018-03-25',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ailene Wisden','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1576,'Suzy McCrann','Room 694','2024-01-26',1,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jacky Bilby','09215678901','Child','2025-09-07 22:43:58',NULL),(1577,'Bree Gratrix','Room 1691','2018-08-17',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Doreen Gayther','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1578,'Pepillo Lichfield','Apt 1453','2023-01-21',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Maxie Hartman','09171234567','Mother','2025-09-07 22:43:58',NULL),(1579,'Ritchie Rault','Suite 34','2015-12-14',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Alfreda Sprackling','09348901234','Mother','2025-09-07 22:43:58',NULL),(1580,'Nevile Laste','Suite 29','2020-09-07',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ellissa Stiling','09171234567','Child','2025-09-07 22:43:58',NULL),(1581,'Beau Schott','Room 1137','2016-01-30',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Thorin Dennis','09171234567','Mother','2025-09-07 22:43:58',NULL),(1582,'Adelheid Kubacek','PO Box 31771','2017-09-12',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Sarine Ishchenko','09171234567','Child','2025-09-07 22:43:58',NULL),(1583,'Phelia Narramore','PO Box 96210','2018-10-23',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Carla Goldsworthy','09348901234','Child','2025-09-07 22:43:58',NULL),(1584,'Mitchael Kassidy','Room 1884','2016-02-21',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Loralyn Brocks','09171234567','Child','2025-09-07 22:43:58',NULL),(1585,'Perle Malins','Suite 75','2021-11-17',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Aarika Smallman','09215678901','Child','2025-09-07 22:43:58',NULL),(1586,'Tobe Ducker','PO Box 63968','2022-10-22',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Brianna de Quesne','09171234567','Friend','2025-09-07 22:43:58',NULL),(1587,'Bev Ellings','Room 111','2024-12-27',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Shane Fyrth','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1588,'Tait Teissier','Room 1431','2024-05-25',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Nadine Shippard','09215678901','Father','2025-09-07 22:43:58',NULL),(1589,'Shandra Levy','18th Floor','2015-12-17',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Weidar Poker','09171234567','Father','2025-09-07 22:43:58',NULL),(1590,'Vincents Elstow','Apt 1552','2022-10-29',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Granger Deneve','09215678901','Mother','2025-09-07 22:43:58',NULL),(1591,'Roselle Aland','Room 1438','2014-10-03',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','See Elderton','09348901234','Mother','2025-09-07 22:43:58',NULL),(1592,'Lettie Teligin','PO Box 51426','2020-03-23',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Juditha Cockerham','09171234567','Mother','2025-09-07 22:43:58',NULL),(1593,'Lars Lowndes','Suite 86','2015-01-27',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lombard Tesoe','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1594,'Dane Kinnen','Room 1397','2022-03-06',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Mame Jessard','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1595,'Malissa Hebard','Room 1187','2017-02-23',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Candi Faustin','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1596,'Aymer Ormshaw','16th Floor','2016-01-08',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gratia Tracy','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1597,'Micheal Pettitt','Room 1821','2025-07-04',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tobi Hoffmann','09348901234','Child','2025-09-07 22:43:58',NULL),(1598,'Lara Millmoe','Suite 58','2018-11-05',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Shaylyn Huniwall','09215678901','Child','2025-09-07 22:43:58',NULL),(1599,'Chaddy Verrell','8th Floor','2019-03-18',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lew Dibnah','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1600,'Godfree Yegorov','Room 1505','2025-05-27',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Guinna Cough','09348901234','Child','2025-09-07 22:43:58',NULL),(1601,'Iver Emig','2nd Floor','2021-06-30',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Port Scane','09171234567','Mother','2025-09-07 22:43:58',NULL),(1602,'Ced Tennock','Room 1921','2025-05-27',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Irita Hoggin','09215678901','Mother','2025-09-07 22:43:58',NULL),(1603,'Carissa Sebborn','7th Floor','2015-10-18',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jillayne Rudd','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1604,'Saxe Gracewood','Apt 1313','2016-01-28',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Denyse Tooting','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1605,'De witt Simonnot','7th Floor','2017-06-22',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Selma McKeachie','09171234567','Mother','2025-09-07 22:43:58',NULL),(1606,'Aleksandr Callway','19th Floor','2015-07-14',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jarrod Barefoot','09348901234','Mother','2025-09-07 22:43:58',NULL),(1607,'Estel Dacey','Room 1771','2021-11-29',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alva Tebbitt','09348901234','Mother','2025-09-07 22:43:58',NULL),(1608,'Marcelle Bazoge','PO Box 91371','2025-01-30',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Westbrook Lumsdon','09171234567','Friend','2025-09-07 22:43:58',NULL),(1609,'Mona Stanmore','10th Floor','2024-03-06',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Falkner Sealove','09215678901','Mother','2025-09-07 22:43:58',NULL),(1610,'Marge O\' Dooley','Apt 1070','2015-10-10',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Birdie Burlingame','09215678901','Mother','2025-09-07 22:43:58',NULL),(1611,'Minor Spaduzza','Apt 1258','2018-05-05',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tildie Stawell','09171234567','Friend','2025-09-07 22:43:58',NULL),(1612,'Tobias Calvey','Suite 38','2016-07-21',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Marika Cavendish','09348901234','Father','2025-09-07 22:43:58',NULL),(1613,'Braden Sproul','Apt 343','2016-03-24',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hazel Iacovino','09348901234','Mother','2025-09-07 22:43:58',NULL),(1614,'Othella Cottie','PO Box 60353','2017-01-24',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kimberlee Killough','09215678901','Child','2025-09-07 22:43:58',NULL),(1615,'Melisenda Haylock','PO Box 98875','2025-01-08',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Janey Curle','09171234567','Child','2025-09-07 22:43:58',NULL),(1616,'Celesta Rupel','Room 1899','2018-01-28',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cesar Orrice','09348901234','Father','2025-09-07 22:43:58',NULL),(1617,'Clyde Zanni','PO Box 37945','2017-08-29',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Sharity Jakovijevic','09215678901','Friend','2025-09-07 22:43:58',NULL),(1618,'Hendrik Watford','PO Box 43186','2023-12-11',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Leonore McCaughey','09171234567','Mother','2025-09-07 22:43:58',NULL),(1619,'Corrinne Bedding','19th Floor','2021-08-27',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Rab Titcombe','09348901234','Friend','2025-09-07 22:43:58',NULL),(1620,'Dean Askham','Suite 96','2021-02-02',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Trey Grishakin','09215678901','Friend','2025-09-07 22:43:58',NULL),(1621,'Ingeborg Cordner','Room 106','2024-12-18',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Charlean Schuster','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1622,'Octavius Cawdery','Room 1735','2024-03-09',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Aidan Barlow','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1623,'Beryle Stoller','Apt 1107','2018-12-03',6,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Roselin O\'Cahsedy','09215678901','Father','2025-09-07 22:43:58',NULL),(1624,'Indira MacQuaker','PO Box 77765','2018-11-19',6,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Candi Smallacombe','09171234567','Child','2025-09-07 22:43:58',NULL),(1625,'Bernelle Dunseath','Apt 698','2023-06-26',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Katharine Chittleburgh','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1626,'Daffy Rubinek','Suite 2','2018-09-29',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Michaella Santorini','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1627,'Pauli Delahunty','Room 822','2021-10-07',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kalle Rawstorn','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1628,'Antonin Bearne','PO Box 55563','2020-09-09',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Mayor Elvy','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1629,'Charles Peplow','Apt 413','2023-08-10',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Chad Colquitt','09215678901','Father','2025-09-07 22:43:58',NULL),(1630,'Andriana Deners','Apt 1745','2019-01-28',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Juan Coghlan','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1631,'Karlotta Ludgrove','PO Box 4773','2017-03-09',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kellyann Comolli','09215678901','Child','2025-09-07 22:43:58',NULL),(1632,'Ikey Bragginton','Room 709','2018-03-24',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Evvie Delgua','09215678901','Friend','2025-09-07 22:43:58',NULL),(1633,'Zenia Duck','Apt 1461','2017-05-28',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Rogers Screen','09215678901','Father','2025-09-07 22:43:58',NULL),(1634,'Orbadiah Connal','Suite 97','2023-05-31',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Saundra Itzkovwitch','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1635,'Nanette Louw','18th Floor','2016-12-22',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Roxine Mateuszczyk','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1636,'L;urette Sybe','Apt 235','2022-12-09',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ferne Trump','09215678901','Father','2025-09-07 22:43:58',NULL),(1637,'Annalee Nice','Room 82','2023-08-07',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Fredric Egle','09171234567','Father','2025-09-07 22:43:58',NULL),(1638,'Maighdiln Buckby','PO Box 17903','2019-08-24',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Roy Wanka','09215678901','Child','2025-09-07 22:43:58',NULL),(1639,'Brigitta Coughlin','PO Box 74288','2022-04-01',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Innis Bazylets','09171234567','Friend','2025-09-07 22:43:58',NULL),(1640,'Thekla Bulch','Suite 23','2016-06-03',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Garv Snassell','09348901234','Child','2025-09-07 22:43:58',NULL),(1641,'Raye Jopp','1st Floor','2024-09-30',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Elly Dies','09171234567','Child','2025-09-07 22:43:58',NULL),(1642,'Vanny Adhams','Apt 901','2019-07-12',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Elinore Gianuzzi','09348901234','Friend','2025-09-07 22:43:58',NULL),(1643,'Donovan O\'Duane','PO Box 40276','2024-01-17',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ulrikaumeko Delamaine','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1644,'Orran MacFaell','5th Floor','2022-10-27',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Tasia Andries','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1645,'Rodolph Fairman','Room 72','2019-01-15',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Florie Grocock','09348901234','Father','2025-09-07 22:43:58',NULL),(1646,'Colman Crookshanks','Suite 34','2017-05-07',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Auroora Pawnsford','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1647,'Arvin Wenban','Suite 52','2023-10-16',1,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Erhart Tweede','09171234567','Mother','2025-09-07 22:43:58',NULL),(1648,'Kim Saywood','4th Floor','2015-06-23',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lizabeth Gutman','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1649,'Katina Norgan','19th Floor','2022-03-24',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Susanetta Foucard','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1650,'Sherlock Winkworth','Apt 368','2022-07-31',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Stormy Jouanot','09215678901','Father','2025-09-07 22:43:58',NULL),(1651,'Spenser Dury','Apt 1424','2020-12-15',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Deidre Drewell','09348901234','Child','2025-09-07 22:43:58',NULL),(1652,'Harris Ovett','Suite 70','2017-04-16',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Forester Pressey','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1653,'Damita Szanto','Apt 1869','2016-07-13',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Tessa Eykelhof','09171234567','Child','2025-09-07 22:43:58',NULL),(1654,'Dot Podd','17th Floor','2016-03-26',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Nicolas Reiglar','09348901234','Child','2025-09-07 22:43:58',NULL),(1655,'Noell Wallbrook','Suite 48','2018-10-27',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Chicky Ackeroyd','09348901234','Mother','2025-09-07 22:43:58',NULL),(1656,'Shirleen Hartwell','7th Floor','2022-06-25',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Adina Chalke','09171234567','Child','2025-09-07 22:43:58',NULL),(1657,'Nathalia Elce','Suite 41','2018-12-07',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Daisi Seary','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1658,'Winny Leverton','6th Floor','2016-05-01',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Karrah Stoyle','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1659,'Carolan Coate','Room 93','2022-01-10',3,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Evey Clayworth','09348901234','Father','2025-09-07 22:43:58',NULL),(1660,'Rolland Cowdery','12th Floor','2019-03-22',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Zulema Lockery','09171234567','Child','2025-09-07 22:43:58',NULL),(1661,'Valene Varvara','1st Floor','2022-06-13',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Micky Fibbit','09215678901','Friend','2025-09-07 22:43:58',NULL),(1662,'Myra Covell','Room 1747','2016-10-10',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Maure Tremblay','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1663,'Albert Bernier','6th Floor','2016-12-04',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Junia Crummey','09171234567','Father','2025-09-07 22:43:58',NULL),(1664,'Lorilee Rigglesford','10th Floor','2020-12-29',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Avigdor Oneill','09171234567','Father','2025-09-07 22:43:58',NULL),(1665,'Liza Grassin','Suite 16','2024-05-26',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cosette Danbye','09171234567','Father','2025-09-07 22:43:58',NULL),(1666,'Annora Tzuker','PO Box 25833','2015-08-10',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Robinia Mixer','09171234567','Mother','2025-09-07 22:43:58',NULL),(1667,'Cornelia Willan','Room 1448','2016-07-20',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Judi McCorry','09215678901','Child','2025-09-07 22:43:58',NULL),(1668,'Padraig Hugh','Suite 45','2021-07-08',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Joy Dartnall','09215678901','Mother','2025-09-07 22:43:58',NULL),(1669,'Rikki Brunone','Suite 99','2023-05-09',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Laina Stopforth','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1670,'Lyman Morkham','Room 725','2015-10-21',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Suzanne Schild','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1671,'Del Spink','9th Floor','2023-05-09',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ernestine Sidary','09171234567','Child','2025-09-07 22:43:58',NULL),(1672,'Belva Gascoigne','PO Box 36853','2022-11-09',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Emmye Bottle','09171234567','Child','2025-09-07 22:43:58',NULL),(1673,'Rozalie Gligori','PO Box 30264','2021-06-09',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Reta Rutt','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1674,'Toma Haggleton','PO Box 34413','2019-03-01',6,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Henrietta Tickner','09348901234','Child','2025-09-07 22:43:58',NULL),(1675,'Hilario Spinola','Apt 385','2018-03-01',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Amabelle Husthwaite','09215678901','Mother','2025-09-07 22:43:58',NULL),(1676,'Prue Cutbirth','Room 1637','2023-10-06',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Humberto Frusher','09348901234','Father','2025-09-07 22:43:58',NULL),(1677,'Rog Widdocks','5th Floor','2015-01-01',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Noby Roycroft','09348901234','Friend','2025-09-07 22:43:58',NULL),(1678,'Kelcie Rutherforth','PO Box 75224','2019-11-09',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Pepe Cattemull','09215678901','Mother','2025-09-07 22:43:58',NULL),(1679,'Thomasina Josefer','Apt 1573','2022-12-30',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lilas Kave','09215678901','Father','2025-09-07 22:43:58',NULL),(1680,'Marv Kerry','6th Floor','2021-02-04',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Evered Comettoi','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1681,'Darbie Cleveland','Apt 1033','2020-10-28',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Alfredo Weiss','09348901234','Friend','2025-09-07 22:43:58',NULL),(1682,'Angelita Hartshorn','PO Box 87652','2021-11-20',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Elset Stokoe','09215678901','Father','2025-09-07 22:43:58',NULL),(1683,'Tessi Tomasoni','Suite 93','2023-09-15',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kennie Bulward','09348901234','Child','2025-09-07 22:43:58',NULL),(1684,'Verene Puttrell','PO Box 69992','2019-04-25',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dot Kirton','09215678901','Child','2025-09-07 22:43:58',NULL),(1685,'Darren Coupe','Apt 1869','2025-06-10',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Katalin Rawsthorne','09171234567','Father','2025-09-07 22:43:58',NULL),(1686,'Jere Booty','8th Floor','2021-06-02',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cathrine Ferrelli','09171234567','Father','2025-09-07 22:43:58',NULL),(1687,'Katina Lisciandri','Apt 791','2021-12-16',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Frederik Shemwell','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1688,'Ina Elgey','Apt 883','2023-03-24',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Cathleen Jentgens','09348901234','Friend','2025-09-07 22:43:58',NULL),(1689,'Purcell Friedank','Suite 6','2017-09-05',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Caz Milbank','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1690,'Damien Lemmers','Apt 909','2024-04-13',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Idaline Collingworth','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1691,'Gregorius Barabich','Room 1801','2018-01-28',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kordula Larrett','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1692,'Mavis Rickword','13th Floor','2016-06-14',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Carolynn Wakenshaw','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1693,'Kaia Capps','PO Box 81503','2016-09-15',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Corey Duck','09171234567','Friend','2025-09-07 22:43:58',NULL),(1694,'Randolph Beatey','2nd Floor','2021-06-25',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Drusy Colebourne','09348901234','Friend','2025-09-07 22:43:58',NULL),(1695,'Deina Houseman','Room 104','2020-01-13',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Mavra Nellen','09348901234','Child','2025-09-07 22:43:58',NULL),(1696,'Grace Crawshaw','Suite 58','2023-12-18',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Fifi Van Leijs','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1697,'Maisie Speakman','3rd Floor','2016-09-16',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alex Greber','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1698,'Solly Pareman','Room 1163','2021-10-27',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jae McNeice','09348901234','Friend','2025-09-07 22:43:58',NULL),(1699,'Talbot Beasant','Suite 30','2023-11-18',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Emmi Mitroshinov','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1700,'Kirstyn Janosevic','Apt 976','2018-02-09',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kelly Ranscome','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1701,'Gusta Cale','5th Floor','2025-05-23',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ranice Cosh','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1702,'Muffin Heymes','PO Box 77078','2018-10-23',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Austin Wedmore.','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1703,'Dawn Stepney','Suite 90','2024-10-02',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Whitby Gosnay','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1704,'Shani Benedit','Suite 92','2023-12-19',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dix Skillman','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1705,'Alikee Rantoull','Apt 460','2022-05-10',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Carolan Itzchaki','09348901234','Friend','2025-09-07 22:43:58',NULL),(1706,'Marty Boydell','Suite 40','2021-03-26',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Emyle Gillebride','09215678901','Mother','2025-09-07 22:43:59',NULL),(1707,'Whittaker Falco','Apt 1328','2018-12-01',6,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Burg Shemwell','09348901234','Father','2025-09-07 22:43:59',NULL),(1708,'Hana Laroux','12th Floor','2014-12-27',10,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Berky Cragell','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1709,'Mellisa Gavini','8th Floor','2024-01-17',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Carree Colvill','09348901234','Spouse','2025-09-07 22:43:59',NULL),(1710,'Ardene Booker','PO Box 49649','2019-10-20',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Martin Drohane','09215678901','Father','2025-09-07 22:43:59',NULL),(1711,'Theadora Olivetta','Suite 54','2019-04-14',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Tish MacKomb','09171234567','Father','2025-09-07 22:43:59',NULL),(1712,'Sybila Yurevich','PO Box 85795','2020-05-15',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jemmie Tettley','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1713,'Redd Weich','Room 1328','2022-05-20',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Annora Lufkin','09171234567','Friend','2025-09-07 22:43:59',NULL),(1714,'Gayleen Clowney','Apt 410','2023-10-24',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rene Brandon','09171234567','Friend','2025-09-07 22:43:59',NULL),(1715,'Amelie Rase','Apt 613','2022-01-03',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Anstice Kittley','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1716,'Cart Houldey','Room 92','2023-12-05',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Griffy Lyston','09348901234','Child','2025-09-07 22:43:59',NULL),(1717,'Buddie Beernaert','Room 1763','2015-05-16',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gena While','09348901234','Mother','2025-09-07 22:43:59',NULL),(1718,'Ynez Reany','Suite 99','2015-12-12',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Raddie Perren','09348901234','Friend','2025-09-07 22:43:59',NULL),(1719,'Kary Cosby','Apt 1116','2016-01-05',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Freddy Hayfield','09348901234','Mother','2025-09-07 22:43:59',NULL),(1720,'Kennan Aps','5th Floor','2020-02-26',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rorie Eland','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1721,'Rebeca Court','Suite 15','2023-08-29',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Svend Lardnar','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1722,'Terrell Blanket','Room 1952','2023-08-28',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kingston Bartolacci','09171234567','Father','2025-09-07 22:43:59',NULL),(1723,'Zondra Ochiltree','Room 1948','2025-07-04',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Bernardine Laughtisse','09348901234','Father','2025-09-07 22:43:59',NULL),(1724,'Adah Twizell','PO Box 58860','2025-09-04',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Newton Atty','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1725,'Syd Percy','PO Box 6927','2016-06-28',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Elly Margrett','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1726,'Wolf Rennolds','Room 1326','2018-08-07',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Fran Thompkins','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1727,'Dedie Branford','Apt 467','2017-04-17',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Manolo Fursey','09215678901','Friend','2025-09-07 22:43:59',NULL),(1728,'Sosanna Feehery','12th Floor','2016-08-10',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Faulkner Thurlow','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1729,'Allyson Swalowe','Suite 45','2016-05-21',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Nicolette Beak','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1730,'Budd Oleksiak','Room 741','2015-01-31',10,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Andree Keneforde','09348901234','Mother','2025-09-07 22:43:59',NULL),(1731,'Sallie Izhakov','Apt 954','2016-10-21',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Frannie Sturt','09215678901','Mother','2025-09-07 22:43:59',NULL),(1732,'Fredericka Ballinghall','Suite 49','2018-04-07',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kinna Skates','09215678901','Child','2025-09-07 22:43:59',NULL),(1733,'Darbie Kilgour','Room 968','2016-02-25',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Gavin Kohnemann','09215678901','Child','2025-09-07 22:43:59',NULL),(1734,'Dick Trouel','Room 757','2018-05-22',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Farr Knowlman','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1735,'Cathi Philipson','Suite 27','2024-10-18',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Fayina Blenkinship','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1736,'Hephzibah Crockett','Room 1825','2022-04-30',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Robinett Gawkes','09215678901','Father','2025-09-07 22:43:59',NULL),(1737,'Erena O\'Carran','Room 59','2016-01-27',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Normand Lillegard','09171234567','Father','2025-09-07 22:43:59',NULL),(1738,'Layton Jeffery','10th Floor','2015-04-13',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Montgomery Metschke','09215678901','Mother','2025-09-07 22:43:59',NULL),(1739,'Mauricio Pappi','Apt 500','2019-02-23',6,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Iorgo Perin','09215678901','Child','2025-09-07 22:43:59',NULL),(1740,'Joey Kirkpatrick','PO Box 98342','2025-04-24',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hewie Willmont','09348901234','Child','2025-09-07 22:43:59',NULL),(1741,'Lief Roseburgh','Apt 1514','2025-08-07',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Enriqueta Solly','09348901234','Child','2025-09-07 22:43:59',NULL),(1742,'Caryl Simonaitis','Room 499','2020-06-16',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Christye Purvess','09215678901','Mother','2025-09-07 22:43:59',NULL),(1743,'Denise Jesson','Apt 1170','2015-03-28',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Gilbert Dawkes','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1744,'Seth Aujean','Apt 751','2014-12-06',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ulrikaumeko Sclanders','09215678901','Child','2025-09-07 22:43:59',NULL),(1745,'Rafaello Torns','Room 20','2022-04-20',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Cissy Timbs','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1746,'Howey Mingus','Suite 7','2024-10-26',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Madelon Dossit','09348901234','Spouse','2025-09-07 22:43:59',NULL),(1747,'Mercy Valentinetti','Suite 49','2015-07-28',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Coralie Booty','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1748,'Janessa Lambole','PO Box 17033','2023-05-07',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Mason Chisnell','09171234567','Child','2025-09-07 22:43:59',NULL),(1749,'Caryl Motherwell','1st Floor','2022-03-18',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Dierdre Philipsohn','09348901234','Spouse','2025-09-07 22:43:59',NULL),(1750,'Wynnie Dowse','20th Floor','2018-06-17',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Karrie Toupe','09215678901','Child','2025-09-07 22:43:59',NULL),(1751,'Adrianne Yellowlea','PO Box 98741','2023-10-01',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Peggie Sherbrook','09215678901','Father','2025-09-07 22:43:59',NULL),(1752,'Arie Cosham','Apt 1692','2024-01-18',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Mae Pozer','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1753,'Zaria Antonutti','Room 631','2024-08-29',1,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Krystle Huxley','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1754,'Barrie Bortoluzzi','16th Floor','2019-06-18',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Linea Adlington','09171234567','Child','2025-09-07 22:43:59',NULL),(1755,'Bax Yon','Room 1892','2014-09-29',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tedda Dansken','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1756,'Ella Pennoni','Apt 92','2016-11-27',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ally Semered','09215678901','Father','2025-09-07 22:43:59',NULL),(1757,'Billye Ladloe','8th Floor','2021-07-25',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Monika Sallan','09171234567','Father','2025-09-07 22:43:59',NULL),(1758,'Muriel Bothbie','PO Box 84153','2015-09-23',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Alyssa Kornalik','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1759,'Ethe Orry','16th Floor','2022-05-20',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Liv Scandrick','09171234567','Father','2025-09-07 22:43:59',NULL),(1760,'Nadiya Tommaseo','Suite 41','2020-11-30',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hetty Gilbeart','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1761,'Christiane Fouch','PO Box 50245','2023-11-03',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ursola Guinery','09171234567','Father','2025-09-07 22:43:59',NULL),(1762,'Bab D\'Alesio','11th Floor','2019-10-18',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Goldia Nunns','09215678901','Father','2025-09-07 22:43:59',NULL),(1763,'Brittan Tuffley','Room 709','2017-10-18',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Roseanna Harrigan','09215678901','Friend','2025-09-07 22:43:59',NULL),(1764,'Cleavland McGown','Room 868','2020-06-27',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rowan Bartley','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1765,'Helyn Haugh','10th Floor','2015-05-05',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Barny Lenchenko','09348901234','Child','2025-09-07 22:43:59',NULL),(1766,'Aubree Lawrenson','7th Floor','2023-05-23',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Norah Kellie','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1767,'Briant Stepney','PO Box 80031','2023-06-17',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Emlynne Lehrmann','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1768,'Georgette Gauche','17th Floor','2016-10-10',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jan Gallimore','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1769,'Quintana Duffer','PO Box 64373','2017-08-08',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kaila Fossick','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1770,'Ezekiel Merman','PO Box 5393','2016-11-20',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Aveline Dibbe','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1771,'Nessy Chisnall','Room 568','2019-02-08',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Morlee Traut','09171234567','Father','2025-09-07 22:43:59',NULL),(1772,'Mireielle Girdlestone','PO Box 17246','2016-11-15',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Matilde Strelitz','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1773,'Doralynn Mounch','18th Floor','2016-03-18',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Donovan Jemmett','09171234567','Friend','2025-09-07 22:43:59',NULL),(1774,'Alisun Cassels','PO Box 65802','2022-02-02',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jeramie Ridel','09348901234','Spouse','2025-09-07 22:43:59',NULL),(1775,'Nikita Southon','Suite 15','2021-11-09',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Iormina Fawltey','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1776,'Maison Claeskens','6th Floor','2025-06-12',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Vilhelmina Van Hove','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1777,'Karine Morling','Apt 1423','2020-01-19',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Pearce Aspey','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1778,'Zorana McKirdy','Suite 15','2019-09-20',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Alane Lentsch','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1779,'Imelda Yeude','Suite 41','2020-01-21',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alano Dilleway','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1780,'Seth Oen','PO Box 19651','2014-09-12',11,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Grier Redbourn','09171234567','Father','2025-09-07 22:43:59',NULL),(1781,'Stevie Castagneto','PO Box 97484','2017-02-11',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Terrye Fackrell','09348901234','Child','2025-09-07 22:43:59',NULL),(1782,'Feodora Trundle','Apt 766','2016-06-23',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ettore Sandry','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1783,'Beatrisa Sonier','Room 1092','2021-10-23',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Viola Paulack','09171234567','Child','2025-09-07 22:43:59',NULL),(1784,'Drake Tonbridge','Apt 772','2024-11-29',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Shelba Bockings','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1785,'Lancelot Mount','Suite 75','2025-08-18',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Rebe Senechault','09215678901','Mother','2025-09-07 22:43:59',NULL),(1786,'Jordan Keel','Suite 71','2017-03-26',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Tate Fingleton','09215678901','Child','2025-09-07 22:43:59',NULL),(1787,'Dody Bortolutti','Room 1538','2017-04-18',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Dylan Derbyshire','09171234567','Mother','2025-09-07 22:43:59',NULL),(1788,'Fleur Gilkison','Room 1344','2019-12-04',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Salli Desporte','09215678901','Friend','2025-09-07 22:43:59',NULL),(1789,'Liva Hatry','11th Floor','2024-12-18',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Betty Kristufek','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1790,'Darcee Ianetti','PO Box 64689','2022-01-31',3,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jamesy Padwick','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1791,'Ashly Fountain','Suite 97','2025-05-06',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Eddie Lande','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1792,'Temple Oulet','Room 1431','2021-11-04',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Giff Bynert','09215678901','Friend','2025-09-07 22:43:59',NULL),(1793,'Brucie Howcroft','Apt 1289','2016-09-19',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Cam Parks','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1794,'Cedric Loidl','Apt 436','2019-04-18',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Octavia Benstead','09215678901','Mother','2025-09-07 22:43:59',NULL),(1795,'Feliks Keenlyside','Apt 432','2015-11-16',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Silvie Lucy','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1796,'Shea Konerding','5th Floor','2022-04-01',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Zerk Pentelo','09215678901','Child','2025-09-07 22:43:59',NULL),(1797,'Zarah Coulton','PO Box 65808','2016-05-14',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Aveline Divis','09215678901','Child','2025-09-07 22:43:59',NULL),(1798,'Holmes Sandiland','Room 324','2020-01-22',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Joann Ruddock','09171234567','Child','2025-09-07 22:43:59',NULL),(1799,'Isiahi Bullard','PO Box 55680','2016-10-24',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Bendick Eaves','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1800,'Stavros Sabathe','14th Floor','2022-02-20',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Camila Le Conte','09171234567','Child','2025-09-07 22:43:59',NULL),(1801,'Merilee Caustick','16th Floor','2016-10-17',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Izaak Fussey','09348901234','Mother','2025-09-07 22:43:59',NULL),(1802,'Alix Schubert','PO Box 27715','2019-05-20',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Clarence Holsey','09215678901','Child','2025-09-07 22:43:59',NULL),(1803,'Sanders Greeno','Suite 42','2019-10-26',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Cally Mailey','09171234567','Father','2025-09-07 22:43:59',NULL),(1804,'Hilarius Warrender','Suite 84','2024-10-03',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Duffy Manford','09215678901','Mother','2025-09-07 22:43:59',NULL),(1805,'Killian Siggins','Apt 808','2017-02-08',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hunter Simkiss','09215678901','Child','2025-09-07 22:43:59',NULL),(1806,'Kaitlynn Lack','Apt 163','2024-09-10',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Gibbie O\' Flaherty','09348901234','Child','2025-09-07 22:43:59',NULL),(1807,'Lockwood Bohje','20th Floor','2015-12-16',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Hillard Maccrea','09171234567','Child','2025-09-07 22:43:59',NULL),(1808,'Delcine Decaze','Room 1422','2017-06-15',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Tobit Lambshine','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1809,'Cecil Wallett','Room 1662','2022-03-30',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Joye Beardmore','09171234567','Mother','2025-09-07 22:43:59',NULL),(1810,'Wylie Bettley','PO Box 24866','2019-03-04',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jami Delyth','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1811,'Rabi Roffey','Apt 1351','2020-01-07',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Marcellus Arton','09215678901','Friend','2025-09-07 22:43:59',NULL),(1812,'Aguste Breen','Suite 67','2020-04-12',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Merrill Heatherington','09215678901','Child','2025-09-07 22:43:59',NULL),(1813,'Wilton Shearwood','Apt 1479','2019-09-16',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Edan Wolfendale','09171234567','Friend','2025-09-07 22:43:59',NULL),(1814,'Lynett Servis','18th Floor','2016-11-21',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Adriaens Hartill','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1815,'Krisha Verbeke','Suite 5','2020-08-03',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sonny Eminson','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1816,'Petronilla Eastmead','PO Box 18392','2017-11-14',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Eduard Petchell','09348901234','Spouse','2025-09-07 22:43:59',NULL),(1817,'Kirby Crew','Apt 632','2019-06-11',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Becki Scorthorne','09171234567','Child','2025-09-07 22:43:59',NULL),(1818,'Lenna Wisniewski','PO Box 65634','2023-09-02',2,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Hugibert Minot','09348901234','Child','2025-09-07 22:43:59',NULL),(1819,'Charlton Vaux','PO Box 17835','2017-03-22',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Mercy Jeffes','09215678901','Child','2025-09-07 22:43:59',NULL),(1820,'Ebony Petrovykh','Room 1430','2022-08-26',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rossy Meadmore','09348901234','Friend','2025-09-07 22:43:59',NULL),(1821,'Freddy Touhig','20th Floor','2020-01-28',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Greta Tesche','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1822,'Lammond Croad','Apt 1592','2024-08-22',1,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Olly Robard','09348901234','Child','2025-09-07 22:43:59',NULL),(1823,'Lorant Lakes','Apt 295','2017-09-18',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Malvin Bierton','09215678901','Mother','2025-09-07 22:43:59',NULL),(1824,'Bartolomeo Grigorian','6th Floor','2015-01-18',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Warden Lowcock','09215678901','Friend','2025-09-07 22:43:59',NULL),(1825,'Amata Colwell','Suite 14','2025-06-22',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lorri Wicklin','09171234567','Mother','2025-09-07 22:43:59',NULL),(1826,'Amii Manns','Suite 5','2018-10-13',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Doy Bricknall','09348901234','Child','2025-09-07 22:43:59',NULL),(1827,'Misti Haycox','Suite 14','2017-04-28',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lars Deery','09348901234','Father','2025-09-07 22:43:59',NULL),(1828,'Leola Caroll','Room 1400','2018-06-11',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Modesta MacBain','09348901234','Child','2025-09-07 22:43:59',NULL),(1829,'Tobin Noke','PO Box 74769','2022-03-17',3,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Venita Heifer','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1830,'Priscilla Elcoate','Apt 1729','2016-07-10',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Baxter Ricoald','09348901234','Friend','2025-09-07 22:43:59',NULL),(1831,'Carny Enochsson','Room 442','2017-12-30',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lynnea Khrishtafovich','09171234567','Friend','2025-09-07 22:43:59',NULL),(1832,'Garner Cowey','Suite 94','2023-04-13',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Reggy Shapcott','09171234567','Child','2025-09-07 22:43:59',NULL),(1833,'Lionello Nutt','Suite 61','2015-06-23',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Melania Siss','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1834,'Les Cundey','Suite 53','2018-10-25',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Cortney Trittam','09348901234','Friend','2025-09-07 22:43:59',NULL),(1835,'Iona Hedworth','PO Box 71954','2025-07-06',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Billye Zipsell','09215678901','Friend','2025-09-07 22:43:59',NULL),(1836,'Noella Arunowicz','5th Floor','2018-10-10',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Nate Clemitt','09215678901','Child','2025-09-07 22:43:59',NULL),(1837,'Rebecca Clew','Suite 81','2014-11-10',10,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rory Louis','09215678901','Father','2025-09-07 22:43:59',NULL),(1838,'Tonye Gwillyam','3rd Floor','2016-03-26',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sibelle Rouke','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1839,'Dominik Gerrell','Apt 571','2015-10-07',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Wilden Wigmore','09348901234','Friend','2025-09-07 22:43:59',NULL),(1840,'Marga Imore','12th Floor','2018-11-13',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Eleni Dahlman','09215678901','Friend','2025-09-07 22:43:59',NULL),(1841,'Deni Sprull','Suite 78','2015-09-06',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Libbey MacInerney','09171234567','Child','2025-09-07 22:43:59',NULL),(1842,'Kaleena Roll','Room 1865','2015-08-12',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Leann Neenan','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1843,'Harriette Whiles','Room 1950','2020-03-13',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Tarrance Kondrachenko','09171234567','Father','2025-09-07 22:43:59',NULL),(1844,'Elisa Deverock','8th Floor','2019-05-20',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Laverne McMonnies','09348901234','Mother','2025-09-07 22:43:59',NULL),(1845,'Kathleen Penvarne','PO Box 1841','2020-06-08',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sidonia Treweke','09215678901','Mother','2025-09-07 22:43:59',NULL),(1846,'Geoffrey Helbeck','Room 1559','2016-02-15',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Gardner Bensley','09215678901','Mother','2025-09-07 22:43:59',NULL),(1847,'Felix Whicher','PO Box 82810','2021-02-19',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ethelind Hambrick','09171234567','Friend','2025-09-07 22:43:59',NULL),(1848,'Romonda Blabber','Apt 775','2024-08-14',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Carolann Agius','09348901234','Mother','2025-09-07 22:43:59',NULL),(1849,'Benedicto Tuma','PO Box 53819','2020-02-22',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dorolisa Fairfull','09215678901','Father','2025-09-07 22:43:59',NULL),(1850,'Brooks Hancell','Room 940','2019-06-18',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Mathilda Couth','09215678901','Child','2025-09-07 22:43:59',NULL),(1851,'Xever Sessuns','Suite 72','2017-09-23',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Humfrid Figge','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1852,'Danya Ferrandez','Room 419','2015-11-05',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Worthington Lysons','09215678901','Friend','2025-09-07 22:43:59',NULL),(1853,'Wally Runnalls','Suite 39','2017-12-07',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lorin Partington','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1854,'Thomasina Fereday','PO Box 76095','2022-06-20',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Franklin Jiru','09171234567','Mother','2025-09-07 22:43:59',NULL),(1855,'Flore Rivilis','Room 1428','2016-12-20',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jere Woodvine','09348901234','Father','2025-09-07 22:43:59',NULL),(1856,'Constanta Fante','Suite 69','2020-06-02',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Vivi Lafont','09171234567','Father','2025-09-07 22:43:59',NULL),(1857,'Cesaro Utridge','12th Floor','2025-09-03',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Martynne Hoffner','09215678901','Mother','2025-09-07 22:43:59',NULL),(1858,'Isabeau Muzzall','PO Box 97274','2021-08-05',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Caty Checci','09215678901','Friend','2025-09-07 22:43:59',NULL),(1859,'Felita Helix','Room 1779','2024-08-04',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Tandy Goude','09215678901','Child','2025-09-07 22:43:59',NULL),(1860,'Meade Agiolfinger','2nd Floor','2015-05-20',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lanita Creavin','09215678901','Mother','2025-09-07 22:43:59',NULL),(1861,'Haywood Balke','PO Box 90720','2023-06-05',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Oby Culkin','09171234567','Friend','2025-09-07 22:43:59',NULL),(1862,'Helsa Innis','Apt 1545','2018-07-24',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Valentia Lampitt','09171234567','Friend','2025-09-07 22:43:59',NULL),(1863,'Randie Rainy','Room 333','2022-09-09',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Winfred Marrion','09171234567','Mother','2025-09-07 22:43:59',NULL),(1864,'Elberta Peaurt','Apt 1127','2019-04-01',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ignazio Killcross','09215678901','Child','2025-09-07 22:43:59',NULL),(1865,'Chane Wittey','Apt 192','2020-06-06',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Anson Deval','09348901234','Mother','2025-09-07 22:43:59',NULL),(1866,'Linc Skyram','Suite 37','2021-11-22',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Emilee Scoble','09215678901','Friend','2025-09-07 22:43:59',NULL),(1867,'Hagan Kas','5th Floor','2017-06-20',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Porty Saich','09215678901','Child','2025-09-07 22:43:59',NULL),(1868,'Doloritas Bulch','Suite 81','2021-07-14',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Carver Abramowitz','09215678901','Mother','2025-09-07 22:43:59',NULL),(1869,'Elysia Edgson','PO Box 92640','2018-02-06',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Packston Middlemist','09215678901','Child','2025-09-07 22:43:59',NULL),(1870,'Pepi Gudgeon','Room 1250','2022-11-06',2,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Christie Wilfinger','09171234567','Child','2025-09-07 22:43:59',NULL),(1871,'Klement Haslen','Apt 626','2022-02-27',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hermon Fellona','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1872,'Ab Turgoose','Apt 818','2018-11-12',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Wilbert Kidman','09348901234','Mother','2025-09-07 22:43:59',NULL),(1873,'Amandie Dumphrey','PO Box 52750','2017-04-18',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sam Alvarado','09215678901','Mother','2025-09-07 22:43:59',NULL),(1874,'Grant Meaders','Room 1575','2016-09-03',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ally Purton','09348901234','Mother','2025-09-07 22:43:59',NULL),(1875,'Suzi Reeme','4th Floor','2025-02-24',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Fancy Zuanazzi','09171234567','Father','2025-09-07 22:43:59',NULL),(1876,'Julia Sheasby','PO Box 22981','2021-05-12',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Chelsae Strognell','09348901234','Father','2025-09-07 22:43:59',NULL),(1877,'Guillermo Dagnan','Apt 298','2015-08-28',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Claude Laughrey','09171234567','Child','2025-09-07 22:43:59',NULL),(1878,'Welby McCaughen','Apt 810','2023-06-15',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Barnett Wyllis','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1879,'Bell Gush','Suite 62','2024-05-11',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Yorke Quinnet','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1880,'Marinna Daborne','1st Floor','2020-10-23',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hobard Chessill','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1881,'Leta Perrinchief','Suite 5','2019-01-22',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Donny Campbell','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1882,'Florenza Hearns','Apt 21','2020-08-04',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Burch Munden','09348901234','Friend','2025-09-07 22:43:59',NULL),(1883,'Lexis Davenall','Suite 52','2022-06-27',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Eustacia Addicott','09171234567','Child','2025-09-07 22:43:59',NULL),(1884,'Jephthah De Fraine','11th Floor','2016-06-17',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Eugine Maciejewski','09348901234','Friend','2025-09-07 22:43:59',NULL),(1885,'Isadore Petrillo','Room 1625','2024-04-06',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tera Scawen','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1886,'Tanya Gligoraci','PO Box 55570','2022-04-19',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kalina Swadlen','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1887,'Margarete Tilbrook','Suite 82','2018-10-13',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lyon Hrihorovich','09348901234','Mother','2025-09-07 22:43:59',NULL),(1888,'Erda Benes','Apt 1019','2021-06-26',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Teddie Bog','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1889,'Esma Purves','Room 73','2017-08-29',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Barrie Bowdler','09215678901','Child','2025-09-07 22:43:59',NULL),(1890,'Pasquale Dallimore','Suite 64','2019-10-14',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Rici Lampet','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1891,'Aileen Battson','Apt 1845','2021-01-30',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Angelico McIlwrick','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1892,'Malynda Amberger','Apt 1990','2020-06-01',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Patty Jersh','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1893,'Naoma Dreger','1st Floor','2018-09-17',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Karol Chatfield','09215678901','Father','2025-09-07 22:43:59',NULL),(1894,'Charley Ketteringham','1st Floor','2017-11-21',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hurleigh Malia','09215678901','Child','2025-09-07 22:43:59',NULL),(1895,'Kristyn Eburne','PO Box 8954','2022-12-03',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Stevy Burel','09215678901','Child','2025-09-07 22:43:59',NULL),(1896,'Brett O\'Henehan','Apt 1323','2016-11-01',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Solly Stoakley','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1897,'Kiley Elliss','12th Floor','2015-07-03',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Caroline Recher','09348901234','Mother','2025-09-07 22:43:59',NULL),(1898,'Meredith Shillabear','PO Box 21512','2016-09-20',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Bank Lanigan','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1899,'Huntley Chesney','Suite 40','2023-04-07',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lissi Colnet','09348901234','Friend','2025-09-07 22:43:59',NULL),(1900,'Janaya Killoran','13th Floor','2018-05-20',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Nedda Nanuccioi','09348901234','Child','2025-09-07 22:43:59',NULL),(1901,'Barr Rodear','Apt 402','2016-10-03',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jimmy Dignam','09171234567','Father','2025-09-07 22:43:59',NULL),(1902,'Mignon Brakewell','Suite 8','2018-01-05',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jacqueline Lemmon','09348901234','Father','2025-09-07 22:43:59',NULL),(1903,'Franklyn Leamon','Apt 407','2019-05-13',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Bidget Markie','09215678901','Child','2025-09-07 22:43:59',NULL),(1904,'Jedidiah Jaslem','Suite 10','2019-07-31',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Maryl McFaul','09348901234','Mother','2025-09-07 22:43:59',NULL),(1905,'Jimmy Newhouse','Room 1655','2022-10-23',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Joni Hessentaler','09171234567','Father','2025-09-07 22:43:59',NULL),(1906,'Rhonda Boother','PO Box 71783','2020-01-07',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Madelin Demcik','09171234567','Father','2025-09-07 22:43:59',NULL),(1907,'Eugen L\'oiseau','Room 449','2016-01-25',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Dick Kendred','09215678901','Father','2025-09-07 22:43:59',NULL),(1908,'Wendall Whines','Apt 1997','2017-09-03',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Taddeo Shapera','09215678901','Mother','2025-09-07 22:43:59',NULL),(1909,'Anjela O\' Connell','14th Floor','2023-11-18',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Faina Crumpe','09348901234','Mother','2025-09-07 22:43:59',NULL),(1910,'Feliks Padilla','2nd Floor','2022-01-23',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Iseabal Vanichev','09171234567','Father','2025-09-07 22:43:59',NULL),(1911,'Morey Hadny','Suite 91','2023-02-08',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Titus Wegner','09348901234','Friend','2025-09-07 22:43:59',NULL),(1912,'Charlena Iacovaccio','Room 60','2016-02-21',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Fulvia Ramsay','09348901234','Spouse','2025-09-07 22:43:59',NULL),(1913,'Byron Himpson','PO Box 19035','2020-07-14',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Royce Matyugin','09215678901','Friend','2025-09-07 22:43:59',NULL),(1914,'Nathalia McGloughlin','Apt 652','2016-07-19',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Davida d\' Elboux','09348901234','Child','2025-09-07 22:43:59',NULL),(1915,'Siouxie Ciciura','19th Floor','2020-08-29',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Chandra Lippini','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1916,'Abbi Arnaudet','PO Box 18068','2018-12-03',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Woodrow Scruby','09348901234','Spouse','2025-09-07 22:43:59',NULL),(1917,'Delaney De Giorgi','Suite 50','2017-06-03',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Wait Lasslett','09348901234','Mother','2025-09-07 22:43:59',NULL),(1918,'Andra Frammingham','Suite 99','2016-01-31',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jilleen Cremen','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1919,'Ellary Garioch','Room 1169','2021-01-25',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Emilee Feaster','09348901234','Child','2025-09-07 22:43:59',NULL),(1920,'Gwenora Tock','PO Box 56623','2023-10-22',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ellerey Mockford','09215678901','Father','2025-09-07 22:43:59',NULL),(1921,'Jerome Norridge','Apt 1654','2016-05-10',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Allsun Leason','09171234567','Father','2025-09-07 22:43:59',NULL),(1922,'Silvio Koppens','Apt 948','2022-01-26',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cordula Tower','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1923,'Heath Semorad','Suite 59','2022-01-19',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lyn Poppy','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1924,'Opal Jaggers','Suite 33','2025-07-07',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Yorgo Becker','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1925,'Shelby Blagdon','Apt 671','2017-12-27',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Sandi Bridgestock','09348901234','Child','2025-09-07 22:43:59',NULL),(1926,'Sheila-kathryn Mayling','Apt 249','2020-06-25',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Cindra Eager','09215678901','Mother','2025-09-07 22:43:59',NULL),(1927,'Martie Stobbs','Room 623','2023-07-24',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Reece Arsey','09348901234','Friend','2025-09-07 22:43:59',NULL),(1928,'Rani Coffin','Room 1038','2020-11-24',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Sigismondo Garth','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1929,'Ileana Brodnecke','Suite 46','2023-10-30',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Tabbi Robjents','09215678901','Friend','2025-09-07 22:43:59',NULL),(1930,'Marie-jeanne Brownett','6th Floor','2022-10-07',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Mitchael MacAlinden','09348901234','Father','2025-09-07 22:43:59',NULL),(1931,'Kristine D\'Hooge','7th Floor','2016-08-27',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kurt Nassie','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1932,'Obie Santino','Apt 1783','2020-07-23',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Misty Pobjoy','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1933,'Norah Snodin','Apt 438','2022-06-08',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Hoebart Mathon','09171234567','Friend','2025-09-07 22:43:59',NULL),(1934,'Odilia Klassman','PO Box 54263','2018-03-18',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Sandro Greste','09348901234','Mother','2025-09-07 22:43:59',NULL),(1935,'Merrili Oakenfull','Apt 138','2019-01-03',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Marijo Pond-Jones','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1936,'Imogene Jacobsen','Room 1861','2023-07-21',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Patrizia Drysdell','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1937,'Cassy Corington','18th Floor','2021-11-27',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Rafaelia Salatino','09215678901','Mother','2025-09-07 22:43:59',NULL),(1938,'Sylvia Larchier','PO Box 28803','2024-04-07',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Devan Telega','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1939,'Chere Maides','4th Floor','2022-03-09',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Armando Folli','09171234567','Mother','2025-09-07 22:43:59',NULL),(1940,'Meggi Strethill','Room 47','2016-08-13',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Goran Eyam','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1941,'Charisse Cage','Room 1242','2016-12-08',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Gregorio Sendall','09348901234','Spouse','2025-09-07 22:43:59',NULL),(1942,'Darryl Chatters','Suite 15','2015-02-12',10,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ancell Naire','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1943,'Evonne Avo','15th Floor','2021-05-18',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Denis De Witt','09348901234','Child','2025-09-07 22:43:59',NULL),(1944,'Barbee Savege','13th Floor','2021-02-18',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lark Donoher','09171234567','Father','2025-09-07 22:43:59',NULL),(1945,'Tait Hallybone','Apt 380','2015-04-18',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Luelle Hake','09215678901','Child','2025-09-07 22:43:59',NULL),(1946,'Horacio Gutcher','5th Floor','2017-09-27',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Prentice Brightey','09348901234','Friend','2025-09-07 22:43:59',NULL),(1947,'Rochester Kingwell','Apt 1664','2022-04-18',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Trescha Runham','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1948,'Babbette Cornfoot','PO Box 97138','2023-08-04',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kordula Lazonby','09171234567','Mother','2025-09-07 22:43:59',NULL),(1949,'Garrett Sudy','7th Floor','2020-01-13',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Blanca Perett','09215678901','Child','2025-09-07 22:43:59',NULL),(1950,'Sheridan Capnerhurst','PO Box 14005','2022-07-26',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Raynor Feehily','09171234567','Father','2025-09-07 22:43:59',NULL),(1951,'Malinda Jansens','PO Box 922','2017-09-11',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Andreana Cammis','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1952,'Julita Desvignes','Apt 432','2021-02-10',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Stacee Clother','09171234567','Father','2025-09-07 22:43:59',NULL),(1953,'Barby Butters','Suite 73','2017-11-05',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jessi Luter','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1954,'Mandi Heard','Suite 90','2023-09-29',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Hadrian Iacoboni','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1955,'Langsdon Gellan','Suite 66','2016-10-26',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Arleta Kimmitt','09215678901','Friend','2025-09-07 22:43:59',NULL),(1956,'Jaimie Brushfield','Suite 77','2019-06-09',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cobby Vannuccinii','09171234567','Father','2025-09-07 22:43:59',NULL),(1957,'Thekla Harmond','Apt 1980','2018-02-27',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Arlen McFater','09215678901','Child','2025-09-07 22:43:59',NULL),(1958,'Jorge Ackred','Room 424','2022-01-25',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Sanders Dunsmuir','09348901234','Mother','2025-09-07 22:43:59',NULL),(1959,'Joline Weed','19th Floor','2015-09-22',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Eachelle Kindon','09348901234','Mother','2025-09-07 22:43:59',NULL),(1960,'Roberto Reede','13th Floor','2021-04-18',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Selestina Cabell','09348901234','Friend','2025-09-07 22:43:59',NULL),(1961,'Adriane Millgate','PO Box 73204','2017-04-18',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rosalyn Peddersen','09171234567','Father','2025-09-07 22:43:59',NULL),(1962,'Cleo Clem','5th Floor','2020-07-17',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kelsi Hebden','09348901234','Friend','2025-09-07 22:43:59',NULL),(1963,'Gunter Bodleigh','7th Floor','2020-04-26',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Trip McParlin','09215678901','Mother','2025-09-07 22:43:59',NULL),(1964,'Shalom Fontenot','PO Box 11393','2024-08-10',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Stefania Cartwight','09171234567','Child','2025-09-07 22:43:59',NULL),(1965,'Ozzie Trimming','Room 49','2017-06-28',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kathe McQuillin','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1966,'Tallou Aylett','Suite 42','2015-04-11',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Codie Tolan','09215678901','Father','2025-09-07 22:43:59',NULL),(1967,'Letta Oehm','Room 279','2020-05-08',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Fallon Kubecka','09171234567','Mother','2025-09-07 22:43:59',NULL),(1968,'Rutter Nucator','PO Box 54545','2019-10-29',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Agnes Neely','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1969,'Remy Pedler','14th Floor','2023-08-07',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Tobey Morewood','09348901234','Spouse','2025-09-07 22:43:59',NULL),(1970,'Ina Ennals','PO Box 37656','2025-06-19',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ben Vedyasov','09171234567','Friend','2025-09-07 22:43:59',NULL),(1971,'Shaw Cess','Apt 1070','2017-08-08',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Mandel Kernar','09348901234','Child','2025-09-07 22:43:59',NULL),(1972,'Deane Naisby','Room 941','2019-06-05',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Pieter Chant','09215678901','Child','2025-09-07 22:43:59',NULL),(1973,'Troy Gilliatt','Apt 179','2015-04-11',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Silvanus McEwen','09348901234','Friend','2025-09-07 22:43:59',NULL),(1974,'Gigi Coupar','Suite 61','2021-04-04',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Consolata Girth','09215678901','Child','2025-09-07 22:43:59',NULL),(1975,'Leonelle Toohey','PO Box 44793','2025-04-11',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Derward Axell','09171234567','Child','2025-09-07 22:43:59',NULL),(1976,'Charis Ruskin','Suite 21','2024-11-16',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Pattin Lead','09171234567','Father','2025-09-07 22:43:59',NULL),(1977,'Duncan Goulborne','PO Box 7062','2016-05-02',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jena Hutcheson','09215678901','Mother','2025-09-07 22:43:59',NULL),(1978,'Dallas Jellings','Room 410','2022-02-04',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ryan Fannin','09215678901','Child','2025-09-07 22:43:59',NULL),(1979,'Rebe Liffe','Apt 1594','2016-05-27',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Emily O\'Hagerty','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1980,'Annis Van der Hoven','PO Box 2635','2024-06-23',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ericka Pattenden','09171234567','Mother','2025-09-07 22:43:59',NULL),(1981,'Stanleigh Dumsday','Room 1305','2015-02-04',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kristofer Reedie','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1982,'Donelle Scohier','Apt 540','2020-06-09',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Olenolin Whittlesea','09215678901','Child','2025-09-07 22:43:59',NULL),(1983,'Dore Klimt','Room 149','2025-08-26',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jourdan Wear','09348901234','Father','2025-09-07 22:43:59',NULL),(1984,'Robby Archambault','Suite 35','2017-09-04',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Heidi Nare','09171234567','Friend','2025-09-07 22:43:59',NULL),(1985,'Dolores Tidd','Suite 52','2022-12-28',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Mattias Jessep','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1986,'Francine Tidmarsh','Apt 1641','2020-08-06',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lyndel Bulcroft','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1987,'Micah Heditch','PO Box 93209','2016-09-01',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Filbert Forber','09215678901','Father','2025-09-07 22:43:59',NULL),(1988,'Knox Clever','Apt 952','2019-04-12',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Chrystal Nucciotti','09171234567','Friend','2025-09-07 22:43:59',NULL),(1989,'Margarita Connal','Suite 45','2020-12-31',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Cordula Ledeker','09215678901','Mother','2025-09-07 22:43:59',NULL),(1990,'Chrystel Rooke','PO Box 24364','2020-07-04',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Huey Whitmarsh','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1991,'Gwenny Rickis','Suite 17','2021-02-20',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lacie Grieveson','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1992,'Daphne Yallop','2nd Floor','2015-01-24',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Arnaldo Harmour','09215678901','Friend','2025-09-07 22:43:59',NULL),(1993,'Taylor Yoell','Suite 49','2025-01-02',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Sioux Hollyer','09171234567','Friend','2025-09-07 22:43:59',NULL),(1994,'Marven Pert','Suite 56','2021-08-25',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tedra Cuttelar','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1995,'York Sproat','Apt 1291','2022-12-01',2,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Alexina Greve','09215678901','Child','2025-09-07 22:43:59',NULL),(1996,'Gabriell Siggery','Apt 1603','2019-01-13',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Finlay Spridgeon','09215678901','Father','2025-09-07 22:43:59',NULL),(1997,'Immanuel Sheryne','7th Floor','2017-01-26',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Malena McInnery','09171234567','Mother','2025-09-07 22:43:59',NULL),(1998,'Florenza Bartalin','20th Floor','2016-02-08',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Harper Legh','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1999,'Eveline Summergill','Suite 9','2021-01-04',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Christopher Spiteri','09215678901','Sibling','2025-09-07 22:43:59',NULL),(2000,'Heather Bolderson','Apt 1001','2018-12-26',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Karen Bullan','09171234567','Mother','2025-09-07 22:43:59',NULL),(2001,'Giraud Audiss','13th Floor','2016-12-15',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lorelle Giacovazzo','09171234567','Sibling','2025-09-07 22:43:59',NULL),(2004,'Asdasd Asd A. ','Asd','2025-09-17',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09511365191','Asd A Asd. ','09511365191','Father','2025-09-17 12:39:44',_binary 'ÿ\Øÿ\à\0JFIF\0\0`\0`\0\0ÿ\Û\0C\0		\n\r\Z\Z $.\' \",#(7),01444\'9=82<.342ÿ\Û\0C			\r\r2!!22222222222222222222222222222222222222222222222222ÿÀ\0\0À\0À\"\0ÿ\Ä\0\0\0\0\0\0\0\0\0\0\0	\nÿ\Ä\0µ\0\0\0}\0!1AQa\"q2‘¡#B±ÁR\Ñð$3br‚	\n\Z%&\'()*456789:CDEFGHIJSTUVWXYZcdefghijstuvwxyzƒ„…†‡ˆ‰Š’“”•–—˜™š¢£¤¥¦§¨©ª²³´µ¶·¸¹º\Â\Ã\Ä\Å\Æ\Ç\È\É\Ê\Ò\Ó\Ô\Õ\Ö\×\Ø\Ù\Ú\á\â\ã\ä\å\æ\ç\è\é\êñòóôõö÷øùúÿ\Ä\0\0\0\0\0\0\0\0	\nÿ\Ä\0µ\0\0w\0!1AQaq\"2B‘¡±Á	#3Rðbr\Ñ\n$4\á%ñ\Z&\'()*56789:CDEFGHIJSTUVWXYZcdefghijstuvwxyz‚ƒ„…†‡ˆ‰Š’“”•–—˜™š¢£¤¥¦§¨©ª²³´µ¶·¸¹º\Â\Ã\Ä\Å\Æ\Ç\È\É\Ê\Ò\Ó\Ô\Õ\Ö\×\Ø\Ù\Ú\â\ã\ä\å\æ\ç\è\é\êòóôõö÷øùúÿ\Ú\0\0\0?\0ñ\Ø”žrƒŠÔŽùJ”+L™$u `A\n\ZvFß¥5¹2Z\É\0}y÷ô¦õV‚¤Ê•^;P›Y@=\é‘vG¸\íñ òz´F¹m§µw3-\0´Óšœ9\ÆTx\Ì ÷4\Õ\àu¬\ê+šÒ•™/˜Þ™£\Î9û¼T}º\Ðk+#mDwfa´\â¦íŠ†¤=i’\Ä+“IÓN\Ü4\ã8ö ]/€)A\È\Í7¨\Å(\à@\î/¥Cs@©»Tw(Ò©­E\'µbœ)´´†‹\äT/y1=ù¦“ø\ÒÔ’¶<Šˆvõ,\Ù	š¸pj“%¤H †ü(\É‘œ©È§•T\ã\Ð\Ü6;0\ÅYNU‰…’r=*D#\Ëø¨\Ð\r\àLQ¨´öŠ}1C1ªC\ÌkS-QQz¤£üõ¤=k :Ô˜\ÊqÚ£÷§Ú¦Ka‡ ŸZx\äŠ@A\äŽiW­;ˆ=©F})sM\í\ïHQ\è\Ë×ŠLÓ“\ït¦&R<phü)ó.\ÉX~4\Ê\Ðœ)¾Ô¢\Ëô‡§¦ö,‘²}\Ã\ïU˜ò*Ë–«òŠ´Id\ë´\Õ\ÏC\ÔSÐ„ö¦g,x\ç5¡˜FÀuM\0¼|\Óz~†œ\çµ ;.Jdu\æš2rGj	\ë\ïÒ\É);\Òr?ýT¾õƒ\Ð\éN\èJJ8£µ\0\Ä^4õ9 ö 7Zw Z1M9$ÚœM\0¤Fù‡±£<TQKù¡%¼\\ª\Éø\Z©Z<\ËR;Žk>­1Ú”Si{\Ó(\ÐÀ¤ bœE7•$\r=1š¬x\ÇÖ¬¶p09ª¯Öš–7)ö4ˆ>r\Ô\äb\Ç~i°	ö­®¿Ž‡Š06‚;S2p¡¥\ÜqzQ\Ñ\àŒR6\Ü) x=h\É?‰¤{c\å<t¤\Ç_ð §õ¢²š\Ô\Þ\è8QG¥IB£v£\Òc\æ\â—$\Z\ÄRIÁ¢Žù¦’qL&£ýi÷§dõ¨ó‰A÷ªD²õ¹\çi\ï\ÅR•6J\Ë\èx«1®)·\Ëû\ÅqüBšU¥¤¥¦3P›§\Æ\Z\ìSE·—vo °u`]<1(\èM-ne©ld8\éÁ¨$\éW~\Ò\êz\ÅG%\ÙÁ\Â/\åI6Qg@<R\à‚}i¾cH	<c°õdúŽ3VˆhŒH=)\î ûŠcpÿ\0­Jü¾´\Ä1PzŽ):’=ñ@by\è(\Æ\ç\ã½\0.sœr\rœ†Ç¿O­.+9š\Ó\n;RŠ9\ÅfmaÁ\Í)+Ûšctª\Ù*x&©+‘\"\ÏCÖõ¨<\Æõ\Í/˜{Š®RnKš‰þöiCŒ÷\Ö9¡ -©\èi÷?=¸n\êj\Îc8ù¡eõ›R¢Š*†z‹sÞ¸ÿ\0.\ÝDãŒ¨\Ç\å]•Ô“N\é1`\Ø\ÎÆ\ç±\ïY!_ô\Õ\ã9AZ\É\Ý\\\åŒ\'fs\ë\Ð\ç4\É:T\äc¥E \â²\ênÈ£?7\áR° )\ÇlT(>qõ«\r\Ì*OPj‘~dS\ìiRGb\r9A\n ûÒ•ý\ÖE2Dq\Î}E4„w ÒŸ_L\Zs•ô4\0\íÏ·ò¥n´£`zM\ê^•3ZM\ê-%ÑŠ\Ä\Üoj‚LgŠ•¸¨[5q&Ch¢Š²BŠ(bò\èj\Ä}MV·?z¬/\Þ¥‡[•¤da\ïM©nÏŸZŠ„Q\ßÛ¼K.ûx®f\ÝÀ‘øUùlZ\Îñ>|Dÿ\0wú\Öéˆ³1fc¸Œô¬o\ãd\r\îGò­\ä­8¶\ä®s\ÓÖ¢~•!\Å1¹±GC+tj°>daøŠ\Ë\ëN\ägh™4J@òÑ½ý*N\0\ÏN”3b\Ääž´]“aÀŸ‘O\Î\ÔS2Ã¡ \0ô¥qØ“®\ìw¨\ã\ëšTdžy\ÎqH88úÊ‡°-~4\Þ\Ýih\íX(‰ª6\éR1ö¦Õ¢YQVHQE.3@\Émþþ=EXZ$dzT°8\Ìyô¨g\éVˆ\ÌGš¯I2’¹\ÐÍ¬]\È\ïJ\çû¼U¦’Só±c\îiø¤%vœõ\ìsZ\Þ\ç5’Ø„ƒM\"¥\È4\Â3È£f\r‘\ãšJ~)\í@\Æ”\Úy¦ž\ë@&#ø\éÀ\ì)¸§Rs\ïHcy§£ww\ïM468ÛŸ|Ò¸\ìJÀ©ô¤\Úq\è=M1_o½G3¹8c\Æ=k>Ws[¡]\ÕOšˆ’i(«J\Ä\Ü\\\ZJ\\ñIL\"\ä\Ó\È\n:ŒPƒ\å¥aI€Ø¿\Ö/Ö­UTÿ\0X1\ëV\é0t5TŒ9hTœ\ÒE£E€\Í4Š®æ™ŒVÛœ\Ã1Þ¯5.\Üö§(\Ãg\0û\Zr½ý©1Ž\Ùú\ÕØ­š\æM¨§ð$º|¶ Jñ“z‘\Å\'š\Æl„3\nÂ¢#ðú\Õ\éR\0\×q\äc ¨•b’=®\Ä8<Ø |\Úñ\ÅN;[³«.\0\É\ä\ÓgŒFûC†¢£\íÖ‘DtqKHjlR‘¾n¼û\ÒÑŒ\Ò\Zd%qEKŠaS\Ôt¦1”R\à\Òâ’ \èr1Jç§­D8\éKƒ\×5 IÀ-V;\Õ|\â!\îj\Ça@\Ä\ïQ1ËŸ­JNAH¸hl˜ô«0i—W$\ávú\nôKmO´#l*\ÇÕ¹«±¬Q\ÝÆ«ô½\ÑÀ\æ\îpöž¾˜f]±s[v¾²ˆ:Bÿ\0¥tcw4…±Ç§Zoc;¶Ê¶šm“‘Ó­M<qº:´jA\éK\ç!\ÏJxù´Xz\ØóO\éB\Âÿ\0®!n_oZÁq‚G¥z?Š\ì\ÍÆšePBsøw¯>•7žæ†º—Ð¨G4ÓŠŠefj2“½8\ç­%!\"Šp\ë\Í6‚„£´R»A©@)¸¥™ie\Ûô /Ëšy\ä~\Å\ã¯J!\è*\Ð9ªžI5e9E¤\Æ$‡\n}\êõ$¾ž•(.\'¸»@5ž£¶EA’NI\Í4:š\èHó5d\Í3WMRXr~¢ 2„­K‚E$s@t¹ \Æ«\Ýý\r@¸§@q#.x4u\Ìbh6\0†\Zò½BÙ­nä¿ˆ¯[\Æ;\n\á¼[§²]%\Â.DŸ)Ç­2“¶§ \ËÁö¨¯µ[š6Š°ÁEWa\ÍA²dF›ŠŠmIC(\Å:—Ò\Þ\ÃqÚ€)qJ“ŠAqó[I\0R\ã†A¨‚\ç®4w3ª‡ÁFi\ë¦\Êy¦(•#Œ\ÓvóœÖ¢in\Ç\ælS.ô\ã\0N\áßŠ‘©\Åx§«\áp{PF3H8¢\Å\ÜF\É4”ö;Ž}i1JÆŠG®9e¸X˜òG@*Ë¬6ë¸Œ“\íHŠ<÷\'¢\Ð>{•ó‘=+®\ÑV<~iª‡L•ÊžØªŽ‚\â‰÷YsZƒ\0c¥gÝ/bõ Š\Ì\Ñ6ÇŽ˜ ²\Û<\Òg°¤<:ƒM½Œ3Yº¼m“œ\Ñü\Ê>•¤º% v¨¤Ut!‡\ÇZ[§“\ß\Êf¸g#\Õ&­¬\Û}›P–<`nÈ¬Â„“ŠMGb9¦‘Sù,\ÝO‹°\Üzw©.\å¹\íV\Ñ\ØŽµ¯\r¬AFW‘W6/–\ÜP&\ÚfU¾“ó\r\íWb\Ó\ã„\ä®jÚ²gŠ‘Ád÷…¸\Ï-Bý\Þ*= t=\êx\Ü4x\'§J‰¸|”…\æ4\0šI\0‘H#žÀc\"˜NOÖ\ØÅ»±\ÆZ>y\äV{!G5\ÔI#ôª·	<[—‡­&\ÍS9þôT\Ò\ÂÑ±V*<Ph¬ÿ\Ù'),(2005,'John Doe D. Jr.','Buntatala Jaro Iloilo City','2002-03-03',23,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09511365191','Monica D Doe. ','09124433223','Mother','2025-09-17 13:52:01',_binary 'ÿ\Øÿ\à\0JFIF\0,,\0\0ÿ\á\0VExif\0\0MM\0*\0\0\0\0\Z\0\0\0\0\0\0\0>\0\0\0\0\0\0\0F(\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0,\0\0\0\0\0,\0\0\0ÿ\Û\0C\0	\Z!\Z\"$\"$ÿ\Û\0CÿÀ\0h\0ÿ\Ä\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0	ÿ\Ä\0I\0\0\0!1A\"Qa2q‘#R¡±BrÁ\Ñ$3Sb\áðC‚’%45c²&\'¢Ds“ÿ\Ä\0\0\0\0\0\0\0\0\0\0ÿ\Ä\0*\0\0\0\0\0!1A\"2Qa#q$C‘ÿ\Ú\0\0\0?\0ôDq¯±\æ1XkU5ˆ\ZAª… =*ÒˆIj(´ -(J-@ZT\Ð-*‚ÒŠ\ZQ¥EbhØ´)¥\Øwi¡ªÍ¡M!\Ê&\Ý\Â\ÔDœ&\éUCJÒ†\ÃJ\0\Z¢”¥U\r(‚ÒŠ\ZP\r(l4 -!\r†•iT…4lZF\Äan¦—cŠ1Ù¡N¦\Î9\ï>A]\'/óWI°\Ó\æšJ\0\ÕA\éD\r(5\é@x@` 0…A€€°†\É-Cb,MCª…\Ä\ì³a³1\Ò\ä\îblói€è´€bT6ø\Ô!£è©¬‰@ó\"D-±ª#Ce¶5Aˆ\Ðb€i@’\ÔZ€´ N@° -(¡„¥a4\r(”¥\0\Â,˜@m`”\0jÒ€i@a¨”J¤ \ZB\ÂÒ€aa\0\Â„”„@a]„\ÐM\0†Æ†À < ›lh»Ce60€ÀVD*”s\æ\Ò@‚¡%¨¤ i\ÍP6Yºi‚!\ä’6/D\ÐX4ƒM.Ç¡Sa¥\r‹J\n) I\n\áa€°€\Ø!± @Hi`\Ô\0*h\Zi4 € 46B,ln§8ñM„6®˜\ã0üT\Ü]l±»\Ý{Hó*\Ä/(	 ; $\0@    \0€Á@P\Z]‚¨0P(EEJSnE6y º\rWLl \ÔÑ°Ò‹±\é@X@D(P  A@’ IE\Ù9P\Ù9@Y@YCb\Ê\Ê(ee\0\Ê©j@z\0\äR©z‘@9\0Ô‚Öº:Jg\Ë$±±­qq\Ø-\Ò\ã6\ã|{\ÚM\r’/o!ùð±±—»\æ6\æ¸[kÑŽ:r;¯k·Q9ö&ƒ]Xq. ;JF¼ií›ˆ i®ŠBÓ\×\Æa“ sr\ÌSQ\Ó83·«|²6+£\ß\0k÷?\å\ê>aY•Œ\Þ9}:Ÿ´>\Z¸F.0\ÄÓ¶·<iÏ©\Î\ß5¹œr¼v5QL\Ébl±=¯\ã-sNA`­±\èz\rHlZØµ„Xó@5 -hl5„Z\Ö_ª€w\0\ïš\Þ5Awƒ\Í4x<\Ððy¨H<\Õ¼j\ÞúªJ<\Ò	Gš Ä£\Í‰Gš	Gš û\Ñ\æ€wƒ\Íš!&Q\æQæ§J<\ÑM™Gš›6\Ð\ê[d¤RÔ Az†\È/EÙ²ôM^†\É/SK²‰ “\"™2 I•\r‹½@]ò)=ò wÞ¨¾Eù]ò.\Åß¡°\ï\Ð\Øw\èl=¡ThQIuNP\Û\Ç=¥pÿ\0Bÿ\0\Ú\ì\0e´ñR;\å\Ó\æ±rtÇŽ×œûG\íš\ï\Ä\Z\â¤l””`øcxœ?\ÌG%\Ï\Íö\ïŽ3&¯¼W\Õ8÷µD‚s§Y\ÆU‘v‚ú\ê–l[¨|HZ˜³\Ø\ã*£—\r‘¥§¡\'ù§[Ç€•Žkœ\á\Ó\'u4«:z¹\ßLZ$p o‚wø©¥\ÛM\Ù\ïkKÁW\ÛT\ÓÛµý\í3\ß\àp\ëŒû§\Ô+%ž™º¾Þ²\ìß´[7\ZZû\ê\n–{C\Z°»g·ä·Ž[p\Ï‹[\í%oN{´\Ò\ì^\ÐSI±{AM´94»´Ñ±{C“F\Ã\Ú\æšA{C“F\Å\íõWF\Ã\Úê¦‡´94l]û¼\ÓFÃ¿wªhØ»÷¦‡~\ï2®—aß»Ì©£aß¿Ì¦“c=4¾wª¦ÀN\ä61;¼\Ê.\Ê?\Õ4l¡3¼\Êi\n¿\Õ÷õWF\Å\Þ;\Õ4l“#ýSF\È/zhÙ·9\é£fËŸ”Ñ¶\Ï\n a\0@H¨*(P  A@“\Õ@’$ B\Â\Â)8D„P\Â °€ai@P\r(	\ÚZ\Ò\\pT3·Ž\×Å‰\ÒØ¬\Ípð\Í7øg\È5\Ç,÷\â=|~7^i¸\\**f}U\\¯–y	sœ÷d’š\ç#º²Y2\Ýs<c Z‘w\Ûa¬ ’ÜŒ›qfù\Ï\\­2Lg<˜>mÂ¢E4õ0\â/‹¨i\Î²R[´³¶B\Écp\ë\Ó\æ°\ÔŒ\Ê2>ìŽt¤*Ïo7N¼\Åp·L\è* xÎ“–ŸC\èB–Vj½‰\Ù\ÇZ¸²\Þ\Ç1í‚µ­Zwî¥¾`®¸g¿o6|w\ÏH[`4\"G¢(hH‚\Ñèª‹J y \ZHòE\rJ¡h”JÒ€hPj¥† 0\Ä\Z¨SZmj =(X¬BK6@‚\Ä,\Ý\r5£’Š<z @”	*Ê\n	(P%J”	!p8@X@X@0€ @0€a\ák´\ËÃ¬rº2f\Ç$0žì“Ù¸ù\ãeœ\î£\\x\ï-<#]U5eÎ¢ªy)¹\Ïq\És‰\ÜüI\\#Ú¨š¡Ïí“ŸWHi\Æi\\]“òVY\rlm…\Ä\î×¹6½S)\èÃ†$N\ëø\ÓYe{\ØLo“\àZ\n½\Óñ§[xZ¾G4¶2}tóú)y#Xð\Ô\Ñ\Â±9\Ï1\×qù¬þIWñXŸTTÓ‘0p%¸\ØnS¾–q\íE~°\ß,q\ÅTù\áÀ÷»™µNY[\Ç9—‡,ø\î>V¼Æ’\Û\ê™;\è\åf\Ç\Z\ØNÿ\01„¸\Ø\ÌÕbva\Ä\Ñq/²~ð>Vl\ãø‡š\ë…\Üy³\ÇU¬À[s¢\Â„¥\0Ò€aa\0\Â\Â a0€a\0\Â\Â\0\Â\0\á€ma ,*J F”ôu\r‘¼÷Y—bP\ÝT\"’P  AP  AP  A@’%J%p€°€°€°€°€a\0\Â\Â„6A\æ¶W\ËU¿†i§xa¨˜\\\Èúr\\²›®ü^&\Þu£ A#‹\Þ\â\ì{\Ë9;b¶²pýL¬ï¥@wºÓ¾™õ\\ò\Ïúw\ã\ã¾\êþ›†AÀp\Ï\És\ì\í0ZRp¤DŒ‡E;µ8\âîƒ„\éÛ‡>VnM\Î6‚ƒ‡\à\È\Ä\r\à³Ù®\rª\ÆÑ´rXµ¹ŒJ6\è\ïF\ÓòIR\âr+|?\á´|–¥N¥T\Ù\éj`|R\Â\Çn7\ÂÜ¬e#‰ö‰\ÙÜ–\Z©n¶Æ½\Ô2xÆŒ˜\Ñ\àyy…\èÃ“~+\Ç\É\Ã\×\ÌZ}ž»B?\Ä\"†\ïS,v÷“­±»\ÈÀ;ô\ç°]§‡“,{=yi¸\Ñ\\i„´’\ë‚\Ò3\æ\n\é,¾œ,³\Úf\Åi„@E„\0€°€a\0\Â\Â@0 @xT)¨(\n °$\"[º&Œ\ÐU˜\äÃŽË„­\ß-Ceh\ßu\Ö]³¤žŠ„”(P ¨z¨P$ A@”P$ I@œ „a\0@ \0 06A\ãµ\r\\7.\Ökó‚(be8À\ê£ù•\Ã+\æ½|sö\Æƒ-¾¨O4X…„\ÄV2ºG;»®‘MoŒt.½R&\Ò\Ñ4¿\ZV‘sGG\æ\ÑôY\Ûr.)\éX#÷T\ÛzM§§\Ûl\0‚Lqcm–kRõ8V%? ôV2pAœ-\ÊÅ†ªè£¨§|R°=® ŽkR¹\×\í;‚\ã²\Ý[YGm4\Î\È-ý\Çy||rß·““ŽK·aû2ñ•\Ô\ÒÙª_ý¦Œ\02}\è\Ï\ï/#Ó—ª\í\Ç^>\\|mÝ±²\ìó‹\n¨„@\Â( , @0€\0@\0€\Ð@¡\Éša\0V \"’B\áY§\ÄW-.Ö–W»½\ÒN\ÊÁ¢\è[	!\n A@‚ AV%@’\'`P!\0@”P„€ „\n 03°\æUíž­·NÓ¸†¦˜ž\î[Œ‘°žž\"¼\Öù¯fÄ‰üZ\0ŽU\ì\âšk¡hÀ\\¶\ï\é\ã\0òY®’-)\0\ÆB\ËQeNvôQR\â\Ø\ärWB@\ætÔ¥Œ\áA:\áŽkQƒ\Íh;an9\Ú3\Ôb²}¡[á¬±M \ã‘]1ö\åœ\Ücþ\ÏóCm\íR–Û½d@\Óø_\ï‘ÁÑ‡·‡–~\Úõ)^—ŒH¢@H\0€\"	P\0€ \0€Á@h 0€\ÂŠÀE„øñ,*}£ûõ!¦‘ž\à[B² …\n	!@’	!1„B|\Ð(°ˆ	\0\Â„€ 0GºM\ìÖªº€\à\Óx\'¡\r\')}\ÛÀµ\Ò\ÖI+\Ü	ysœ\ïRNªò¾†1°á¨´5˜¹\\r¯Wi\âî¹»\Ä\È_—\ì£QoB\ÍC-- ˆ5ª*KŽCe¤—¶VT\í,~<¢¬b\0·X”ôY\0Žat\Å\Ç#˜\ZIZ‘Š£\âxL–ùZ\0;n£ž¯„%/j6y™†–\Ö7s¶	v1ó\É]ñxù\'Šõšõ<BÊ e \0€ @X@H @H\0€a‚@a‚H\0ª)FUýVMµ\í!š1\às\Ò\n²!Á@‚ …H@‚	!ap€°€°€°€°€°„ @` @` w­\à{Ó¢\Ùþ\Å.qû¥Lÿ\0‹\\\Ê<B\ã4ú5Kð¼\Õ\î\Å\Òxz™ñ\Ó\ëw3\Éyò¯fn\×\îNšt•[\íº–O¾œy©…©ù$ Y\ápc{\Çq\É_\ÅYüÑª±ñM²\âu!iåƒ²\Å\ÃN˜rJ\ÕSi{qœ¬ºl·D\åÈ¬\è\ÚTQwq\ëp\Û\Ö\æ)r‘Qu\â\Û¦G2º¬5\Í-h\És\r¹\åÉ¢­|a\Ã\×&‡R\\\â$œiw…\ßCº½,c¼«jj\È\'$G+G‘Q}Š½‚HªF+’\Ø\ã’N\ÒmÌ\r{®†‡\r¶9^Œ^,þÞª\Òü{¥zžK]øJ\á\ÞE\0\rw\á)°a¯ò)°aü%÷oò)°}\Ûü\í\ÞHh;·y!¡wnôCB\Ð}\Ðh>a\rP›4->£\ê›4,\Äf…\á¾\Ô\Ø-LümMš\rqþ6ýSfƒ½¬ú¦\ÍMøMšžÿ\01©¸hbx?\Å	¸h¡Qø¡7\rÚˆ?\Ä	¸º(Oø&Q4>þñ½¡¢{\è?\ÄN\Ð\Ó8û\Å3d\Ò\ç€~*F\î5cc¹S\ÉT]—I\Öé²Š².\ìd\'i\Ð\Z\È|”\ïD\Z\Ø|“¼MkaòS¼4A­‡\É;\ÅÑ³]’w†ˆ5ðþ;\ÃD\Zø\nw†ˆ7\nw†ˆ7\n\áÔƒr‹ð§xu Ü¢ü)\Þ/RM\Î/À§\ä‰Ô“sð\'\ä‡Q~Òð\'ä‹¡~Óð\'\ä‡RuŒshNñt‰S\Äö\ês‰¥c©Wºh\í-ú–¡¹„µ\ÃÐ©\Ýz¤›?\0SòC©M¹0þ\èWòKmÁŸ„\'\ä:–\Úøÿ\0N\çT>$©†~¹Bö\×\Ò\Èÿ\0¤©s–5Ž>^\áx„·=\'›¥$šåŸ§³Žyuh\ãB\ZÀ`//\Û\ß<FNñp¼W×š+Td`\é/<¾%u\ÆI\í\Ç,­º†Gf×š÷‡\És‚,ó\0\ær1x²©ô½”\×@\Ì\Éq†S\×\rr~X¸ðØ›C\Ãÿ\0±gišw«9kq²\ç–N¸a¦þ\×wkšÆ‡ô+…¯Lž\Z\Ûh\ï\á\ÉòIY¾^dp§tmy\Ío™¸\í\Ë/|[t¨™ñ\ÔÇ©\Î$œ€»c\É#–|v³ñö]\Å0\Î\×S\nJ€Nu6M8ù•¹Éy\ïQmAð­kj+hekAÃž×‡^˜K%\\mŽµ`¹\Ãvµ²ª<ŒŒ9§›O’\å|:û`8(2~\ØadŒ\ê h?\è»\ÏooF›«¿\Ø[ü7R\r\Õ\ß\ì\'\ä:’n\Îóü“òa?µ\çù\'\ä:€º¿\Ìý¹\Ôb\ç)\ê~‰\Ü\ê?\Ú3®ú\'z½G\í\Óÿ\0›\è\é\Ö\Û*Gý½:Á{MO“Ó½:\ÂLõ\'z\é¨A–«\É\éÞš\'¼ª?ºô\íMAj«ü/N\Õuš¯\Âõ;SPDU~\'jj¢¨þë¾©Úž\r\ÈÚ¦´%^\Õ5ž4âª›8! “ñWj\à\Ûým\êœH\Z~©mV²:z\×78+;¦‹u§§æ›¦¡m¡­òM\ÓÁ\Ö\Ð\Õù\Ý²†«\Ñ_\'ƒÌ¢ªôW\Êx,Q\Õz+\äð?cªôO\'‡½V\Õ2ñ#D\Æ\åÓ¾X\í¤\ìö\á,—jx%‘\Øz{K­;½N\Ò\\y)\Óo>\Ë6\èü\ÊtMm±ù•:.\É6\Øü\Êt6A¶G\æT\èlƒk‹Ì§Cd›T^e: Ú¢õS¢\ì“i‡\Õ:!&\Óªt]’m0ù\èl_²`ò*u6O\ì˜<Šu6/\Ù0y\êl_²`ò)\ÔØª\ÂS©´+<p—c\nX»y{\ív4u”\Õ2FD£8wª\ëÅŽ\ÙÊºWa•´\×$N’]N#«—,±ý\ÍK\á\×! \Ã\Â\Ìü¡Ø±C\08,W¡Ø¶PFyFS¡\Ø~\Å;°„\èv7]mŽ{}DZq®77?R\á\áf^^±5\Ô\\gSL\íX†²H€#g•Œÿ\0‹\×\Çs¬\ËynKr1¼¯z\ÓR\Úi_9h\r`\ä\äô\n\ïi#?r\â‹\ã\íµw\n¶)€\Øm«|s\ç×žË¾r\Í\×.Nn·P\Çñeúûw†\ß5]3\Ì\ØZ\Ö\ÔI\ÞZH8\ÆÛž‹¶<8\ßN\ä\å7jö¾¾´T\ÉAU+œñ,˜xŽ9\éw\ï\Íp\Ïµ\ê\Ã9œ\í‰\ÎžcT\È\ÆsžKÏœwÂº\å‰ò2”4ð¦8µ|«/³\È\é\00r·Ž1\Î\å\åÎ¸‡‰n\Ô4_´X$\îL\Ý\Ìq@À‚y»\Ð.¸ño\Íc>\\qñöw‚øÛŠ«û·\Çi­|ò1ÙŽ@ö³\Z±¤óð»g¨ó—…ö\è.$£½\Ó÷d\Æ\âöòi\ÙÀs`Ž ®õ®ýe›‰{{-³U˜š\ÆA \Ö\Ð	ÛžF¾Yô¬û9\Úoö¥yª\È\ËI«\Ä22\ç\ão¢õa‡oŸË–ž…—‡\Ës9ø-þŸºº¢\ÙÝ¸‚\ÖýüGqGg‘\í\Ô#\0z„œGp6¬tEgw4hšÃ‚\ÑôWñ\Ëe;3#èŸˆ\îy´\ì\è\Ñ?\ØóišGºüI\Ø\ìtA\Û§\ã;´­Œ{¿’t\ÒöGkZO ³\Ô\ÚM%#&—I+0;.#²Ò¹ž(÷]\'N\Õ\nº\Õx1Œo…‹„„\É*’\ÇN\èó $•¬x\áÚ£W\Ú#ˆÁ\ÈeK\Ævª\æ\Ò	\Ç$˜/f?®\í³QºB@\ß\Õf\âLžl\íK‰ª+e•Ñœ4E¬0K–\Ý\'°\Z—UZ\ã88#Ÿš\ÎXyje\áÛ©\é\Ü\ì\0“²tV÷¼x\á^‰Ü™)Ë“¡\ØB,§C°¶\nh\ìv6©Šv9~,²LWi>Å\Ç%¹\Æ\Î\Ü:JZjÛ£›\á\'–\ß\ÆM½yg¥ÍŽ\Ô\Ê;ý&‘Œ8®ºð\ç\Ûn\ãof)[ðVO‹\ÐIbšQ\'Pž\íN »´\êl“\ZM‹ºN«²{¥:¦\Å\Ýz\'Sbî“ª\ì;”\êl]Ï¢u6\Âu\Üz)\Ô\Ù.ƒ\Ñ:›S\ß\éÿ\0±¿n…f\ÂWˆþÐ¡Ãˆ\ÒN\Òÿ\0%×‹\Ó5±>0«µ\ß)\è§Dãƒ\Ék,%ò›{Ÿƒ\\*\ì‘Oø†T\Æ\ëckj˜\ß\ÄT°‹jjhû®AYŒi¾(\Øñ²j#–ö“\ÚUe‚ü\ë-–*7\ÍN\Æ\É9¨av¬\ï€\Û»¯?\Ê\éŸL~ŸS\â|	\É\Åù3ûy6ûO3{F’µ\Ð2_Xglq¸–´¹\Ù;ù\ç\ê¤Ï¶-e\ÅxòÓ§EHyk×ŒV^-,­v‡’\æg\ÝR])Šk-d\Ì$loic˜\Ð\\9o\æº\ã\Éc7Eo°Xm2­”l\äi\Ö\Ö\é |WI\È\Ì\âŸÑ©©´¸¹°\Æ\×gŸ¼~¥c,\åt˜&püZ+„¯\æ6»jM:¥œƒ#\Æ\ëx­D®ˆ8\Éšy®õRc¶V\é\ÂTµq>	\ã{©\\\à\í  ŽDc‘[™\Öo?p\ïðM¯‡¤’¦\Ó0‚y#\î\Éx/-nrCs\Ë\'ŸÁuÃ›¯ªó\çÁ†_\Êm2‹„™Gu}u$˜{Ÿ©\ã<ùüw+Ï¹]»cdš\ÓKZ\r¶g;\0¶7>@¦5\Ë(…ö5«¶\Å_\Ä÷*º\Úh$ÁO–f³^\ïq\0¿Eô8²˜\ß5óyq\Ë)\â=_WoÁ#$/E¯*šy˜ú€N1•‘i£\ä\ã\nÁMYWe-j±óM­û*A0¿<–j¤DÇ“žŠ	,%¼ð·?M.Ç’šQV8º/U›\\\Ï	æ¹´™E)d™QPW`\ámî•€Dyz,\å\èHµ\Ý\"–¨\à\á1\È&º JHo,+D\ØN¥\Æû}…Í·‰3\áªŸc\Î|fÀ\êwž¿è·Š:—\Ùj¥\ÒY\Ù9\Ò\ì~k{k\á\éz8N¦·\n\ÆWcX\á\ÕhF¯s;‚7PW°\ïtM¢U7IX°\Ù\ê\'x°OEb|Ž‹Q\'n‰‡©ë‹¢!n_ó\ç\n=ÿ\0ñUC^ò@y\Æ~+ŸÓ¦^\Ý®kx‚žZ•jzu\ÚJ\Ò<–\ç§\Ù\n‚\È@œ…\0\Èò@œ$6N¶¨¢\Ö<ØŒCb\È!°\ïZØ»\Öú(l;\æz!±w\ÌôCb\ï\Ù\æ\Ø\Úý[4®«x…®2Þ…g,i·…þ\ÐÅ¯\âI\\Ã‘\ÞU®(•‘\ì¶.÷¨#óré—¦~Ÿ@x&Q\r††F–…\Ë\ábUd\ÂJ¶}\Õ-X¸¤Ÿ\î’V´W™$Â—O:ö\åC\Üv›)Áž¥\ê4\ãù/‹ò±ÿ\0ž¿Iú}\ßÄŸ\é\Ío±U\\(\\@l\ÐU0¹¹Ý¹\È ýA]8¯·>lwªÐ°e\Ä)W•\Z\Í\Î	R5£\âm\Øù¦ô\×Tjš@Ü—nš˜ªkX7À\06–·œTª°\é–F¿ös^q\Ék\Z\Õ,€òC·òZ©‰\ÈZ\îŒ|e\ÓW¦Ao;\Ä\Â|ð·2q\Ë\rt-k€’1¥}Í…\Öù\ØF\æ\'È©<T±‹\á«L<?\ÂP\ZjvKQ$m|º¹‘“Ÿªœ\Ùøµ\Û\âqK¬]\ã‡\åŸþ¶ûCó/³7Wô_Oƒwÿ\0Oòu9²\×övy\Èq9]tá³¢\ã\'s¤’UÚ½Ó—H\\\ãº\ÔCM’ª_|Ö¬\íR ¥¡ 7É©\ËQ’\á;¢œ•\Ùa5PN¥\ÎÆ¥J§8Â¸¢FWHˆuÚœ\Ò2³‘·ÅŒUj\Íù­¡\Æ`5ÁEŽ7ö†~ž“\æ¤öW›¸\Ózÿ\0E¸\ÎÛ¿³Otù\ê³\É<®/UQT¤\ã;)‰SVÑ;-H›C™ïœ–´\Z–0„\Ò#L÷HV*$\Ó5\Ûa§è¨iŸ(Æ’Jºj(&f@\nu­mÄ¨mSSñ•-Û¬}:e\íª\Ä]©\ä\å¨:\å–MT,9\è·“sê®|\ÓACj\è+-ó	 Ð€\ã÷\ÚA+\ÙCsºÜ‚$\Î\ÒÜŒ«¦v:y˜z¦™¬”$¬Ø»0\'\ÛrSHoŠiF\éIfFSB•Ncóº\Åv9›32·¤…q(hµL\ï&•­\è±\à>Úµ\Ëz®‘ý*ˆ\r\Ö1þ\Ê#\ã\ÛkË¼[\Ë\Ñô÷·\r\Ê\×\Ú##a\Érô%µ \Î+Q}G\î¹$t#\0T`¬«‰ý¡i˜8ö†q\Ì\Ú\Æ~=\ã‡\è¾wÌŸòKþŸoôÜ¿ñ\î?\íÅ™oe=UMÁ®\Ô\çT08\Ëoù®<^\Ýù¯‰Q;/#\Õn³…O€áŸšË¤YÀ\Öl©k‹CN¢²\×\Ó9_9|\Ý\Ô^\'~Š\Æ2£¶E\'~±\Ìn¶\çö\êF¹´-a\ß+XC#s9Ì”—7Á\æ™F°L¦œœ®q´\èc8]ds´°\Ò\ÐZJÖ˜\É\n¼f?„\ÉF*—‡©ç¬²G\Ð\èž9\Ù\æ5\0>k¦ž‰g^üi\Ü\ê,ý\Ôlc€Æ†ð_su4ü¶WvÚ¢¬\ÑHCˆ!TX \È\ÜHUˆv”sYX”«EJ„7’±\Z\ÎEj!\Ð\Ò\nÁ#\"$ÑJ\Åj@\nEHÕ²\éÔ›©H:v`¬F’\ÚHZ\Ó#d›¦‰\\o\í\àl/žšHZó\å\î2hóþù-ÈoÙ¶?\íó?ªœ“f/SQm~f*N‡¿[‘“ðR=À…­&\ÒmnŸ\å]m\ì÷BÆ ±§¥`oº¤4’\ÈXÑ°¦\Þ\Æ\ê)¡\Ç+´C—H\È\ê¼ñ\é\Öêµ•}õ\Ö\äV¡§`°\Éý…Ÿ¸\à³lƒu¤œU\Ò$Ê¦Í½ù\0z¨‰lA\n6‹Lµ;V\ç;,Obt\Ù\ÊØ¨©o0˜VöHpT+‰¬\Õ%¹Ó•E\Ç*‡b9aHˆu\r\Z\ÈX¢m†^\êRÒ®7I¸’PmRÕ¥L¯†\ãÂ²\Åý²½\Äo\í9ü\Õ\ãJ\Çör\Ý\\kmoœ¸]2ô\Î\Þ÷\áJr\Û<@œøB\â«8G $¬Õ‹JJöwDt”Ä•\Ì\ç=VtÓý ¤ñU\í\å5¿Dg<\Ü×»#ó^—?|¯·úf¯\rÿ\0·­­š\nj¨§ñ\É#ƒ™‘\Ì\är^~)\åßŸ]|-hª\ZùÀ\'›C·]rŽ8d·‚@B\æôb°¦~¦\í\Égmªo²±\Í\ÆUf\Ö]\ÓII3\åkD™n\'\n\ÈÅº.\Ñ}\ïjCd‡¸{Hý\àZ~k}X™º…–ÿ\0¢›Kt\î\Ünºcàº¢Š\ë-ES\áögwzs\ß4\ç\È\åg\'L!\Ç\Ô:Šx¼^lW\á\Ö]´”U\r–0\áŒavÂ¸\å’@N|–\ØTÝªDmkK´k‘Œ\Õ\å—oùe©›Á“_apsL\Ä\â–÷½|œz\âòô\\\Ð\ëõ_{O\Ê3\×{Sœá’µ0\Û6\éA¢F\Ôw\nuó¤\í\áoMc09\Ãu¾‘%µ*`;©\Ö/”\Ø,±0\î2¦¢Í¥\\:q¤)¨¾@[!\Ï%|&©Áo‡«SpÑªš\ÚÃ¤a5)\éIRÀ\á\ä¹XÜ¨ñòX‹Oc!tBt©`ŸEN\Ò7Z\Ç–¤šPz-uM’(‰\Îu†Üƒ\íjy°\Ìð„eN¬Üœ\ëF]@6\è5_gJBÊ¹6\ÛYýV\î;I^¢·R\åÛ¢“\Úêž‘¡»…}&’[\È\"\èOn\ÛM\Zlq\ÊÊ¤G€\æ®\×E;¢lÑ³NI\É)\àsû•–šª0’\á#¬\Êíš“‡E5{%‰\ÇH<Š7\Þi\Ñl¾\nƒ\äºGñ&\n°-\ÒsZ\Û:6%;¦\ÓF]0ƒžE6ºZA t:‰\ÆV~š‚·°½ü÷S2F\å…hV\Ï2£_4yJÜ;n²\ÐE-ÂšXnJw•°@H*\Ä3UL\ì\í\ÍK\ÔN5ú”\ÒCœ@\Â-òô*X\Þ\Þ)\í–g¯8ß¿\Ïæµ‡¶k	ÀŒt|elsFýø\Âé—¦^ô\á†U›$N, –…\çòÜ©\ÒUI°iRcKD\Ë}[A\'8ø®“”\Ä\Ì{\Îr³”nT\Ð8 ñ\nˆ -\n\\\ÉL\\pz°ž™\Û¡r\æ\àü˜øöõü/—þ>~}_o4q\Í<ôVù\é§câ’˜h{dxp;ƒó_3u–«\ìrYp¶#[¦ŒIËŒa\Ç<\Ê\éc–5oK9pkr58\ï\è¹eø\ê\Âk¥-+Ÿ+ò\à\Ü\éo2¹ãµ\Ó,\äŽY\Äu\Õ{!!¤\á¥Ç§Áz?/å´‰}e$‚0\àþMÀ\æ¬\ÆD·*ŸÂœ)VÖ¾j’^H\É\ÆùWÁM$4\î«ô§»c\â\Ô1\Ðõ\ßL’úûMO³ýä…­`#\Þ=wVc*K–%Ü¯Îª£Ì¬tDn\Ç1\Ñs¼mN]6\'Xe£h{$sœ\Üv\ßiµ\Ë\ä\0\×L\\rªŽj®´ô37,™\î.\rv\à5§?ª\ÖSÃž÷7<\r\Ã\â¶ýO1”P–\Í9\rÜ†>¤-ü~.\Ù\ÍzO“\Í8ø\í¾\ï§h_YðLU–÷N\ÈX³—¦2£O\í€B»òÄ›U\Ú!\nÛµ\Å)¯-\Þ\0Š3 Â†\Å\Þ Px)£hõ²b3…c5›«wŒ\åc%ˆ¬x\ÉŒ®šHŽA…¹S@]“²¢\â\Ø\ÒYº\Þ5š²c\Ò\r\ä¦\×NgÛ´L</S‘ÿ\0,­\ÏU\Ë/\äót!\Ööm\Ì\Ê:5?gøCjeÿ\0šWYé½3mŒš}j,Zp\Õ\Z\\]Œ y­YXV\0E\È\0ƒ›ºi\ZÝœW£}\ë¤xÕŽjlh\í¿\Ü\0»cé”²Òµ ‡rÂ¡­òˆjf\Ñ\é¤’0«mr4°\ã\ÍLU2G€\ÞkA¬µÀ•b!T—òD\Ñ£G%a¤\ì€L\Ð\æ\ì\0À\ÇT€\å‹Qä¨•H\Ö5¼”x\Í÷\àtA\ã^\ØbýÀ\î5œ}ŒOg0ñý•®\Z¦\åt·\Â>„\Ø\éce² \ÚB\ÌjCîv6P(Á˜B\ÒiCv¤cd\È\Ï5›-,À6&ƒ\äcû]\ìß‡8ž\ÍrºT\ÓK\Æ*9^\É —Gxö°–\ëÁ\Ü\rùú®<¼eû¾Þ®“ž§\ÓÈ±‘,,•¯\Ã\Äa˜ø\áx+\é\ãV6\ê–\0Ü¸·ú.yG\\*\ÄR‰It˜y”m€³†\ï–+‰e«°VG+m\r¨¤{´	\Û&4žš¶üù/Fg=¸\å¼<\é¢\áÿ\0Ú—q«†ŽF\×\Æ\Ð5døñŽ2³fšÇš7\Ö¿\Í_UDÚŠVrtœ\áœ,ÿ\0\Ó_\åcŒ\Úæ‡†\î\â‚j\ç>‘‹V@i9 \àþˆ¹|¬-˜Ï³w+]þž	åšŽ’¡°µ®\Ù\ÚI~¾Krycóñ\×!“Œ\à\âye6\Þ¬l\Òi’¯-\ÐNq°\ê®XuûIû½7œ\rQ4µ1µº\Ý›—\å¸\0ŽG\è¸e\å\Ó©ZºÚ×º68j`$€®1œ²#³>¸ñG\Ô\ÔEYQÐ³\Äùv{\Ây\×ò^œ8o$Ô¯&_#»6ô\r‚\ÏKc¡4ô\å\ÏsŽ©$w7Ÿ\ä=¿‹Šq\ÍG\Í\æ\æË—-\Ôö»=WW\Ç\îN\é±µ†V~\ÖO\rH\Ö\ã+\niu”YOQKdyæ¢—Ý„Ù¡†„Ù£q\æ2¬KŠönX\È\Å­v£\Ípu‰´€µG7æ¨»¶¸i+x\Ö±\îˆR+›ö\éøN¬;¬etžœ¯·¥n«c \\£m7a2\ã3ø¥uŒ=+B~\é£\Ñe¸Ÿ$(kpŠZŠ\0@a\0A\Ì\å÷Wž´i‡\Æ>*Djmc\îFW|}2°#-Z\å\Ù\rÁ\Ñ ` I”6-$%žQ[j­Á¼¼–qi6Y¤þKH“­( —n›Rû½°¥4A‰!¡†xQHkpõ6š<\àtrWf‘$¨tY\Ùgb‡Š..ö€Sl×•;Y\Z\ç¯õvS0<8ö\ÆpMc\0?5\Òÿ\0ôB\Ä1l…§£B\Æ-ž\"\çd%Æ´†rV\nk &Lˆ²µÇˆFBH±*¢\ÍO$/÷dik¾a4O\×Q›W\Ü,ò€&¢žH]‘°,qnW\Ë\Îiöx²—\Ê0‰\Ú07,;;\Ï7h±£®.°·:›·OU‹\Z•6º(ª`tR4:9;¦7N\Þ\â®\ß\Æ\ÌC¢€\È\Ù4\ã F \Ùu\Þ\Öpq\ç\í¹\á\Þ8¿\Ò\\*g«¶Á<R\é,\îr0\ÜI8<•ž.\Üù?O\Üýµ£¦\í2 \Û\êi›\ÃÒ‰$\Ñ\Þ<i\Ô\ï<nºwñ­9cúVv\î\ä‡¾^xŽŠkh¦m=DmŽM\Ëð9ŒôX¹\×l>œ®\Õß°\íö~m²†(\àˆa¤\Õq\Ë-´n\ËP\Ê:31\Ô2\ZR2¦1\Ç,´DuOd\Ô\Î\ç\í³\æî¿–ImÓµý\íò\ÃÂµwI€\áR\\ÃŽl`\Ò?=K\é|luŽ\ß/\ä\å¼õý:5Ftœ.\ï1˜\\q¹H†.o-„­D¬c\Þ_T\ï<¬7ô°…\ï\ÆUM46—E¨¢I\åjZ0‘­\ÎJ)]@\ÕO÷nø+Š_L\Õhñ8¬ä˜«ÁÃŠ\à\ë‰ö»eb\è¨\ÚurU4´¶µ\Ù.˜¹\Õ\ÔCÂ´BðŠ\ç]ºS:N©x\Î\Ì+rør\Êyy\â6\æ\Ô\Ã\è7aP\æñ?ÿ\0Ú·ô\Ç\Û\Òô0}\Ósä³·M&±¡¡CE\"€@h%jh9¤‡Â¼õ£QŸø¨u«xGÁwÅ•‹[•¤\"XA\Ê*>9VD\Ù/asr«&*\â\"¤F·‚×³Œ]®\"h:Vô‘9ƒ\rY\ÓDŽjh(‘v\r\0©£`ZJ@Œe4›; ¦•YuŒ5¤…,\î\"i4O9XfÇ›ûPgöŠÐµ‰\Ú\Ã1¤\â«UKy\ÇT\Çû.ŸHú%\Ãl´\Ò~&ù,\â\ÚÄªR\Ý%\r—in:¡\Ñ”Šò\'Úƒ†§\á\îÒ¿nCe¾ò\Òñ\'6÷À}\ã><ó>K\ÅÏ†®\Þïžæ¿§8£~²\ç‚áœœ\íñ+\ËcÝ(N\Ø\çv3Œ\ãm²°\Þ\×V©…D:\\\0#\È\åbøu\Ç-¬¢\Óû\æjožÆ·Ûªe5u\r8ik†3Ë’\é+s\äkš\n\Êd`¯5©•‡ùžÐ˜Csh\çœ.w&7rö­¼M\Ý\Â\\\æ‚\Ò@>ƒ?\ì,IºedŠùk\ê#0C6÷\ÜþEt“O-»1QQ\ß\Ö\Ãn£Œ\ÔI<­†Ñ’^]†€=r\\1\Ýp\Ï=G¬øR\Ñ‹‡(m?X¥…±—þ#\Ôü\ÎJúxÎ³O••\ív°•\Í+[gF8\ê›M!\Ý˜HWic)0ö‡\ê;\åq·Ë®8¬!\ÑYSM\r”5”ø\Î\ëR¦“\Ü@Õ€FF+(\0\Âª7a\n\Æj¶xZF2j3ó06¡\Í\Ï\"¸¶o…Žwˆd+Œ]­¢¤a\0´m*\Zv\Ç\È-\ã4\Åò’Õ¤\ZŸj\Ô\â~¯oQ\äQ-\ÓÌ´Í¡Ÿ\Âµ=‚\0oõ,?\â·}3òzbœb!ðXu:9 h\r\\p7A]WZ\Zý\r;¬ZC\Ð\èÁ\'šÔ†\Ü\âI™Œey\ÚnñŠH­–<Óƒ\è»\ãé•£Y…¥\Æ\È\"º2\\vV3¡÷xjl\Ò%c‡w%-ð’\"R\ÈJ`X³ƒ\Þh[ed\ÖøTlA»\åCCsvM‚hPCNP-£\ì\Ö÷$¨ŒO(_5\ÌyÓ´\Ææª³>KX²\åT\Î»Q;\ÊVþ«·\Ò>viqŠ»„\èœ\Ç[Aú.q¼}4\ÇU$Š_ÕŠj\ÈÛ«\ZŠÍ¾W\ÕYÎª\Ý\æ\Ø\Å5Fœo\íqo\Î)\ß\Ñ\Ü\ZøŸÕ®\î\ä\Ç\Ë\Íy¾M\Ö2½_\ÙYþžI¢­1—\Ã$b*¸\ÃCš\ã\î\ï\Ï\àW’\Íù{eú\nª¹.cñ¹vvü\Ö4\ße¥®¸¶FD#m\Æ\ßU›‹xd\ÚCWK$\Zš\ì’98.]k\Ó\ÚSB#Z\×D\ØÁ\'\'Ã±øetžò›[\Øhû†‡I[ \0¬òôVù1šjbž\Ó³chÏŸûžw$cø®\äÚ‡\ÇœE^AÁ#—\Ó%t\Ã<Ü™\ìŠj‘o°M3XÜŒ9\ÇO\Ã\ê·1\Ýr\í¨\Þ}—x]·\ÕO\Ü\Zde3Chö\ãKd\Çð‚Ä•\ë\à’\å\Ó\Ãò-“þÞŒ×­\ä&G†´’‚;\'kó‚‚-\Íÿ\0rpf³Tò´\Ô?<ò¸\Ù\å\Ó\á6G\ä‚\Ú\Ö\Þ\çl\ã)t®xµá”¸ƒ´¨Ð¼Zñ\ÑðE50%¥X•\n <4\à,ÕŒ\ÕW†¡\àó\Ê\ã}µm\Z‰\ÏD\ÅZ=\Õ\Ö3³\àì´ƒ‚\Çi\Î-\àÛ†9÷.ý\n¸±Ÿ§˜m\ç6†üiûaIRþ@Jº}9\ÏoLÓœ\Äß‚\æ\íP@@\Ô\Ù\Òp‰TsB\ã\\	\Zò‘w\Z#mt\áOru¶\È\\@.¬\ã†Ú·Ê¶\Õ\Åb¢¥Œ\Öw)\ÓH\î¼5÷–\è\ß\Ï-Y4‘hZªˆ·!{¡”M,ciU\\\Ï	Y¾¡fO$\Å0·\Ói¥›=Õ– uD¤r*\ÄPEE­«\ì.\ÆP\Û3s»\Ë&Z€³j(®\Î\ï(ºÀ\à§OR\Ê±GkÏ¶Ó‘\ÒAú®\ì}½\Ø}d±F\ÍG\Ü^}ùuŽ§µi# õZ•t˜\×e›­#	Æ‰\îc|\Ï/kð\ÙY0(# ]k8§(Ó—}¥@wR7©¯oÿ\0¯\'\Ëþþ\ÞÏƒü\ïý<“\Å\Önõþ\Û»©¢iÃ˜<G\Èz¯_O&ò\Ìw\Ïöycªd»in0\×.šÛ–ÿ\0²©*\Ìm\å‰	\Ü|”\Ò\Ê\Õp\íl`\Õ#[¿\'½\Ön.¸\ä\ÞXfc˜uG^&žy\Ë\ÑgN–sV²7i|¬\çNþ_ªº;!\\«\r(kßƒû\Ï4‚75&,\Ü\Ø¥k\æ½\Å\Îñ8ã›ýYk–\Ú\áû—V\ÃYw\Ý~8\àižg\Ì%\Ë^\"\ã‡o5\ê>Ç©™Oi¬°1¢F0\09a¿\ê½x¯/Ì¿º6\í;•\ëxÉ•ºšB±G\0`) \Ís¡ +±UN0]s³ŠåŸŠ\Þ\ÃKo¦09\ÞKxÄ«Šxƒ#Â¨x0&\ÍP\Æù@ €ˆA³\rtzòT\â9.\ÛX¤Z\ä\Ò\â\Ô\ÅW‘\Èt…¾É¤È·nV\â\Þj¡Mp<Š\ÇiÄž­n61;?B®,\å\é\æYÿ\0Â‡§õDk;7ˆk\Äò[ß†dòô\Ì->‹›¬H€‚è·Œ‚ˆ¯©:&¬\Ò$GSži\Ùtó\çj|ùú®\Ü^\Ê\Åð³õOLq\éù¬\ç\ìz§„‡þOüe\"\ã¤\á\0$ «¸ƒ¤¬PÝµ£\n\â\'°xÖ„\Æ{¨ª €Šk9*\ÆJjè ®¹´\ÎQc/p`\Ôt¬SJ\Ê\æ;Øœp¤K+µ‡{QŽ­Xûe\Ä\ÓW¤ƒõ]˜û{±)Z\ÛM>v¡y¾\Ý\ã´\ÂöF0v[úõÕŽ„8€p³\ÛEŒ]Ê­ó\×·\\®{­L|5\n×¶\0\×n»Ì·‘jn\0~\éWj\àÝ¶ñ«o·q`¤\Ó\ì´.\Ö\çu’M\ÚOÀn\Íxþ]Ü‘ô>:¶¹}U ’@#\Õx_F\Æ&ÿ\0m„\ÊCó\Ýns¤\ã\Ók®5\Ã,T&‚²!\ßGx7À\Û y•\ÓnZ§h.Bƒ$.k˜r\Ý4L´\Ó[ø€º7¾9œ‡ w>Ÿšukºc/°µƒY g$—s‰\Õ;‘Up©ºJ)hi\ÞøÝœ¸\r¼²JI¤\ío¦«„x6\"È¤«c…H\Æ\ä\ä´oùl¥»\\qÓ¥\Ú\è™KŽ0Ö±»1£ è³§_¥\çñ\Ä~;‹…*\ËD5\Ô\Þ\Ð\×\Ü~¢\Üü6\Ùñ}Xùÿ\0.yÛ²½/!©Ÿ z*ˆ\æ ±Sj‹S>#v£…v\Ës”Kui\Î\Í9\\3ó“®#Ol¯ci\Æ]Œ\ÓáŠº·\Î\'ˆ<‚µ½‰D\áX€e\0\Î\È+n\r’BCB›4¡¬†H\Þ\\\æ«X\Ãt2†O¿\"\Z^A;t{Ã’–º\ã4²4\Å\Íu\Âøs\Êjª¸’µ\ÐSb7aùY\Ï;=&8\ìŽ«–o\ï•xò·\ÚY£||\Æ\É\ÃU€ŸùNý\ní‹žo+ÛŸ¦\Ü\æù?5\"5=‹¿ÿ\0º*‡«J™µ\Çöô\Íþ]Ÿ%H\0q\0”Fz\ïR\rPc.k9¨LyYn8\Ïi\Ì\Õc?/\Õu\á¾Rûs¾©kj`fF\Î#ó[\Î9÷–\é\ë.:¬t\çüsj.QD€Š¿’\nËŽ4Š·\ç1ZN µ±6?uP5¢·\Z\ê«%Ç’‹ €¢£\ÔS™˜B˜»QKM.\\<\'‘Y \n-®YôR#v\×Dh\ëfaô`Œ|UžÒ¼öáª±£ÿ\0wù®\ÎO\\öB4\Ø\àó\Ðšûz#²Ù²\ê`IÊ¸©WH…\ÛtL Á\\[¢´»W¶\Ú3¸ò]ñô\ä\Æ÷vÙ¬3\Îò#®£\×\ä7U¨ò\å\Å\Ó\Óö —ûššº2z¹¯:¿P¼Ÿ+\Ðøul\Ø[(\Ò\æ‚\nñ\Ç\ÐS\Þm\Í\ÃÀk‹@Á\0go\Z\ÆQššº{±¨ï–ŒcýôZöç¥…\r‚’¦òx\ãs]³@\ïÁY\á:Ê·‹€m]\Ó@§~¬{\ÂLŸE®\Ìþ9W6\ÞµÀ\Z\éhb\r\r\Õ÷Ä¸ü<½Rnd]Û­QE»¸\ãñ¼<µ¬À—\éþ÷WK\ZšZV’4gPç²Š Œ`„Ñ·\ãk¤”Ÿh\Ë$@\Ï\Ù_\\\Ê\ì¯O\Æñ\å\âù>|§¯8&\änVHÌŽ\Ìð\â9<Ï‘ù…\ë\Êj¼8\ÝÅ½D%\ì –‘£§,÷›… ‰r‡Tn\ÙTb®tŽ«½`9\Ê\çg••6\Þ\Ç\ËM»HIm?1ñS\é,\ìºc[<\ì¬)lÝ¨°	ÁP <l©h\'5v¢¼\ÖC ´]\ä¹d±\ïp\âVZ‰Õ®×§r¬\Çmw\Òþ\ß;\Ý+¦3Nw-«¯ùs	;\ás\åñš\á©$8“4Ž=•Ã‡ª€ÿ\0þ‹Ù„y¹+\Ë4.þ\Å òs¿U\é¥\ì~¡°ñd\á\Ç\Z´•l\Úc–«\Ô\ÉC\é˜Aè³­7´ÀSJ5D\á5÷q—y*›gk/2;SZ1\ê¹öM+#‘Ï˜—o“\Íeb\î™\í€RWy#Žvš\í6ú×‡Û–N]\Â\Ôo5°Ë¾\ï\'?5\Û:ó\Ì,\É\ë®ÿ\0\Ð)¿€..\ÑvŠ K¹ ¬¸\åb‡hX\ÑÀ&!ü49hHh\ÙPÛÕ² \Ît o«ô#E…žEMä‚›‰\×À#\ÎVhCYDZ\ã‚\Õp^\ÝgmMS\Ü\ÞM‹ù«=£\ÎX\Å\ÏS5\Û\é\Ï\í\ë>Ê‹¿d@G\áÍ—·l]^\ÓTaŒ4’GEž\ÚnM¬\å/–py-û\Û\Ô\'¿\'.x«Kp¦·Rº¢®f\ÅNN\ç\á\æ»c\é‡1\â\ËýEú\æ\ê™2\È[\á†<û­þ§ª\é#L\ÇÚ½¦\Ç\r\Öj©¶¿¼Û›£;=¿Mþ!N^>ümðrô\ÌÍ°6X\Úö\æ¸\ê\ÉÖŸg¸›-6[Ž˜M+-x·†H\ï	,;\'uc6\Zµ\Ê\ÚY˜Kœb\Î\å\Ï\ÜU¿¦]\"\Ì\Ö\Ö\Ó2v?`´g\æ¦>Zô™W£wvC€\0\êv\ß5\ÖG<®Â™’M \r\ÎiÄ„o\äŒÆ†\ÃCF=TÑ³røs“„y\ãy\Åw\Ú:\Ü\Ø÷öz\ÒGLdÿ\05ß‚xy¹ý½5\Ã7Z›QŠª9®`a\äñþú¯¥q\í*]VÆ›\ì‘±\ÖI5YY\à\Ïñþx\\ºWYœ­-U5dzJˆ§ˆò|o\æ5¦\Õ\ÄÓ„E\rÆ•® ãª•-T €‘V\ÔÑµƒ\0*hþ2F0HG$\ni@2cA\'–c®Œ¨yk†kŠ¶Viø,4Ÿg¥lš\ëƒ5§‚!XtE]\Þ \ã…\Ï<v\é…\ÑV(X\×\ãex\æ˜\Î\î—\ÆQG%–¢7‚\Ãú/F9úy&™\ÚYR\Ð}\Ù\\?5µžšÉ©_W\Æ\Æ6œ\r\0Ÿª\é‹oU[i[\r+¾@Xµ\×–6Qtl‡\ç¢•ls\áp\ÏE6Œµ\\ŽB\Ò:®5` a\ÎTö¥k©\Ù\ÈûQn®›ƒ*ñ_\Ý.>Ø®\Þ\ÏL\â?yw\ÏÛ•z‚ô\no\à’\Å\â\0€$	w$·Œ\åf†hd{YŒ¬\â§\Ù\Þ9\Ù\ÝQc\\¹[ˆP·d?gB\ØQay@‡Ÿ	AKs`s‰<\Ö/µS\Ö0H=+.\rÚ¹\Ô\é\É<˜U¼Xyö¡\Ún\Ïô›ù®ó\Óo\\v8\æIe‡øòg\í\ßYµ@\Çì³š\é\ê.*%¥£¥|õ2\Å,s\äpk@õ%z$sr?\í\n’IM`ŒIU#|?ô·¯Ä©ø÷|¬ºs¨\êªkuT\Ô\Ï$\Ò<û\ÏvvôòZ“AM\".¬!’5ðI‚×·+¶f\Ê\ÅBl\×i-®vaw\ÞSøs»~Go†\Íù<]2}_‹\Í\ß\Ìp÷\È^m=R«\î´\à’7ø(\Ö\ÔN·€\Ò\Ò\ÍG¢}2\ÒpeC\é£}$\í{K]©›\ì¦7\Ëw\ÓK!\ïs\äiv¯Å¹\'\ÍvŽ5gGM{µšI\Ø\î«)‡»\ì¼CZ\Ê*I&q\r8Qc\Ï\\®ñ\Úý\Þ\èÿ\0`¦\Õ\Ïþ€¯Wòòs×§-C¼µ\Â\ïòôg§\Ë\Êj£\×Ñ²V–¸d\"¦\ß\Ú\ÑPf·\Õ\ÏN\í^üNÀ#\Ôuù«­´\×\Ù;H®‡\î/\ìª`\Ø\Í\Ðÿ\0˜\ä%\ÎñO¦\æm|Am¹µ®£©kœO¸\ï‡\Ép\Ï‹x\ÙW–·\å›óR*\Æ<“²ª{8\Zt»\ì\Å\ç\Ð X\'\n\ÄP1Tÿ\0\ÝEfk\ÓPB\ãš\ãk\Z4.m&X_Œ.¸_Ö²<\Ööˆõ-¤•EuŽ°\à\ì±/’¡ö‡_,\\;R\æœ\ì\ïò]±®y\ÍÇ•\è¤%•9\ê\âT¾\Æß±\'üt]Kv™[—\ÃoTBOtÜŒ,;A“¾ä¢‰\ØÁDEšF\0A(Š+ž—;b¹R\"\ÆBAa›ÝŽK¤ô9¿Xj/–\ãI[©¸.\Ç%ËŽ\ê\Ê\ï•\Ö\Ò\ì\Ão ‰²=\Ò=ƒ™]r\Î\×\'N\áºf¶E\äÑ„”Yª¸l‚²½…\Î\ÂÅT‘47t‚[@\n‡\Úá… E\Ã(v\ÈyË•Œ‡QaAÁM¨¤p\r)±QZü’¹ÕŒ\å\ÎC\ÝÈŒ\×\í?hþµ‹>\Ýe\Ú_\ãwžœï·¨;¬pµB\Òp4\ä\äöô`\è—=¡µBb¡g¶Õƒˆ\Ú}O_€[\â\â\Êù­e”ž\ç‰o·{\ä\Æ{¥[\æh9lCh\Ùðo/\æ½]5ö\Ï^Ë©­’J=òÝ½‘-\Ðwt3\Ê6þ‹—¶\áý8&Y\ÞYRø\Ø\Ï\ÒgZ½¾½„i‹\ï`#©ý\æü\ÇòNn/É†¾×ƒ—ñå¿¥%–±³D2|X\Üy/‘c\ì\ã|,+#+67*=+&A	¦—445\Íðz™\éwMb\ë#•Jf¬™ª±°’p‹Çµ\ÙmºX\Ú\î‰!X\ÞÃ¨O²\\®O:š½ ú4cõ%zþ<x¹ï—£l\â\Ñ#}!{\ç§\Í\Êù‘\îUHfhô\Ä\çc`®)Y©b{%xv\ì\'oE­†µŽ?O‘³¦£Id\âå½£º¨QÜ”gó\æ¹\Þ9Z™X\ÙY¸þ\×!\Üá–‚O\ÄF¦˜\Ü};\Åg¦\æq«¦«¦¬ƒ¾¤ž)\ã<ƒ‡\ä¹Ù¦\ç’48»®±¤J…r@‡“‚›*Ä\Ù:JÍ«#-Ró\íDu\Ê\å•\\g’ª)\ç0—asò\é\àv\äv\Íi8]1a|u\Æ\0p\Â\Ù\ìz_#H\0•­³¥|‘º)Ásp±¿+¥h„ºÁP:\Ólyf–l>¥¾D­°\égJºaÅµ1\È\æ‰_tg¨\É\Ê\Þ8\í\Ïz¯Pµ\Í\îÁ\ÏEyv\Ú4ó†¼`«\é6r)\ÚG5•‚šp\ÖU\r]K)Á+5”W8¼¬)MŒ€U\ÐS2\Ö\ãu¸‡\éb†€\09\á»wJ–¨¸i`ÀM‹‹Aþ\ÈÕ¼}\"j\Ð	H\"T€IY¡¦?KT\Ø##ŽwY\Ú\È~6\æµ) \ï|K[d\ëNZª’F\è\Ée ”X5\Î\æVt§_[Œ«¡®„\ä\åN£7pƒi\æi\ÃûL‚G\ÖTÁ÷pÖŒ’µ‹\Zò\ætÝ›Ü®7\'\Ô\Ö\Ì\Ê(	\ãü¹™^ŒwY¸ºu¶•–\ÊQÀ_Ý€Œ\îïŠ³ŽK¶·ý,\â¤sZ$Ÿ\r\Û\Ü>+´žû!¸ûEN†€\Ó\È,_5¹\â+8½£\Ø\Ì%«<$[µ‘\Ð\0¸\é\ÒRKrS´\êòWl\å\é~ú˜¡¡\ï*f†\Úùd\Ìo\ÍwŽ,_D-\×H\î¤:’³\Ät†?¯/>Uó¾O\\»O·\Óøœ½±\ë}Å½Í¨§nvBñ½°B²@GDn,\éf#\Ãu#VxY@\íc}–ã•‡ä•±FJ2÷«–\Zð\×l:¨²9Õº H\'¢\Ó5º\ì\Î\Ï\ì\n*b\ß\ïþ\'Ÿ\Õ}†±|\Î|·k¯\Ú(c‹\ÔñS\ÒÇœ”DZ¨‹\ée`\æXqñÂ±4\ÔSH\Ð\é	ôUQ/n‚=a6°ýŽmQ†¹EN®\Üz„Œ£\ÛæžŠ_h¢¨–šPy\Æ\ìg\â:¥’¬¶ztã˜¥¦½†Á!Ùµ\rc¿ˆt>¼¾Ž\\_Ó®9ÿ\0m\ÄndŒcš\æ¸dr\\]\æ€5ƒ\É\'Td%,«€\ÝÀ#l\î¼÷\Ûs\Òú8óv\ÆYÙ›+Y\ÞH\Z\0Ã“›XÕ´cªµaúH€’¸\Â\Ô\äa°\Ég9\à•‚\í±±Ø¦qpB¸\ÖmyIµ««\ÆÀ’2º0¼\ìÖ²hxÎ‘Ð¼µ\Äc ¯O\Ç\ÊK\å\ç\å\Æ\Ù\á\ë»EmK\íÑ¹\î\'nª\ç\×ma\ÛA5D…ù\Êòg|½\Ï	´{ù”\Ç\ÊÔ©¢v“’®‘SQ—•›f\án\ë™tø9-\îØ®“\Òy\É,\É9\\] ýŸÿ\0,L}	\ë@ ’»’qM\ë.pÕŒ\æ¸\ÎL{õß”\ÚD°FX| 0ºY4±^\í²&\áPB«¢¤\Ý^\É\Õ\"œøV±¬Ù¡\Èð\Ðrµ´\ÒMSDd‚±\"=5\Þ;;\nL¢\ê¬p§{u5ùWh¨¼\Þ\é\âÄ¸5£›œp–£z\ãZ65ñPDj\å<\Þ|1\æS,²ö»‘‰¨–jú‡T\Î[©\ç÷€½qH\Å\È)\éÌ’€]=F=Ó¶*GL÷U\Ê\Þ^\àòLfü™xð+\Ô\ÚA§\Äyú-et˜\Ãº}-/wUŒcVª8¥„±ø·Z©[I‚Ø½X\n\çZ„\é/0lKŽÀ|\Öz\Û\é{Iíž½q#¨É§´D\Ù\æý\ê‡±Ÿ\Â?xú¾+¶zö\çs\Û%r³V\\\æ7:Ê™\ë$\æ\îñ\åÅŸ\Ð|N±·‡\nIi\æ«w€†\Ç@\ÃH\æ>i1UürÑŠ™c™­Œ±\Øl¬Àxóÿ\0`¯77\Æ\Ã?^+¿\ÉÏÇ¸°lQ\É¦½¯oBÓ¾_\'\\wY>¯6<“x„Q4\ËsÓ¼©1\Ê\Ò\ìu	)bê¬²2\Ñ\ÍRF2ñ?\Ý\çs\è\Ø\è\r\Æ÷Zs®O,\rñó[\ãÇµs\åË®.Á\ÂpfŸXfwmê¾·ðø¼—\ËkDY¯{\ÕÀ-¹lûdŽ_\î\äcÿ\0…Àþ‰¤6æŸ\ÍXˆ\Ý\Ø\rUb²øÀiIEŠ;n¦\Ê\äZ¤…DWJ\ÃžŠ¡\Æ\0XF2‚U£ˆ¯\0\Ö\ÑNŠF¦|¼¾K9a2ö\Ö9X\êœ%\Ä0_¨]+#0\Í±“œ\Ô¡y²\Â\â\íŽ]—:‚\Ë@HÁAŽ¿µŸµo-³õ^lýµ¥­;„`\í…\Ö_š¡…Îªy`À\ÊAp)ò¢µ \ã `•Ö„[”O–À\r½Vró\ím\Îm²¡šÀðò\Ê\ç=¥›yŒ\È_8\Ï0Wt\ÓCÙŒ¬<aF\âFy«½$›{\Í$\Ú\Ã\ÞA]\ÓP©¤§y\0Y±©R¨+i[°xW–¦\Ï_L\0\Z\Æþ«Zfä¥¬¸\Ò÷\Ø\Ö1ñY±%Deâ‘„\å\ãoU\ËMl§qa&FýU†Ì·ˆ(HÎ¶š\í\'†.ZX\Ä@y\ÝD<\ÔÒ¯,ÒŽ\çIVg%\Õm …\ÒYAª¼xJ—\Ð\Ã\Ý\Ý+ø‰‘°¸h\Ü`\á~K\æ\çž¨cŒúò\åv\Ó\Ó:~\à	esŽ:¯\Òñö\ë\æº\Â%\0\Î½5(˜ö´l\n\çs‘©N1\ì“l©Žs/\rm&6\àl½\Ç;vf¡…ù\0¦“h2P9\ìÆ¢§Mµ2Ñiñj\Ýja¤¹Z\Éñõþ>c)©_•²\Ú]´C\Í\Ã× Z˜²\ç\Õ5•÷i-mK\åa#Ky4|\ï‡Žw!HÁ©´ñ\0þx\èOô\Ìþ\Öq\Ó2A8\ZB¬ªžp¸6™\ÐIS\Ý\Ós\Ä\ÛES¦ŠƒDMVýG9\æ³\í§2I\ÞI¹>kŸI­\r8*Ê®\åBú†¸c˜C\ÑT\í}=7s$’\é\Å\Ç\rh™IŒß’\åS\ëhD‘÷.>{ `}lun\ë9Widr;\íðZLLÁ\è\åµ¹Œó.wÃ´ò²}ª	˜*\é\Z\Z:\ã÷J\ÔòÇ˜DL–\ÇTz˜NvVz\é©v·³UÀ\é\ÜÜ¬`ƒ¶\n\á\Ï\Å\ß\r;ü~N™\í`KA<†\ë\ä_k¿&ƒÀ‘\ÄesûuúQ\Þ\ç\rs‹ˆ\0j’²\ÒÁ=isÀ-§¾v\Ï\Ãú®\Ü|?7\Ó\Í\Ëò1\Ã\ÄöŸb¢lO\Óa\Ïy\Ç.^«\ÛÇŒž#\çrgo›]\Z†ŸE+X\Z\Í8\0uõ^\ÌcÅ•WVpõº¡\å\ÓS±\ß\ÓnqžŸ„m’\\³6JY\Zr\Ù!ycš|Á	¨²Öš\Çÿ\0\Û\'Žž¶_\Û.8;\r¨‹\ã\Ñ\ãóY².\Ú)\\öõYj).S÷Ÿr\Ý\ÇT\Ðb\Z}\0#Q6\ÄdEDW¡Ñˆ\Ø\à1…ATDLy#¨DK¶UU[*#ª¤±\í rpò>ŠeŽá²º•ž\àË•¾:¨‰\Z†\Üû®\ê‹,lºz±»‡\ç–p\Â\ZrV-­xR›eEMA‘ûg©X\éjnE\Õ\"Œ‘\æºc†™·i1wlv\Z}ˆÃª•²\nž%»SZ\í\ÒO6§Ó†´d•Œ® ñ\Ïj=¥\Ïr¿\ÖP\Ç£kNU\Ç¦\Ü\ß\\Éœs]4\Î×œ5R\ê\Z\Ø\ê#‘­sw<Õ‘º\Íj5pQ¶²2@\æ^µ¨Êª¿´\ë\ë\å!½Î“\Èä¨°ªN\Ñ.ñÇ©õL.=0vI4—\É\æö‹z”œ\Ö\0?€«´\ê\'q•|¾õc\É<\ÈjžH³ñkÁª›U5(3]®5³ÿ\0Üš‡”Sp¯\Éþ\Û?ý\åXÅ›z¢y»¸¹²óZô ~\Óc]‡m”ŒÛ¦³‡\\\Ù\è\Ã\Ç5›„É¨´hs	\ß!cr\ã¿\éNµ\à¯F9J• D\'*_C,ð»ˆ\ä\0M#\ä¾˜eòò³Û–\æ\×bp[ˆ·õ_V\ç\ã\Ã{0ö¹\ç:×‹—½¾\Óbù\ÌT\Ç¯¼–S\ÑE#7Î¡\æ»cÅ–>Z™&Á&F\nöq\å¹\å6wuP\0y Àv•\Ç?²œûM\í5\Ø\Ä\Ósúó~ŠÉ±\Èe«¨t²½\Ò9\Î\Ô÷<\ä¸õ\É]0Å›t¶†&@\×w$\Û5/½S(\ÝÜ²¤-7y¨\Äe­ä–˜¤p\Íwª<o\ä˜\Ä\Ê\í:\è\Ü\Ä’\Òb©§g{Q¤rYoi•L\îÙ€Œ\Ä@\á¬T\Ú(\Þ4Œ«=49~J\Ö\ÓF]GœÀVTÑš«dn…\Í\ÒŠš%Q\Ã1“‚’\ÎôÞ·\Ó\Ñ\Ç%91nÓ¸=B\é.\Üý TZ»\àeƒ\Ã+Fre\Ð\âšI©œdOûÁø›ø¾K\æ|\Þñÿ\0\ë\ê|‘«\Ó/þ#\\.tÔ”Î–Y\Z0<\×\Í\Åôêš––{›ý¶½¥g1Bzú»ú/¡Áñ·û²|ÿ\0‘òµûp?\Ü\Í]Vii£/-iv^¼¦üG‡z›­Opø¦\Äõ=øÙ£þ«xqõö\åŸ.üF‘\ç9]v\âY†1\ç\È$]*­L\ïk\\óæ­¤iZ\Ð\Öü–v¨µ#ÀIQb¨1¯˜†Œž¥3¹:Á\r›\rÁ@\ãFF\ê-‘$\Ø\\‡0uHQ\0\Ð1æ´‹ž¸{\ÐR\Ê\ìAQ†ŸGt?\Éq\å\Ãsnœyj\é\ÑK,/3º9ys\Ëbaw¯D\Úh\ë s‡\Þ;\äUN†²6í†„ñ]\ë‰=\Ø\ÈóP$HýX{¾A6)¸Ò—Ú¬s±£Å¤\á,\ÜG‚8þ/g\ã\Ú\Ö8`“•¼=3P£!m”˜\\L‰\Ê	Qœ¨šH‰ÁQ&7ˆÞ‹³Í‘ý¹«EÛª\\JÖº1ž«Çz¸ñ\Ýb¸\Òv\Ñ9Ži#|.s?\r\çÁ\çq¾\à\n–º\Ïœ\îk¯[qÖš­m-\ÎF_‡H\Ðò\Ê\á|_\0T\Æ4—Küœx\æòM©\îüA¼1\ÙpÃŸ\ê¸_y¬ežœÚŠ\ê\é8‚W¹\ç\\‡% ¯‘ñ¹/\ç\Ë+ö\ã2ò\Ù\ÃtkbB\Ê\Æõ_[/†Ë¤\È\ÅM}\Þ@]MJ\ZÞšŠðòþ¥ò?õñún«é¸‚\ê\Êö\ÓOJ]“Œ·|._õnnNN™ñ\ëþ“u´ «,¸‘\æ¿G\Ç\É$t•>\Äñ–¸n»\ã«\é¨|.Š\Éö•\Å#‡\í}\Í+‡\í\nD_ûm\êóü½~8sµ\È÷=\î.{Ž\\\ârIó]1‰³ô\ìÀ]1šb­`€9­\È\ëÌ®Ž{Y‘¦<€+b§5Õœ¾\é‡\ê²×¦Š\Æ\Z9\r–£\ìCb?\"-¦0\ÈF\åE´wc(EwPQ¥½º¬f¤\Ô7lú*\Ô\'t¼xJ\"¶º²@n`­\Ò\ÝI»\nJXž^\È<8\Ô\×\î\Ò:­Ë·5-\Úh)\äö¨œ\Ö\È\Ïy®\ä\æž`¥›žZ\Æ\êød­\Ù.7™ª\êOyGŸ\ÙNuŽ?C\á•ó¸þ,\Ç;o¯§\Ó\Ï\åÜ°’{ûlK÷8#CÛ§ƒgøb\Ñ-=dõ²·C^\Î\í­<\È\ÎI?E1\ÆË´\Ï-\Í4dí°[s\ÑMiB+.§1Šb\Ì\Í\')H½è²ºW\\\Ü\í\ZZR\Û)t’]¹(©\'NÁ\r\Ð:À SI/\0\"¤†\ä\î¬@‘¹8U“:^$È…:…†¤W\Úi\ê^u=\Í\Ãÿ\0ˆlW,utõcw§¨‚¹–F°y¢Å²*²¢ò]–\Ò\Çÿ\0Sÿ\0¢\Å\äþ—HŒª•ò‡Lò\ã\ë\ÉI‘¥½<¡Ñ®’¡¹œy…(p>\àùak\Z•\ážß¨iµ`\Ã%n¡õ]5§<n\ã#ž¨§\â‘\ÈdõDKŠ@ ‘¨ŽM\Óh’\ÉtX—UŒŒIž¨ºðy¢Ç§xzü\Ú\Úf\ä8;¢òü™ø\îžßŽ\Ù\Î\Ô^\ÖÓ‡ñ[ú¯\Õ\É5\Z>¬•–ša¶ .÷.¸\íò¹/–Þ”M%)\Ô÷n6 òL3\íŠc4µO·\×=’\Ì÷4µ\ç\â\Æð\çe»‰½Tû´ýõ+½Ÿ.ynpýK‹òñYµ·ú`jdpJj¢–7“\Í\ã\è¿=úO\ÃÏ‹ŠþIw¿·¿µ\ÂÙ¯r¼Mf\Åßˆù/gù*c<º¾:xÎ§\ç•õ8~ö¾\ÝbE\Æ÷AD\Í/{Iò^žLxññV\Ù¶MMVD\í\Ò\Ã\Ì\ãÃŽy„\Õ=|¨Ò¹´\à9\Äsk›-\ã¨eXvñE\ê\ÑTWt%\Ø\Î\Â\äù\Üÿ\0/3ÃŒ\Ï)\í¼´ñP>ªwwl…\ï\' uöþ\'Ëœ\Ø\í\è\Ç\'\â[µEö÷QqœŸ¼v\ßÀÁ\î·è¾Ž1\Ñ6d®\Ò08fV¤aq\07Ëš\é¦\n™Ž¨=\Ó6o\ï•žž\î–!cs²2‘¡\Ü#3JÉ£š,¥0ÃäŠ¨¨\É)uE˜(«;v\à+©³±VPÁ\Ó\"*G6¢\Z#9EGž Fz ®¬®\Â\è\æ>`ù}&¶£§¶\\xª¥†ºh¢“g»Hyº<ýU·d\Ö>\Ú\Û}£\ØGw#C\\Ýˆp\ß\é\Ñ&1.v¬¡›:\"$¸\à\06\n\0Áº(\ÞpÒ‚\rS57t\rQ7K°‘o\ËTTZˆµ\Ê9 “C\Z‚-CrJvÊ€šíŠ¡\Ú1©\äù(&cl«¤\Éò¤¸ \ÒpEdlôA\ä÷øõþK\Ï\Í>Ý¸\ï\ÑûŽ¡9s‰\'Ì•\â\Ê;J]&\Ý1†Ê‘‡¼\Zy-XJ·¢\Ý\Þ\ër!\écv“…lWƒ,Tò||\Äx\×\í6¾2‰\Îç \Íu\Â\î1&œ\æ9=Vƒñ\ËêŠ•Þ¨$\Ç?ªšb›\Õû&CG\ãŸ\Õs¿õH…	ö\æ¨.ÿ\0\ÕVtõÖ‘nªt‘e­\Ö	_;\ægo—\Öø\Ó\'ÚWö03\ÊF~«Ç†[ºzyðý»n{-du6H&~–ì½½fS\Ë\á\å\í®º\×\Z(<\0‘\Ó+\Ç\Ír\á›\ÄÞ˜k\í\Õ\ÕdF>š†\Ë\ãüŸŸ–Zúr·k®Î®ñI4\Ôu“#H-q<\Âö~Ÿóñ\å·\ï–ø¯¸¶\í¦‚¨’G0žì†Ì•\ìù\ß\'‹ŽÝºg¸M¢üh’\à\Òœguò¾?>±\ìòLµ4]YY+ŒRha\ç…ß‡\çe\Éu™\ÛZ‹M’¦\ì\Ñ,®\ËORw^\Üxo\'š\ë1\Ûoe³\n{¶\ä]Á^\Î.Œ\Ôt“Kjhc\×\â\×e\×#QŠ,tõ”/iˆ8\ä±ò>.\Ø\\lL°šrûUEº\Ë5\ÅÄº\\j<ô\rð¼?¥ü|¸­\Ç/¤\âŒ\ë0W\Þ\ÅÖ¦Ñ³Q®.v­#‡%i„ŠmôZdó^\Øb.v\Ê)š=U=\ãùg`²µtÁ\áZ`\Ì\Ã”Ue\Âm,À*5hc2<¸Œ¨´õH\rv$K¶òV%Ox\ËUDI\âE(;ÀP4\âÀB\çŒ÷$¢1\\J%¨©Š†™ª%lL¤\á+Xøtº:h-ñ\Ñ\ÅOMcG\"NsŸ^ª\Æg¨=ò»\Ä\ã\Ï^˜.\0Ö°\0r£P§oº‹\Ær€žr}!™Ç\"£À0õR,\áw…E9\Â¾\ê\Ü\ÔˆÃ‹U&~‡† Ÿoi—¨%¿EY5ŸoR¢—¥«,þ\Ëu‚Rp\ÝXwÀ\ì¹\ç7\Ã\Åj\îq³\'’ð\å…s$ò+\è-•mu½¢î‚©f\ÅnUJt­\ÒUØ¢¿\Î\ÃNñŽ‹6¦ž+ûM}\ßS¼m’V¸\è\æ‘K\á££—\Õürú ‘È±&9½T\Ç6È‡›6:¦‚½£n|•ô\í>¨\Ú}TÙ§³o×¬yó>W§\Óøž\årn\Ó\å&77\É\Ãõ^¼\ì}/‘‡ü;t^\Å%G\ÄC°\Ü`¯©\Ö\×\æ\ï·Bž\Ô×³%ûù-\Îöucxª\Ì\Ñ¯„x±\Èuÿ\0U\áù§ñòcu¸¹)¿T[¸‰±™4¼m\å¨/\Â|þ>_‰•±¯ü‘g\Æ\×Ú™\ì¥Î–\ã8\Êù?õLþG,\Ç:ú\ß#‚N;§\'·ÝÆ¶FÇœþñ\'`¿c\Åû§\\_Ÿ×–Ã„id5¬‚\ç`t+\Ýñø&>š“N÷\Â4õÓ´T\ÆFÁ§’û\\X\å¯.\ØÆ”\Äül\çˆ]õ¦¡\á\Ù\Ê\Ùbp’\"\Ç¨\Ôq\ÑgŽ^(©†wp\ïn®ÿ\0?\Ñ\\1“\ÒÉ¦~F‘=B\ï\'„YØ¾óš\Þy¯f‹CC\Ú6\êF\Ó\à9\Íº+\Å^©*DC’\ÍXvD5L„s(¿K\Èý\ÕXG«8CVó,úû¨\ÔY\ÑA\Ý\Å\Éh•§\ïpX™oj±ðªˆ•ið”!—(±<°:,z+“·E\Þv™C†DQK0¡¸ü•«ô\èr» ©¶\Ò„t\Ê\Ðr6´L\Û\ÑMš83Œ\"ÀhQJÒˆbq† f.h±.Jh\Ù@D(Š°G|~\"p¨¦«~j~h/(\Û÷,	—;\Ã#.;R#Ñ“#)\å\ÑEKo\"~ˆðF\ë5cI-Sª(`”\ÜÁŸ%\àäš¶=8\Ý\Ã:§%rTW8dŒ«Å¦P\Z7]1r<h;­¦Ô—=.\àž‹\Û\È?k[}$\Í\ç\Þ`­qÄµ\Ç “1ƒž‹ªdž¨lž¨h\ìrú¢Ä˜¦õP>\ÉýP(O·5a \ïñ\Õ\ßú¢¿õSFžâ‘ó\ä«\æóM\í\ï\á½\\›´øH2úóx¼r\×\Ú\æ›øÍŸdUóPp\ÜN\r\Ô7\Ù}ŽÚ\ÎññÌ²®©\Ã\×\'])DŽŒ3§<­ñrw›9xÿ\0\ÑW\Ëxš–Añ\Ì-\å<8\é\åN\ÔY%\Ä\ÉAi×–»òü\Ç\êü3“±¿\í8ÿ\0o$YñD¬ÿ\0†\\Iß»_\Ì>\'Xü_\í÷ù¬¼U\Çø6\á5Ueþ\"]†šþ›ñð’?5\'šôGbö9&sk\';<\Zzö>/\Ýo¦\ÝÖ–( ­\rÁ\Ç5ô¤\Ór‘þÇš\ç\Ô]	’4\ã\ä—\ÅM®ªŠ†‚¦µ\Äi†7<ü‚\ïŸ\'‘ó\ÔI4‡/‘\Å\Î>dœ®˜¨\Üßº]X=\ÃR\é©tG˜*ñøðg\æ6Of¸2<—G­ktKñZÄ¤\ÎÀ\Ù;Þ¸R‘]G!š\ì	<Š\ÇÛ§\ÓXÁ\áZrW^\'°Œ\îQdB´RºY;\×\r”j\Ý-ªKa„Ž¨Ê–B_6TiiD\Ü5i”\áî ‹P\ß\nœ²ŠrŸ\Ä\á…£†|•Ff\Ãÿ\0“e\ÛA&>ob´ž›w\rŽTE={p\âU\rRK\â\ÆvQS\Ú†P\Ð\Ú\Ìu@­\"#\ÔQˆÒ‚M)A1¼–A€´«S78õ\Âmò\Âj~jªþ!¦6ÿ\0\nFj\âf³DYËˆ\ÎDšÓ‚\îª4—\Ê=EAÉ¬»\É*­\íÐµœô8…\à\çñ“\Ñ\Å7o\Ýr^}»iO3^vIR\Ã¶`·‚fxhendÍŒ\çöµ`´B\îö¶=@{ ä®²±\\\Þ\ç\Û\í¢Bñ\í\Çñd<¼ÿ\0\Û3Š+\Ú#=vF\n\Þ3I¦Všv˜‡ˆrZ!\æ\Ô0~ðE,U\Æ?|&\Ôbº!û\èe\Æ,\à9â½ g(h\Û\îñ3™DÑ“{‹¡Sk¢?m´ò	´\ê¶¾	µ\Ó\è]¥ý\íD\Íò_;/olñ7.ý³U#\ê˜DE\Û7\Íxøþ>W’\å^þ_—\'\Â{ZSØ¢´Ú»˜š\ZÖ‚ú6k\Ìâ¿¹ \ìû{Ì§\Çþ-|¯\æ\ÕHÖ–\îK\Òò¼\Óö£ŠŠ•ôµ#@¨\ïs9\ã©_\'\çñvô–9%\ë‰L\ÖS\0“$³ò³ôkù;k\í\éÿ\0\"\Üt\çv·\Ë\rCž	vW\èxþ6O.8yz‹\ì\Ù\Å1N\ç\ÓUI—±¡¬_O‚\\=ºe$ôô)\"HÛ¤\äó^­³a$aK6º1QNð\ß\nòe\Çeði—\í\Z¦J^–2HuL‹\å\Ìþ‹\×\Çm‰§&krW§Í§´øH]E =\ÕÙ££\Æ\Õ=\Æ\æ†]P\à\ï…\Ò8\ÔZ\Ö\è—nYZˆZüS“ä”Š«eº’9º\ç=º\ßM”ŽlP—8\ãi\ÉG2\\kˆ=\Ø*5\éw¦:H0\0\n§µ]L†WytQb,m&\\¨«Za…¦R‡º©ZPA“bQa\Ëxû\Å\Æ<\n¢‚\Í8þ¡\Øÿ\0ø\'ÿ\0›U¤j\ÞvPUW7r‘\Ì%²X±¥“#\n	Œ\Ü(FÊ¢%HULi\É@\ä[‚c2²1”\çtU‚”\Çý«—UE›\ÈicO–UŒ\ÖrZ‘Qx\'!\ï\Ã þKVxF¦6\á­n6\r\nºA\'¢A\n—F\ç©E\Ï÷ò²x f¢qô^?‘\Çr²Ç£‡9%•v\ê+«£ðB\Ò~+\Íørvü‘UpµqX\ç\ÇHÇŸ ý\Öp\Üq®Ù¸´*›D\ï¶Ð¸d\è”j\Â\éÇŽ®òŒe|<\ÆU\0ù ®’v\Ê\Ça\ìy9\ÕzrÖ¼1Ž\ÙFK ;8¬6»\á\Û\Æ÷(87\'\0¸¥\ËC¢Z{\ãjøÁ¤}#üq\Égò©\ì´˜Ž=‚\'yô™\ìD=ˆö“¨·öP\Ïñ­K´\Ü5YØŸiT\ÔÏ¨–\Ò44d\âM\Õ\ÕN\Ñ\Ï.\Ü-uÏ£®ð\ÌÃ‡5\È\Ôò\Ûð·g¼c\ÄV\ßl ¢&2\ÝG\Êm—N\âJ‘¡«·T²l\à\0\Âr›#¤vy\Øm\ê\ï\Ý\Ô\Ü\èjM;°KZ\í\'\n–º%×°K<EÔ¶Zþù£`N£\ë•u™d\Ðp\Ïa6óhŒ\Ëg{^N\á\ã%Oº\î<\ÆOs©sðCNÀù¯yW·’\ë\ØW˜`nv\Ôy\Ú\É}Ú ½¼º†G\"¹\ç\éÓ‡ù\ì\à[5t\ÉZøóöµòš\ç‰\êä¡´\ÏP\Æ\ê,a .Ö¼\ïv‘\Å%¿\ÔTÖ¸\å¯,cs\Ð	\Ø.W	—š¿é’‘ùf’¤\â‰°\ZrúElû¹\Õ\Òñ•,pJX×¸\09î³”ð}½\ÍÃ¥Ï \ïÜ–\Öñô-q·%£bsr³cœv\Ï(\ÛiW>B>^9\äµ\Î\ÌX\Ü/TŽG\ZÌµj\"²½¦)\ã˜~\ã*U³Aýž\É)Ý‡†‡#\èWX\áLK3*(„\ÌÁ\Üy\Ì+]\Î]4\Îß¢_F$pX’IÌ“ú®q\Ó&†¡’VK\Ý4áƒ™U™\á9ŒŠŠŸ\0¤eSS;¦\î«R§\r$¬¨©Ù¾J±\àU\É@4óA¥˜(¥ÐŒ;(-‡¸ª(m®¦gS@ùµZ“\ÓNþEEW\Õ’‚ªm¤  ~‘\Ä µ€\å«!\Ìl´ˆÓŒ”SE¸ 6c(%\Æ2Õ¼ 0€=¹aAY\Ýhù­\"\'WGoƒ[Ý‡9šZ<\Ê\Þld¨\àˆ[Q%|»†œ7\É2ð±²\ÔÖ‚÷œ5¼\Ö\ZR\Õ\ÎúÊ­-Ù€\ìõ`\ÈôA€:(«\î\Î$\rº\Ô\Äy¾G\É\ß\ê¸òút\âö\Þ.\à‚\Ö\ßm3\ã8kˆ\Ø\áA\àŸ´\ßg/U\\ªi¢uQ\Ô\É\Ë>£¡S}TŽab\à*›üÅ´08øB»Vÿ\0†m<3Yq¡™ð\ëð\çæ¥’Ov9p§¼SG/°÷@\È\ås\Ð\ì\ÑÒˆ°cz… \Ä\Â\Ýu±ƒŠ\ËUxª´\ZGµ\Æ<c–\Ê÷‰§™»M\àEû‰_qm@1\áç‚¹\\\Ú\Ç:g\0\Ëfµ\ÚY\Ã!s€\Õq\ÏûK\Ü!µ\ÖÝ…S›´‰\Â\\Ó«ej»Zii\Ãc¨Ž\Ç%fqt°‚õA+Æº¶\Ód\ìigâŒÀªg\Õ^È¯\á\ÊnšY[œ;r¹\ÌzÝ»\åŸi¤ù\çtó8¦\Ü\Ño_úlŸ2ô\é\Åü“û4Á±4\ç|•®\â\×?óZñc\ZûK]Õ…u\ËÓŒ|ü\â(û»\Õs?\rC\Çÿ\0±X\Ç\ÑUm• \ÃÊª\ÑöW+\"\ãzH\à\Ö÷\É?Œý${\ã†\ê)\Ým‹C\ÚF‘\Õ\\oƒK7T@Ñ»\ÛõZ\ÜM\Z}}3FL­ú§h9giõ‘\×ñ-<q\æC\0d“ýN/52ñš˜Œd\ès‚¾`‡r€>7tJ‘o\Ã\Õ&[+ZOŽ¡\ËXzs\Îj£NóIqtòªÇ£À\ÈúŒ¢\Ú{Šûœ\à\Óú&^ŒS8*7:—Pý\â¹\Æò­ŒMe<ZŠ¬)\îFit·’›jBcf‘ºN\ß`ŠTcQ*eH”¼\Êó7*„SŒI„]-	j\"‚Ÿ\r\í%­w[$\Ïÿ\0\èÅ«\é#T\ïuebº±Á¹UŽÁ$”R\â8AcJü±DI\è¨j@\n,7 7Å”\à>Ÿ`„\ni jH|aÁX9\çj•·..³PS¹Í„Ó½òœÿ\0œ\Ñu\Â\ê1[k5$\ës fZ\Ý\Ê\ÅóVxˆµÕŽ¨qN5ôù¤š­\ÔBû\É]Ô©jÈ’ò\Ç\È •Áò˜8Ž˜ô˜\Ï\Ì,rM\â\ßòt¼¯+\Ð,ú ,Ÿ$\Ñ8\Ó\Æv\ç\Ñ]!\Ã\æ¹\åŽ\Îû\áþeæ“¶³•&7\ì]qŸeü;\Ä\Ô^\Ï]H\×\0rØ‚¯Z%ðWPp\Í;i\é†7a”iª\ÆDr@\ÙQ\Ì8\ÞýKMS\Ý\Z€\×\È9q´c«nT\Ï:½¤Ì¦–*å¬¢\Îó·\ê“±±[@7\ï›õZ˜§b]v·Gÿ\09¿Uf)\Ù^%·EŸ¼iù«Ô—hsq\Õ9\rxú§TE=£Óƒ€\åzÕNK’},W±\Þ\"°\Ñ“ÿ\0†IðL½7\Ç\íˆ\á=Š\ÇQSGTIc¶8r×‡_‘Ø¸÷µúÙ¦†ŒfG4\âµ»yf\ÞWº\È\é\ë&\Ü\äyqù•dÒª¦ê¨Šò\Ë|ò\Ó\Ö2hIi\ÈÂ„Ž›j\íg\è\è™MG\rT­\Â<®}±\Ì2©o\í\'´Ê¡à¦¸ŒùGþ‹“\í¿Å™\Äª\Ö\ä2–\ê\ìúü–.\Ú\Î,7†a¦£m\Å\î}X…¢bã“¯…ô8=<ÜŸmÎŸ1‡\0½68cP¡lj4EL\'Al‰*/\È)\î’S<\â:†\à|Sª¹\ÍÁ_\æ\Ó\Î\Ò\Ó<;\è¢\ï§\ÏÝª¾\ä€\æi3²\Î^šÅ¿\á:F\Ó[\ã\È\äÐ¹Æ­9w­\æÆ”$WÓ‚N£\ÕEJü\Ð8\Æ\àegt!V2’9(\ÔS2\Õ<yEX3\ÝH\Ë*\æö©MøM²oŸ‰‹W\ÒFÁ¤¹\ïf¦±±\Â\é\åy\ÒÀ@\Øu$§¢Mª¸€IH\êy\\ñ$,\×À\Æ\ÙÁ\È\éºO1u¤8@x\È#t9¥£($\Ò;pOf\áD%\ãuBvE$\Ã\É@µToP€µ&‘K=¨\âAV\á“?WI|3}‘v©\Ãû¢\â\Ø\Û\åÌ¤ˆL\Ô\ÌC\Zy\ág&¢\â¹ý\Õ1\Æ=b˜¤:©I\êJ·Ø‘mwst¥—–™˜5œ¿\\}º^G¨(+\îµ\æÀ\\ò£‘q\ßT\Ñ	\"\Ðöô\È+ž­6ó·\Z_k\ênªeLš³È¹jcN¯Š¯,6LüÖºŠ©ø²öIÌ¿š¦»Šn\â2Lý¢i]\'ÝŽCªO\ÕoDˆ5M^sª¦Oª\Ê\é>\"—V_Q!ø”0ñ,06ÿ\0f‘ôžc÷%pX©dž3¿U#Au~m’ü\Ë\ÓX&7†».§\â-Ê¾w¶9–µ¾KËŽ\å|=¹òaŒòOöiŠ\Ñ+\íòK\ß5¤‚J\ß^N?6¸\ËÇŸ‰_½Q\ÉG_QI :á±\ßW«¥›y®6]*%ÄŸ	ú+¸u¨¯‚CÉ‡\è\â\Ì+kØ\rS_x\æ\nK‹qN¢\ïo\ÉyþG,˜øv\á\â»\Ý{r\Å\Â<;EEp[i€kF€§M\Ó>l¥\Ô[Gi¶F<4pú\ëø8\çÓŸ\å\Ïû\n¶P\ÑQORa‰­Š7<\ì9\0J³Ž}\'|ÿ\0·¤º·¼w7;W\Õ{8\æœ3ó\Z£š—\ÎË»Š­ð˜žZyt*.\ÊtEÑ“\ÍR(nº)\Ì\Ù\ìvAQ¹\ç\Â?HEF\Ç0\Ñ(òr\í\Üp³U¶N.\ZJ@s­\Í.ÿ\0¤ÿ\0¢\Î^š\Å\×]P\ÚZ&°+$ò¦\Ö\é\å.\'e–õ¤\ÈÀk}Q!oR‰²Ý€„7rUTˆŽ2‘\é°\ãF\Å@‡ÕŠT#tD¦$û4Œ\ã{=cF\Z\èj!wý¡\ÃôZúF˜\ä†–ni\æ\Ò\Ä\"§³Ò†\ìúšš€\Ù\åh$05crHhaüù«<A£\ÉE‰\r~¦\à vŸ\Þ\ÙŒG!§\r \ì‹\ÆB\è†H\Ù .J€ð4’¤Õ•‚Ÿ½~q\Ñt‘²²\ÕI]]\Ý0\ÏE½j#sg¤´lŒ†ë»n\"\Ýj\æ7VA6–=4¬n9\î§ÚŽF\éÃ˜9S\èt›ul”Í–)\Zì\àù/\ÎnÇ³W[Hs\ÚÑ’\à\í!ª-Ê†#‡\Ô\Äß‹”\íH\Òñ¢?~¾ñxNðD›Œxz!—\Ý)F<\ä\nw‹¥=Ã´\Î¦7ZbG“ÁY¼‡Z\Çñl\\=¡\Ñ\ÃT\ÙùwX¹Z\Ô\Å\Åxÿ\0v{ý’žWƒ\Ô5\\Y\ê\æ\í¼T¼–\ÑK¿¢Ü°\ëU“\Ù/\Ò\r¨_º½¢õ¨\çƒ8ž¢7I!\r\Îû\'x³\n\Ë^\á¸Ú¦0V\ÆXN\Ã\Õ\\l¬Ù¤[uUÆ©°S·Sž~‹VøFò›±Ž$¬¤lì‘¡®j\åù#Ž Vv/Å°\çCcÈ…!ø\ê±ý”ñ“[\ìM>¹*Ì¶\ÏWÒ“š\"Ö\nÅ¤›T>\ßU\Ü\â\è\ÈÏž\ë3\ËV¸8þÏ•¤n\Ö^“h˜ñ4”ñTPUE#£‰ÿ\0v\æ·;y/ù‹/O£ŸÇœ’YZ{\ç¾jb££\ÏpÀ.n\ç\Íór\Ëa^‰Ž7ye\"\éÙu}|õo¦\Z\æy{¾$¯&9ükOm\Ç\ãh¿ý ¬qþ\éƒ\ä·?É¿L\ï\ãÃ‘v9R}\à\ÑòNŸ\"¯\åøñmcì²²\×Z\Ê\ÊYLr°\ì\ào\Ç\ç\Êj\Ò|ž	ô\èôg‹b…±{k@h\Ç÷kxð|™52qË›\ã_=J‘œU\'½t@[ÿ\0\ä_y³þGÇž±Tß¡¼\Ó[\ÞúË•D¬\è,.\Ø\åz>\'\Ä\ÏY–Ym\Ç\äü¬2\ã³tÊ–˜¦k—Ûž%­´H$¦\Ö9R«iƒ  FJEV\Þ)sˆ	Lj¦\Zh\î6Z\ÛD\ÇF’\Ãø]\ÐýUÁ2Ÿnc\Ùû\ÝK\Ú”u‡D°\Å&–ž®\Èúü\ÕÈ‘\Õg\Ó;\ÄpÀ¹Ö¤\ÒE<{\r*\ÄJ:bo‹r\èƒ\âˆ\É\Æ02Šq›\Õe&\"¢Ÿh\Ù‡\Õ…»¨%0+±TRJG÷rþ,pU“’û…]$9y%hð‘„\Þ\â€‹©ßº	\Ô\ï\Ê\"Xjj€N°xRT(6\åDjª†\Å\Þã†±¥\Î\' «\"Z\çw[\Ûj\Æ!\'ßš\í&œö½\à{a.5Sª\çúkØ½\Â8_)\ê0>œiš3Wø|\Öþ†š&a š“3|(*\êYuL°¹ß\Ø_\åq_\Ëlû}oœ¼rX\ÏU\Òñ\rSže¾\\=;\â?E\Âq\å÷^žøýEO\Ô\ÕH_5Æ±\äó\Õ;¿ª\ïŽ\äÓ†R_\"³\Úcý\ä²?ø¤%jZÎ¢T=žZÇ½O\Å]\Ô\ÔL‹€­\î#ÿ\0µ<²Di¹fsøU1ð}©­þ\áŸö«¤„»…\ímv\Ð7è¢œÿ\0†\í¡¹7?4\ív\êzI\\ h\ßeM¼oöŠª¦w¶š˜\0\ÆNn9¨\ã’d2\Ä8º‘²´¸ƒñ[\ÏÑ·¹,4ô¦\Õ˜™#¢óGj’ú*7s…¿ESf]k \'=\Ã~‹LZ\Øw\Ú\"\Ô$\ÊxL=³n\âsû\\\Ñ÷!Àõ\Ê\ç\Åmu\ä’$\×?¼¦”ùµw³ÃŽ>\ÏömC©žòÁ[\ì¹á„®™ò]¶ÂŽ{ƒ\èºþ8\çÞ\Ù!ü\è!Ú‡²\Â?p+\Ö&è½ž/À¬7I0GøBu†\é&~¦Û˜\ß ƒ\ÚCÆŠ\Zaû\Ïs\Ï\Ècù®¼S\Êeé‹¯ƒÁ¨\è³Ãœ«¨\î\É\ä®7\Ã9F…\Ín9­0ƒ$a¯%¢=l;sEg\Û²\Ür6¤>œ³¶›|¶Ž\"£\âZÌvG\ã?\êW\ÒOé·¡¹\Åq§§š™À\Å4m{q\äFV+q¨§\Ó\r0\'ž”a)š_@QS\âÀ…\Îz P\å„Aƒº	\ç%\Øù\" ñaP\ä-\Â	- ¯¼T˜jm°ùõzOÀ1\çù¨‰{¢£\ÎÑ\Ñ\àœy!_.!–¿\Ä0‚m<šPXS\È0¢C¤d\"’89(šóT+ln¦ˆmû`\Çñ\å\âš&6uMZÖœ\Çû\Äùy|\×\\1ûb\Õoð\Í¶\È\ËG&—ek,´’m¾£‚8)›mÀ\\[ô‰}©\î\é\ËZUÄª¾g{T\ê®Dj\Û\ÉsR$n\È+\ë[  6^•\î\Û\Ûñ²ý¶pË§§dlMV!m!T)\î\Äg\ÕX„R\0eÏ’\"axš©] .\æ²dh\ÑbŸ‰\ê\rªyýÒ©^\íJ°\Õñ\Õtš²ð=¢O	=Ÿ\Õ{?PÉœb@/K·ºø>½’\Ø\á:‡º™\Ýjj\Ùø‚m\rš\Ög\ßUv\Åÿ\Ù'),(2006,'Jessa M Dali. ','Buntatala Jaro Iloilo City','1980-12-12',44,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09512244121','  . ','','','2025-10-09 07:57:46',NULL),(2007,'Desk D Top. ','Suite 10','1999-10-09',26,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09151345454','  . ','','','2025-10-09 07:58:54',_binary 'ÿ\Øÿ\à\0JFIF\0\0`\0`\0\0ÿ\Û\0C\0		\n\r\Z\Z $.\' \",#(7),01444\'9=82<.342ÿ\Û\0C			\r\r2!!22222222222222222222222222222222222222222222222222ÿÀ\0\0À\0À\"\0ÿ\Ä\0\0\0\0\0\0\0\0\0\0\0	\nÿ\Ä\0µ\0\0\0}\0!1AQa\"q2‘¡#B±ÁR\Ñð$3br‚	\n\Z%&\'()*456789:CDEFGHIJSTUVWXYZcdefghijstuvwxyzƒ„…†‡ˆ‰Š’“”•–—˜™š¢£¤¥¦§¨©ª²³´µ¶·¸¹º\Â\Ã\Ä\Å\Æ\Ç\È\É\Ê\Ò\Ó\Ô\Õ\Ö\×\Ø\Ù\Ú\á\â\ã\ä\å\æ\ç\è\é\êñòóôõö÷øùúÿ\Ä\0\0\0\0\0\0\0\0	\nÿ\Ä\0µ\0\0w\0!1AQaq\"2B‘¡±Á	#3Rðbr\Ñ\n$4\á%ñ\Z&\'()*56789:CDEFGHIJSTUVWXYZcdefghijstuvwxyz‚ƒ„…†‡ˆ‰Š’“”•–—˜™š¢£¤¥¦§¨©ª²³´µ¶·¸¹º\Â\Ã\Ä\Å\Æ\Ç\È\É\Ê\Ò\Ó\Ô\Õ\Ö\×\Ø\Ù\Ú\â\ã\ä\å\æ\ç\è\é\êòóôõö÷øùúÿ\Ú\0\0\0?\0ò\Ð\Äw4˜\È\ä\Òð9õ4\Üó\ÐW5®\ãZ\ås‡^µduÇ­# h\È8\Å4\ì\ÄõCPù‘†\ÏZxN5ZÜZ3\ë\ÅZ\\mÀ¥$öeup c§ÿ\0Z›“Ö¤À\éQ³(8¤µ\Ñ7­8Iá¡¶žôÃƒÓš»{\'=E60vt\îE¥Iý\ß\âkH#*¯A1ŠFf¥\"“kMN{\ãšN’·\Ð*õ¤\Ûû\Óî¢”¶4‹\ÔrdŒb›ý\ëT\èE\Í9£Du\n\â¹o­B[›9Y¤\Å?\Å\nµ(éŠ†FzƒKŽØ§¨\ïù\ÒL\ä°íš«\n\ã;zRc\'œ\ÓÌŠ=N})¹-\ÑI\\¬\Í\Íw)\ã9\ãŽ)IŽM0ž>©¹Á¬lÙ­õ0[\â‚\ìv  zŠsc¯4zR_’p\àt9©ÁÇ­6e\Êgó¦DKGƒÛŠ«]\\„\í+3œS\Ë÷¥-…\æAr˜B£µ7\0t&?J0\âª\Âr°Íµ$\0”>&\áÿ\0ê¥‰Š‚žNj\Ò\ÔÂ¤•´z\ãÒš~”õŽV\Îó\íO²¤Æ¬\Æö+\Ï=¨Àó9ô«bÓ¹=ý*Qjž„þ445$È­\ä/’aK$¾l»’3Œq\ÅN°¨\è ~*\Ç\íG*¨Û¹LG3r\ä\ÓÅ¼§«þUuSž• Lö§\È\Ñ÷)‹0~ó152\ÙF\Üü\Í[X¹©sU\ÊC‘Y`@s° §\ìt\ëS\ì\ã¿\ï,úSJ\â¹Ç‡4õ U¼\ÂGJPòt\Ë\Èvó¤Z\Ý\È\È\Ï\ãHÍ·ž?:…#.p\Ïø\nœ[F994ù	uH\ÚU+‚j(Ùƒ6œúUõ‰¢R¨ü?\n¾U±“ª\îQLÇˆ\ãS-´\íÔª\ÕÐ§#š‘TÓ±£*-“c™O\Ð\n‘lc\Äûš¶5\"§=)Ø—\"²\ÛÆ½V‚\é¡S{\Ïj\0-Œ’ ô\àqMŽ4ó™»gñm\ëZ_\0y\0®1µœ‘\ÇN?È§fG2*\Ýi\ßdˆ—v2>Sô9\çJ¥·Ò¯\Ï#\Ý^4Ý•ylz\Ò-¬‡¢1ü*¹D\äRò\è\Û\ê+@YÉõd~\ác/R\0úš«2yÑž±\äÔ=«@YrÀcÒ¤K1œn\'ð¦¢/h¬PH‰\ëÉ©’šô\rx*\×R\Ò\rÙŠ#±~c+>I\Æx\ÚEAg\á\é/¾\ÞlœB\Ý\ÆXVB\Ù\É8,Œš\ÑE^\ÄóJÉ¥¹Áù`O­YƒM»¸,!´ž]£\'dl\Ø³]Nƒkq{q<Q\ÝKo\Æ\ìB\Å‰ \íöþT\íKƒPñ<V· \Éö3`¾À\ÝÛ¥i\ìô»1ö\Órq[œ\Ð\Òn<ÀŒ±©Á?<È¿ÌŽ}º\Ó\ÛL\ØS7VüŽ¯­t“Y\Ø[ø·\ìù_°%Ò‡\Ù7\r\Ã=À\äUŸ&”úº\è\â B7˜Sh-¸õ÷\Æ+6\Òi.¦©N×¾Ç…¤Kõ©–1\Ø¾•0·p¥¶ü \í-Žô?•H\"\é\Írrœ\Úªsš“nMXHj\Ìp/÷j¹Hs)„\È\é\ÅL1#j±úÖŒP€x\\\Z½g®)ò™¹ö2ã±¸Æ®E¢\Ü\É\Ô*ýMkÀž„Ö„1–l`ŸlQ\Ê\ÉU‹‡œ\ã|\Ê=p3V\ã\Ð`V\æsŸ@tú}\ÜòyqZ\Ë#\ã%Q	8õÀ\íZpø[W—‘§\Ì?\Þ\î\Âó{ºh¶K\ÕYÿ\0\Þð©–\Æ\Ñ\Þ/NW?ÎºUð\Õð<iznuÿ\0\ZÔƒÀ7²\"³\ÜÛ¨<ðI?Ê­\ÅE{\ÄAº­¨\êqR¨\Â\"¢\ÕwCšô¡ðô%õŸ\î¬_\×54\0\ÓYñ4\×\'¸\Æú\Z¨\Ê	\\·Jw³<¡ \'¯Zo‘ƒ^\Õ´(\ÎZ\Ý\äÿ\0}\Ïô\ÅJ\Þ\Ñ!dÙ§ \àôf\Ç\ã\Í\Ú=_Uš[ž$-\Ë?…Y¶\Ó\æ”\â8þƒ5\îvšUº|–V\êrH`€œ}qW€\0`Sõ\"\Ö\ÌóR¸\Ò4¿\ìóf\æYW;Y[v\Æ9ú\ÖlV\Z\ë]Kod—·7#FN\Ñý\â:ú×¥^i¿k¸‰~@¸*Þ¤öa\ëÞ¢µÓš\Ú\â<[@61-:ð\Î0p1øúö¦ªõCxw¢oC‰\Óü®Ú–hn!¶,0\ß7QøSÿ\0Â¾œ0k‹\èÁfäª–$þ•\è”\ÖP\ã?\Å\'^o©T¥½Ž\0Z‡\Û5\ë•\ìDEsùñZ\Þ\ÑönfžNH\å\Æ:ý+§6ñö>½O\ëR*…\\\n‡R]Ë®‡Î‰\àŸ\\……\í\ÒTÚždé¼Œdz1\ÍX‹\á6µò\ÝØ ’;ý½V\Ú(a9‰0…x_Ë¥]VµºŠ\êyó©+hy¯\Â\'u\r6°‹\ì–\å¿R\ÃùVøa¥Ù”k«û\É\ÉER[ŒÁ\ë\Íz|½*t4\Ú\ìDd\í©\Éiÿ\04!\n¼ð]–\Éù%˜dñ¾\Ø\ïZ-\à\Í\n\ÛkÃ¦£(\á·H\Í\Üs\Éÿ\09®‰M<qY\ì\Í»™š^…¥CWL¶V\ÏVŒ1ü\Íi\\\éVW¶/g5º˜•>^‡#‘\î*E5(j\ÎGU6­fdiµ\Ñu)î­¥}’&Å‰¹\Ø8\îy=+r\ZZ\Éß©\Ó’²\"’\Ú)\Ý\nsœ¨Á©\0\n¡G@0)h¡¶ô\ZŒSºAIœâ–ŠC\n(¢€\n(¢€\n(¢˜E%-0\n(¢˜„GVT\Õ(Or28®\Ã\Æ,-N•]MN½hb\'ZTJjAY²\âÇŽµ(¨…H*\ÓM’ŠuVk˜\ã#6\Ô\È\ê\ã*ÀPk9E\îtS©;&;4µ³E	d¸-\Ðw?J­ý­§ý§\ìÿ\0j`v”`A\Í.Rù\í¹~Š(¨,(¢Š\0(¢Š\0(¢Š\0(¢Š\0Z))j“‹„ð*\ä}Ef\Û>Tb¯\Ä\Þýk°ñËˆ8©\ÔsUÑª\Â\Zl’e©F´ñY²\â<S\ÅGšx5\Þ-š®Vø1 žþ\Ãß§\ëZ6°\Éip\î]\Ì2üÀ3\ä+ÀcŠ´\Ð\Ç)\Ôt¬½fð\ì0[™L\Ù\Ú>œþ¿ýj©Kž*(Æ…cVu\ßb=[\Ëxn¦²•Ðƒªóqƒõýk3M\Óå¹Š=RòTmp98ùsÇ½:\ÃE»3?ú38dVv\Ü\í\Ç\áú\0j9öL¶K;\\ª\à’¡\ÎH=y\Ïó©Šm\Ú\'\\¥i­Y\ÛQ@\éEsaEPEPEPEPEPœXN$‚7^Œ ŠÓ‰Ï­rþ¸\èV/œþ\åTý@\Çô®†&\â»S<w¹§•f7\ã¥gF\ÜÕ¸ÛŠ¡\Ñ\ê`j¢\Z²§Š™ D‚š`§T3H±%¸Ž\Þ#$\Ò* \êI\ÅrúEÄ£\ÅFYxºWŒ`}\à?b´üMcq¨\èrAjž]\n‚\Ø\Ï\Ì3\Ï\Ó5›c¥j¶\ég3\Ûš7óAŸ\Ëó¡]?&\\”\\o\Õ4ÎTB–ó·\Ê#”d\ã Á®KYžhu+¸cm£q8P2A\ç¯^õÓ–\Õg³™\Ú\â•\Õ\Ç\Ã~¹\Ås“iz\Õ\ìžmÝ£´§©Þ€\Ë\éJ•\î]O[~kfÛ¬ l\ç1©ý*j‚\É\Z+x\Ýv²ÆªG¡§®w¹\Ý‚Š(¤0¢Š(\0¢Š(\0¢Š(\0¢Š(\Âüs\æøz%\Î|·dýw\ì\Õ\Ù\ÄüpZó‡3\æ\Ö\î~\ë«~`\é^‰	\àWb<š‹\ÞeøÛŠ·\Õ(\ê\Ü}*\Ñ\è\ØÕ”n*¤dU”<PÁ§ŠMH+&\\GM{a½r(\"Š–or4½\'\ãR\äÿ\0yÿ\0*dò\É\n+G	—ž@<\ÔbúO›u¬Š@\îG\\t¥«\Ô\Õ$´l¸§*)i‘I\æ&\ìc’1O¬^\çJ\Ø(¢ŠC\n(¢€ŠZ(„¢ŠZaa(¥¢\ì|\Ç\à	\Ìz´ñz<þ Šõ(_ŠñŸOö[\à\à6Tþ ÿ\0\\W©‰.ZUXd‰2¤üñ\Î1\è\Ãk®eU­Ž‚7«q¸\ÏZÇ¶˜\É8\ãr†\ÇÖ–\rAšðÀ\Ê3ƒžµ¬\"\å±\ÍR¬)´¥\Ô\èczÕ¨\ßÞ±%¹h-e™Sy\í\Î3œf¦³Ô¢¸\Ã\ÊN¹\ßŒ0FH\äz\Ò5±¼Su¬ß´$KºF\n½2zU¨®#“;$F\Ç]¬*ZZ¤Sóu\ãjx\ãÚ³h\Ò,O\àŽj¸j5CG\\*\"E\n£\n\0‚–š\r.k6™²’ŠLÑšVc¸´Rf’‹\ÇQL\Í§\Ê.aÙ¥¦fŒ\Ñ\Ê.aôSh¢\Ås\Ø\Íö}R\Úl\ãlŠO\àk\Ø8\çEó^U±ÊŸP{\ZñfùY{×°\ésyöPMœù‘«þc5µ7¡ÁY-\r›|G\ZF2U(\É\ÏAR,@N²¯\\óU\ãj²\Åm	8\ìsT£\n–\æ[\Øy\ÖòE¸®õ+‘\Û\"³´¿´Ag\É(Ê²\"§•û¾fm\Þc\î\åG\\uü¯Fõn68\àÓ¸\Ùr`f¶’1Õ—iLAlŒ1Œ“ƒžœóÀ\Å1\æ­GÛ¶\Ý3Š—´ûö.\ÜyonDLy]Á‡\È\ëPiÈ‘8Øª:¡eŒ.O\\qÁ\Æ:û\ÔÑ¶F§\Ço\Ê$A´ú)À\'\×3\ïC\ËdñJŒ6Œi™¦€\Ãõ\ÍE‹R\Ö\å°j36Ù‚Á\ï@4\×\nX1\\t¬¥\î¦ÎŽiJÜ¬Ÿ>”3„]\Ìx¦)ùr§¡¤¯k³^n\Ãò=h¨Py§šQm\Ü9´š3QJ_\Êa<\Â\Üô\Í2\Ú6†\ãg.Ê \'94Û±<Ú–(¦\Õ-F\í\íP:t\0“Ç¥\\b\äìˆ«Z4 \ç-‘¡EeZjRO\"©\\d\à\ä`Š\Ô\íD\à\à\ì\ÅC\nñ\æñÌ£÷yô5\éz\ã/…’\åT;\Å§¡ÛœÒ¼\Õÿ\0Õ°ô•\Ýx*O7Exd/˜@¸ õ\éR\n\Û\\\é4MgûO\ÌV€D\èÀ“x ÷\è1[‚E^X\îkž\Òô¥\ÓošH›w‹aW|• ñŽ:c=\ël¹\ÛòŒžõ´w³9e+&\Ñz7\r‚\"¬¤ˆ)eF@\Ï&³-˜€\ÇN)\Ì\à\\»i$c\ç#\èjÚ³±0Ÿ<T™¸Œ*\Ìl+2w*ŸQœQÓ‹“ðŽ¹>õœ\ê¨ZýJK˜Þ\ê\Ò0\È\æ²\ã”p3Ioª\ÛI;DX¬ªHÁCŒu\ëŒtÁ­\Äõ6§U–QR,ŠN22:\Ô4Re€iÙ¨CS·TX\ÑL—\"šƒu85+\Z*„¼g=\è\ÍGº—p¥b¹\Ðó\Í\Í\Ôn¢\Ã\æDj¥\í§Ú”€0A½O»\éwSM\Å\ÝR0«	leZÚµ½\â¡\Üy\ÉcÞ¶i™\ÍÓœœ\ÝÙž\ZŒhE\Æ/sÿ\Ù');
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patients_backup`
--

LOCK TABLES `patients_backup` WRITE;
/*!40000 ALTER TABLE `patients_backup` DISABLE KEYS */;
INSERT INTO `patients_backup` VALUES (3,1001,'Giraud Audiss','13th Floors','2016-12-15',8,'M','Married','09171234567','Lorelle Giacovazzo','09171234567','Sibling',NULL,'2025-09-27 03:49:24');
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
  `note` text,
  `consultation_id` int DEFAULT NULL,
  PRIMARY KEY (`prescription_id`),
  KEY `patient_id` (`patient_id`),
  KEY `item_id` (`item_id`),
  CONSTRAINT `prescription_ibfk_1` FOREIGN KEY (`patient_id`) REFERENCES `patients` (`patient_id`) ON DELETE CASCADE,
  CONSTRAINT `prescription_ibfk_2` FOREIGN KEY (`item_id`) REFERENCES `items` (`item_id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prescription`
--

LOCK TABLES `prescription` WRITE;
/*!40000 ALTER TABLE `prescription` DISABLE KEYS */;
INSERT INTO `prescription` VALUES (40,2,32,1,'2025-10-07 13:18:38','ASD',120),(41,2,35,1,'2025-10-07 13:18:38','ASDAS',120),(42,2,34,1,'2025-10-07 13:18:38','ASD',120),(46,1005,34,1,'2025-10-07 19:29:47','wow',127),(47,1002,34,1,'2025-10-07 19:30:42','asd',128),(48,1006,34,1,'2025-10-07 19:35:03','asdasd',129),(49,1003,35,3,'2025-10-09 14:42:45','DRINK ONLY IF HAVE HEADACHE',148),(50,1003,34,10,'2025-10-09 15:00:50','DRINK WHEN RASH IS VISIBLE',149),(51,1003,33,10,'2025-10-09 15:00:50','DRINK IF ONLY HAVE HEADACHE',149);
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
  `note` text,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `fk_prescription_other_patient` (`patient_id`),
  KEY `fk_prescription_other_consultation` (`consultation_id`),
  KEY `fk_prescription_other_item` (`item_id`),
  CONSTRAINT `fk_prescription_other_consultation` FOREIGN KEY (`consultation_id`) REFERENCES `consultation` (`consultation_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_prescription_other_item` FOREIGN KEY (`item_id`) REFERENCES `other_items` (`item_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_prescription_other_patient` FOREIGN KEY (`patient_id`) REFERENCES `patients` (`patient_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prescription_other`
--

LOCK TABLES `prescription_other` WRITE;
/*!40000 ALTER TABLE `prescription_other` DISABLE KEYS */;
INSERT INTO `prescription_other` VALUES (4,2,141,35,1,'','2025-10-08 22:43:50'),(5,1003,149,35,1,'','2025-10-09 07:00:50');
/*!40000 ALTER TABLE `prescription_other` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `prescriptions_detailed`
--

DROP TABLE IF EXISTS `prescriptions_detailed`;
/*!50001 DROP VIEW IF EXISTS `prescriptions_detailed`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `prescriptions_detailed` AS SELECT 
 1 AS `prescription_id`,
 1 AS `patient_id`,
 1 AS `patient_name`,
 1 AS `item_id`,
 1 AS `item_name`,
 1 AS `quantity`,
 1 AS `note`,
 1 AS `created_at`,
 1 AS `consultation_id`*/;
SET character_set_client = @saved_cs_client;

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
  `status` enum('waiting','examining','done','skipped') DEFAULT 'waiting',
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
  `called_at` datetime DEFAULT NULL,
  `finished_at` datetime DEFAULT NULL,
  PRIMARY KEY (`queue_id`),
  KEY `patient_id` (`patient_id`),
  CONSTRAINT `queue_ibfk_1` FOREIGN KEY (`patient_id`) REFERENCES `patients` (`patient_id`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `queue`
--

LOCK TABLES `queue` WRITE;
/*!40000 ALTER TABLE `queue` DISABLE KEYS */;
INSERT INTO `queue` VALUES (29,990,1,'waiting','2025-09-16 16:01:02',NULL,NULL),(30,1000,2,'waiting','2025-09-16 16:01:05',NULL,NULL),(31,2,1,'done','2025-09-23 02:16:51',NULL,'2025-09-23 02:17:15'),(32,1010,2,'waiting','2025-09-23 02:16:52',NULL,NULL),(33,1071,3,'waiting','2025-09-23 02:17:08',NULL,NULL),(34,1329,1,'waiting','2025-09-30 18:27:34',NULL,NULL),(35,1329,1,'waiting','2025-10-01 19:38:30',NULL,NULL),(36,1002,2,'waiting','2025-10-01 19:55:30',NULL,NULL),(37,1006,3,'waiting','2025-10-01 19:55:30',NULL,NULL),(38,1009,4,'waiting','2025-10-01 19:55:31',NULL,NULL),(39,2,1,'done','2025-10-02 22:20:48',NULL,'2025-10-02 22:21:02'),(40,990,2,'skipped','2025-10-02 22:20:51',NULL,NULL),(41,2,1,'examining','2025-10-07 21:19:22','2025-10-07 21:29:15',NULL),(42,2,1,'waiting','2025-10-09 15:43:47',NULL,NULL);
/*!40000 ALTER TABLE `queue` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `queue_overview`
--

DROP TABLE IF EXISTS `queue_overview`;
/*!50001 DROP VIEW IF EXISTS `queue_overview`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `queue_overview` AS SELECT 
 1 AS `queue_id`,
 1 AS `patient_name`,
 1 AS `queue_number`,
 1 AS `status`,
 1 AS `created_at`,
 1 AS `called_at`,
 1 AS `finished_at`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `returns`
--

DROP TABLE IF EXISTS `returns`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `returns` (
  `return_id` int NOT NULL AUTO_INCREMENT,
  `item_id` int NOT NULL,
  `quantity` int NOT NULL,
  `return_date` datetime NOT NULL,
  `reason` varchar(255) DEFAULT NULL,
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
  `updated_at` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`return_id`),
  KEY `item_id` (`item_id`),
  CONSTRAINT `returns_ibfk_1` FOREIGN KEY (`item_id`) REFERENCES `items` (`item_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `returns`
--

LOCK TABLES `returns` WRITE;
/*!40000 ALTER TABLE `returns` DISABLE KEYS */;
/*!40000 ALTER TABLE `returns` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `returns_overview`
--

DROP TABLE IF EXISTS `returns_overview`;
/*!50001 DROP VIEW IF EXISTS `returns_overview`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `returns_overview` AS SELECT 
 1 AS `return_id`,
 1 AS `item_name`,
 1 AS `category`,
 1 AS `quantity`,
 1 AS `reason`,
 1 AS `return_date`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `revenue_report`
--

DROP TABLE IF EXISTS `revenue_report`;
/*!50001 DROP VIEW IF EXISTS `revenue_report`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `revenue_report` AS SELECT 
 1 AS `revenue_date`,
 1 AS `revenue_type`,
 1 AS `revenue_amount`*/;
SET character_set_client = @saved_cs_client;

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
  PRIMARY KEY (`sale_id`),
  KEY `item_id` (`item_id`),
  CONSTRAINT `sales_ibfk_1` FOREIGN KEY (`item_id`) REFERENCES `items` (`item_id`)
) ENGINE=InnoDB AUTO_INCREMENT=265 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales`
--

LOCK TABLES `sales` WRITE;
/*!40000 ALTER TABLE `sales` DISABLE KEYS */;
INSERT INTO `sales` VALUES (209,30,120,12.00,0.00,172.80,1612.80,'2025-09-27 10:53:33'),(210,34,2,6.61,0.00,1.59,14.81,'2025-09-27 20:59:02'),(211,30,1,12.00,0.00,1.44,13.44,'2025-09-27 20:59:02'),(212,31,1,15.00,0.00,1.80,16.80,'2025-09-27 20:59:02'),(213,33,4,12.31,0.00,5.91,55.15,'2025-09-27 20:59:02'),(214,32,3,12.00,0.00,4.32,40.32,'2025-09-27 20:59:02'),(215,34,1,6.61,0.00,0.79,7.40,'2025-09-27 21:03:37'),(216,32,2,12.00,0.00,2.88,26.88,'2025-09-27 21:03:37'),(217,33,3,12.31,0.00,4.43,41.36,'2025-09-27 21:03:37'),(218,34,1,6.61,0.00,0.79,7.40,'2025-09-27 21:05:49'),(219,32,2,12.00,0.00,2.88,26.88,'2025-09-27 21:05:49'),(220,33,3,12.31,0.00,4.43,41.36,'2025-09-27 21:05:49'),(221,34,1,6.61,0.00,0.79,7.40,'2025-09-27 21:06:36'),(222,32,2,12.00,0.00,2.88,26.88,'2025-09-27 21:06:36'),(223,34,1,6.61,0.00,0.79,7.40,'2025-09-27 21:30:06'),(224,32,2,12.00,0.00,2.88,26.88,'2025-09-27 21:30:06'),(225,33,3,12.31,0.00,4.43,41.36,'2025-09-27 21:30:06'),(226,34,1,6.61,0.00,0.79,7.40,'2025-09-27 21:30:56'),(227,32,2,12.00,0.00,2.88,26.88,'2025-09-27 21:30:56'),(228,33,3,12.31,0.00,4.43,41.36,'2025-09-27 21:30:56'),(229,34,1,6.61,0.00,0.79,7.40,'2025-09-27 21:34:33'),(230,32,2,12.00,0.00,2.88,26.88,'2025-09-27 21:34:33'),(231,33,3,12.31,0.00,4.43,41.36,'2025-09-27 21:34:33'),(232,34,1,6.61,0.00,0.79,7.40,'2025-09-27 21:35:02'),(233,32,2,12.00,0.00,2.88,26.88,'2025-09-27 21:35:02'),(234,33,3,12.31,0.00,4.43,41.36,'2025-09-27 21:35:02'),(235,34,1,6.61,0.00,0.79,7.40,'2025-09-27 21:35:36'),(236,32,2,12.00,0.00,2.88,26.88,'2025-09-27 21:35:36'),(237,33,3,12.31,0.00,4.43,41.36,'2025-09-27 21:35:36'),(238,34,1,6.61,0.00,0.79,7.40,'2025-09-27 21:42:30'),(239,32,2,12.00,0.00,2.88,26.88,'2025-09-27 21:42:30'),(240,33,3,12.31,0.00,4.43,41.36,'2025-09-27 21:42:30'),(241,34,1,6.61,0.00,0.79,7.40,'2025-09-27 21:45:12'),(242,32,2,12.00,0.00,2.88,26.88,'2025-09-27 21:45:12'),(243,33,3,12.31,0.00,4.43,41.36,'2025-09-27 21:45:12'),(244,34,1,6.61,0.00,0.79,7.40,'2025-09-27 21:47:49'),(245,32,2,12.00,0.00,2.88,26.88,'2025-09-27 21:47:49'),(246,33,3,12.31,0.00,4.43,41.36,'2025-09-27 21:47:49'),(247,34,1,6.61,0.00,0.79,7.40,'2025-09-27 22:22:54'),(248,32,2,12.00,0.00,2.88,26.88,'2025-09-27 22:22:54'),(249,33,3,12.31,0.00,4.43,41.36,'2025-09-27 22:22:54'),(250,34,1,6.61,0.00,0.79,7.40,'2025-09-27 22:25:45'),(251,32,2,12.00,0.00,2.88,26.88,'2025-09-27 22:25:45'),(252,33,3,12.31,0.00,4.43,41.36,'2025-09-27 22:25:45'),(253,34,1,6.61,0.00,0.79,7.40,'2025-09-27 22:30:24'),(254,32,2,12.00,0.00,2.88,26.88,'2025-09-27 22:30:24'),(255,33,3,12.31,0.00,4.43,41.36,'2025-09-27 22:30:25'),(256,34,1,6.61,0.00,0.79,7.40,'2025-09-27 22:34:49'),(257,32,2,12.00,0.00,2.88,26.88,'2025-09-27 22:34:49'),(258,33,3,12.31,0.00,4.43,41.36,'2025-09-27 22:34:49'),(259,34,1,6.61,0.00,0.79,7.40,'2025-09-27 23:22:08'),(260,32,2,12.00,0.00,2.88,26.88,'2025-09-27 23:22:08'),(261,33,3,12.31,0.00,4.43,41.36,'2025-09-27 23:22:08'),(262,34,1,6.61,0.00,0.79,7.40,'2025-09-27 23:24:31'),(263,32,2,12.00,0.00,2.88,26.88,'2025-09-27 23:24:31'),(264,33,3,12.31,0.00,4.43,41.36,'2025-09-27 23:24:31');
/*!40000 ALTER TABLE `sales` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `sales_summary`
--

DROP TABLE IF EXISTS `sales_summary`;
/*!50001 DROP VIEW IF EXISTS `sales_summary`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `sales_summary` AS SELECT 
 1 AS `invoice_id`,
 1 AS `customer_name`,
 1 AS `invoice_date`,
 1 AS `invoice_type`,
 1 AS `invoice_subtotal`,
 1 AS `discount_percent`,
 1 AS `invoice_discount`,
 1 AS `invoice_net_total`,
 1 AS `amount_received`,
 1 AS `change_due`,
 1 AS `invoice_note`,
 1 AS `item_id`,
 1 AS `item_name`,
 1 AS `item_category`,
 1 AS `item_quantity`,
 1 AS `item_unit_price`,
 1 AS `item_total_price`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `sales_view`
--

DROP TABLE IF EXISTS `sales_view`;
/*!50001 DROP VIEW IF EXISTS `sales_view`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `sales_view` AS SELECT 
 1 AS `Invoice ID`,
 1 AS `Customer Name`,
 1 AS `Invoice Date`,
 1 AS `Invoice Type`,
 1 AS `Invoice Subtotal`,
 1 AS `Discount Percent`,
 1 AS `Invoice Discount`,
 1 AS `Invoice Net Total`,
 1 AS `Amount Received`,
 1 AS `Change Due`,
 1 AS `Note`,
 1 AS `Item ID`,
 1 AS `Item Name`,
 1 AS `Category`,
 1 AS `Quantity`,
 1 AS `Unit Price`,
 1 AS `Item Subtotal`,
 1 AS `Item Discount`,
 1 AS `Item Net Total`*/;
SET character_set_client = @saved_cs_client;

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
  PRIMARY KEY (`movement_id`),
  KEY `item_id` (`item_id`),
  CONSTRAINT `stock_movements_ibfk_1` FOREIGN KEY (`item_id`) REFERENCES `items` (`item_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=203 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stock_movements`
--

LOCK TABLES `stock_movements` WRITE;
/*!40000 ALTER TABLE `stock_movements` DISABLE KEYS */;
INSERT INTO `stock_movements` VALUES (131,34,'IN',120,'2025-09-27 18:03:37','2025-09-20 02:02:59',6.61,NULL),(132,30,'IN',120,'2025-09-27 18:03:46','2025-10-11 02:02:59',12.00,NULL),(133,30,'IN',150,'2025-09-27 18:03:51','2025-10-11 02:02:59',12.00,NULL),(135,30,'OUT',1,'2025-09-27 18:34:42',NULL,12.00,NULL),(136,30,'OUT',1,'2025-09-30 10:11:31',NULL,12.00,NULL),(137,31,'OUT',2,'2025-09-30 10:11:31',NULL,15.00,NULL),(138,31,'OUT',2,'2025-09-30 10:11:31',NULL,15.00,NULL),(139,32,'OUT',2,'2025-09-30 10:11:31',NULL,12.00,NULL),(140,34,'OUT',2,'2025-09-30 10:11:31',NULL,6.61,NULL),(141,35,'IN',120,'2025-10-01 11:44:03',NULL,150.00,NULL),(142,30,'OUT',1,'2025-10-01 13:25:24',NULL,12.00,NULL),(143,33,'OUT',2,'2025-10-01 13:25:24',NULL,12.31,NULL),(144,34,'OUT',1,'2025-10-01 13:25:24',NULL,6.61,NULL),(145,34,'OUT',1,'2025-10-01 13:25:24',NULL,6.61,NULL),(146,30,'OUT',1,'2025-10-01 13:25:24',NULL,12.00,NULL),(147,34,'OUT',5,'2025-10-01 13:25:24',NULL,6.61,NULL),(148,35,'OUT',1,'2025-10-01 13:25:24',NULL,150.00,NULL),(149,33,'OUT',1,'2025-10-01 13:25:24',NULL,12.31,NULL),(150,34,'OUT',1,'2025-10-01 13:25:24',NULL,6.61,NULL),(151,33,'OUT',1,'2025-10-01 13:26:23',NULL,12.31,NULL),(152,34,'OUT',1,'2025-10-01 13:26:23',NULL,6.61,NULL),(153,34,'OUT',1,'2025-10-08 07:43:47',NULL,6.61,NULL),(154,35,'OUT',2,'2025-10-08 07:43:47',NULL,150.00,NULL),(155,35,'OUT',1,'2025-10-08 08:29:48',NULL,150.00,NULL),(156,30,'OUT',1,'2025-10-08 08:33:24',NULL,12.00,NULL),(157,35,'OUT',1,'2025-10-08 08:34:28',NULL,150.00,NULL),(158,34,'OUT',1,'2025-10-08 08:36:00',NULL,6.61,NULL),(159,31,'OUT',2,'2025-10-08 08:37:23',NULL,15.00,NULL),(160,34,'OUT',1,'2025-10-08 08:43:39',NULL,6.61,NULL),(161,35,'OUT',1,'2025-10-08 08:43:39',NULL,150.00,NULL),(162,34,'OUT',1,'2025-10-08 08:56:49',NULL,6.61,NULL),(163,35,'OUT',1,'2025-10-08 08:56:49',NULL,150.00,NULL),(164,34,'OUT',2,'2025-10-08 08:57:37',NULL,6.61,NULL),(165,30,'OUT',1,'2025-10-08 09:18:38',NULL,12.00,NULL),(166,34,'OUT',1,'2025-10-08 09:18:38',NULL,6.61,NULL),(167,35,'OUT',1,'2025-10-08 09:27:53',NULL,150.00,NULL),(168,34,'OUT',1,'2025-10-08 09:27:53',NULL,6.61,NULL),(169,34,'OUT',1,'2025-10-08 09:41:16',NULL,6.61,NULL),(170,35,'OUT',1,'2025-10-08 09:41:16',NULL,150.00,NULL),(171,34,'OUT',1,'2025-10-08 09:41:45',NULL,6.61,NULL),(172,31,'OUT',1,'2025-10-08 09:41:45',NULL,15.00,NULL),(173,34,'OUT',1,'2025-10-08 09:46:02',NULL,6.61,NULL),(174,35,'OUT',1,'2025-10-08 09:46:02',NULL,150.00,NULL),(175,31,'OUT',1,'2025-10-08 09:49:26',NULL,15.00,NULL),(176,34,'OUT',1,'2025-10-08 09:49:26',NULL,6.61,NULL),(177,34,'OUT',1,'2025-10-08 09:57:34',NULL,6.61,NULL),(178,30,'OUT',1,'2025-10-08 10:26:22',NULL,12.00,NULL),(179,34,'OUT',1,'2025-10-08 10:26:22',NULL,6.61,NULL),(180,35,'OUT',10,'2025-10-08 10:26:22',NULL,150.00,NULL),(181,35,'OUT',11,'2025-10-08 10:27:14',NULL,150.00,NULL),(182,34,'OUT',10,'2025-10-08 10:27:29',NULL,6.61,NULL),(183,32,'OUT',1,'2025-10-08 10:29:56',NULL,12.00,NULL),(184,31,'OUT',1,'2025-10-08 10:30:12',NULL,15.00,NULL),(185,34,'OUT',1,'2025-10-08 10:30:12',NULL,6.61,NULL),(186,30,'OUT',1,'2025-10-08 10:37:25',NULL,12.00,NULL),(187,35,'OUT',1,'2025-10-08 10:37:25',NULL,150.00,NULL),(188,34,'OUT',1,'2025-10-08 11:00:59',NULL,6.61,NULL),(189,32,'OUT',1,'2025-10-08 11:05:42',NULL,12.00,NULL),(190,30,'OUT',1,'2025-10-08 11:05:42',NULL,12.00,NULL),(191,34,'OUT',1,'2025-10-08 11:07:29',NULL,6.61,NULL),(192,35,'OUT',1,'2025-10-08 11:07:29',NULL,150.00,NULL),(193,32,'OUT',1,'2025-10-08 12:02:29',NULL,12.00,NULL),(194,30,'OUT',1,'2025-10-08 12:02:29',NULL,12.00,NULL),(195,33,'OUT',1,'2025-10-08 12:05:09',NULL,12.31,NULL),(196,32,'OUT',1,'2025-10-08 12:05:09',NULL,12.00,NULL),(197,31,'OUT',1,'2025-10-08 12:05:09',NULL,15.00,NULL),(198,30,'OUT',1,'2025-10-08 12:07:44',NULL,12.00,NULL),(199,32,'OUT',1,'2025-10-08 12:07:44',NULL,12.00,NULL),(200,34,'OUT',1,'2025-10-08 12:07:59',NULL,6.61,NULL),(201,31,'OUT',1,'2025-10-08 13:06:00',NULL,15.00,NULL),(202,33,'OUT',1,'2025-10-08 13:12:59',NULL,12.31,NULL);
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
        SET stock_quantity = stock_quantity + NEW.quantity
        WHERE item_id = NEW.item_id;
    ELSEIF NEW.movement_type = 'OUT' OR NEW.movement_type = 'WRITE-OFF' THEN
        -- Outgoing or written-off items decrease stock
        UPDATE items
        SET stock_quantity = stock_quantity - NEW.quantity
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
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_stock_movements_update` AFTER UPDATE ON `stock_movements` FOR EACH ROW BEGIN
    -- 1. Insert into history log
    INSERT INTO stock_movements_history (
        movement_id, item_id, movement_type, quantity, expiration_date, 
        action_type, old_quantity, new_quantity
    )
    VALUES (
        OLD.movement_id, OLD.item_id, OLD.movement_type, OLD.quantity, OLD.expiration_date, 
        'UPDATE', OLD.quantity, NEW.quantity
    );

    -- 2. Adjust stock quantity
    IF OLD.movement_type = 'IN' THEN
        UPDATE items 
        SET stock_quantity = stock_quantity - OLD.quantity + NEW.quantity
        WHERE item_id = OLD.item_id;
    ELSEIF OLD.movement_type = 'OUT' THEN
        UPDATE items 
        SET stock_quantity = stock_quantity + OLD.quantity - NEW.quantity
        WHERE item_id = OLD.item_id;
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
        SET stock_quantity = stock_quantity - OLD.quantity
        WHERE item_id = OLD.item_id;
    ELSEIF OLD.movement_type = 'OUT' THEN
        UPDATE items
        SET stock_quantity = stock_quantity + OLD.quantity
        WHERE item_id = OLD.item_id;
    END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Temporary view structure for view `stock_movements_detailed`
--

DROP TABLE IF EXISTS `stock_movements_detailed`;
/*!50001 DROP VIEW IF EXISTS `stock_movements_detailed`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `stock_movements_detailed` AS SELECT 
 1 AS `movement_id`,
 1 AS `item_name`,
 1 AS `movement_type`,
 1 AS `quantity`,
 1 AS `movement_date`,
 1 AS `expiration_date`,
 1 AS `unit_price`*/;
SET character_set_client = @saved_cs_client;

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
) ENGINE=InnoDB AUTO_INCREMENT=325 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stock_movements_history`
--

LOCK TABLES `stock_movements_history` WRITE;
/*!40000 ALTER TABLE `stock_movements_history` DISABLE KEYS */;
INSERT INTO `stock_movements_history` VALUES (9,12,30,'IN',120,NULL,'INSERT','2025-09-27 02:44:59',NULL,120),(10,13,30,'IN',120,NULL,'INSERT','2025-09-27 02:50:13',NULL,120),(11,12,30,'IN',120,NULL,'DELETE','2025-09-27 02:52:01',120,NULL),(12,12,30,'IN',120,NULL,'DELETE','2025-09-27 02:52:01',120,NULL),(13,13,30,'IN',120,NULL,'DELETE','2025-09-27 02:52:06',120,NULL),(14,13,30,'IN',120,NULL,'DELETE','2025-09-27 02:52:06',120,NULL),(15,14,30,'IN',120,NULL,'INSERT','2025-09-27 02:52:50',NULL,120),(16,15,30,'IN',120,NULL,'INSERT','2025-09-27 02:52:54',NULL,120),(17,16,30,'IN',120,NULL,'INSERT','2025-09-27 02:52:56',NULL,120),(18,17,30,'OUT',120,NULL,'INSERT','2025-09-27 02:53:33',NULL,120),(19,16,30,'IN',120,NULL,'DELETE','2025-09-27 02:53:47',120,NULL),(20,16,30,'IN',120,NULL,'DELETE','2025-09-27 02:53:47',120,NULL),(21,14,30,'IN',120,NULL,'DELETE','2025-09-27 02:54:26',120,NULL),(22,17,30,'OUT',120,NULL,'DELETE','2025-09-27 02:54:49',120,NULL),(23,18,30,'IN',120,NULL,'INSERT','2025-09-27 02:55:32',NULL,120),(24,19,30,'IN',10,NULL,'INSERT','2025-09-27 02:58:09',NULL,10),(25,20,30,'IN',120,NULL,'INSERT','2025-09-27 02:58:26',NULL,120),(26,15,30,'IN',120,NULL,'DELETE','2025-09-27 02:58:47',120,NULL),(27,18,30,'IN',120,NULL,'DELETE','2025-09-27 02:58:47',120,NULL),(28,19,30,'IN',10,NULL,'DELETE','2025-09-27 02:58:47',10,NULL),(29,20,30,'IN',120,NULL,'DELETE','2025-09-27 02:58:47',120,NULL),(30,21,30,'IN',120,NULL,'INSERT','2025-09-27 02:58:52',NULL,120),(31,22,30,'IN',120,NULL,'INSERT','2025-09-27 02:58:58',NULL,120),(32,21,30,'IN',120,NULL,'DELETE','2025-09-27 02:59:14',120,NULL),(33,22,30,'IN',120,NULL,'DELETE','2025-09-27 02:59:14',120,NULL),(34,23,30,'IN',120,NULL,'INSERT','2025-09-27 02:59:54',NULL,120),(35,23,30,'IN',120,NULL,'DELETE','2025-09-27 03:00:00',120,NULL),(36,25,30,'IN',120,NULL,'INSERT','2025-09-27 03:07:33',NULL,120),(37,25,30,'IN',120,NULL,'DELETE','2025-09-27 03:07:38',120,NULL),(38,26,30,'IN',120,NULL,'INSERT','2025-09-27 03:09:22',NULL,120),(39,26,30,'IN',120,NULL,'DELETE','2025-09-27 03:09:25',120,NULL),(40,27,30,'IN',120,NULL,'INSERT','2025-09-27 03:10:48',NULL,120),(41,27,30,'IN',120,NULL,'DELETE','2025-09-27 03:10:50',120,NULL),(42,28,30,'IN',120,NULL,'INSERT','2025-09-27 03:15:30',NULL,120),(43,28,30,'IN',120,NULL,'DELETE','2025-09-27 03:15:33',120,NULL),(44,29,30,'IN',120,NULL,'INSERT','2025-09-27 03:19:10',NULL,120),(45,29,30,'IN',120,NULL,'DELETE','2025-09-27 03:19:17',120,NULL),(46,30,30,'IN',120,NULL,'INSERT','2025-09-27 03:20:08',NULL,120),(47,30,30,'IN',120,NULL,'DELETE','2025-09-27 03:20:11',120,NULL),(48,31,30,'IN',120,NULL,'INSERT','2025-09-27 03:25:43',NULL,120),(49,32,30,'IN',120,NULL,'INSERT','2025-09-27 03:25:45',NULL,120),(50,31,30,'IN',120,NULL,'DELETE','2025-09-27 03:25:59',120,NULL),(51,32,30,'IN',120,NULL,'DELETE','2025-09-27 03:26:09',120,NULL),(52,33,30,'IN',120,NULL,'INSERT','2025-09-27 03:27:11',NULL,120),(53,33,30,'IN',120,NULL,'DELETE','2025-09-27 03:27:17',120,NULL),(54,34,30,'IN',120,NULL,'INSERT','2025-09-27 03:39:15',NULL,120),(55,34,30,'IN',120,NULL,'UPDATE','2025-09-27 03:39:32',120,140),(56,34,30,'IN',140,NULL,'DELETE','2025-09-27 03:39:39',140,NULL),(57,35,30,'IN',120,NULL,'INSERT','2025-09-27 03:45:06',NULL,120),(58,35,30,'IN',120,NULL,'DELETE','2025-09-27 03:45:12',120,NULL),(59,36,30,'IN',120,NULL,'INSERT','2025-09-27 03:46:58',NULL,120),(60,36,30,'IN',120,NULL,'DELETE','2025-09-27 03:47:05',120,NULL),(61,37,30,'IN',120,NULL,'INSERT','2025-09-27 03:53:40',NULL,120),(62,37,30,'IN',120,NULL,'DELETE','2025-09-27 03:53:55',120,NULL),(63,38,30,'IN',120,NULL,'INSERT','2025-09-27 03:57:55',NULL,120),(64,38,30,'IN',120,NULL,'DELETE','2025-09-27 03:58:02',120,NULL),(65,39,30,'IN',120,NULL,'INSERT','2025-09-27 03:59:46',NULL,120),(66,39,30,'IN',120,NULL,'DELETE','2025-09-27 03:59:51',120,NULL),(67,40,30,'IN',120,NULL,'INSERT','2025-09-27 04:01:52',NULL,120),(68,41,30,'IN',80,'2026-01-17','INSERT','2025-09-27 04:04:57',NULL,80),(69,42,30,'IN',80,'2026-01-17','INSERT','2025-09-27 04:06:07',NULL,80),(70,42,30,'IN',80,'2026-01-17','DELETE','2025-09-27 04:06:16',80,NULL),(71,40,30,'IN',120,NULL,'DELETE','2025-09-27 04:06:18',120,NULL),(72,41,30,'IN',80,'2026-01-17','DELETE','2025-09-27 04:06:19',80,NULL),(73,43,33,'IN',120,NULL,'INSERT','2025-09-27 04:11:56',NULL,120),(74,44,31,'IN',120,'2025-10-11','INSERT','2025-09-27 04:12:04',NULL,120),(75,43,33,'IN',120,NULL,'DELETE','2025-09-27 04:12:12',120,NULL),(76,44,31,'IN',120,'2025-10-11','DELETE','2025-09-27 04:12:13',120,NULL),(77,45,30,'IN',0,NULL,'INSERT','2025-09-27 04:20:50',NULL,0),(78,46,30,'IN',12,NULL,'INSERT','2025-09-27 04:21:02',NULL,12),(79,47,34,'IN',100,'2025-12-20','INSERT','2025-09-27 04:22:49',NULL,100),(80,48,34,'IN',100,'2025-12-20','INSERT','2025-09-27 04:22:52',NULL,100),(81,49,34,'IN',100,'2025-12-20','INSERT','2025-09-27 04:22:59',NULL,100),(82,50,34,'IN',100,'2025-12-20','INSERT','2025-09-27 04:23:06',NULL,100),(83,51,34,'IN',100,'2025-12-20','INSERT','2025-09-27 04:23:16',NULL,100),(84,51,34,'IN',100,'2025-12-20','DELETE','2025-09-27 04:37:39',100,NULL),(85,50,34,'IN',100,'2025-12-20','DELETE','2025-09-27 04:37:40',100,NULL),(86,49,34,'IN',100,'2025-12-20','DELETE','2025-09-27 04:37:41',100,NULL),(87,48,34,'IN',100,'2025-12-20','DELETE','2025-09-27 04:37:42',100,NULL),(88,47,34,'IN',100,'2025-12-20','DELETE','2025-09-27 04:37:43',100,NULL),(89,46,30,'IN',12,NULL,'DELETE','2025-09-27 04:37:44',12,NULL),(90,45,30,'IN',0,NULL,'DELETE','2025-09-27 04:37:45',0,NULL),(91,52,34,'IN',100,NULL,'INSERT','2025-09-27 04:38:50',NULL,100),(92,52,34,'IN',100,NULL,'DELETE','2025-09-27 04:38:53',100,NULL),(93,53,34,'IN',100,NULL,'INSERT','2025-09-27 04:40:51',NULL,100),(94,54,34,'IN',100,NULL,'INSERT','2025-09-27 04:40:59',NULL,100),(95,55,34,'IN',100,NULL,'INSERT','2025-09-27 04:41:10',NULL,100),(96,56,34,'IN',100,NULL,'INSERT','2025-09-27 04:43:10',NULL,100),(97,57,30,'IN',100,NULL,'INSERT','2025-09-27 04:49:36',NULL,100),(98,58,30,'IN',100,NULL,'INSERT','2025-09-27 04:49:44',NULL,100),(99,59,34,'IN',500,NULL,'INSERT','2025-09-27 04:53:15',NULL,500),(100,60,34,'IN',500,NULL,'INSERT','2025-09-27 04:53:20',NULL,500),(101,60,34,'IN',500,NULL,'DELETE','2025-09-27 04:55:44',500,NULL),(102,59,34,'IN',500,NULL,'DELETE','2025-09-27 04:55:45',500,NULL),(103,53,34,'IN',100,NULL,'DELETE','2025-09-27 04:55:46',100,NULL),(104,58,30,'IN',100,NULL,'DELETE','2025-09-27 04:55:48',100,NULL),(105,54,34,'IN',100,NULL,'DELETE','2025-09-27 04:55:48',100,NULL),(106,55,34,'IN',100,NULL,'DELETE','2025-09-27 04:55:48',100,NULL),(107,56,34,'IN',100,NULL,'DELETE','2025-09-27 04:55:49',100,NULL),(108,57,30,'IN',100,NULL,'DELETE','2025-09-27 04:55:49',100,NULL),(109,61,34,'IN',12,NULL,'INSERT','2025-09-27 04:58:14',NULL,12),(110,62,34,'IN',12,NULL,'INSERT','2025-09-27 04:59:06',NULL,12),(111,63,34,'IN',12,NULL,'INSERT','2025-09-27 04:59:09',NULL,12),(112,61,34,'IN',12,NULL,'DELETE','2025-09-27 04:59:43',12,NULL),(113,62,34,'IN',12,NULL,'DELETE','2025-09-27 04:59:43',12,NULL),(114,63,34,'IN',12,NULL,'DELETE','2025-09-27 04:59:43',12,NULL),(115,64,34,'OUT',2,NULL,'INSERT','2025-09-27 12:59:02',NULL,2),(116,65,30,'OUT',1,NULL,'INSERT','2025-09-27 12:59:02',NULL,1),(117,66,31,'OUT',1,NULL,'INSERT','2025-09-27 12:59:02',NULL,1),(118,67,33,'OUT',4,NULL,'INSERT','2025-09-27 12:59:02',NULL,4),(119,68,32,'OUT',3,NULL,'INSERT','2025-09-27 12:59:02',NULL,3),(120,69,34,'OUT',1,NULL,'INSERT','2025-09-27 13:03:37',NULL,1),(121,70,32,'OUT',2,NULL,'INSERT','2025-09-27 13:03:37',NULL,2),(122,71,33,'OUT',3,NULL,'INSERT','2025-09-27 13:03:37',NULL,3),(123,72,34,'OUT',1,NULL,'INSERT','2025-09-27 13:05:49',NULL,1),(124,73,32,'OUT',2,NULL,'INSERT','2025-09-27 13:05:49',NULL,2),(125,74,33,'OUT',3,NULL,'INSERT','2025-09-27 13:05:49',NULL,3),(126,75,34,'OUT',1,NULL,'INSERT','2025-09-27 13:06:36',NULL,1),(127,76,32,'OUT',2,NULL,'INSERT','2025-09-27 13:06:36',NULL,2),(128,77,34,'OUT',1,NULL,'INSERT','2025-09-27 13:30:06',NULL,1),(129,78,32,'OUT',2,NULL,'INSERT','2025-09-27 13:30:06',NULL,2),(130,79,33,'OUT',3,NULL,'INSERT','2025-09-27 13:30:06',NULL,3),(131,80,34,'OUT',1,NULL,'INSERT','2025-09-27 13:30:56',NULL,1),(132,81,32,'OUT',2,NULL,'INSERT','2025-09-27 13:30:56',NULL,2),(133,82,33,'OUT',3,NULL,'INSERT','2025-09-27 13:30:56',NULL,3),(134,83,34,'OUT',1,NULL,'INSERT','2025-09-27 13:34:33',NULL,1),(135,84,32,'OUT',2,NULL,'INSERT','2025-09-27 13:34:33',NULL,2),(136,85,33,'OUT',3,NULL,'INSERT','2025-09-27 13:34:33',NULL,3),(137,86,34,'OUT',1,NULL,'INSERT','2025-09-27 13:35:02',NULL,1),(138,87,32,'OUT',2,NULL,'INSERT','2025-09-27 13:35:02',NULL,2),(139,88,33,'OUT',3,NULL,'INSERT','2025-09-27 13:35:02',NULL,3),(140,89,34,'OUT',1,NULL,'INSERT','2025-09-27 13:35:36',NULL,1),(141,90,32,'OUT',2,NULL,'INSERT','2025-09-27 13:35:36',NULL,2),(142,91,33,'OUT',3,NULL,'INSERT','2025-09-27 13:35:36',NULL,3),(143,92,34,'OUT',1,NULL,'INSERT','2025-09-27 13:42:30',NULL,1),(144,93,32,'OUT',2,NULL,'INSERT','2025-09-27 13:42:30',NULL,2),(145,94,33,'OUT',3,NULL,'INSERT','2025-09-27 13:42:30',NULL,3),(146,95,34,'OUT',1,NULL,'INSERT','2025-09-27 13:45:11',NULL,1),(147,96,32,'OUT',2,NULL,'INSERT','2025-09-27 13:45:12',NULL,2),(148,97,33,'OUT',3,NULL,'INSERT','2025-09-27 13:45:12',NULL,3),(149,98,34,'OUT',1,NULL,'INSERT','2025-09-27 13:47:49',NULL,1),(150,99,32,'OUT',2,NULL,'INSERT','2025-09-27 13:47:49',NULL,2),(151,100,33,'OUT',3,NULL,'INSERT','2025-09-27 13:47:49',NULL,3),(152,101,34,'OUT',1,NULL,'INSERT','2025-09-27 14:22:54',NULL,1),(153,102,32,'OUT',2,NULL,'INSERT','2025-09-27 14:22:54',NULL,2),(154,103,33,'OUT',3,NULL,'INSERT','2025-09-27 14:22:54',NULL,3),(155,104,34,'OUT',1,NULL,'INSERT','2025-09-27 14:25:45',NULL,1),(156,105,32,'OUT',2,NULL,'INSERT','2025-09-27 14:25:45',NULL,2),(157,106,33,'OUT',3,NULL,'INSERT','2025-09-27 14:25:45',NULL,3),(158,107,34,'OUT',1,NULL,'INSERT','2025-09-27 14:30:24',NULL,1),(159,108,32,'OUT',2,NULL,'INSERT','2025-09-27 14:30:24',NULL,2),(160,109,33,'OUT',3,NULL,'INSERT','2025-09-27 14:30:24',NULL,3),(161,110,34,'OUT',1,NULL,'INSERT','2025-09-27 14:34:49',NULL,1),(162,111,32,'OUT',2,NULL,'INSERT','2025-09-27 14:34:49',NULL,2),(163,112,33,'OUT',3,NULL,'INSERT','2025-09-27 14:34:49',NULL,3),(164,113,34,'OUT',1,NULL,'INSERT','2025-09-27 15:22:08',NULL,1),(165,114,32,'OUT',2,NULL,'INSERT','2025-09-27 15:22:08',NULL,2),(166,115,33,'OUT',3,NULL,'INSERT','2025-09-27 15:22:08',NULL,3),(167,116,34,'OUT',1,NULL,'INSERT','2025-09-27 15:24:31',NULL,1),(168,117,32,'OUT',2,NULL,'INSERT','2025-09-27 15:24:31',NULL,2),(169,118,33,'OUT',3,NULL,'INSERT','2025-09-27 15:24:31',NULL,3),(170,119,34,'OUT',1,NULL,'INSERT','2025-09-27 15:38:52',NULL,1),(171,120,32,'OUT',2,NULL,'INSERT','2025-09-27 15:38:52',NULL,2),(172,121,33,'OUT',3,NULL,'INSERT','2025-09-27 15:38:52',NULL,3),(173,122,34,'OUT',1,NULL,'INSERT','2025-09-27 16:06:32',NULL,1),(174,123,32,'OUT',2,NULL,'INSERT','2025-09-27 16:06:32',NULL,2),(175,124,33,'OUT',3,NULL,'INSERT','2025-09-27 16:06:32',NULL,3),(176,125,34,'OUT',1,NULL,'INSERT','2025-09-27 16:14:36',NULL,1),(177,126,32,'OUT',2,NULL,'INSERT','2025-09-27 16:14:36',NULL,2),(178,127,33,'OUT',3,NULL,'INSERT','2025-09-27 16:14:36',NULL,3),(179,128,34,'OUT',1,NULL,'INSERT','2025-09-27 16:58:29',NULL,1),(180,129,32,'OUT',2,NULL,'INSERT','2025-09-27 16:58:29',NULL,2),(181,130,33,'OUT',3,NULL,'INSERT','2025-09-27 16:58:29',NULL,3),(182,64,34,'OUT',2,NULL,'DELETE','2025-09-27 18:03:09',2,NULL),(183,65,30,'OUT',1,NULL,'DELETE','2025-09-27 18:03:10',1,NULL),(184,66,31,'OUT',1,NULL,'DELETE','2025-09-27 18:03:10',1,NULL),(185,67,33,'OUT',4,NULL,'DELETE','2025-09-27 18:03:10',4,NULL),(186,68,32,'OUT',3,NULL,'DELETE','2025-09-27 18:03:10',3,NULL),(187,69,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:10',1,NULL),(188,70,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:10',2,NULL),(189,71,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:10',3,NULL),(190,72,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:11',1,NULL),(191,73,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:11',2,NULL),(192,74,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:11',3,NULL),(193,75,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:11',1,NULL),(194,76,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:11',2,NULL),(195,77,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:11',1,NULL),(196,78,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:11',2,NULL),(197,79,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:12',3,NULL),(198,80,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:12',1,NULL),(199,81,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:12',2,NULL),(200,82,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:12',3,NULL),(201,83,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:12',1,NULL),(202,84,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:13',2,NULL),(203,85,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:13',3,NULL),(204,86,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:13',1,NULL),(205,87,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:13',2,NULL),(206,88,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:13',3,NULL),(207,89,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:13',1,NULL),(208,90,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:13',2,NULL),(209,91,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:14',3,NULL),(210,92,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:14',1,NULL),(211,93,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:14',2,NULL),(212,94,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:14',3,NULL),(213,95,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:14',1,NULL),(214,97,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:16',3,NULL),(215,96,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:17',2,NULL),(216,98,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:17',1,NULL),(217,99,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:17',2,NULL),(218,100,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:17',3,NULL),(219,101,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:17',1,NULL),(220,102,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:17',2,NULL),(221,103,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:18',3,NULL),(222,104,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:18',1,NULL),(223,105,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:18',2,NULL),(224,106,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:18',3,NULL),(225,107,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:19',1,NULL),(226,108,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:19',2,NULL),(227,109,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:19',3,NULL),(228,110,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:19',1,NULL),(229,111,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:19',2,NULL),(230,112,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:19',3,NULL),(231,113,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:20',1,NULL),(232,114,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:20',2,NULL),(233,115,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:20',3,NULL),(234,116,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:20',1,NULL),(235,117,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:20',2,NULL),(236,118,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:20',3,NULL),(237,119,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:21',1,NULL),(238,120,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:21',2,NULL),(239,121,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:21',3,NULL),(240,122,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:21',1,NULL),(241,123,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:21',2,NULL),(242,124,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:21',3,NULL),(243,125,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:22',1,NULL),(244,126,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:22',2,NULL),(245,127,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:22',3,NULL),(246,128,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:22',1,NULL),(247,129,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:22',2,NULL),(248,130,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:22',3,NULL),(249,131,34,'IN',120,'2025-10-11','INSERT','2025-09-27 18:03:37',NULL,120),(250,132,30,'IN',120,'2025-10-11','INSERT','2025-09-27 18:03:46',NULL,120),(251,133,30,'IN',150,'2025-10-11','INSERT','2025-09-27 18:03:51',NULL,150),(252,134,34,'OUT',1,NULL,'INSERT','2025-09-27 18:34:42',NULL,1),(253,135,30,'OUT',1,NULL,'INSERT','2025-09-27 18:34:42',NULL,1),(254,136,30,'OUT',1,NULL,'INSERT','2025-09-30 10:11:31',NULL,1),(255,137,31,'OUT',2,NULL,'INSERT','2025-09-30 10:11:31',NULL,2),(256,138,31,'OUT',2,NULL,'INSERT','2025-09-30 10:11:31',NULL,2),(257,139,32,'OUT',2,NULL,'INSERT','2025-09-30 10:11:31',NULL,2),(258,140,34,'OUT',2,NULL,'INSERT','2025-09-30 10:11:31',NULL,2),(259,134,34,'OUT',1,NULL,'DELETE','2025-10-01 05:26:10',1,NULL),(260,141,35,'IN',120,NULL,'INSERT','2025-10-01 11:44:03',NULL,120),(261,142,30,'OUT',1,NULL,'INSERT','2025-10-01 13:25:24',NULL,1),(262,143,33,'OUT',2,NULL,'INSERT','2025-10-01 13:25:24',NULL,2),(263,144,34,'OUT',1,NULL,'INSERT','2025-10-01 13:25:24',NULL,1),(264,145,34,'OUT',1,NULL,'INSERT','2025-10-01 13:25:24',NULL,1),(265,146,30,'OUT',1,NULL,'INSERT','2025-10-01 13:25:24',NULL,1),(266,147,34,'OUT',5,NULL,'INSERT','2025-10-01 13:25:24',NULL,5),(267,148,35,'OUT',1,NULL,'INSERT','2025-10-01 13:25:24',NULL,1),(268,149,33,'OUT',1,NULL,'INSERT','2025-10-01 13:25:24',NULL,1),(269,150,34,'OUT',1,NULL,'INSERT','2025-10-01 13:25:24',NULL,1),(270,151,33,'OUT',1,NULL,'INSERT','2025-10-01 13:26:23',NULL,1),(271,152,34,'OUT',1,NULL,'INSERT','2025-10-01 13:26:23',NULL,1),(272,131,34,'IN',120,'2025-10-11','UPDATE','2025-10-01 16:49:35',120,120),(273,131,34,'IN',120,'2025-11-11','UPDATE','2025-10-01 16:52:32',120,120),(274,131,34,'IN',120,'2025-09-11','UPDATE','2025-10-01 16:53:02',120,120),(275,153,34,'OUT',1,NULL,'INSERT','2025-10-08 07:43:47',NULL,1),(276,154,35,'OUT',2,NULL,'INSERT','2025-10-08 07:43:47',NULL,2),(277,155,35,'OUT',1,NULL,'INSERT','2025-10-08 08:29:48',NULL,1),(278,156,30,'OUT',1,NULL,'INSERT','2025-10-08 08:33:24',NULL,1),(279,157,35,'OUT',1,NULL,'INSERT','2025-10-08 08:34:28',NULL,1),(280,158,34,'OUT',1,NULL,'INSERT','2025-10-08 08:36:00',NULL,1),(281,159,31,'OUT',2,NULL,'INSERT','2025-10-08 08:37:23',NULL,2),(282,160,34,'OUT',1,NULL,'INSERT','2025-10-08 08:43:39',NULL,1),(283,161,35,'OUT',1,NULL,'INSERT','2025-10-08 08:43:39',NULL,1),(284,162,34,'OUT',1,NULL,'INSERT','2025-10-08 08:56:49',NULL,1),(285,163,35,'OUT',1,NULL,'INSERT','2025-10-08 08:56:49',NULL,1),(286,164,34,'OUT',2,NULL,'INSERT','2025-10-08 08:57:37',NULL,2),(287,165,30,'OUT',1,NULL,'INSERT','2025-10-08 09:18:38',NULL,1),(288,166,34,'OUT',1,NULL,'INSERT','2025-10-08 09:18:38',NULL,1),(289,167,35,'OUT',1,NULL,'INSERT','2025-10-08 09:27:53',NULL,1),(290,168,34,'OUT',1,NULL,'INSERT','2025-10-08 09:27:53',NULL,1),(291,169,34,'OUT',1,NULL,'INSERT','2025-10-08 09:41:16',NULL,1),(292,170,35,'OUT',1,NULL,'INSERT','2025-10-08 09:41:16',NULL,1),(293,171,34,'OUT',1,NULL,'INSERT','2025-10-08 09:41:45',NULL,1),(294,172,31,'OUT',1,NULL,'INSERT','2025-10-08 09:41:45',NULL,1),(295,173,34,'OUT',1,NULL,'INSERT','2025-10-08 09:46:02',NULL,1),(296,174,35,'OUT',1,NULL,'INSERT','2025-10-08 09:46:02',NULL,1),(297,175,31,'OUT',1,NULL,'INSERT','2025-10-08 09:49:26',NULL,1),(298,176,34,'OUT',1,NULL,'INSERT','2025-10-08 09:49:26',NULL,1),(299,177,34,'OUT',1,NULL,'INSERT','2025-10-08 09:57:34',NULL,1),(300,178,30,'OUT',1,NULL,'INSERT','2025-10-08 10:26:22',NULL,1),(301,179,34,'OUT',1,NULL,'INSERT','2025-10-08 10:26:22',NULL,1),(302,180,35,'OUT',10,NULL,'INSERT','2025-10-08 10:26:22',NULL,10),(303,181,35,'OUT',11,NULL,'INSERT','2025-10-08 10:27:14',NULL,11),(304,182,34,'OUT',10,NULL,'INSERT','2025-10-08 10:27:29',NULL,10),(305,183,32,'OUT',1,NULL,'INSERT','2025-10-08 10:29:56',NULL,1),(306,184,31,'OUT',1,NULL,'INSERT','2025-10-08 10:30:12',NULL,1),(307,185,34,'OUT',1,NULL,'INSERT','2025-10-08 10:30:12',NULL,1),(308,186,30,'OUT',1,NULL,'INSERT','2025-10-08 10:37:25',NULL,1),(309,187,35,'OUT',1,NULL,'INSERT','2025-10-08 10:37:25',NULL,1),(310,188,34,'OUT',1,NULL,'INSERT','2025-10-08 11:00:59',NULL,1),(311,189,32,'OUT',1,NULL,'INSERT','2025-10-08 11:05:42',NULL,1),(312,190,30,'OUT',1,NULL,'INSERT','2025-10-08 11:05:42',NULL,1),(313,191,34,'OUT',1,NULL,'INSERT','2025-10-08 11:07:29',NULL,1),(314,192,35,'OUT',1,NULL,'INSERT','2025-10-08 11:07:29',NULL,1),(315,193,32,'OUT',1,NULL,'INSERT','2025-10-08 12:02:29',NULL,1),(316,194,30,'OUT',1,NULL,'INSERT','2025-10-08 12:02:29',NULL,1),(317,195,33,'OUT',1,NULL,'INSERT','2025-10-08 12:05:09',NULL,1),(318,196,32,'OUT',1,NULL,'INSERT','2025-10-08 12:05:09',NULL,1),(319,197,31,'OUT',1,NULL,'INSERT','2025-10-08 12:05:09',NULL,1),(320,198,30,'OUT',1,NULL,'INSERT','2025-10-08 12:07:44',NULL,1),(321,199,32,'OUT',1,NULL,'INSERT','2025-10-08 12:07:44',NULL,1),(322,200,34,'OUT',1,NULL,'INSERT','2025-10-08 12:07:59',NULL,1),(323,201,31,'OUT',1,NULL,'INSERT','2025-10-08 13:06:00',NULL,1),(324,202,33,'OUT',1,NULL,'INSERT','2025-10-08 13:12:59',NULL,1);
/*!40000 ALTER TABLE `stock_movements_history` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `stock_overview`
--

DROP TABLE IF EXISTS `stock_overview`;
/*!50001 DROP VIEW IF EXISTS `stock_overview`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `stock_overview` AS SELECT 
 1 AS `item_id`,
 1 AS `item_name`,
 1 AS `description`,
 1 AS `category`,
 1 AS `stock_quantity`,
 1 AS `cost_price`,
 1 AS `selling_price`,
 1 AS `created_at`,
 1 AS `updated_at`*/;
SET character_set_client = @saved_cs_client;

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
) ENGINE=InnoDB AUTO_INCREMENT=84 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `system_settings`
--

LOCK TABLES `system_settings` WRITE;
/*!40000 ALTER TABLE `system_settings` DISABLE KEYS */;
INSERT INTO `system_settings` VALUES (21,'default_currency','PHP','Default currency of the system','2025-09-12 17:11:00','2025-09-12 17:11:00'),(22,'currency_symbol','â‚±','Currency symbol for displaying prices','2025-09-12 17:11:00','2025-09-12 17:11:00'),(23,'invoice_prefix','INV','Prefix used when generating invoice numbers','2025-09-12 17:11:00','2025-09-12 17:11:00'),(52,'allow_negative_stock','0',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(53,'low_stock_threshold','10',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(54,'clinic_name','MA. CANDIE PEARL O. BASCOS-VILLENA, MD. FPSO-HNS',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(55,'clinic_address','388 E. Lopez St., Jaro, Iloilo City',NULL,'2025-09-24 08:17:22','2025-10-09 01:04:31'),(56,'clinic_tel','329-1796',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(57,'clinic_mobile','0925-5000149',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(58,'clinic_hours','Monday, Tuesday, Thursday, Friday, Saturday,                                       11:00 AM â€“ 2:00 PM',NULL,'2025-09-24 08:17:22','2025-10-09 07:10:22'),(59,'clinic_affiliations','St. Paulâ€™s Hospital, Iloilo Doctorsâ€™ Hospital, Iloilo Mission Hospital, Western Visayas Medical,Center,WVSU Med Center, Medicus Ambulatory, Metro Iloilo Hospital & Med. Center Inc.',NULL,'2025-09-24 08:17:22','2025-10-09 00:33:49'),(60,'report_header','ENT CLINIC ',NULL,'2025-09-24 08:17:22','2025-10-01 12:03:16'),(61,'report_footer','','MA. CANDIE PEARL O. BASCOS-VILLENA, MD. FPSO-HNS','2025-09-24 08:17:22','2025-10-08 22:41:18'),(62,'date_format','yyyy-MM-dd',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(63,'time_format','hh:mm tt',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(64,'printer_name	','XP-58',NULL,'2025-09-24 08:17:22','2025-10-08 13:47:14'),(65,'markup_percentage','50',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(66,'clinic_subtitle','Fellow, Phil. Society of Otolaryngology, Head & Neck Surgery',NULL,'2025-09-24 08:28:15','2025-09-24 08:53:37'),(67,'clinic_email','cpbascosmd@yahoo.com',NULL,'2025-09-24 08:42:52','2025-10-08 23:26:47'),(68,'license_number','99566','LIC. NO. 99566','2025-09-25 06:27:50','2025-10-08 13:47:14'),(82,'ptr','12345',NULL,'2025-10-08 13:47:14','2025-10-08 22:38:11'),(83,'stwo','54321',NULL,'2025-10-08 13:47:14','2025-10-08 22:41:34');
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
INSERT INTO `user` VALUES (1,'a','a','Receptionistssss','Receptionist'),(2,'d','d','Doctor','Doctor'),(3,'admin','admin','Admin','Admin'),(4,'q','q','q','Receptionist');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

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
  `unit_price` decimal(10,2) NOT NULL,
  `expiration_date` date DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`write_off_id`),
  KEY `item_id` (`item_id`),
  CONSTRAINT `write_off_movements_ibfk_1` FOREIGN KEY (`item_id`) REFERENCES `items` (`item_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `write_off_movements`
--

LOCK TABLES `write_off_movements` WRITE;
/*!40000 ALTER TABLE `write_off_movements` DISABLE KEYS */;
/*!40000 ALTER TABLE `write_off_movements` ENABLE KEYS */;
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
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_write_off_insert` AFTER INSERT ON `write_off_movements` FOR EACH ROW BEGIN
    DECLARE current_stock INT;

    -- 1ï¸âƒ£ Decrease stock quantity in items table
    UPDATE items
    SET stock_quantity = stock_quantity - NEW.quantity
    WHERE item_id = NEW.item_id;

    -- 2ï¸âƒ£ Insert into stock_movements table as OUT
    INSERT INTO stock_movements
    (item_id, movement_type, quantity, unit_price, expiration_date)
    VALUES
    (NEW.item_id, 'WRITE_OFF', NEW.quantity, NEW.unit_price, NEW.expiration_date);

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Temporary view structure for view `write_off_overview`
--

DROP TABLE IF EXISTS `write_off_overview`;
/*!50001 DROP VIEW IF EXISTS `write_off_overview`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `write_off_overview` AS SELECT 
 1 AS `write_off_id`,
 1 AS `item_name`,
 1 AS `category`,
 1 AS `quantity`,
 1 AS `reason`,
 1 AS `unit_price`,
 1 AS `expiration_date`,
 1 AS `created_at`*/;
SET character_set_client = @saved_cs_client;

--
-- Dumping events for database 'ent_clinic_db'
--

--
-- Final view structure for view `billing_overview`
--

/*!50001 DROP VIEW IF EXISTS `billing_overview`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `billing_overview` AS select `b`.`billing_id` AS `billing_id`,`b`.`consultation_id` AS `consultation_id`,`b`.`patient_id` AS `patient_id`,`p`.`full_name` AS `patient_name`,`b`.`fee` AS `fee`,`b`.`discount_percent` AS `discount_percent`,`b`.`discount_amount` AS `discount_amount`,`b`.`total_amount` AS `total_amount`,`b`.`amount_paid` AS `amount_paid`,`b`.`balance` AS `balance`,`b`.`payment_status` AS `payment_status`,`b`.`created_at` AS `created_at`,`b`.`updated_at` AS `updated_at`,`b`.`note` AS `note` from (`billing` `b` join `patients` `p` on((`b`.`patient_id` = `p`.`patient_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `billing_payment_history`
--

/*!50001 DROP VIEW IF EXISTS `billing_payment_history`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `billing_payment_history` AS select `bp`.`payment_id` AS `payment_id`,`b`.`billing_id` AS `billing_id`,`p`.`full_name` AS `patient_name`,`bp`.`payment_date` AS `payment_date`,`bp`.`amount` AS `amount`,`bp`.`balance` AS `balance`,`bp`.`change_due` AS `change_due`,`bp`.`note` AS `note` from ((`billing_payments` `bp` join `billing` `b` on((`bp`.`billing_id` = `b`.`billing_id`))) join `patients` `p` on((`b`.`patient_id` = `p`.`patient_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `billing_report`
--

/*!50001 DROP VIEW IF EXISTS `billing_report`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `billing_report` AS select `b`.`billing_id` AS `billing_id`,`b`.`consultation_id` AS `consultation_id`,`b`.`patient_id` AS `patient_id`,`p`.`full_name` AS `patient_name`,`b`.`fee` AS `fee`,`b`.`discount_percent` AS `discount_percent`,`b`.`discount_amount` AS `discount_amount`,`b`.`total_amount` AS `total_amount`,`b`.`amount_paid` AS `amount_paid`,`b`.`balance` AS `billing_balance`,`b`.`payment_status` AS `payment_status`,`b`.`note` AS `billing_note`,`b`.`created_at` AS `created_at`,`b`.`updated_at` AS `updated_at`,`bp`.`payment_id` AS `payment_id`,`bp`.`payment_date` AS `payment_date`,`bp`.`amount` AS `payment_amount`,`bp`.`balance` AS `payment_balance`,`bp`.`change_due` AS `change_due`,`bp`.`note` AS `payment_note` from ((`billing` `b` left join `billing_payments` `bp` on((`b`.`billing_id` = `bp`.`billing_id`))) left join `patients` `p` on((`b`.`patient_id` = `p`.`patient_id`))) order by `b`.`billing_id`,`bp`.`payment_date` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

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
-- Final view structure for view `consultation_detail`
--

/*!50001 DROP VIEW IF EXISTS `consultation_detail`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `consultation_detail` AS select `c`.`consultation_id` AS `consultation_id`,`p`.`full_name` AS `patient_name`,`c`.`consultation_date` AS `consultation_date`,`c`.`doctor_name` AS `doctor_name`,`c`.`chief_complaint` AS `chief_complaint`,`c`.`history` AS `history`,`c`.`ear_exam` AS `ear_exam`,`c`.`nose_exam` AS `nose_exam`,`c`.`throat_exam` AS `throat_exam`,`c`.`diagnosis` AS `diagnosis`,`c`.`recommendations` AS `recommendations`,`c`.`notes` AS `notes`,`c`.`follow_up_date` AS `follow_up_date`,`c`.`follow_up_notes` AS `follow_up_notes` from (`consultation` `c` join `patients` `p` on((`c`.`patient_id` = `p`.`patient_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `dispense_history`
--

/*!50001 DROP VIEW IF EXISTS `dispense_history`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `dispense_history` AS select `dp`.`dispense_id` AS `dispense_id`,`dp`.`patient_id` AS `patient_id`,`p`.`full_name` AS `patient_name`,`dp`.`item_id` AS `item_id`,`i`.`item_name` AS `item_name`,`i`.`description` AS `description`,`i`.`category` AS `category`,`dp`.`quantity` AS `quantity`,`dp`.`invoice_item_id` AS `invoice_item_id`,`dp`.`dispensed_at` AS `dispensed_at`,`dp`.`note` AS `note` from ((`dispense_prescription` `dp` join `patients` `p` on((`dp`.`patient_id` = `p`.`patient_id`))) join `items` `i` on((`dp`.`item_id` = `i`.`item_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `expired_items`
--

/*!50001 DROP VIEW IF EXISTS `expired_items`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `expired_items` AS select `m`.`movement_id` AS `movement_id`,`i`.`item_name` AS `item_name`,`i`.`category` AS `category`,`i`.`description` AS `description`,`m`.`quantity` AS `quantity`,`m`.`expiration_date` AS `expiration_date` from (`stock_movements` `m` join `items` `i` on((`m`.`item_id` = `i`.`item_id`))) where (`m`.`expiration_date` <= curdate()) order by `m`.`expiration_date` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `expiry_report`
--

/*!50001 DROP VIEW IF EXISTS `expiry_report`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `expiry_report` AS select `m`.`movement_id` AS `movement_id`,`m`.`expiration_date` AS `expiration_date`,`i`.`item_name` AS `item_name`,`i`.`description` AS `description`,`i`.`category` AS `category`,`m`.`quantity` AS `quantity`,(case when ((`m`.`expiration_date` >= curdate()) and ((to_days(`m`.`expiration_date`) - to_days(curdate())) <= 30)) then concat('Expires in ',(to_days(`m`.`expiration_date`) - to_days(curdate())),' days') when ((`m`.`expiration_date` < curdate()) and ((to_days(curdate()) - to_days(`m`.`expiration_date`)) <= 30)) then concat('Expired ',(to_days(curdate()) - to_days(`m`.`expiration_date`)),' days ago') else NULL end) AS `note` from (`stock_movements` `m` join `items` `i` on((`m`.`item_id` = `i`.`item_id`))) where (((`m`.`expiration_date` >= curdate()) and ((to_days(`m`.`expiration_date`) - to_days(curdate())) <= 30)) or ((`m`.`expiration_date` < curdate()) and ((to_days(curdate()) - to_days(`m`.`expiration_date`)) <= 30))) order by `m`.`expiration_date` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `low_stock_report`
--

/*!50001 DROP VIEW IF EXISTS `low_stock_report`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `low_stock_report` AS select `i`.`item_id` AS `item_id`,`i`.`item_name` AS `item_name`,`i`.`category` AS `category`,`i`.`stock_quantity` AS `stock_quantity`,`i`.`cost_price` AS `cost_price`,`i`.`selling_price` AS `selling_price` from (`items` `i` join `system_settings` `s`) where ((`s`.`setting_key` = 'low_stock_threshold') and (`i`.`stock_quantity` <= cast(`s`.`setting_value` as unsigned))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `patient_lab_requests`
--

/*!50001 DROP VIEW IF EXISTS `patient_lab_requests`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `patient_lab_requests` AS select `lr`.`request_id` AS `request_id`,`p`.`full_name` AS `patient_name`,`lr`.`request_date` AS `request_date`,`lr`.`test_ids` AS `test_ids`,`c`.`consultation_date` AS `consultation_date` from ((`lab_requests` `lr` join `patients` `p` on((`lr`.`patient_id` = `p`.`patient_id`))) left join `consultation` `c` on((`lr`.`consultation_id` = `c`.`consultation_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `patient_summary`
--

/*!50001 DROP VIEW IF EXISTS `patient_summary`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `patient_summary` AS select `p`.`patient_id` AS `patient_id`,`p`.`full_name` AS `full_name`,`p`.`birth_date` AS `birth_date`,`p`.`age` AS `age`,`p`.`sex` AS `sex`,`p`.`civil_status` AS `civil_status`,`p`.`patient_contact_number` AS `patient_contact_number`,count(`c`.`consultation_id`) AS `total_consultations`,max(`c`.`consultation_date`) AS `last_consultation` from (`patients` `p` left join `consultation` `c` on((`p`.`patient_id` = `c`.`patient_id`))) group by `p`.`patient_id` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `prescriptions_detailed`
--

/*!50001 DROP VIEW IF EXISTS `prescriptions_detailed`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `prescriptions_detailed` AS select `pr`.`prescription_id` AS `prescription_id`,`pr`.`patient_id` AS `patient_id`,`p`.`full_name` AS `patient_name`,`pr`.`item_id` AS `item_id`,`i`.`item_name` AS `item_name`,`pr`.`quantity` AS `quantity`,`pr`.`note` AS `note`,`pr`.`created_at` AS `created_at`,`pr`.`consultation_id` AS `consultation_id` from ((`prescription` `pr` join `patients` `p` on((`pr`.`patient_id` = `p`.`patient_id`))) join `items` `i` on((`pr`.`item_id` = `i`.`item_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `queue_overview`
--

/*!50001 DROP VIEW IF EXISTS `queue_overview`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `queue_overview` AS select `q`.`queue_id` AS `queue_id`,`p`.`full_name` AS `patient_name`,`q`.`queue_number` AS `queue_number`,`q`.`status` AS `status`,`q`.`created_at` AS `created_at`,`q`.`called_at` AS `called_at`,`q`.`finished_at` AS `finished_at` from (`queue` `q` join `patients` `p` on((`q`.`patient_id` = `p`.`patient_id`))) order by `q`.`created_at` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `returns_overview`
--

/*!50001 DROP VIEW IF EXISTS `returns_overview`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `returns_overview` AS select `r`.`return_id` AS `return_id`,`i`.`item_name` AS `item_name`,`i`.`category` AS `category`,`r`.`quantity` AS `quantity`,`r`.`reason` AS `reason`,`r`.`return_date` AS `return_date` from (`returns` `r` join `items` `i` on((`r`.`item_id` = `i`.`item_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `revenue_report`
--

/*!50001 DROP VIEW IF EXISTS `revenue_report`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `revenue_report` AS select cast(`b`.`created_at` as date) AS `revenue_date`,'Billing' AS `revenue_type`,sum(`b`.`amount_paid`) AS `revenue_amount` from `billing` `b` where (`b`.`payment_status` in ('FULLY PAID','PARTIALLY PAID')) group by cast(`b`.`created_at` as date) union all select cast(`i`.`invoice_date` as date) AS `revenue_date`,'Sales' AS `revenue_type`,sum(`i`.`amount_received`) AS `revenue_amount` from `invoices` `i` group by cast(`i`.`invoice_date` as date) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `sales_summary`
--

/*!50001 DROP VIEW IF EXISTS `sales_summary`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `sales_summary` AS select `i`.`invoice_id` AS `invoice_id`,`i`.`customer_name` AS `customer_name`,`i`.`invoice_date` AS `invoice_date`,`i`.`invoice_type` AS `invoice_type`,`i`.`subtotal` AS `invoice_subtotal`,`i`.`discount_percent` AS `discount_percent`,`i`.`discount_amount` AS `invoice_discount`,`i`.`net_total` AS `invoice_net_total`,`i`.`amount_received` AS `amount_received`,`i`.`change_due` AS `change_due`,`i`.`note` AS `invoice_note`,`it`.`item_id` AS `item_id`,`it`.`item_name` AS `item_name`,`it`.`category` AS `item_category`,`ii`.`quantity` AS `item_quantity`,`ii`.`unit_price` AS `item_unit_price`,`ii`.`total_price` AS `item_total_price` from ((`invoices` `i` join `invoice_items` `ii` on((`i`.`invoice_id` = `ii`.`invoice_id`))) join `items` `it` on((`ii`.`item_id` = `it`.`item_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `sales_view`
--

/*!50001 DROP VIEW IF EXISTS `sales_view`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `sales_view` AS select `i`.`invoice_id` AS `Invoice ID`,`i`.`customer_name` AS `Customer Name`,`i`.`invoice_date` AS `Invoice Date`,`i`.`invoice_type` AS `Invoice Type`,`i`.`subtotal` AS `Invoice Subtotal`,`i`.`discount_percent` AS `Discount Percent`,`i`.`discount_amount` AS `Invoice Discount`,`i`.`net_total` AS `Invoice Net Total`,`i`.`amount_received` AS `Amount Received`,`i`.`change_due` AS `Change Due`,`i`.`note` AS `Note`,`it`.`item_id` AS `Item ID`,`it`.`item_name` AS `Item Name`,`it`.`category` AS `Category`,`ii`.`quantity` AS `Quantity`,`ii`.`unit_price` AS `Unit Price`,(`ii`.`quantity` * `ii`.`unit_price`) AS `Item Subtotal`,round((((`ii`.`quantity` * `ii`.`unit_price`) / nullif(`i`.`subtotal`,0)) * `i`.`discount_amount`),2) AS `Item Discount`,`ii`.`total_price` AS `Item Net Total` from ((`invoices` `i` join `invoice_items` `ii` on((`i`.`invoice_id` = `ii`.`invoice_id`))) join `items` `it` on((`ii`.`item_id` = `it`.`item_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `stock_movements_detailed`
--

/*!50001 DROP VIEW IF EXISTS `stock_movements_detailed`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `stock_movements_detailed` AS select `sm`.`movement_id` AS `movement_id`,`i`.`item_name` AS `item_name`,`sm`.`movement_type` AS `movement_type`,`sm`.`quantity` AS `quantity`,`sm`.`movement_date` AS `movement_date`,`sm`.`expiration_date` AS `expiration_date`,`sm`.`unit_price` AS `unit_price` from (`stock_movements` `sm` join `items` `i` on((`sm`.`item_id` = `i`.`item_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `stock_overview`
--

/*!50001 DROP VIEW IF EXISTS `stock_overview`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `stock_overview` AS select `items`.`item_id` AS `item_id`,`items`.`item_name` AS `item_name`,`items`.`description` AS `description`,`items`.`category` AS `category`,`items`.`stock_quantity` AS `stock_quantity`,`items`.`cost_price` AS `cost_price`,`items`.`selling_price` AS `selling_price`,`items`.`created_at` AS `created_at`,`items`.`updated_at` AS `updated_at` from `items` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `write_off_overview`
--

/*!50001 DROP VIEW IF EXISTS `write_off_overview`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `write_off_overview` AS select `w`.`write_off_id` AS `write_off_id`,`i`.`item_name` AS `item_name`,`i`.`category` AS `category`,`w`.`quantity` AS `quantity`,`w`.`reason` AS `reason`,`w`.`unit_price` AS `unit_price`,`w`.`expiration_date` AS `expiration_date`,`w`.`created_at` AS `created_at` from (`write_off_movements` `w` join `items` `i` on((`w`.`item_id` = `i`.`item_id`))) */;
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

-- Dump completed on 2025-10-09 16:09:11
