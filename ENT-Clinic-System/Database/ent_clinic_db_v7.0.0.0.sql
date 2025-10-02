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
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `attachments`
--

LOCK TABLES `attachments` WRITE;
/*!40000 ALTER TABLE `attachments` DISABLE KEYS */;
INSERT INTO `attachments` VALUES (5,86,2,'C:\\Users\\wenwe\\Documents\\ENT_CLINIC_Attachments\\2\\2025-09-30\\Images\\image_20250930_182041_182111036.png','Image','(no category)','2025-09-30 18:21:11',''),(6,86,2,'C:\\Users\\wenwe\\Documents\\ENT_CLINIC_Attachments\\2\\2025-09-30\\Images\\image_20250930_182043_182111048.png','Image','(no category)','2025-09-30 18:21:11',''),(7,86,2,'C:\\Users\\wenwe\\Documents\\ENT_CLINIC_Attachments\\2\\2025-09-30\\Images\\image_20250930_182044_182111052.png','Image','(no category)','2025-09-30 18:21:11',''),(8,86,2,'C:\\Users\\wenwe\\Documents\\ENT_CLINIC_Attachments\\2\\2025-09-30\\Images\\image_20250930_182045_182111057.png','Image','(no category)','2025-09-30 18:21:11',''),(9,86,2,'C:\\Users\\wenwe\\Documents\\ENT_CLINIC_Attachments\\2\\2025-09-30\\Images\\image_20250930_182045_182111062.png','Image','(no category)','2025-09-30 18:21:11',''),(10,86,2,'C:\\Users\\wenwe\\Documents\\ENT_CLINIC_Attachments\\2\\2025-09-30\\Images\\image_20250930_182046_182111065.png','Image','(no category)','2025-09-30 18:21:11',''),(11,86,2,'C:\\Users\\wenwe\\Documents\\ENT_CLINIC_Attachments\\2\\2025-09-30\\Images\\image_20250930_182049_182111070.png','Image','(no category)','2025-09-30 18:21:11',''),(12,86,2,'C:\\Users\\wenwe\\Documents\\ENT_CLINIC_Attachments\\2\\2025-09-30\\Images\\image_20250930_182051_182111075.png','Image','(no category)','2025-09-30 18:21:11',''),(13,86,2,'C:\\Users\\wenwe\\Documents\\ENT_CLINIC_Attachments\\2\\2025-09-30\\Images\\image_20250930_182053_182111079.png','Image','(no category)','2025-09-30 18:21:11',''),(14,86,2,'C:\\Users\\wenwe\\Documents\\ENT_CLINIC_Attachments\\2\\2025-09-30\\Videos\\video_20250930_182040_182111083.avi','Video','(no category)','2025-09-30 18:21:11',''),(15,96,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-02\\Images\\image_20251002_214721_214735394.png','Image','(no category)','2025-10-02 21:47:35',''),(16,96,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-02\\Images\\image_20251002_214722_214735451.png','Image','(no category)','2025-10-02 21:47:35',''),(17,96,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-02\\Images\\image_20251002_214722_214735515.png','Image','(no category)','2025-10-02 21:47:35',''),(18,96,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-02\\Images\\image_20251002_214723_214735565.png','Image','(no category)','2025-10-02 21:47:35',''),(19,96,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-02\\Images\\image_20251002_214723_214735626.png','Image','(no category)','2025-10-02 21:47:35',''),(20,96,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-02\\Images\\image_20251002_214723_214735687.png','Image','(no category)','2025-10-02 21:47:35',''),(21,96,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-02\\Images\\image_20251002_214724_214735744.png','Image','(no category)','2025-10-02 21:47:35',''),(22,96,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-02\\Images\\image_20251002_214724_214735802.png','Image','(no category)','2025-10-02 21:47:35',''),(23,96,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-02\\Images\\image_20251002_214725_214735852.png','Image','(no category)','2025-10-02 21:47:35',''),(24,96,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-02\\Images\\image_20251002_214725_214735926.png','Image','(no category)','2025-10-02 21:47:35',''),(25,96,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-02\\Images\\image_20251002_214725_214735986.png','Image','(no category)','2025-10-02 21:47:36',''),(26,96,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-02\\Images\\image_20251002_214726_214736055.png','Image','(no category)','2025-10-02 21:47:36',''),(27,96,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-02\\Images\\image_20251002_214726_214736116.png','Image','(no category)','2025-10-02 21:47:36',''),(28,96,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-02\\Images\\image_20251002_214726_214736165.png','Image','(no category)','2025-10-02 21:47:36',''),(29,96,2,'D:\\ENT_CLINIC_Attachments\\2\\2025-10-02\\Videos\\video_20251002_214721_214736246.avi','Video','(no category)','2025-10-02 21:47:36','');
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
) ENGINE=InnoDB AUTO_INCREMENT=206 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `autocomplete_entries`
--

LOCK TABLES `autocomplete_entries` WRITE;
/*!40000 ALTER TABLE `autocomplete_entries` DISABLE KEYS */;
INSERT INTO `autocomplete_entries` VALUES (201,'chief_complaint','HAHA '),(202,'chief_complaint','HEHE '),(203,'chief_complaint','HUHU '),(204,'chief_complaint','HIHI '),(205,'chief_complaint','HMMM ');
/*!40000 ALTER TABLE `autocomplete_entries` ENABLE KEYS */;
UNLOCK TABLES;

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
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `billing`
--

LOCK TABLES `billing` WRITE;
/*!40000 ALTER TABLE `billing` DISABLE KEYS */;
INSERT INTO `billing` VALUES (6,86,700.00,100,700.00,0.00,'Friend','UNPAID','2025-09-30 18:22:20','2025-09-30 18:22:20',0.00,NULL,2),(7,91,700.00,20,140.00,560.00,'Friend','FULLY PAID','2025-10-01 20:46:30','2025-10-01 21:24:23',560.00,0.00,2),(8,92,500.00,10,50.00,450.00,'enemy','UNPAID','2025-10-01 20:52:52','2025-10-01 20:52:52',0.00,NULL,2),(9,93,500.00,100,500.00,0.00,'Friend','UNPAID','2025-10-01 21:00:46','2025-10-01 21:00:46',0.00,NULL,2),(10,94,700.00,100,700.00,0.00,'Friend','UNPAID','2025-10-01 21:06:52','2025-10-01 21:06:52',0.00,NULL,2),(14,95,700.00,100,700.00,0.00,'Friend','FULLY PAID','2025-10-01 21:14:25','2025-10-01 21:14:25',0.00,0.00,990),(15,96,700.00,100,700.00,0.00,'','FULLY PAID','2025-10-02 21:47:52','2025-10-02 21:47:52',0.00,0.00,2),(16,97,500.00,10,50.00,450.00,'sr citizen','UNPAID','2025-10-02 22:33:36','2025-10-02 22:33:36',0.00,NULL,2),(17,100,700.00,0,0.00,700.00,'','UNPAID','2025-10-03 01:08:24','2025-10-03 01:08:24',0.00,NULL,2),(18,101,500.00,20,100.00,400.00,'','UNPAID','2025-10-03 01:11:02','2025-10-03 01:11:02',0.00,NULL,2),(19,102,500.00,20,100.00,400.00,'','UNPAID','2025-10-03 01:12:25','2025-10-03 01:12:25',0.00,NULL,2),(20,103,700.00,20,140.00,560.00,'','UNPAID','2025-10-03 01:17:15','2025-10-03 01:17:15',0.00,NULL,2),(21,104,500.00,20,100.00,400.00,'sr citizen','UNPAID','2025-10-03 01:20:37','2025-10-03 01:20:37',0.00,NULL,2),(22,105,700.00,10,70.00,630.00,'enemy','UNPAID','2025-10-03 01:21:45','2025-10-03 01:21:45',0.00,NULL,2),(23,106,500.00,100,500.00,0.00,'enemy','FULLY PAID','2025-10-03 01:27:00','2025-10-03 01:27:00',0.00,0.00,2),(24,107,500.00,0,0.00,500.00,'enemy','UNPAID','2025-10-03 01:28:48','2025-10-03 01:28:48',0.00,NULL,2),(25,108,500.00,20,100.00,400.00,'sr citizen','UNPAID','2025-10-03 01:33:49','2025-10-03 01:33:49',0.00,NULL,2),(26,109,500.00,0,0.00,500.00,'','UNPAID','2025-10-03 01:34:18','2025-10-03 01:34:18',0.00,NULL,2);
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
INSERT INTO `billing_payments` VALUES (29,7,'2025-10-01 21:21:11',400.00,'',160.00,0.00),(30,7,'2025-10-01 21:24:23',200.00,'last payment',0.00,40.00);
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
  PRIMARY KEY (`consultation_id`),
  KEY `patient_id` (`patient_id`),
  CONSTRAINT `consultation_ibfk_1` FOREIGN KEY (`patient_id`) REFERENCES `patients` (`patient_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=110 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `consultation`
--

LOCK TABLES `consultation` WRITE;
/*!40000 ALTER TABLE `consultation` DISABLE KEYS */;
INSERT INTO `consultation` VALUES (86,2,'Dr. Receptioist','2025-09-30 18:21:11','‚Ä¢ Fever ','‚Ä¢ Surgeries \n‚Ä¢ Travel history ','‚Ä¢ Redness of ear canal \n‚Ä¢ Swelling \n‚Ä¢ Discharge \n‚Ä¢ Fluid behind eardrum \n‚Ä¢ Foreign body presence','‚Ä¢ ','‚Ä¢ ','‚Ä¢ Otitis media ','‚Ä¢ Rest ','worth to hahaha','2026-01-02',NULL,NULL),(87,2,'Dr. Receptioist','2025-10-01 19:47:55','‚Ä¢ ','‚Ä¢ ','‚Ä¢ ','‚Ä¢ ','‚Ä¢ ','‚Ä¢ ','‚Ä¢ ','','2025-10-01',NULL,NULL),(88,2,'Dr. Receptioist','2025-10-01 19:49:06','‚Ä¢ ','‚Ä¢ ','‚Ä¢ ','‚Ä¢ ','‚Ä¢ ','‚Ä¢ ','‚Ä¢ ','','2025-10-01',NULL,NULL),(89,2,'Dr. Receptioist','2025-10-01 19:50:21','‚Ä¢ ','‚Ä¢ ','‚Ä¢ ','‚Ä¢ ','‚Ä¢ ','‚Ä¢ ','‚Ä¢ ','','2025-10-01',NULL,NULL),(91,2,'Dr. Receptioist','2025-10-01 20:46:03','‚Ä¢ Shortness of breath','‚Ä¢ Surgeries','‚Ä¢ Swelling','','','‚Ä¢ Sinusitis','','','2025-10-01',NULL,NULL),(92,2,'Dr. Receptioist','2025-10-01 20:52:09','‚Ä¢ Abdominal pain','','','','','','','','2025-10-01',NULL,NULL),(93,2,'Dr. Receptioist','2025-10-01 21:00:34','','','‚Ä¢ Swelling','','','','','','2025-10-01',NULL,NULL),(94,2,'Dr. Receptioist','2025-10-01 21:06:41','‚Ä¢ Vomiting','','','','','','','','2025-10-01',NULL,NULL),(95,990,'Dr. Receptioist','2025-10-01 21:10:22','','','‚Ä¢ Earwax buildup','‚Ä¢ Septal deviation \n‚Ä¢ Septal deviation','','','','','2025-10-01',NULL,NULL),(96,2,'Dr. Receptionist','2025-10-02 21:47:35','','','','','','','','','2025-10-02',NULL,NULL),(97,2,'Dr. Receptionistssss','2025-10-02 22:33:19','‚Ä¢ Shortness of breath','‚Ä¢ Family medical history','‚Ä¢ Swelling','‚Ä¢ Discharge type','‚Ä¢ White patches','','','','2026-04-10',NULL,NULL),(99,2000,'Dr. Santos','2025-10-03 01:06:14','‚Ä¢ Shortness of breath\n‚Ä¢ Abdominal pain\n‚Ä¢ Vomiting','‚Ä¢ Previous illness\n‚Ä¢ Chronic diseases','‚Ä¢ Redness of ear canal\n‚Ä¢ Discharge','‚Ä¢ Runny nose\n‚Ä¢ Congestion','‚Ä¢ Sore throat\n‚Ä¢ Swelling','‚Ä¢ Common cold\n‚Ä¢ Gastroenteritis','‚Ä¢ Rest\n‚Ä¢ Hydration\n‚Ä¢ Medication as prescribed','‚Ä¢ Patient advised to follow diet\n‚Ä¢ Monitor symptoms',NULL,'‚Ä¢ Follow-up in 1 week\n‚Ä¢ Return if worsens',25),(100,2,'Dr. Receptionistssss','2025-10-03 01:08:17','‚Ä¢ hahaha\n‚Ä¢ heheeh\n‚Ä¢ huhuhu\n‚Ä¢ hihihihi\n‚Ä¢ hohohoh','','','','','','','','2025-10-03',NULL,NULL),(101,2,'Dr. Receptionistssss','2025-10-03 01:10:54','‚Ä¢ haha\n‚Ä¢ hehe\n‚Ä¢ huhu\n‚Ä¢ hihi','','','','','','','','2025-10-03',NULL,NULL),(102,2,'Dr. Receptionistssss','2025-10-03 01:12:18','‚Ä¢ haha\n‚Ä¢ hehe\n‚Ä¢ hihi\n‚Ä¢ hoho\n‚Ä¢ huhu','','','','','','','','2025-10-03',NULL,NULL),(103,2,'Dr. Receptionistssss','2025-10-03 01:17:09','‚Ä¢ haha\n‚Ä¢ hehe \n‚Ä¢ huhu \n‚Ä¢ hoho \n‚Ä¢ hihi','','','','','','','','2025-10-03',NULL,NULL),(104,2,'Dr. Receptionistssss','2025-10-03 01:20:23','‚Ä¢ haha\n‚Ä¢ hehe\n‚Ä¢ huhu\n‚Ä¢ hoho\n‚Ä¢ hihi','','','','','','','','2025-10-03',NULL,NULL),(105,2,'Dr. Receptionistssss','2025-10-03 01:21:39','‚Ä¢ haha \n‚Ä¢ hehe \n‚Ä¢ hihi \n‚Ä¢ huhu \n‚Ä¢','','','','','','','','2025-10-03',NULL,NULL),(106,2,'Dr. Receptionistssss','2025-10-03 01:26:52','‚Ä¢ haha \n‚Ä¢ hehe \n‚Ä¢ huhu \n‚Ä¢ hoho \n‚Ä¢ hihi','','','','','','','','2025-10-03',NULL,NULL),(107,2,'Dr. Receptionistssss','2025-10-03 01:28:41','‚Ä¢ haha\n‚Ä¢ hehe\n‚Ä¢ hihi\n‚Ä¢ hoho\n‚Ä¢ huhu\n‚Ä¢','','','','','','','','2025-10-03',NULL,NULL),(108,2,'Dr. Receptionistssss','2025-10-03 01:33:37','‚Ä¢ HAHA\n‚Ä¢ HEHE\n‚Ä¢ HUHU\n‚Ä¢ HIHI\n‚Ä¢ HMMM','','','','','','','','2025-10-03',NULL,NULL),(109,2,'Dr. Receptionistssss','2025-10-03 01:34:14','‚Ä¢ HAHA \n‚Ä¢ HEHE \n‚Ä¢ HUHU \n‚Ä¢ HMMM','','','','','','','','2025-10-03',NULL,NULL);
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
            IF LEFT(line,2) = '‚Ä¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            -- Ignore invalid lines (empty or only bullet)
            IF line <> '' AND line <> '‚Ä¢' THEN
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
            IF LEFT(line,2) = '‚Ä¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> '‚Ä¢' THEN
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
            IF LEFT(line,2) = '‚Ä¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> '‚Ä¢' THEN
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
            IF LEFT(line,2) = '‚Ä¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> '‚Ä¢' THEN
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
            IF LEFT(line,2) = '‚Ä¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> '‚Ä¢' THEN
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
            IF LEFT(line,2) = '‚Ä¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> '‚Ä¢' THEN
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
            IF LEFT(line,2) = '‚Ä¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> '‚Ä¢' THEN
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
            IF LEFT(line,2) = '‚Ä¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> '‚Ä¢' THEN
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
            IF LEFT(line,2) = '‚Ä¢ ' THEN
                SET line = TRIM(SUBSTRING(line,3));
            END IF;

            IF line <> '' AND line <> '‚Ä¢' THEN
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
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `invoice_items`
--

LOCK TABLES `invoice_items` WRITE;
/*!40000 ALTER TABLE `invoice_items` DISABLE KEYS */;
INSERT INTO `invoice_items` VALUES (1,105,34,1,6.61,6.61,23),(2,105,32,2,12.00,24.00,24),(3,105,33,3,12.31,36.93,25),(4,106,34,1,6.61,6.61,NULL),(5,106,30,1,12.00,12.00,NULL),(6,107,30,1,12.00,12.00,31),(7,107,31,2,15.00,30.00,27),(8,107,31,2,15.00,30.00,33),(9,107,32,2,12.00,24.00,29),(10,107,34,2,6.61,13.22,26),(11,108,30,1,12.00,12.00,28),(12,108,33,2,12.31,24.62,30),(13,108,34,1,6.61,6.61,32),(14,108,34,1,6.61,6.61,34),(15,108,30,1,12.00,12.00,35),(16,108,34,5,6.61,33.05,36),(17,108,35,1,150.00,150.00,37),(18,108,33,1,12.31,12.31,38),(19,108,34,1,6.61,6.61,39),(20,109,33,1,12.31,12.31,NULL),(21,109,34,1,6.61,6.61,NULL);
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
) ENGINE=InnoDB AUTO_INCREMENT=110 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `invoices`
--

LOCK TABLES `invoices` WRITE;
/*!40000 ALTER TABLE `invoices` DISABLE KEYS */;
INSERT INTO `invoices` VALUES (105,'Duzzy D. Buzz Jr.','2025-09-28 00:58:29',67.54,6.75,60.79,100.00,39.21,'ITEMS','Initial invoice!!!','10'),(106,'Walk-in','2025-09-28 02:34:42',18.61,1.86,16.75,20.00,3.25,'ITEMS','','10'),(107,'Duzzy D. Buzz Jr.','2025-09-30 18:11:31',109.22,10.92,98.30,100.00,1.70,'ITEMS','senior citizen','10'),(108,'Duzzy D. Buzz Jr.','2025-10-01 21:25:24',263.81,0.00,263.81,300.00,36.19,'ITEMS','wow','0'),(109,'Walk-in','2025-10-01 21:26:23',18.92,0.00,18.92,20.00,1.08,'ITEMS','qw','0');
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
INSERT INTO `items` VALUES (30,'Paracetamol','100Mg','Medicine',10.00,12.00,266,'2025-09-27 02:44:51','2025-10-01 13:25:24'),(31,'Paracetamol','200Mg','Medicine',12.00,15.00,-4,'2025-09-27 04:09:11','2025-09-30 10:11:31'),(32,'Paracetamol','300Mg','Medicine',12.51,12.00,-2,'2025-09-27 04:11:29','2025-09-30 10:11:31'),(33,'Paracetamol','400Mg','Medicine',12.51,12.31,-4,'2025-09-27 04:11:42','2025-10-01 13:26:23'),(34,'Citirizine','200Mg','Medicine',5.00,6.61,109,'2025-09-27 04:22:34','2025-10-01 13:26:23'),(35,'Ear Buds','Small','Supplies',120.00,150.00,119,'2025-10-01 11:43:50','2025-10-01 13:25:24');
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
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lab_requests`
--

LOCK TABLES `lab_requests` WRITE;
/*!40000 ALTER TABLE `lab_requests` DISABLE KEYS */;
INSERT INTO `lab_requests` VALUES (11,2,'[7, 8, 11, 10, 12, 9, 13, 35, 36, 34, 33, 1, 6, 3, 2, 4, 5, 30, 32, 29, 31, 28, 18, 16, 15, 17, 14, 23, 20, 21, 19, 22, 37, 38, 41, 40, 39, 27, 24, 25, 26]','2025-10-01',92),(12,2,'[22]','2025-10-01',92),(13,2,'[7, 8, 11, 10, 12, 9, 13, 35, 36, 34, 33, 1, 6, 3, 2, 4, 5, 30, 32, 29, 31, 28, 18, 16, 15, 17, 14, 23, 20, 21, 19, 22, 37, 38, 41, 40, 39, 27, 24, 25, 26]','2025-10-01',94),(14,2,'[7, 8, 11, 10, 12, 9, 13, 35, 36, 34, 33, 1, 6, 3, 2, 4, 5, 30, 32, 29, 31, 28, 18, 16, 15, 17, 14, 23, 20, 21, 19, 22, 37, 38, 41, 40, 39, 27, 24, 25, 26]','2025-10-02',97);
/*!40000 ALTER TABLE `lab_requests` ENABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lab_tests`
--

LOCK TABLES `lab_tests` WRITE;
/*!40000 ALTER TABLE `lab_tests` DISABLE KEYS */;
INSERT INTO `lab_tests` VALUES (1,'Hematology','Complete Blood Count (CBC)'),(2,'Hematology','Hemoglobin (Hgb)'),(3,'Hematology','Hematocrit (Hct)'),(4,'Hematology','Platelet Count'),(5,'Hematology','White Blood Cell Count (WBC)s'),(6,'Hematology','Differential Count'),(7,'Biochemistry','Blood Glucose (Fasting)'),(8,'Biochemistry','Blood Glucose (Random)'),(9,'Biochemistry','Liver Function Test (ALT, AST, ALP, Bilirubin)'),(10,'Biochemistry','Kidney Function Test (Creatinine, BUN)'),(11,'Biochemistry','Electrolytes (Sodium, Potassium, Chloride)'),(12,'Biochemistry','Lipid Profile (Cholesterol, Triglycerides, HDL, LDL)'),(13,'Biochemistry','Uric Acid'),(14,'Microbiology','Urine Culture and Sensitivity'),(15,'Microbiology','Stool Culture'),(16,'Microbiology','Sputum Culture'),(17,'Microbiology','Throat Swab Culture'),(18,'Microbiology','Blood Culture'),(19,'Serology','HIV Screening'),(20,'Serology','Hepatitis B Surface Antigen (HBsAg)'),(21,'Serology','Hepatitis C Antibody'),(22,'Serology','Rheumatoid Factor (RF)'),(23,'Serology','Antinuclear Antibody (ANA)'),(24,'Urinalysis','Routine Urinalysis'),(25,'Urinalysis','Urine Protein'),(26,'Urinalysis','Urine Sugar'),(27,'Urinalysis','Microscopic Urine Examination'),(28,'Hormones','Thyroid Stimulating Hormone (TSH)'),(29,'Hormones','Free T4 / Free T3'),(30,'Hormones','Cortisol'),(31,'Hormones','Insulin'),(32,'Hormones','Estradiol / Progesterone / Testosterone'),(33,'Coagulation','Prothrombin Time (PT)'),(34,'Coagulation','International Normalized Ratio (INR)'),(35,'Coagulation','Activated Partial Thromboplastin Time (aPTT)'),(36,'Coagulation','Fibrinogen'),(37,'Special Tests','C-Reactive Protein (CRP)'),(38,'Special Tests','Erythrocyte Sedimentation Rate (ESR)'),(39,'Special Tests','Vitamin D'),(40,'Special Tests','Vitamin B12'),(41,'Special Tests','Iron / Ferritin');
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
) ENGINE=InnoDB AUTO_INCREMENT=2006 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patients`
--

LOCK TABLES `patients` WRITE;
/*!40000 ALTER TABLE `patients` DISABLE KEYS */;
INSERT INTO `patients` VALUES (2,'Xuzzy D. Buzz Jr.','Buntatala Jaro Iloilo City','2007-03-08',18,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09511365191','Marry F. Buzz','09511365191','Spause','2025-09-07 18:15:54',NULL),(990,'Joshuah Suffieldsss','PO Box 100','2017-06-17',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Charmian Feavyour','09171234567','Child','2025-09-07 22:42:42',NULL),(1000,'Winne Earingey','Suite 200','2017-04-20',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Pierson Stainson','09171234567','Spouse','2025-09-07 22:42:12',NULL),(1002,'Corette Coppin','Suite 11','2020-09-12',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lennie Ormshaw','09215678901','Sibling','2025-09-07 22:43:57',NULL),(1003,'Arri Caldera','Tagbac Jaro Iloilo City','2024-08-31',1,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Hazlett O\'Hearn','09171234567','Friend','2025-09-07 22:43:57',NULL),(1004,'Edgardo Ham','Apt 376','2023-05-05',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','John Lindfors','09171234567','Friend','2025-09-07 22:43:57',NULL),(1005,'Yetta Wrathmall','3rd Floor','2021-07-09',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Brett Trevett','09348901234','Child','2025-09-07 22:43:57',NULL),(1006,'Morgun Yakovliv','PO Box 2849','2019-10-22',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Brand Potbury','09348901234','Father','2025-09-07 22:43:57',NULL),(1007,'Shanie Thomazet','PO Box 72324','2015-07-17',10,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Minette Simenel','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1008,'Emily Hankinson','PO Box 68345','2014-11-11',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ingeborg Paraman','09215678901','Sibling','2025-09-07 22:43:57',NULL),(1009,'Eileen Kleinstub','Room 1136','2018-11-22',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Andres Dhenin','09348901234','Friend','2025-09-07 22:43:57',NULL),(1010,'Valerye Jodrellec','Room 1429','2017-07-30',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Payton Borres','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1011,'Lisha Kenelin','Room 881','2017-03-10',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Bartholemy Hubatsch','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1012,'Tory Tharme','Suite 80','2014-10-30',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Hermon Twohig','09215678901','Sibling','2025-09-07 22:43:57',NULL),(1013,'Daniele Bethune','Suite 15','2025-08-19',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Walsh Wilbud','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1014,'Valentin Espie','Room 1847','2018-12-06',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Laverne Cribbins','09348901234','Friend','2025-09-07 22:43:57',NULL),(1015,'Ford Pachmann','1st Floor','2017-04-07',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Isa Kesley','09171234567','Mother','2025-09-07 22:43:57',NULL),(1016,'Xaviera Marc','20th Floor','2015-04-15',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rica Prantl','09348901234','Mother','2025-09-07 22:43:57',NULL),(1017,'Sande Donovan','Apt 999','2024-10-10',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Korella Saleway','09348901234','Child','2025-09-07 22:43:57',NULL),(1018,'Carissa Astman','Apt 980','2017-12-15',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Eliot Domek','09171234567','Friend','2025-09-07 22:43:57',NULL),(1019,'Claudius Clacson','PO Box 2360','2023-02-04',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Collen Bartoli','09215678901','Child','2025-09-07 22:43:57',NULL),(1020,'Harriette Cyples','Suite 18','2025-03-11',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gusti Kidde','09348901234','Child','2025-09-07 22:43:57',NULL),(1021,'Fidole Staresmeare','Room 1943','2019-02-26',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jimmie Billin','09215678901','Sibling','2025-09-07 22:43:57',NULL),(1022,'Torie Wilshaw','3rd Floor','2024-05-28',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rayshell Hucker','09215678901','Father','2025-09-07 22:43:57',NULL),(1023,'Michaeline Haryngton','12th Floor','2019-02-20',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ellis Burling','09348901234','Mother','2025-09-07 22:43:57',NULL),(1024,'Elwira Lehrahan','Suite 15','2022-09-25',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Albert Berford','09171234567','Mother','2025-09-07 22:43:57',NULL),(1025,'Persis Tassell','Apt 775','2020-12-22',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Carole Diego','09348901234','Father','2025-09-07 22:43:57',NULL),(1026,'Daffy Coleford','2nd Floor','2024-12-26',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Morlee Petrushka','09348901234','Father','2025-09-07 22:43:57',NULL),(1027,'Rois Beadham','PO Box 18764','2025-08-20',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Magdaia Jouhandeau','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1028,'Billie Killcross','PO Box 20543','2025-07-31',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Delilah Hurll','09348901234','Mother','2025-09-07 22:43:57',NULL),(1029,'Quentin Bansal','PO Box 84175','2014-12-30',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Darwin Conws','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1030,'Gardy Macauley','Suite 69','2020-01-24',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Sydel Gueny','09171234567','Friend','2025-09-07 22:43:57',NULL),(1031,'Josie Lishmund','Suite 3','2022-03-23',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gussie Gresty','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1032,'Kamilah La Padula','Suite 59','2022-05-23',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sergent Castelin','09215678901','Mother','2025-09-07 22:43:57',NULL),(1033,'Ashlen O\'Fergus','Suite 5','2023-09-04',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jemimah Padell','09171234567','Father','2025-09-07 22:43:57',NULL),(1034,'Sarina Castiglioni','Room 81','2020-11-03',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ermanno Eynald','09348901234','Mother','2025-09-07 22:43:57',NULL),(1035,'Arabel Dumper','13th Floor','2024-11-30',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Justine Lennon','09348901234','Father','2025-09-07 22:43:57',NULL),(1036,'Agnese Farrington','Apt 1880','2016-09-27',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Stanley Fiske','09171234567','Father','2025-09-07 22:43:57',NULL),(1037,'Bertrand Kincla','Apt 1009','2020-11-14',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kara Abrahamsohn','09171234567','Mother','2025-09-07 22:43:57',NULL),(1038,'Joyan Crank','Suite 3','2023-10-16',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kinny Kellard','09348901234','Father','2025-09-07 22:43:57',NULL),(1039,'Thebault Cooksley','18th Floor','2016-02-28',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alejoa Gianetti','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1040,'Julieta Laurant','Suite 9','2015-05-29',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Sarene Ellis','09171234567','Child','2025-09-07 22:43:57',NULL),(1041,'Melvyn Alsopp','Apt 1469','2021-01-13',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Moses De Santos','09215678901','Mother','2025-09-07 22:43:57',NULL),(1042,'Haydon Brownill','6th Floor','2020-10-08',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dickie Yantsev','09348901234','Friend','2025-09-07 22:43:57',NULL),(1043,'Vicki Shemelt','Room 1368','2015-10-15',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Derward Kneale','09171234567','Mother','2025-09-07 22:43:57',NULL),(1044,'Tobin Muddimer','Room 1369','2023-08-14',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Zorana Lindelof','09171234567','Friend','2025-09-07 22:43:57',NULL),(1045,'Pierette Gregol','13th Floor','2016-12-20',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ericka Oakly','09171234567','Friend','2025-09-07 22:43:57',NULL),(1046,'Edithe Tinkler','PO Box 54597','2017-01-26',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Shay Wharfe','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1047,'Berni Banting','PO Box 99889','2019-07-13',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Arie Templeman','09348901234','Child','2025-09-07 22:43:57',NULL),(1048,'Robb Angerstein','PO Box 40523','2020-06-21',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alaine Ebbage','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1049,'Hayley Flinn','7th Floor','2014-10-23',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Florrie Burkett','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1050,'Genvieve Dollin','Apt 129','2015-07-11',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Mauricio Brute','09171234567','Friend','2025-09-07 22:43:57',NULL),(1051,'Faydra Kingston','Apt 987','2023-04-09',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Petey Woodthorpe','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1052,'Grissel Wahlberg','Apt 1310','2016-09-13',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Man Weeke','09215678901','Child','2025-09-07 22:43:57',NULL),(1053,'Brina Ballantine','Suite 99','2022-04-12',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Corella Tythe','09215678901','Friend','2025-09-07 22:43:57',NULL),(1054,'Rockey Hundley','Room 1002','2014-12-10',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Coriss Cudihy','09215678901','Father','2025-09-07 22:43:57',NULL),(1055,'Inesita Wasiel','Apt 1359','2017-06-06',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Rosalynd Inseal','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1056,'Shelba Gegg','Suite 89','2018-01-15',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lita Levis','09348901234','Father','2025-09-07 22:43:57',NULL),(1057,'Briant Leggitt','Apt 138','2020-08-25',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Annnora Hailes','09171234567','Child','2025-09-07 22:43:57',NULL),(1058,'Karl Denisyev','10th Floor','2016-08-29',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Min Brunskill','09215678901','Sibling','2025-09-07 22:43:57',NULL),(1059,'Laurent Coulson','PO Box 39175','2023-11-24',1,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Madelon Piercey','09348901234','Friend','2025-09-07 22:43:57',NULL),(1060,'Abbie Redit','PO Box 44177','2021-04-07',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Fawnia Andreix','09348901234','Mother','2025-09-07 22:43:57',NULL),(1061,'Libbie Anstead','PO Box 72483','2025-01-05',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Nora Ovens','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1062,'Renard Mulles','PO Box 51361','2020-12-01',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Bendicty Lammie','09348901234','Mother','2025-09-07 22:43:57',NULL),(1063,'Imogen Bickerdike','Room 897','2015-03-30',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Grethel Pedrozzi','09348901234','Mother','2025-09-07 22:43:57',NULL),(1064,'Giacopo Beddon','Room 866','2020-02-28',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Tripp Wrightham','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1065,'Lilian Klimontovich','PO Box 50430','2016-08-25',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lynn Gilstin','09348901234','Mother','2025-09-07 22:43:57',NULL),(1066,'Rancell Shelsher','Suite 34','2016-05-07',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Adaline Squibbs','09171234567','Father','2025-09-07 22:43:57',NULL),(1067,'Cybill Ebbotts','5th Floor','2014-10-16',10,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rosmunda Entres','09348901234','Father','2025-09-07 22:43:57',NULL),(1068,'Stepha Peacham','Room 1420','2019-09-19',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Judy Clipston','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1069,'Mona Middle','14th Floor','2023-10-28',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jeanna Houtbie','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1070,'Jervis Dimmick','Room 1813','2021-02-12',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Locke Schukraft','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1071,'Tabb Rame','PO Box 34268','2022-03-19',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Yoshiko Collyns','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1072,'Dannye Escudier','14th Floor','2025-07-07',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Aymer Del Castello','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1073,'Emlen Cunniff','Room 1361','2020-11-01',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Steffen Marielle','09215678901','Mother','2025-09-07 22:43:57',NULL),(1074,'Isidore O\'Lunney','Room 696','2020-12-08',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Genny Riccardini','09215678901','Mother','2025-09-07 22:43:57',NULL),(1075,'Trefor Riddich','Suite 13','2020-11-25',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Francisco Guiot','09348901234','Mother','2025-09-07 22:43:57',NULL),(1076,'Mareah Dunsmuir','20th Floor','2022-03-12',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Barry Harbidge','09171234567','Father','2025-09-07 22:43:57',NULL),(1077,'Maris Pirouet','Apt 1288','2023-12-06',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Abra Sawnwy','09171234567','Mother','2025-09-07 22:43:57',NULL),(1078,'Karrah Faircliffe','PO Box 16121','2018-07-15',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Loella Moyle','09215678901','Child','2025-09-07 22:43:57',NULL),(1079,'Silvana Punshon','PO Box 39910','2020-06-17',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Nora Loughton','09215678901','Friend','2025-09-07 22:43:57',NULL),(1080,'Raleigh Polo','Room 1070','2022-02-27',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Athena Fairey','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1081,'Quintin Tague','PO Box 95438','2023-05-13',2,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ketty Bassindale','09215678901','Father','2025-09-07 22:43:57',NULL),(1082,'Delly Gotliffe','2nd Floor','2021-04-03',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jessie Shackleford','09171234567','Father','2025-09-07 22:43:57',NULL),(1083,'Katha Rigardeau','PO Box 46174','2021-02-17',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Friedrick Blackaby','09215678901','Child','2025-09-07 22:43:57',NULL),(1084,'Artemas Brannon','PO Box 81730','2022-05-04',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Glen Burtenshaw','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1085,'Corina Colt','Apt 1270','2021-04-17',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Brand Peaker','09171234567','Child','2025-09-07 22:43:57',NULL),(1086,'Ambrose Mussettini','Apt 1701','2021-09-18',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Latashia Cleynaert','09171234567','Child','2025-09-07 22:43:57',NULL),(1087,'Virgina Hablet','Suite 42','2021-11-06',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Athena Drewson','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1088,'Israel Lowndsbrough','PO Box 36917','2021-07-03',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Franni Watts','09215678901','Father','2025-09-07 22:43:57',NULL),(1089,'Maisey Persent','Room 1774','2017-12-02',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Frederic Spittall','09215678901','Father','2025-09-07 22:43:57',NULL),(1090,'Bernelle Mohring','PO Box 76725','2020-07-18',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Georgianne Checcucci','09215678901','Father','2025-09-07 22:43:57',NULL),(1091,'Lucais Maxworthy','Suite 49','2020-10-03',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Elvyn Chastney','09215678901','Child','2025-09-07 22:43:57',NULL),(1092,'Katherina Dowson','Room 1007','2016-08-25',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Red Fancet','09215678901','Father','2025-09-07 22:43:57',NULL),(1093,'Sayer Scarre','Suite 26','2025-05-04',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dorey Varnam','09348901234','Friend','2025-09-07 22:43:57',NULL),(1094,'Datha Rakestraw','Suite 49','2019-08-08',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Bryant Kwiek','09215678901','Father','2025-09-07 22:43:57',NULL),(1095,'Raphael Akram','Suite 53','2024-11-27',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Robert Vowell','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1096,'Cornell Mayow','Room 1606','2025-09-02',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Barn Sheraton','09348901234','Friend','2025-09-07 22:43:57',NULL),(1097,'Harmony Jowers','Room 1759','2020-09-02',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Olwen Rohlfing','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1098,'Gregor Boatman','Apt 841','2016-03-23',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Tish Crawley','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1099,'Edd Carnoghan','PO Box 5278','2015-05-14',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Giavani Willoughey','09348901234','Mother','2025-09-07 22:43:57',NULL),(1100,'Darby Grosvener','5th Floor','2016-09-23',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Cathrine Heatley','09171234567','Father','2025-09-07 22:43:57',NULL),(1101,'Olenolin Grafton','PO Box 67682','2021-03-27',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Nelie McGettigan','09171234567','Child','2025-09-07 22:43:57',NULL),(1102,'Waverly Temblett','Suite 85','2021-12-14',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Cordell Sonschein','09215678901','Friend','2025-09-07 22:43:57',NULL),(1103,'Leoine Wylam','Apt 695','2024-04-21',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gino Sainz','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1104,'Raviv Caitlin','PO Box 52071','2016-06-27',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jaye Benasik','09348901234','Friend','2025-09-07 22:43:57',NULL),(1105,'Richard Kemmis','14th Floor','2023-10-20',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Juliane Bretherick','09348901234','Mother','2025-09-07 22:43:57',NULL),(1106,'Deane Dillway','Room 1777','2015-06-24',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Tyrus Simonds','09215678901','Friend','2025-09-07 22:43:57',NULL),(1107,'Novelia McArtan','2nd Floor','2023-11-24',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Faunie Warfield','09171234567','Friend','2025-09-07 22:43:57',NULL),(1108,'Ronny McDugal','Room 1703','2023-12-01',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Pammi Bonsul','09215678901','Father','2025-09-07 22:43:57',NULL),(1109,'Abner Drinkel','Apt 1602','2021-06-04',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Berti Smart','09171234567','Child','2025-09-07 22:43:57',NULL),(1110,'Robinett Paunton','Room 1915','2017-08-26',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Trumann Thying','09348901234','Child','2025-09-07 22:43:57',NULL),(1111,'Bealle Biss','PO Box 65759','2016-07-19',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tanitansy Titheridge','09171234567','Father','2025-09-07 22:43:57',NULL),(1112,'Anet Cahey','4th Floor','2019-01-16',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Bondy Broader','09171234567','Father','2025-09-07 22:43:57',NULL),(1113,'Ree McGillacoell','Room 275','2025-04-14',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Katleen Wilshin','09171234567','Mother','2025-09-07 22:43:57',NULL),(1114,'Rice Blazdell','Room 693','2015-09-07',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Blanche Domokos','09348901234','Friend','2025-09-07 22:43:57',NULL),(1115,'Belia Fawke','PO Box 40083','2019-05-16',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Elizabeth Ellacott','09171234567','Mother','2025-09-07 22:43:57',NULL),(1116,'Ritchie Pearton','PO Box 3842','2021-06-12',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ethel Feragh','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1117,'Morris Fritschmann','10th Floor','2020-03-09',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jaquenetta Meacher','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1118,'Joye Warkup','Room 1443','2016-10-15',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rafe Gibbons','09171234567','Mother','2025-09-07 22:43:57',NULL),(1119,'Cull Gilstoun','8th Floor','2019-08-17',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lief Adderley','09348901234','Father','2025-09-07 22:43:57',NULL),(1120,'Sallie Olle','Room 1332','2018-01-15',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jaye Kinge','09171234567','Child','2025-09-07 22:43:57',NULL),(1121,'Kary Paschek','Room 150','2021-01-31',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Sonja Kennermann','09215678901','Friend','2025-09-07 22:43:57',NULL),(1122,'Gabriel Pitchers','Room 334','2016-05-05',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jammal Scimonelli','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1123,'Leeland Clacson','Apt 855','2024-09-03',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Philip Sineath','09215678901','Friend','2025-09-07 22:43:57',NULL),(1124,'Leslie Edison','PO Box 21944','2017-09-06',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Roseann Elcomb','09215678901','Friend','2025-09-07 22:43:57',NULL),(1125,'Roberta McMackin','Suite 47','2016-08-02',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lothaire Verecker','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1126,'Carissa Harbor','Apt 1272','2025-07-19',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Brade Staden','09171234567','Father','2025-09-07 22:43:57',NULL),(1127,'Barret Goulthorp','Apt 322','2017-01-29',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Toma Titmus','09348901234','Child','2025-09-07 22:43:57',NULL),(1128,'Fairleigh Buss','Room 908','2017-02-21',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Rubetta Illiston','09348901234','Friend','2025-09-07 22:43:57',NULL),(1129,'Darrin Parnby','PO Box 42033','2025-01-10',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Charlotta Vosper','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1130,'Vikky Shee','11th Floor','2015-08-30',10,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Salmon Von Helmholtz','09171234567','Friend','2025-09-07 22:43:57',NULL),(1131,'Gavrielle Agent','Room 1826','2021-07-11',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Mill Dudley','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1132,'Marten Falkner','Room 363','2023-10-04',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Berk Dulling','09215678901','Father','2025-09-07 22:43:57',NULL),(1133,'Ripley Crapper','PO Box 25859','2021-02-02',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Rosaline Cominello','09171234567','Mother','2025-09-07 22:43:57',NULL),(1134,'Dredi Apark','PO Box 1701','2021-07-04',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Juditha Jimeno','09171234567','Friend','2025-09-07 22:43:57',NULL),(1135,'Kamila Rathe','Suite 90','2024-07-30',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Wylma McCallion','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1136,'Nickolaus Van Leijs','PO Box 19123','2025-08-25',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jaquenetta Sings','09348901234','Father','2025-09-07 22:43:57',NULL),(1137,'Carita Andriveaux','19th Floor','2018-02-13',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Bald Bremmer','09215678901','Father','2025-09-07 22:43:57',NULL),(1138,'Reinald Klehn','PO Box 24669','2016-07-08',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tierney Cornfield','09171234567','Father','2025-09-07 22:43:57',NULL),(1139,'Hayden Ciobotaru','Room 393','2016-08-21',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Garik Soutar','09215678901','Father','2025-09-07 22:43:57',NULL),(1140,'Jamie Haken','Apt 601','2015-12-03',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Mark Hiddersley','09215678901','Child','2025-09-07 22:43:57',NULL),(1141,'Jayme Elleray','9th Floor','2021-01-28',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lazarus Ferrario','09348901234','Friend','2025-09-07 22:43:57',NULL),(1142,'Zarah Jouanny','Suite 70','2025-08-09',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Linda Kemish','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1143,'Hazel Janz','20th Floor','2023-06-07',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Etheline Jaram','09171234567','Father','2025-09-07 22:43:57',NULL),(1144,'Magda Mallett','Suite 53','2016-01-07',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Verina Cartwight','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1145,'Cilka Amerighi','Room 427','2015-05-27',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Babbie McLachlan','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1146,'Hube Tearle','PO Box 17504','2021-08-26',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lanita Oxton','09171234567','Child','2025-09-07 22:43:57',NULL),(1147,'Lurline Howgill','18th Floor','2017-01-25',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Pauli Kwiek','09171234567','Mother','2025-09-07 22:43:57',NULL),(1148,'Andy Byrde','Apt 1988','2017-11-25',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Greta Maplethorpe','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1149,'Evelin Shorter','Room 1320','2016-11-13',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Archibaldo Spragge','09348901234','Friend','2025-09-07 22:43:57',NULL),(1150,'Rickey Alexandrou','Suite 45','2022-04-19',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jerrie Peascod','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1151,'Elspeth Massingberd','Room 1805','2025-01-07',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Giffy Pollins','09215678901','Friend','2025-09-07 22:43:57',NULL),(1152,'Cammie Tattershall','PO Box 32065','2015-02-05',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Cello Hasson','09171234567','Friend','2025-09-07 22:43:57',NULL),(1153,'Fredrika Sheering','Suite 56','2014-12-16',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Bendite Beaven','09171234567','Mother','2025-09-07 22:43:57',NULL),(1154,'Liva Barthelmes','PO Box 29692','2020-05-27',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Dell Batchan','09171234567','Father','2025-09-07 22:43:57',NULL),(1155,'Cyrus Edelman','Apt 647','2015-09-08',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hillary McOmish','09215678901','Friend','2025-09-07 22:43:57',NULL),(1156,'Aurore Kelleher','Room 610','2014-09-25',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Blakeley Taw','09171234567','Mother','2025-09-07 22:43:57',NULL),(1157,'Cass McGriffin','PO Box 38858','2017-12-06',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Adriaens Sidebottom','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1158,'Ara Cearley','16th Floor','2015-04-08',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Zackariah Thickins','09215678901','Father','2025-09-07 22:43:57',NULL),(1159,'Celestyna Sinisbury','Room 607','2023-12-12',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Helen-elizabeth Brugsma','09215678901','Sibling','2025-09-07 22:43:57',NULL),(1160,'Spense Estoile','PO Box 69075','2018-02-07',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hort Sinderson','09215678901','Mother','2025-09-07 22:43:57',NULL),(1161,'Aldus Connors','4th Floor','2020-06-06',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Hastie Chennells','09215678901','Mother','2025-09-07 22:43:57',NULL),(1162,'Rolland Kornilyev','Suite 56','2016-10-23',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Danell Garett','09348901234','Child','2025-09-07 22:43:57',NULL),(1163,'Gilli Edler','16th Floor','2016-08-03',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lindsy Carriage','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1164,'Ferrel Kerridge','Room 1117','2024-06-21',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Germain Haw','09348901234','Friend','2025-09-07 22:43:57',NULL),(1165,'Fanya Nowaczyk','3rd Floor','2017-06-28',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Giffard Bouttell','09171234567','Father','2025-09-07 22:43:57',NULL),(1166,'Ibbie Dumbrill','5th Floor','2017-08-17',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Henriette McCorley','09215678901','Child','2025-09-07 22:43:57',NULL),(1167,'Penelope Djordjevic','Suite 85','2020-09-27',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kizzee Edmunds','09348901234','Mother','2025-09-07 22:43:57',NULL),(1168,'Mala Foskett','Apt 1589','2020-10-20',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Arlen Sollom','09215678901','Mother','2025-09-07 22:43:57',NULL),(1169,'Florencia Dugan','20th Floor','2018-05-28',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Koral Leathart','09171234567','Sibling','2025-09-07 22:43:57',NULL),(1170,'Ollie Tripean','Apt 1927','2023-10-17',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Calla Pessel','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1171,'Dorothea De Atta','Suite 88','2023-11-25',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Nikaniki Bakhrushkin','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1172,'Berk Capron','Room 1234','2018-02-13',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lethia Ericssen','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1173,'Dulcie Stanmer','10th Floor','2016-06-27',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Dagmar Shillabear','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1174,'Marty Withers','4th Floor','2019-08-12',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Philippa Bartleman','09171234567','Father','2025-09-07 22:43:57',NULL),(1175,'Mirabella Nys','Apt 917','2017-06-03',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Englebert Siburn','09215678901','Sibling','2025-09-07 22:43:57',NULL),(1176,'Christabel Simonato','Apt 292','2022-04-24',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kristina Capitano','09215678901','Friend','2025-09-07 22:43:57',NULL),(1177,'Poul McLaine','Apt 505','2022-11-07',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Danette Clac','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1178,'Rodina Ambrogioni','Apt 1545','2025-08-23',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Randene McIlwraith','09171234567','Mother','2025-09-07 22:43:57',NULL),(1179,'Kaila Aurelius','PO Box 39768','2023-05-18',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kin Habbal','09215678901','Mother','2025-09-07 22:43:57',NULL),(1180,'Mariana Pearn','PO Box 85350','2025-07-21',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Melisent Rushe','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1181,'Dianemarie Redborn','Apt 3','2019-04-12',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Norri Farris','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1182,'Coletta Dumbar','16th Floor','2020-12-26',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Padgett Dibdale','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1183,'Micky Roche','PO Box 72402','2022-05-06',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Edyth Pughe','09348901234','Mother','2025-09-07 22:43:57',NULL),(1184,'Alina Simonetto','4th Floor','2020-01-27',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Erwin Allison','09215678901','Child','2025-09-07 22:43:57',NULL),(1185,'Irma Lippett','16th Floor','2021-06-18',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lou Daveley','09215678901','Friend','2025-09-07 22:43:57',NULL),(1186,'Ernaline Muehler','Suite 52','2023-09-07',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Dee Gwatkins','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1187,'Chuck Fockes','Apt 106','2022-12-20',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alta Adamides','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1188,'Aurelie Devigne','PO Box 40477','2021-10-16',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Chariot Quainton','09215678901','Friend','2025-09-07 22:43:57',NULL),(1189,'Malvin Vassbender','PO Box 46739','2014-11-29',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Francis Kopps','09348901234','Father','2025-09-07 22:43:57',NULL),(1190,'Iris Menpes','Suite 2','2024-12-29',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Clifford Maliffe','09215678901','Child','2025-09-07 22:43:57',NULL),(1191,'Jada Philimore','Apt 164','2025-04-19',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Frankie Duncan','09348901234','Father','2025-09-07 22:43:57',NULL),(1192,'Caye Abyss','Suite 71','2018-12-15',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Marie-jeanne Backe','09215678901','Friend','2025-09-07 22:43:57',NULL),(1193,'Geri Cowgill','Apt 309','2016-10-16',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jefferey Geroldi','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1194,'Jsandye Warricker','Suite 4','2018-01-27',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Doralynne Kyffin','09171234567','Friend','2025-09-07 22:43:57',NULL),(1195,'Dukie Licciardiello','Room 1954','2019-07-07',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Willow Strettell','09171234567','Friend','2025-09-07 22:43:57',NULL),(1196,'Cary Crack','Apt 1258','2018-12-02',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lavena Terron','09215678901','Child','2025-09-07 22:43:57',NULL),(1197,'Berri Pestricke','Room 660','2015-10-10',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Wandie Formby','09348901234','Child','2025-09-07 22:43:57',NULL),(1198,'Hilde Angier','Apt 707','2016-09-20',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Loutitia Riccio','09171234567','Child','2025-09-07 22:43:57',NULL),(1199,'Shandie Carter','Room 1533','2023-03-10',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Corrie Gonnard','09348901234','Child','2025-09-07 22:43:57',NULL),(1200,'Mela Mollatt','5th Floor','2016-02-07',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kaja Sindle','09171234567','Child','2025-09-07 22:43:57',NULL),(1201,'Rochette Brunsden','Room 488','2019-11-15',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Sheffield Longo','09348901234','Mother','2025-09-07 22:43:57',NULL),(1202,'Tristan Stegel','2nd Floor','2022-10-29',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Fabe Tarbert','09348901234','Father','2025-09-07 22:43:57',NULL),(1203,'Darbie Gynn','Apt 1064','2018-07-15',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ravi Farthin','09348901234','Father','2025-09-07 22:43:57',NULL),(1204,'Lennard Monahan','Room 430','2021-05-15',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Craggie Paz','09171234567','Child','2025-09-07 22:43:57',NULL),(1205,'Suzette Ventum','PO Box 52576','2019-07-01',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Tybalt Dowson','09215678901','Father','2025-09-07 22:43:57',NULL),(1206,'Noe Grahl','Suite 78','2019-10-29',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Olivette Bende','09171234567','Child','2025-09-07 22:43:57',NULL),(1207,'Winifred Bednall','1st Floor','2023-10-17',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Larine Farrey','09171234567','Child','2025-09-07 22:43:57',NULL),(1208,'Inglebert Duggan','Suite 54','2024-03-20',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lilla Lamp','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1209,'Rock Borman','Room 378','2024-03-23',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Keeley Parish','09215678901','Sibling','2025-09-07 22:43:57',NULL),(1210,'Darla Tretter','PO Box 12945','2020-02-11',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ellynn Dugald','09348901234','Child','2025-09-07 22:43:57',NULL),(1211,'Corrine Guidotti','15th Floor','2017-03-16',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Nellie Mollin','09171234567','Child','2025-09-07 22:43:57',NULL),(1212,'Elli Dyzart','Apt 1517','2021-09-10',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Neala Hackworthy','09348901234','Mother','2025-09-07 22:43:57',NULL),(1213,'Vanni Penswick','PO Box 14877','2021-01-09',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Leroi O\'Flaverty','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1214,'Court Dominicacci','Room 871','2021-04-18',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sarajane Skitterel','09348901234','Mother','2025-09-07 22:43:57',NULL),(1215,'Marj Filewood','18th Floor','2022-10-16',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kailey Drabble','09348901234','Friend','2025-09-07 22:43:57',NULL),(1216,'Mirella Whyte','Apt 1574','2024-12-18',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tucky Malone','09171234567','Child','2025-09-07 22:43:57',NULL),(1217,'Noellyn Beaze','PO Box 94009','2023-01-30',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lindsay Rispen','09348901234','Mother','2025-09-07 22:43:57',NULL),(1218,'Roxanne Twidale','Apt 900','2014-12-26',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Shea Jerosch','09215678901','Father','2025-09-07 22:43:57',NULL),(1219,'Valina Dansey','Apt 734','2023-06-20',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Burl Swinford','09215678901','Spouse','2025-09-07 22:43:57',NULL),(1220,'Helen-elizabeth Shortin','Apt 1017','2020-01-30',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Stacy Drains','09215678901','Friend','2025-09-07 22:43:57',NULL),(1221,'Jae Dutton','Suite 6','2017-11-17',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Merna Mewrcik','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1222,'Padraig Checkley','PO Box 28302','2017-03-11',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Mickey Heggie','09171234567','Father','2025-09-07 22:43:57',NULL),(1223,'Maurits Burnsides','PO Box 6343','2015-11-18',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Elia Gellion','09215678901','Friend','2025-09-07 22:43:57',NULL),(1224,'Tedman Confort','PO Box 80548','2020-08-30',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Nat O\'Doghesty','09215678901','Mother','2025-09-07 22:43:57',NULL),(1225,'Blair Coast','Apt 1889','2020-12-14',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Salaidh Penhale','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1226,'Curtice Longmore','8th Floor','2014-09-18',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Benni Syphus','09215678901','Father','2025-09-07 22:43:57',NULL),(1227,'Tamarra Mallam','Apt 726','2023-07-25',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Myranda Sandal','09215678901','Mother','2025-09-07 22:43:57',NULL),(1228,'Piper Sides','Room 1695','2024-08-12',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dena Tremoille','09215678901','Father','2025-09-07 22:43:57',NULL),(1229,'Colan Legges','Suite 45','2018-03-08',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Orella Niblock','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1230,'Emilio Jenicke','PO Box 72205','2020-10-18',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cathe Wolland','09215678901','Friend','2025-09-07 22:43:57',NULL),(1231,'Aloisia Nicholes','3rd Floor','2023-10-20',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Val Treverton','09348901234','Friend','2025-09-07 22:43:57',NULL),(1232,'Dilly Dutton','PO Box 98664','2022-05-13',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Torrey Banbrook','09171234567','Friend','2025-09-07 22:43:57',NULL),(1233,'Queenie O\'Neal','Suite 25','2023-05-23',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Juliane Sandercock','09348901234','Father','2025-09-07 22:43:57',NULL),(1234,'Josephine Davion','Apt 145','2024-05-26',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Clementina Lamyman','09215678901','Child','2025-09-07 22:43:57',NULL),(1235,'Roarke Cheng','Suite 2','2015-01-07',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Leopold Bates','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1236,'Benedetta Dwyer','2nd Floor','2025-08-24',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Talyah Vernall','09215678901','Father','2025-09-07 22:43:57',NULL),(1237,'Josi Rableau','Apt 164','2016-12-10',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Rochester Leroux','09215678901','Father','2025-09-07 22:43:57',NULL),(1238,'Moses Elsdon','PO Box 60139','2023-05-25',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Tedman Kyngdon','09348901234','Child','2025-09-07 22:43:57',NULL),(1239,'Cecil Kubal','7th Floor','2023-10-18',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Nikolaus Hannis','09348901234','Father','2025-09-07 22:43:57',NULL),(1240,'Willamina Rabbage','Room 1638','2015-10-31',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Anastassia Noteyoung','09348901234','Sibling','2025-09-07 22:43:57',NULL),(1241,'Wilhelmine Greneham','Room 509','2019-03-24',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Brandise Goodyer','09348901234','Spouse','2025-09-07 22:43:57',NULL),(1242,'Maurise Virgoe','Room 1030','2018-02-05',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Maddy Ludgrove','09215678901','Sibling','2025-09-07 22:43:57',NULL),(1243,'Consalve Huggen','PO Box 72979','2024-11-14',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Fayina Burghill','09171234567','Child','2025-09-07 22:43:57',NULL),(1244,'Lynette Stutely','Suite 52','2018-12-02',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Bradan Budgen','09171234567','Child','2025-09-07 22:43:57',NULL),(1245,'Cindee Stiegers','Apt 1779','2022-07-18',3,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Darrel Dumini','09171234567','Spouse','2025-09-07 22:43:57',NULL),(1246,'Marian Readshall','PO Box 50801','2024-02-16',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cody Ullrich','09171234567','Child','2025-09-07 22:43:58',NULL),(1247,'Ode Hardwich','5th Floor','2017-06-12',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Roman Eliasson','09348901234','Mother','2025-09-07 22:43:58',NULL),(1248,'Evanne Asbery','Apt 583','2023-11-20',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Mortimer Alessandone','09215678901','Friend','2025-09-07 22:43:58',NULL),(1249,'Clerc Troppmann','15th Floor','2018-11-16',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sula Cadman','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1250,'Yolanthe Mansour','Apt 31','2015-01-05',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Elane Adrien','09215678901','Father','2025-09-07 22:43:58',NULL),(1251,'Jone Morrott','Room 806','2021-01-30',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Maye Mac Geaney','09215678901','Mother','2025-09-07 22:43:58',NULL),(1252,'Vinnie Groucutt','20th Floor','2016-07-02',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Carmela Rylett','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1253,'Skip McDonogh','PO Box 18997','2016-04-23',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Horacio MacPike','09171234567','Mother','2025-09-07 22:43:58',NULL),(1254,'Thaxter Laming','PO Box 41004','2019-01-24',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Saxon McCullock','09215678901','Friend','2025-09-07 22:43:58',NULL),(1255,'Domeniga La Croce','7th Floor','2014-12-05',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tammie Frazer','09215678901','Child','2025-09-07 22:43:58',NULL),(1256,'Halsey Tolumello','Apt 1136','2022-01-21',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ivory Thireau','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1257,'Conny Frenchum','Room 833','2025-05-05',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cleveland Littrick','09171234567','Child','2025-09-07 22:43:58',NULL),(1258,'Doralynne Frame','Apt 1985','2019-11-17',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Constantine Learmont','09215678901','Friend','2025-09-07 22:43:58',NULL),(1259,'Aida Arpino','Room 1401','2019-05-15',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Agnesse Spittle','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1260,'Humberto Skamell','Room 1225','2022-08-20',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Davey Beeke','09171234567','Friend','2025-09-07 22:43:58',NULL),(1261,'Emmalynn Sreenan','Suite 69','2024-08-01',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ibby Jarman','09171234567','Mother','2025-09-07 22:43:58',NULL),(1262,'Wandis Blaxall','Suite 74','2019-08-19',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Mirella Steanyng','09171234567','Mother','2025-09-07 22:43:58',NULL),(1263,'Hedi Liff','Apt 448','2016-01-16',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ursola Luddy','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1264,'Benton Benoiton','Room 422','2022-07-22',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Florencia Furst','09348901234','Father','2025-09-07 22:43:58',NULL),(1265,'Eloise Meynell','Suite 21','2017-06-29',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Terrijo Southers','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1266,'Edy Macia','Room 1347','2024-09-07',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Remington Meneer','09215678901','Father','2025-09-07 22:43:58',NULL),(1267,'Lefty Heber','2nd Floor','2018-06-23',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ginny Anton','09348901234','Friend','2025-09-07 22:43:58',NULL),(1268,'Mick Gentil','Suite 53','2018-10-20',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ottilie Malter','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1269,'Nadean Birnie','Room 1443','2019-05-14',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Pyotr Jinkins','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1270,'Keely Fasson','Apt 243','2025-03-16',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Warren Kempshall','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1271,'Kay Fleeman','Apt 1268','2017-12-05',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Blondy Belhome','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1272,'Ginni Kingswell','11th Floor','2021-02-17',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Liza Kilkenny','09215678901','Mother','2025-09-07 22:43:58',NULL),(1273,'Randie Domerq','5th Floor','2014-09-20',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Priscella Mersh','09215678901','Friend','2025-09-07 22:43:58',NULL),(1274,'Butch Dollen','PO Box 93786','2022-01-03',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ailene McKean','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1275,'Helga Broinlich','13th Floor','2021-09-02',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lilith Langstaff','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1276,'Trever Shuttlewood','Apt 1182','2025-09-02',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Toby Kleeman','09348901234','Mother','2025-09-07 22:43:58',NULL),(1277,'Pavla Davern','PO Box 53034','2017-01-14',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Thorstein Fillon','09171234567','Friend','2025-09-07 22:43:58',NULL),(1278,'Andeee Nerger','PO Box 60591','2018-01-28',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Farrand Shearme','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1279,'Magdaia Lebbern','Room 3','2015-04-04',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Pavel Muldowney','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1280,'Maddi Dewis','Apt 1687','2018-03-05',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ambrose Collibear','09348901234','Friend','2025-09-07 22:43:58',NULL),(1281,'Sybilla Simioni','Apt 1406','2015-07-29',10,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Betti Langer','09348901234','Father','2025-09-07 22:43:58',NULL),(1282,'Loree Yitzhakov','Apt 1338','2020-03-18',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Bridgette Crasford','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1283,'Bobette Hurcombe','PO Box 9259','2023-08-18',2,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sallyanne Union','09171234567','Friend','2025-09-07 22:43:58',NULL),(1284,'Niels Helling','Suite 49','2021-02-09',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Alejandrina Bruyntjes','09215678901','Friend','2025-09-07 22:43:58',NULL),(1285,'Ramona Spragg','6th Floor','2025-05-31',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Camila Tregear','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1286,'Laurena Kellock','Suite 79','2024-06-07',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Costa Warin','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1287,'Chloris Blagburn','PO Box 53460','2021-12-22',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cheri Phateplace','09171234567','Mother','2025-09-07 22:43:58',NULL),(1288,'Stacy Waind','Suite 13','2022-05-08',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Matthus Simonetti','09215678901','Friend','2025-09-07 22:43:58',NULL),(1289,'Leora Vasyutichev','Apt 1223','2024-05-12',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Mersey Folder','09215678901','Friend','2025-09-07 22:43:58',NULL),(1290,'Arliene Pinar','Suite 11','2019-03-07',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Patrizia Reddel','09171234567','Father','2025-09-07 22:43:58',NULL),(1291,'Aggi Lethley','PO Box 96507','2016-02-04',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Had Ridpath','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1292,'Dorene Gellier','Suite 2','2024-06-20',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Theodosia Haffard','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1293,'Saw Pointin','Suite 8','2021-12-15',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Salomone Snuggs','09348901234','Child','2025-09-07 22:43:58',NULL),(1294,'Bea Gealle','Suite 57','2021-01-07',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Meredeth Haddeston','09171234567','Child','2025-09-07 22:43:58',NULL),(1295,'Dottie Jehaes','PO Box 79868','2018-10-07',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tabbi Sharrem','09215678901','Father','2025-09-07 22:43:58',NULL),(1296,'Brocky King','Apt 1273','2016-08-03',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lenka Chamberlayne','09348901234','Mother','2025-09-07 22:43:58',NULL),(1297,'Barry Cloney','Room 1949','2024-08-14',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Pip Lawden','09171234567','Child','2025-09-07 22:43:58',NULL),(1298,'Celestia Drillingcourt','PO Box 97130','2018-12-28',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Sianna Iacovolo','09215678901','Father','2025-09-07 22:43:58',NULL),(1299,'Gardener Franckton','17th Floor','2019-07-30',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Devy Grover','09348901234','Child','2025-09-07 22:43:58',NULL),(1300,'Justina Dalley','Apt 1001','2017-12-17',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Alfred Sloley','09215678901','Child','2025-09-07 22:43:58',NULL),(1301,'Orelee Kaesmakers','Room 1354','2015-08-11',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Dane Martinec','09348901234','Child','2025-09-07 22:43:58',NULL),(1302,'Rosene People','Room 1850','2022-02-02',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Reinaldo Arnoud','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1303,'Elizabeth Coot','2nd Floor','2020-07-15',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Pierson Womersley','09348901234','Child','2025-09-07 22:43:58',NULL),(1304,'Raimondo Hanham','Room 1367','2025-05-18',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Barbabas Hambly','09348901234','Friend','2025-09-07 22:43:58',NULL),(1305,'Corenda McMichan','PO Box 10926','2022-05-09',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Demetria Dober','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1306,'Modesty Darrington','Room 1008','2023-03-25',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Halette Botterill','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1307,'Angie Ruppel','Suite 63','2023-06-28',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Sada Ivanets','09171234567','Father','2025-09-07 22:43:58',NULL),(1308,'Carrie Searchfield','Apt 1721','2016-07-13',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Raychel Switsur','09171234567','Child','2025-09-07 22:43:58',NULL),(1309,'Ricky Boorman','Apt 1741','2014-10-11',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jereme Bulfoy','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1310,'Pietro Wong','Suite 17','2021-02-07',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Iorgos Malloch','09348901234','Mother','2025-09-07 22:43:58',NULL),(1311,'Tamarra Culshaw','14th Floor','2023-08-04',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rosalyn Jakes','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1312,'Livvy Birkin','6th Floor','2015-04-26',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Essie Davidovich','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1313,'Blondy Tynewell','Suite 34','2022-01-12',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Opalina Forrestor','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1314,'Lilly Siggin','1st Floor','2020-04-10',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Virge French','09215678901','Friend','2025-09-07 22:43:58',NULL),(1315,'Edeline Kinchlea','13th Floor','2016-11-19',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Tobit Gozzard','09348901234','Father','2025-09-07 22:43:58',NULL),(1316,'Isa Kayne','Suite 49','2024-02-14',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Nikita Brevetor','09215678901','Mother','2025-09-07 22:43:58',NULL),(1317,'Odessa Nisbith','Suite 51','2025-08-05',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rouvin Hulks','09171234567','Father','2025-09-07 22:43:58',NULL),(1318,'Junette Beacom','Room 1967','2016-01-06',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ingeberg Donlon','09171234567','Father','2025-09-07 22:43:58',NULL),(1319,'Ardenia Lunbech','Suite 15','2025-01-15',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Sharona O\'Griffin','09215678901','Child','2025-09-07 22:43:58',NULL),(1320,'Dot Brian','PO Box 20810','2024-05-29',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kris Bartolomucci','09348901234','Friend','2025-09-07 22:43:58',NULL),(1321,'Jemmie Ganniclifft','Room 1872','2015-11-25',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Christyna Chamberlen','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1322,'Tana Degoy','16th Floor','2016-08-28',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Erick Jane','09348901234','Friend','2025-09-07 22:43:58',NULL),(1323,'Francklin Golde','Apt 210','2016-08-25',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Catlin Hakonsson','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1324,'Dasha Fluck','Suite 71','2016-07-31',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Wald Gunningham','09171234567','Friend','2025-09-07 22:43:58',NULL),(1325,'Findlay De Simone','Suite 25','2018-10-27',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Madonna Terney','09171234567','Child','2025-09-07 22:43:58',NULL),(1326,'Delores Toplis','11th Floor','2016-02-20',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ruthanne Dimmock','09348901234','Friend','2025-09-07 22:43:58',NULL),(1327,'Blakeley Dalgardno','Apt 1499','2025-01-27',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ardelis Storrs','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1328,'Jennifer Kelson','Room 420','2015-04-21',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ree McClay','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1329,'Sadella Tonry','15th Floor','2020-02-18',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Odell Minihan','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1330,'Rubie Gingles','PO Box 73867','2018-12-12',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Nalani Credland','09215678901','Friend','2025-09-07 22:43:58',NULL),(1331,'Cheslie Fero','Apt 265','2016-06-06',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Leonardo Matura','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1332,'Adrianna Beldham','Apt 313','2019-10-14',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Andi Jirusek','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1333,'Sydel Luthwood','PO Box 42754','2023-05-13',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Linda Foston','09171234567','Friend','2025-09-07 22:43:58',NULL),(1334,'Daveta Woolam','2nd Floor','2017-08-18',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Fax Gillespie','09171234567','Friend','2025-09-07 22:43:58',NULL),(1335,'Kellen Revely','19th Floor','2016-11-18',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Megen Outhwaite','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1336,'Ursa Kloisner','PO Box 33056','2016-03-17',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Clarabelle Colthurst','09215678901','Child','2025-09-07 22:43:58',NULL),(1337,'Diane Rolf','Suite 59','2016-09-26',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Laetitia Coviello','09215678901','Mother','2025-09-07 22:43:58',NULL),(1338,'Lina Likly','Apt 225','2023-12-29',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kelbee Livingston','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1339,'Melania Crowcher','Room 1446','2015-10-25',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Robb Bromby','09348901234','Father','2025-09-07 22:43:58',NULL),(1340,'Darin Antonelli','Apt 324','2022-03-01',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lucian Boobyer','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1341,'Phylys Grange','Room 1389','2022-01-07',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Julieta Goddard','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1342,'Rickie Lilford','5th Floor','2023-02-17',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Adelind Landrick','09171234567','Father','2025-09-07 22:43:58',NULL),(1343,'Kerri Scay','PO Box 46650','2017-04-13',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Archaimbaud Peerless','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1344,'La verne Benfell','PO Box 34737','2020-05-25',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Irita Hanes','09215678901','Mother','2025-09-07 22:43:58',NULL),(1345,'Roi Biss','Apt 170','2025-03-02',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lorenzo Treske','09215678901','Child','2025-09-07 22:43:58',NULL),(1346,'Addy Heffernan','PO Box 5806','2015-10-03',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Krysta Jandera','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1347,'Marla Andino','Apt 602','2015-10-11',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Camila Ridgers','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1348,'Kirby Dymond','14th Floor','2022-12-09',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Bella Goulthorp','09215678901','Friend','2025-09-07 22:43:58',NULL),(1349,'Doretta Delgadillo','1st Floor','2016-06-14',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Tatiania De Lorenzo','09215678901','Mother','2025-09-07 22:43:58',NULL),(1350,'Gwenni Nelson','Room 1492','2016-03-11',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Elston Becconsall','09171234567','Friend','2025-09-07 22:43:58',NULL),(1351,'Adams Tate','16th Floor','2020-09-23',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Maryjo Corradetti','09348901234','Child','2025-09-07 22:43:58',NULL),(1352,'Steffi Sandhill','Suite 55','2025-04-23',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','August Guillain','09215678901','Child','2025-09-07 22:43:58',NULL),(1353,'Avigdor Edeler','13th Floor','2019-01-25',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Clemmy Grimwade','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1354,'Marie-ann Paffett','Apt 1565','2018-03-24',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Reed Urry','09348901234','Mother','2025-09-07 22:43:58',NULL),(1355,'Galvan Josland','Suite 46','2023-05-15',2,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Harrie Le Friec','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1356,'Karly Balharrie','Apt 733','2017-06-02',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Philippine Kassel','09215678901','Child','2025-09-07 22:43:58',NULL),(1357,'Daryl Garber','PO Box 97991','2020-12-05',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Eldredge Ghiroldi','09171234567','Friend','2025-09-07 22:43:58',NULL),(1358,'Chrissie Piccop','Suite 47','2021-07-26',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Edan Woolbrook','09348901234','Mother','2025-09-07 22:43:58',NULL),(1359,'Vita Jenkyn','Room 671','2017-01-27',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Freddie Loads','09171234567','Child','2025-09-07 22:43:58',NULL),(1360,'Norbie Acreman','PO Box 89754','2024-05-06',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Mill Haston','09171234567','Child','2025-09-07 22:43:58',NULL),(1361,'Thomasine Verryan','Room 1694','2024-08-11',1,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Florina Thornber','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1362,'Nerita Goard','Suite 25','2023-09-23',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Gaven Doret','09348901234','Child','2025-09-07 22:43:58',NULL),(1363,'Judon Conroy','PO Box 67730','2015-11-30',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kathleen Bachura','09215678901','Child','2025-09-07 22:43:58',NULL),(1364,'Madalena Sibery','13th Floor','2025-02-07',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Cozmo Gilbey','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1365,'Ron Restill','Apt 1866','2024-05-22',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Gray Kringe','09171234567','Mother','2025-09-07 22:43:58',NULL),(1366,'Francesca McGuiness','Apt 530','2024-02-20',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Hendrick Hurdiss','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1367,'Avie Minett','Room 491','2018-10-16',6,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kaye Rivilis','09171234567','Mother','2025-09-07 22:43:58',NULL),(1368,'Alex Crowcher','PO Box 53800','2016-12-05',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lin Eves','09348901234','Mother','2025-09-07 22:43:58',NULL),(1369,'Sibeal Samett','Suite 67','2022-08-11',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Antonius Juschke','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1370,'Michelina Baggelley','Apt 1388','2016-07-25',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Isak Guinn','09171234567','Child','2025-09-07 22:43:58',NULL),(1371,'Viviana Stennes','Suite 98','2017-12-12',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ardath Dunnet','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1372,'Gretchen Allridge','Suite 24','2016-10-22',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Wyn Howitt','09171234567','Child','2025-09-07 22:43:58',NULL),(1373,'Leda Nannizzi','Suite 19','2014-11-14',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hildagard Manuel','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1374,'Alec Lamkin','Suite 71','2021-06-19',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ardra Jerdein','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1375,'Salaidh Alvares','Apt 505','2022-09-18',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jeremiah Fluger','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1376,'Kaila Beumant','PO Box 71263','2015-07-11',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Brendon Lecordier','09171234567','Mother','2025-09-07 22:43:58',NULL),(1377,'Had Wilmot','PO Box 67262','2014-12-19',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Bari Bowcock','09171234567','Father','2025-09-07 22:43:58',NULL),(1378,'Loella Dukesbury','13th Floor','2015-11-01',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Tremain Ditter','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1379,'Amara Andrew','Room 1609','2015-06-01',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Chad Kayley','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1380,'Trevar Ahern','PO Box 92981','2022-04-08',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Hesther Addison','09215678901','Mother','2025-09-07 22:43:58',NULL),(1381,'Danny Dyson','Room 345','2016-07-28',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Karlene Juliano','09215678901','Friend','2025-09-07 22:43:58',NULL),(1382,'Devon Fardon','PO Box 94966','2019-04-26',6,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kattie Ould','09348901234','Father','2025-09-07 22:43:58',NULL),(1383,'Sarette Rubenczyk','11th Floor','2018-07-25',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Marty De La Salle','09215678901','Father','2025-09-07 22:43:58',NULL),(1384,'Foss Bizzey','PO Box 53681','2017-12-07',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Clementina L\'Archer','09215678901','Friend','2025-09-07 22:43:58',NULL),(1385,'Juditha Doubleday','PO Box 62635','2016-08-26',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Berty Heard','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1386,'Regine McCaughan','Room 17','2019-07-31',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gui Loton','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1387,'Imelda Ollenbuttel','9th Floor','2016-08-01',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cooper Bartak','09215678901','Father','2025-09-07 22:43:58',NULL),(1388,'Wynn Oglesbee','PO Box 57222','2025-03-21',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jill Toghill','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1389,'Marlie Fielders','Apt 359','2017-04-12',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Derron Baile','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1390,'Lucais Handlin','PO Box 35118','2024-05-01',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kyle Ludovici','09215678901','Father','2025-09-07 22:43:58',NULL),(1391,'Cordy Pellman','Suite 36','2018-10-05',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rosalynd Gurnell','09215678901','Mother','2025-09-07 22:43:58',NULL),(1392,'Bernie Gonnel','Apt 1835','2019-05-08',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Adriane Teek','09171234567','Friend','2025-09-07 22:43:58',NULL),(1393,'Shanna Klaessen','6th Floor','2015-06-20',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Maryrose Gullam','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1394,'Tabby Kippins','PO Box 48347','2020-06-30',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Genny Wheeliker','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1395,'Lazare Ricardo','8th Floor','2021-09-30',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gardner Slyme','09348901234','Mother','2025-09-07 22:43:58',NULL),(1396,'Barclay Keunemann','20th Floor','2021-01-18',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Asher Shiels','09348901234','Mother','2025-09-07 22:43:58',NULL),(1397,'Vere Warmisham','Apt 1445','2024-08-19',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Alta Husband','09348901234','Father','2025-09-07 22:43:58',NULL),(1398,'Callie Plewman','Apt 40','2023-11-25',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Natalina Caine','09348901234','Friend','2025-09-07 22:43:58',NULL),(1399,'Holmes Godspeede','12th Floor','2017-08-12',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Griffith Greetland','09215678901','Mother','2025-09-07 22:43:58',NULL),(1400,'Kit Inchbald','14th Floor','2024-07-22',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Yule Collingworth','09171234567','Child','2025-09-07 22:43:58',NULL),(1401,'Veronica Swithenby','Room 1280','2017-07-17',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Eliza Albisser','09171234567','Child','2025-09-07 22:43:58',NULL),(1402,'Alvie Dumbrall','Suite 21','2014-10-08',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Trever Coles','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1403,'Jedidiah Edworthie','Suite 12','2017-11-24',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dani Miguet','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1404,'Katti Musterd','Room 1655','2015-12-09',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Denny Bercher','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1405,'Kermy Penley','19th Floor','2020-05-18',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Bennie O\'Neil','09215678901','Father','2025-09-07 22:43:58',NULL),(1406,'Jessica Jendrich','Suite 89','2015-05-05',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Hillier Unger','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1407,'Michaelina Zamora','16th Floor','2021-11-18',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Austin Chomicki','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1408,'Parry Tretwell','4th Floor','2016-08-31',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Annabal Slayford','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1409,'Egan Bradock','Apt 1409','2022-05-16',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Amalie O\'Teague','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1410,'Dela Bromige','Room 1892','2021-06-15',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lorie Jockle','09215678901','Father','2025-09-07 22:43:58',NULL),(1411,'Jacob Tubble','PO Box 60555','2024-02-27',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Brandy Snell','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1412,'Ivonne Gregoletti','Suite 20','2025-01-10',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Alex Vanin','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1413,'Dun Impy','Apt 554','2015-04-12',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kristyn Kersaw','09171234567','Mother','2025-09-07 22:43:58',NULL),(1414,'Muire Milkeham','Suite 82','2017-08-15',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rice Kipping','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1415,'Alexandros Sell','Room 601','2015-06-15',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kiele Seavers','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1416,'Corny Flaxman','Apt 1554','2020-10-13',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Alexa Menichi','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1417,'Arlena Beyne','19th Floor','2021-06-01',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alfonso Meatyard','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1418,'Dedra Harradine','PO Box 82610','2022-01-01',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Junette Gladhill','09215678901','Friend','2025-09-07 22:43:58',NULL),(1419,'Worth Hartford','Apt 748','2023-11-08',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sunny Foat','09215678901','Mother','2025-09-07 22:43:58',NULL),(1420,'Reidar Pinner','Suite 95','2021-02-22',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Alano Yegorovnin','09171234567','Mother','2025-09-07 22:43:58',NULL),(1421,'Agathe McAneny','Suite 41','2025-03-23',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Dahlia Bramstom','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1422,'Benedicto Michallat','Suite 95','2015-03-05',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Carmita Tomney','09348901234','Mother','2025-09-07 22:43:58',NULL),(1423,'Harman Lutzmann','Apt 1239','2020-12-21',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Bealle Swynley','09171234567','Father','2025-09-07 22:43:58',NULL),(1424,'Brier Van der Kruijs','5th Floor','2017-10-01',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Inglebert Gimblett','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1425,'Sunny Rafe','PO Box 71926','2021-04-27',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gerick Balderson','09348901234','Friend','2025-09-07 22:43:58',NULL),(1426,'Stefanie Merigon','14th Floor','2021-11-10',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Errol Caulcott','09171234567','Father','2025-09-07 22:43:58',NULL),(1427,'Caleb Grece','Room 1782','2021-01-22',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alayne Casone','09215678901','Friend','2025-09-07 22:43:58',NULL),(1428,'Gerard McIllroy','Room 874','2018-09-23',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Julita Dunaway','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1429,'Idette Mosedall','Apt 702','2016-02-24',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Micheil Styant','09348901234','Child','2025-09-07 22:43:58',NULL),(1430,'Ingmar Keddey','Apt 619','2015-01-18',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lauren Taile','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1431,'Orren Dallosso','Apt 1912','2019-02-07',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Aurora Kibbel','09348901234','Mother','2025-09-07 22:43:58',NULL),(1432,'Perrine Eaglesham','PO Box 13466','2021-10-20',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ardisj Crotty','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1433,'Trev Atteridge','Room 597','2025-02-08',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Briana Sainsberry','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1434,'Adi Coggin','Apt 1076','2021-08-27',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dodie Fencott','09215678901','Child','2025-09-07 22:43:58',NULL),(1435,'Tammie Marguerite','4th Floor','2024-04-13',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Robbi Kinnane','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1436,'Meir Stitch','16th Floor','2022-12-31',2,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Biddie De Pietri','09171234567','Mother','2025-09-07 22:43:58',NULL),(1437,'Wilburt Dunkley','Room 970','2021-06-15',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Trevar Starford','09171234567','Child','2025-09-07 22:43:58',NULL),(1438,'Beatriz Bessent','PO Box 46280','2025-07-07',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alphard Geggie','09171234567','Father','2025-09-07 22:43:58',NULL),(1439,'Sibilla Peplay','Room 402','2020-02-01',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Zaccaria Feathers','09348901234','Mother','2025-09-07 22:43:58',NULL),(1440,'Emelyne Sandey','Apt 1668','2025-08-15',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gannie Larsen','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1441,'Lou Luckwell','Suite 58','2019-09-09',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Nisse Iverson','09215678901','Mother','2025-09-07 22:43:58',NULL),(1442,'Geoff Benedicte','Room 613','2015-09-15',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Any Bein','09348901234','Friend','2025-09-07 22:43:58',NULL),(1443,'Aylmer McCallister','PO Box 9348','2017-05-19',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Briney Rickesies','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1444,'Torry Rimington','Room 320','2020-11-06',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Germain Tong','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1445,'Freddy Salman','PO Box 82278','2021-12-02',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Josy Stapele','09215678901','Child','2025-09-07 22:43:58',NULL),(1446,'Dimitri Greed','Apt 99','2024-12-31',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Zabrina Handrok','09348901234','Child','2025-09-07 22:43:58',NULL),(1447,'Eustace Vurley','15th Floor','2020-06-15',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Megen Babalola','09215678901','Mother','2025-09-07 22:43:58',NULL),(1448,'Any Cicchetto','PO Box 55751','2018-10-28',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Sinclair Unitt','09348901234','Mother','2025-09-07 22:43:58',NULL),(1449,'Christal Kadd','PO Box 13083','2018-10-11',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Brita Corness','09215678901','Child','2025-09-07 22:43:58',NULL),(1450,'Frankie Olufsen','Suite 54','2024-09-13',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Roderick Tolcher','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1451,'Jeddy Whyley','PO Box 19912','2018-05-30',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Anthiathia Fitzgerald','09171234567','Mother','2025-09-07 22:43:58',NULL),(1452,'Natal Hoofe','Apt 305','2017-04-09',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Valaria Oleszczak','09215678901','Mother','2025-09-07 22:43:58',NULL),(1453,'Holli Stoner','Apt 906','2018-02-15',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Giulia Braz','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1454,'Agosto Dametti','PO Box 24798','2018-10-25',6,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cathe Hardiman','09171234567','Father','2025-09-07 22:43:58',NULL),(1455,'Drew Donaldson','Room 767','2018-07-23',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kiel Lopez','09348901234','Child','2025-09-07 22:43:58',NULL),(1456,'Gwyn Misson','Suite 94','2021-12-21',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Gustavus Hawton','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1457,'Damara Youles','11th Floor','2018-10-05',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Klara Gard','09348901234','Father','2025-09-07 22:43:58',NULL),(1458,'Stepha Powling','Apt 918','2023-05-02',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lelia Mifflin','09215678901','Mother','2025-09-07 22:43:58',NULL),(1459,'Doti Scrancher','Apt 1670','2024-04-18',1,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cordy Brower','09215678901','Child','2025-09-07 22:43:58',NULL),(1460,'Matty Hencke','Suite 44','2020-02-15',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Timmy Sherrin','09171234567','Friend','2025-09-07 22:43:58',NULL),(1461,'Emilee Aloshikin','PO Box 49084','2015-03-23',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Briant Quenby','09215678901','Friend','2025-09-07 22:43:58',NULL),(1462,'Marcia Trippack','PO Box 89190','2022-02-22',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Opaline Widmoor','09171234567','Child','2025-09-07 22:43:58',NULL),(1463,'Adrianna Jeves','7th Floor','2019-10-29',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Timmy Jahnel','09171234567','Child','2025-09-07 22:43:58',NULL),(1464,'Tallia Ross','11th Floor','2023-07-02',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Dunn Cornford','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1465,'Lana Maplethorpe','Suite 56','2019-11-26',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Gordon Mawby','09215678901','Father','2025-09-07 22:43:58',NULL),(1466,'Darrel Keigher','Apt 433','2024-01-21',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alane Dunguy','09171234567','Friend','2025-09-07 22:43:58',NULL),(1467,'Melissa Bachmann','Suite 75','2016-03-03',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kevan Buer','09348901234','Father','2025-09-07 22:43:58',NULL),(1468,'Mallory Normavell','PO Box 44789','2020-06-30',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Elvira Jago','09348901234','Child','2025-09-07 22:43:58',NULL),(1469,'Brianne Murton','19th Floor','2015-12-30',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Dori Cayle','09171234567','Friend','2025-09-07 22:43:58',NULL),(1470,'Garald Jakubovicz','Suite 6','2018-03-30',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Matthew Duddan','09171234567','Father','2025-09-07 22:43:58',NULL),(1471,'Illa Slatter','Apt 510','2019-06-18',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Toma Burnet','09215678901','Mother','2025-09-07 22:43:58',NULL),(1472,'Tilda Le Barr','19th Floor','2021-05-30',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Aloisia Ledbury','09171234567','Mother','2025-09-07 22:43:58',NULL),(1473,'Gus Staries','Room 904','2017-12-18',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kip Commusso','09171234567','Mother','2025-09-07 22:43:58',NULL),(1474,'Brandea Ors','Room 560','2020-05-04',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Charmian Zavattiero','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1475,'Ketty Skirven','Room 357','2019-11-27',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Marla Romanet','09348901234','Child','2025-09-07 22:43:58',NULL),(1476,'Eleanore Pechet','Room 1495','2018-03-01',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Dasha Climpson','09348901234','Father','2025-09-07 22:43:58',NULL),(1477,'Lilas Magrane','Apt 863','2022-11-10',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Merlina Cases','09215678901','Mother','2025-09-07 22:43:58',NULL),(1478,'Elwood Cursey','PO Box 64337','2015-08-22',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Gaven Vasilchikov','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1479,'Alejoa Lenox','16th Floor','2021-05-03',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Mireielle Tippler','09215678901','Friend','2025-09-07 22:43:58',NULL),(1480,'Dulcie Botwright','Suite 14','2018-12-18',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Trixy Boler','09215678901','Father','2025-09-07 22:43:58',NULL),(1481,'Julee Scotchmoor','Apt 733','2015-11-23',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rhett Bedinn','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1482,'Pietrek Glasscoe','14th Floor','2022-10-25',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Cathleen Battram','09215678901','Father','2025-09-07 22:43:58',NULL),(1483,'Danice Caulkett','Suite 83','2018-06-08',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Liane Parsley','09348901234','Father','2025-09-07 22:43:58',NULL),(1484,'Philippe Redhead','Room 1073','2018-08-13',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Karney Fewkes','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1485,'Celina Dayer','Apt 239','2018-12-08',6,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Honoria Roddie','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1486,'Timoteo Normanville','Room 650','2021-04-21',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Corissa Satterfitt','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1487,'Wendy Kincla','PO Box 2429','2024-10-13',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Francisco Gildersleeve','09215678901','Mother','2025-09-07 22:43:58',NULL),(1488,'Maressa Wallen','Room 1490','2018-04-08',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Dom Dummigan','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1489,'Misha Piwall','17th Floor','2025-05-28',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Nellie Uwins','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1490,'Marlo Ivakhno','PO Box 35606','2024-06-27',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jesselyn Tivenan','09348901234','Mother','2025-09-07 22:43:58',NULL),(1491,'Aurelea Jandak','Room 1679','2020-06-16',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Horatio Tomczykowski','09215678901','Friend','2025-09-07 22:43:58',NULL),(1492,'Nydia Ewin','Room 1742','2019-12-21',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Clemence Basson','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1493,'Donetta Kenward','8th Floor','2023-07-09',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Trudi McFfaden','09348901234','Mother','2025-09-07 22:43:58',NULL),(1494,'Rivkah Evert','Apt 460','2016-01-31',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Marylynne Carsey','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1495,'Miles Kitto','Apt 1102','2021-09-21',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Timoteo Quinney','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1496,'Netta Kirkhouse','Room 1244','2023-02-02',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Maryann Barwise','09215678901','Father','2025-09-07 22:43:58',NULL),(1497,'Leila Whewill','Suite 25','2023-10-18',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Tine Bolland','09171234567','Child','2025-09-07 22:43:58',NULL),(1498,'Oswald Gronous','14th Floor','2018-04-06',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Fields Lathbury','09171234567','Friend','2025-09-07 22:43:58',NULL),(1499,'Ferrell Belcham','19th Floor','2019-07-28',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Scott Jeanequin','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1500,'Wylie Heber','Room 823','2020-04-16',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ingram Maplethorpe','09171234567','Child','2025-09-07 22:43:58',NULL),(1501,'Arie Liddington','PO Box 78922','2021-05-26',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dionysus Hadkins','09348901234','Child','2025-09-07 22:43:58',NULL),(1502,'Bradney Wandrich','Room 662','2016-10-23',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dirk Verrills','09348901234','Mother','2025-09-07 22:43:58',NULL),(1503,'Dari Frew','PO Box 32767','2024-08-19',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Franciskus Kaplin','09348901234','Father','2025-09-07 22:43:58',NULL),(1504,'Fawnia Winspeare','Apt 491','2021-04-25',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Nancee Bisp','09348901234','Child','2025-09-07 22:43:58',NULL),(1505,'Symon Chsteney','18th Floor','2023-01-27',2,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Anny Daugherty','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1506,'Janene Bottoner','17th Floor','2018-11-02',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Joya Nowakowski','09348901234','Friend','2025-09-07 22:43:58',NULL),(1507,'Had Iron','Suite 78','2018-11-11',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Chlo Pellett','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1508,'Eleen Snap','Room 903','2019-06-24',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Brinn Behr','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1509,'Arnoldo Redsull','Suite 49','2024-01-01',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Karlan Swannick','09171234567','Father','2025-09-07 22:43:58',NULL),(1510,'Erin Copsey','Suite 100','2022-09-03',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Bambi Farebrother','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1511,'Garald Swatradge','Apt 1167','2019-06-27',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Roosevelt Ginity','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1512,'Melodie Laycock','Room 1303','2016-02-20',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Granthem Staden','09215678901','Mother','2025-09-07 22:43:58',NULL),(1513,'Raoul Oxe','PO Box 26911','2016-11-26',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jeffie Rudloff','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1514,'Corene Gumn','PO Box 58750','2019-12-23',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Melva Henri','09171234567','Father','2025-09-07 22:43:58',NULL),(1515,'Rafferty Sherrard','PO Box 74838','2019-04-04',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Grantley MacLise','09348901234','Friend','2025-09-07 22:43:58',NULL),(1516,'Ginny Haversum','Room 1296','2017-09-28',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Valentina Jayme','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1517,'Valentino McMorland','Suite 22','2016-02-29',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Reidar Beahan','09171234567','Child','2025-09-07 22:43:58',NULL),(1518,'Minetta Gorler','Suite 46','2017-03-15',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Pattie Menelaws','09348901234','Mother','2025-09-07 22:43:58',NULL),(1519,'Jonis Pyle','PO Box 54363','2023-07-01',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Leonidas Ridley','09171234567','Child','2025-09-07 22:43:58',NULL),(1520,'Betty Heyfield','PO Box 57254','2017-10-06',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Petr MacAllister','09171234567','Mother','2025-09-07 22:43:58',NULL),(1521,'Damian Luby','Suite 88','2018-12-04',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Crin Lidgley','09215678901','Mother','2025-09-07 22:43:58',NULL),(1522,'Gal Kennifick','PO Box 73082','2018-01-16',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Caria Domini','09215678901','Mother','2025-09-07 22:43:58',NULL),(1523,'Currey McCarrell','PO Box 30595','2014-11-24',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Bourke Sawley','09348901234','Mother','2025-09-07 22:43:58',NULL),(1524,'Chancey Brantzen','Room 1622','2019-01-28',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Russell Fairbrother','09171234567','Mother','2025-09-07 22:43:58',NULL),(1525,'Claretta Tighe','Suite 40','2023-01-27',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Gerrie Hemshall','09215678901','Mother','2025-09-07 22:43:58',NULL),(1526,'Thorin Snowdon','PO Box 89673','2021-04-21',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Aundrea Prue','09215678901','Mother','2025-09-07 22:43:58',NULL),(1527,'Zach Bithany','7th Floor','2015-04-05',10,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Al Pearch','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1528,'Hanna Basden','PO Box 46880','2020-08-21',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Corey Grattan','09171234567','Child','2025-09-07 22:43:58',NULL),(1529,'Laney Ragg','Apt 1647','2016-04-27',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kendre Rama','09215678901','Father','2025-09-07 22:43:58',NULL),(1530,'Brittany Buzzing','Room 1168','2017-09-13',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Umberto Larrosa','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1531,'Sisely Pointer','Room 180','2021-08-19',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jobie McLinden','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1532,'Cristal Cookman','10th Floor','2022-07-13',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Sanders Masseo','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1533,'Torie McCollum','17th Floor','2015-07-19',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Dael Hasley','09348901234','Child','2025-09-07 22:43:58',NULL),(1534,'Tami Smeeton','Room 1421','2020-11-13',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Laurianne Rabjohn','09348901234','Mother','2025-09-07 22:43:58',NULL),(1535,'Charmian Astman','13th Floor','2017-12-31',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Fanny Berkowitz','09171234567','Friend','2025-09-07 22:43:58',NULL),(1536,'Tabbitha Dietz','1st Floor','2016-04-26',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gerrard Cornelius','09171234567','Friend','2025-09-07 22:43:58',NULL),(1537,'Broderick Danielot','20th Floor','2016-01-15',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Marcelo Compson','09348901234','Friend','2025-09-07 22:43:58',NULL),(1538,'Parrnell Leidl','Room 1513','2020-04-03',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Bruce Antwis','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1539,'Jerrome Sauvain','PO Box 91480','2017-02-16',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Joseph O\'Mohun','09348901234','Father','2025-09-07 22:43:58',NULL),(1540,'Kaleb Pell','Suite 56','2021-10-14',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Bell Terbeck','09171234567','Child','2025-09-07 22:43:58',NULL),(1541,'Syd Caswall','Apt 310','2020-02-11',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sarene Charlewood','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1542,'Deloria Gooble','Suite 89','2022-07-21',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Valentia Boother','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1543,'Jemima Littrick','Suite 51','2020-08-28',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Falito Craney','09348901234','Father','2025-09-07 22:43:58',NULL),(1544,'Laurie Shippey','PO Box 17927','2022-03-27',3,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Farris Orr','09348901234','Child','2025-09-07 22:43:58',NULL),(1545,'Tim Gonzalo','Apt 1278','2020-03-03',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Crysta Beardow','09348901234','Friend','2025-09-07 22:43:58',NULL),(1546,'Dina Norville','Apt 1951','2018-02-11',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Prent Vayro','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1547,'Hilario Jarvie','17th Floor','2019-11-06',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jennica Butchart','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1548,'Lothario Sudran','Suite 85','2022-05-08',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Alissa Watting','09215678901','Father','2025-09-07 22:43:58',NULL),(1549,'Angelo Wagner','Room 814','2021-09-11',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Dorthy Keitch','09348901234','Friend','2025-09-07 22:43:58',NULL),(1550,'Quent Twoohy','Suite 5','2024-06-19',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jorgan Stonier','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1551,'Murial Doxsey','14th Floor','2022-06-10',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Stanleigh Dundridge','09171234567','Mother','2025-09-07 22:43:58',NULL),(1552,'Etan Attride','7th Floor','2015-05-26',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Clifford Asty','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1553,'Devlin Worcs','Apt 1881','2016-06-01',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ofelia Garrigan','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1554,'Rosabella Emilien','Room 1741','2025-07-29',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Sorcha Ayer','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1555,'Sybil Swinley','Apt 1545','2024-12-07',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Joella Gumm','09348901234','Child','2025-09-07 22:43:58',NULL),(1556,'Benson Brooksbie','1st Floor','2016-10-28',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Hortensia Conaghy','09348901234','Father','2025-09-07 22:43:58',NULL),(1557,'Levon Trenholm','4th Floor','2018-09-08',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Bertie Scoyne','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1558,'Ludovico Moffat','Room 232','2015-11-04',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Marietta Hounsom','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1559,'Alexandros Willshear','Suite 95','2020-02-20',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lorettalorna Threadgill','09171234567','Mother','2025-09-07 22:43:58',NULL),(1560,'Purcell Grayland','PO Box 39604','2017-06-21',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Carmelia Rivelon','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1561,'Bone Tanswell','Apt 173','2018-08-16',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Allister Morrant','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1562,'Ashlen De Cleyne','Suite 47','2024-05-16',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Allan Decruse','09171234567','Father','2025-09-07 22:43:58',NULL),(1563,'Kath Jeffrey','PO Box 72080','2017-10-04',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ruddie Vasyukov','09215678901','Mother','2025-09-07 22:43:58',NULL),(1564,'Nicky Fewkes','Suite 79','2022-11-21',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Burt Meader','09348901234','Friend','2025-09-07 22:43:58',NULL),(1565,'Pauli Vallintine','Suite 82','2024-10-20',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Charmion Bessey','09348901234','Friend','2025-09-07 22:43:58',NULL),(1566,'Lindy Tebald','20th Floor','2022-09-02',3,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Emmit Kellog','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1567,'Alyda Wastling','Suite 83','2021-10-19',3,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jenda Pagden','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1568,'Andreana Braisby','Apt 1797','2021-03-28',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Pepi Brose','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1569,'Birk Abade','Apt 1141','2023-04-22',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Cary Moralis','09348901234','Friend','2025-09-07 22:43:58',NULL),(1570,'Crissy Strathearn','Room 1968','2020-11-25',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Marianne Ida','09348901234','Father','2025-09-07 22:43:58',NULL),(1571,'Harley Le Surf','Apt 296','2025-02-05',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Torin Wakenshaw','09215678901','Friend','2025-09-07 22:43:58',NULL),(1572,'Iain Chilles','Suite 13','2017-07-09',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Alane Aleksandrev','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1573,'Siffre Glidden','Room 1419','2020-03-22',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Leda Rosendahl','09171234567','Friend','2025-09-07 22:43:58',NULL),(1574,'Grover Rembrandt','Room 335','2016-12-27',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lebbie Eunson','09348901234','Father','2025-09-07 22:43:58',NULL),(1575,'Alistair Hunday','Apt 598','2018-03-25',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ailene Wisden','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1576,'Suzy McCrann','Room 694','2024-01-26',1,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jacky Bilby','09215678901','Child','2025-09-07 22:43:58',NULL),(1577,'Bree Gratrix','Room 1691','2018-08-17',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Doreen Gayther','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1578,'Pepillo Lichfield','Apt 1453','2023-01-21',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Maxie Hartman','09171234567','Mother','2025-09-07 22:43:58',NULL),(1579,'Ritchie Rault','Suite 34','2015-12-14',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Alfreda Sprackling','09348901234','Mother','2025-09-07 22:43:58',NULL),(1580,'Nevile Laste','Suite 29','2020-09-07',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ellissa Stiling','09171234567','Child','2025-09-07 22:43:58',NULL),(1581,'Beau Schott','Room 1137','2016-01-30',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Thorin Dennis','09171234567','Mother','2025-09-07 22:43:58',NULL),(1582,'Adelheid Kubacek','PO Box 31771','2017-09-12',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Sarine Ishchenko','09171234567','Child','2025-09-07 22:43:58',NULL),(1583,'Phelia Narramore','PO Box 96210','2018-10-23',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Carla Goldsworthy','09348901234','Child','2025-09-07 22:43:58',NULL),(1584,'Mitchael Kassidy','Room 1884','2016-02-21',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Loralyn Brocks','09171234567','Child','2025-09-07 22:43:58',NULL),(1585,'Perle Malins','Suite 75','2021-11-17',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Aarika Smallman','09215678901','Child','2025-09-07 22:43:58',NULL),(1586,'Tobe Ducker','PO Box 63968','2022-10-22',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Brianna de Quesne','09171234567','Friend','2025-09-07 22:43:58',NULL),(1587,'Bev Ellings','Room 111','2024-12-27',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Shane Fyrth','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1588,'Tait Teissier','Room 1431','2024-05-25',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Nadine Shippard','09215678901','Father','2025-09-07 22:43:58',NULL),(1589,'Shandra Levy','18th Floor','2015-12-17',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Weidar Poker','09171234567','Father','2025-09-07 22:43:58',NULL),(1590,'Vincents Elstow','Apt 1552','2022-10-29',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Granger Deneve','09215678901','Mother','2025-09-07 22:43:58',NULL),(1591,'Roselle Aland','Room 1438','2014-10-03',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','See Elderton','09348901234','Mother','2025-09-07 22:43:58',NULL),(1592,'Lettie Teligin','PO Box 51426','2020-03-23',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Juditha Cockerham','09171234567','Mother','2025-09-07 22:43:58',NULL),(1593,'Lars Lowndes','Suite 86','2015-01-27',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lombard Tesoe','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1594,'Dane Kinnen','Room 1397','2022-03-06',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Mame Jessard','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1595,'Malissa Hebard','Room 1187','2017-02-23',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Candi Faustin','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1596,'Aymer Ormshaw','16th Floor','2016-01-08',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gratia Tracy','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1597,'Micheal Pettitt','Room 1821','2025-07-04',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tobi Hoffmann','09348901234','Child','2025-09-07 22:43:58',NULL),(1598,'Lara Millmoe','Suite 58','2018-11-05',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Shaylyn Huniwall','09215678901','Child','2025-09-07 22:43:58',NULL),(1599,'Chaddy Verrell','8th Floor','2019-03-18',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lew Dibnah','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1600,'Godfree Yegorov','Room 1505','2025-05-27',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Guinna Cough','09348901234','Child','2025-09-07 22:43:58',NULL),(1601,'Iver Emig','2nd Floor','2021-06-30',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Port Scane','09171234567','Mother','2025-09-07 22:43:58',NULL),(1602,'Ced Tennock','Room 1921','2025-05-27',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Irita Hoggin','09215678901','Mother','2025-09-07 22:43:58',NULL),(1603,'Carissa Sebborn','7th Floor','2015-10-18',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jillayne Rudd','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1604,'Saxe Gracewood','Apt 1313','2016-01-28',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Denyse Tooting','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1605,'De witt Simonnot','7th Floor','2017-06-22',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Selma McKeachie','09171234567','Mother','2025-09-07 22:43:58',NULL),(1606,'Aleksandr Callway','19th Floor','2015-07-14',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jarrod Barefoot','09348901234','Mother','2025-09-07 22:43:58',NULL),(1607,'Estel Dacey','Room 1771','2021-11-29',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alva Tebbitt','09348901234','Mother','2025-09-07 22:43:58',NULL),(1608,'Marcelle Bazoge','PO Box 91371','2025-01-30',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Westbrook Lumsdon','09171234567','Friend','2025-09-07 22:43:58',NULL),(1609,'Mona Stanmore','10th Floor','2024-03-06',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Falkner Sealove','09215678901','Mother','2025-09-07 22:43:58',NULL),(1610,'Marge O\' Dooley','Apt 1070','2015-10-10',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Birdie Burlingame','09215678901','Mother','2025-09-07 22:43:58',NULL),(1611,'Minor Spaduzza','Apt 1258','2018-05-05',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tildie Stawell','09171234567','Friend','2025-09-07 22:43:58',NULL),(1612,'Tobias Calvey','Suite 38','2016-07-21',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Marika Cavendish','09348901234','Father','2025-09-07 22:43:58',NULL),(1613,'Braden Sproul','Apt 343','2016-03-24',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hazel Iacovino','09348901234','Mother','2025-09-07 22:43:58',NULL),(1614,'Othella Cottie','PO Box 60353','2017-01-24',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kimberlee Killough','09215678901','Child','2025-09-07 22:43:58',NULL),(1615,'Melisenda Haylock','PO Box 98875','2025-01-08',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Janey Curle','09171234567','Child','2025-09-07 22:43:58',NULL),(1616,'Celesta Rupel','Room 1899','2018-01-28',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cesar Orrice','09348901234','Father','2025-09-07 22:43:58',NULL),(1617,'Clyde Zanni','PO Box 37945','2017-08-29',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Sharity Jakovijevic','09215678901','Friend','2025-09-07 22:43:58',NULL),(1618,'Hendrik Watford','PO Box 43186','2023-12-11',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Leonore McCaughey','09171234567','Mother','2025-09-07 22:43:58',NULL),(1619,'Corrinne Bedding','19th Floor','2021-08-27',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Rab Titcombe','09348901234','Friend','2025-09-07 22:43:58',NULL),(1620,'Dean Askham','Suite 96','2021-02-02',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Trey Grishakin','09215678901','Friend','2025-09-07 22:43:58',NULL),(1621,'Ingeborg Cordner','Room 106','2024-12-18',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Charlean Schuster','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1622,'Octavius Cawdery','Room 1735','2024-03-09',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Aidan Barlow','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1623,'Beryle Stoller','Apt 1107','2018-12-03',6,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Roselin O\'Cahsedy','09215678901','Father','2025-09-07 22:43:58',NULL),(1624,'Indira MacQuaker','PO Box 77765','2018-11-19',6,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Candi Smallacombe','09171234567','Child','2025-09-07 22:43:58',NULL),(1625,'Bernelle Dunseath','Apt 698','2023-06-26',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Katharine Chittleburgh','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1626,'Daffy Rubinek','Suite 2','2018-09-29',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Michaella Santorini','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1627,'Pauli Delahunty','Room 822','2021-10-07',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kalle Rawstorn','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1628,'Antonin Bearne','PO Box 55563','2020-09-09',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Mayor Elvy','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1629,'Charles Peplow','Apt 413','2023-08-10',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Chad Colquitt','09215678901','Father','2025-09-07 22:43:58',NULL),(1630,'Andriana Deners','Apt 1745','2019-01-28',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Juan Coghlan','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1631,'Karlotta Ludgrove','PO Box 4773','2017-03-09',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kellyann Comolli','09215678901','Child','2025-09-07 22:43:58',NULL),(1632,'Ikey Bragginton','Room 709','2018-03-24',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Evvie Delgua','09215678901','Friend','2025-09-07 22:43:58',NULL),(1633,'Zenia Duck','Apt 1461','2017-05-28',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Rogers Screen','09215678901','Father','2025-09-07 22:43:58',NULL),(1634,'Orbadiah Connal','Suite 97','2023-05-31',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Saundra Itzkovwitch','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1635,'Nanette Louw','18th Floor','2016-12-22',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Roxine Mateuszczyk','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1636,'L;urette Sybe','Apt 235','2022-12-09',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ferne Trump','09215678901','Father','2025-09-07 22:43:58',NULL),(1637,'Annalee Nice','Room 82','2023-08-07',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Fredric Egle','09171234567','Father','2025-09-07 22:43:58',NULL),(1638,'Maighdiln Buckby','PO Box 17903','2019-08-24',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Roy Wanka','09215678901','Child','2025-09-07 22:43:58',NULL),(1639,'Brigitta Coughlin','PO Box 74288','2022-04-01',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Innis Bazylets','09171234567','Friend','2025-09-07 22:43:58',NULL),(1640,'Thekla Bulch','Suite 23','2016-06-03',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Garv Snassell','09348901234','Child','2025-09-07 22:43:58',NULL),(1641,'Raye Jopp','1st Floor','2024-09-30',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Elly Dies','09171234567','Child','2025-09-07 22:43:58',NULL),(1642,'Vanny Adhams','Apt 901','2019-07-12',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Elinore Gianuzzi','09348901234','Friend','2025-09-07 22:43:58',NULL),(1643,'Donovan O\'Duane','PO Box 40276','2024-01-17',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ulrikaumeko Delamaine','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1644,'Orran MacFaell','5th Floor','2022-10-27',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Tasia Andries','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1645,'Rodolph Fairman','Room 72','2019-01-15',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Florie Grocock','09348901234','Father','2025-09-07 22:43:58',NULL),(1646,'Colman Crookshanks','Suite 34','2017-05-07',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Auroora Pawnsford','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1647,'Arvin Wenban','Suite 52','2023-10-16',1,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Erhart Tweede','09171234567','Mother','2025-09-07 22:43:58',NULL),(1648,'Kim Saywood','4th Floor','2015-06-23',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lizabeth Gutman','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1649,'Katina Norgan','19th Floor','2022-03-24',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Susanetta Foucard','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1650,'Sherlock Winkworth','Apt 368','2022-07-31',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Stormy Jouanot','09215678901','Father','2025-09-07 22:43:58',NULL),(1651,'Spenser Dury','Apt 1424','2020-12-15',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Deidre Drewell','09348901234','Child','2025-09-07 22:43:58',NULL),(1652,'Harris Ovett','Suite 70','2017-04-16',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Forester Pressey','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1653,'Damita Szanto','Apt 1869','2016-07-13',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Tessa Eykelhof','09171234567','Child','2025-09-07 22:43:58',NULL),(1654,'Dot Podd','17th Floor','2016-03-26',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Nicolas Reiglar','09348901234','Child','2025-09-07 22:43:58',NULL),(1655,'Noell Wallbrook','Suite 48','2018-10-27',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Chicky Ackeroyd','09348901234','Mother','2025-09-07 22:43:58',NULL),(1656,'Shirleen Hartwell','7th Floor','2022-06-25',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Adina Chalke','09171234567','Child','2025-09-07 22:43:58',NULL),(1657,'Nathalia Elce','Suite 41','2018-12-07',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Daisi Seary','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1658,'Winny Leverton','6th Floor','2016-05-01',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Karrah Stoyle','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1659,'Carolan Coate','Room 93','2022-01-10',3,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Evey Clayworth','09348901234','Father','2025-09-07 22:43:58',NULL),(1660,'Rolland Cowdery','12th Floor','2019-03-22',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Zulema Lockery','09171234567','Child','2025-09-07 22:43:58',NULL),(1661,'Valene Varvara','1st Floor','2022-06-13',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Micky Fibbit','09215678901','Friend','2025-09-07 22:43:58',NULL),(1662,'Myra Covell','Room 1747','2016-10-10',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Maure Tremblay','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1663,'Albert Bernier','6th Floor','2016-12-04',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Junia Crummey','09171234567','Father','2025-09-07 22:43:58',NULL),(1664,'Lorilee Rigglesford','10th Floor','2020-12-29',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Avigdor Oneill','09171234567','Father','2025-09-07 22:43:58',NULL),(1665,'Liza Grassin','Suite 16','2024-05-26',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cosette Danbye','09171234567','Father','2025-09-07 22:43:58',NULL),(1666,'Annora Tzuker','PO Box 25833','2015-08-10',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Robinia Mixer','09171234567','Mother','2025-09-07 22:43:58',NULL),(1667,'Cornelia Willan','Room 1448','2016-07-20',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Judi McCorry','09215678901','Child','2025-09-07 22:43:58',NULL),(1668,'Padraig Hugh','Suite 45','2021-07-08',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Joy Dartnall','09215678901','Mother','2025-09-07 22:43:58',NULL),(1669,'Rikki Brunone','Suite 99','2023-05-09',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Laina Stopforth','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1670,'Lyman Morkham','Room 725','2015-10-21',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Suzanne Schild','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1671,'Del Spink','9th Floor','2023-05-09',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ernestine Sidary','09171234567','Child','2025-09-07 22:43:58',NULL),(1672,'Belva Gascoigne','PO Box 36853','2022-11-09',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Emmye Bottle','09171234567','Child','2025-09-07 22:43:58',NULL),(1673,'Rozalie Gligori','PO Box 30264','2021-06-09',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Reta Rutt','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1674,'Toma Haggleton','PO Box 34413','2019-03-01',6,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Henrietta Tickner','09348901234','Child','2025-09-07 22:43:58',NULL),(1675,'Hilario Spinola','Apt 385','2018-03-01',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Amabelle Husthwaite','09215678901','Mother','2025-09-07 22:43:58',NULL),(1676,'Prue Cutbirth','Room 1637','2023-10-06',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Humberto Frusher','09348901234','Father','2025-09-07 22:43:58',NULL),(1677,'Rog Widdocks','5th Floor','2015-01-01',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Noby Roycroft','09348901234','Friend','2025-09-07 22:43:58',NULL),(1678,'Kelcie Rutherforth','PO Box 75224','2019-11-09',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Pepe Cattemull','09215678901','Mother','2025-09-07 22:43:58',NULL),(1679,'Thomasina Josefer','Apt 1573','2022-12-30',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lilas Kave','09215678901','Father','2025-09-07 22:43:58',NULL),(1680,'Marv Kerry','6th Floor','2021-02-04',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Evered Comettoi','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1681,'Darbie Cleveland','Apt 1033','2020-10-28',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Alfredo Weiss','09348901234','Friend','2025-09-07 22:43:58',NULL),(1682,'Angelita Hartshorn','PO Box 87652','2021-11-20',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Elset Stokoe','09215678901','Father','2025-09-07 22:43:58',NULL),(1683,'Tessi Tomasoni','Suite 93','2023-09-15',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kennie Bulward','09348901234','Child','2025-09-07 22:43:58',NULL),(1684,'Verene Puttrell','PO Box 69992','2019-04-25',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dot Kirton','09215678901','Child','2025-09-07 22:43:58',NULL),(1685,'Darren Coupe','Apt 1869','2025-06-10',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Katalin Rawsthorne','09171234567','Father','2025-09-07 22:43:58',NULL),(1686,'Jere Booty','8th Floor','2021-06-02',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cathrine Ferrelli','09171234567','Father','2025-09-07 22:43:58',NULL),(1687,'Katina Lisciandri','Apt 791','2021-12-16',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Frederik Shemwell','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1688,'Ina Elgey','Apt 883','2023-03-24',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Cathleen Jentgens','09348901234','Friend','2025-09-07 22:43:58',NULL),(1689,'Purcell Friedank','Suite 6','2017-09-05',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Caz Milbank','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1690,'Damien Lemmers','Apt 909','2024-04-13',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Idaline Collingworth','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1691,'Gregorius Barabich','Room 1801','2018-01-28',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kordula Larrett','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1692,'Mavis Rickword','13th Floor','2016-06-14',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Carolynn Wakenshaw','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1693,'Kaia Capps','PO Box 81503','2016-09-15',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Corey Duck','09171234567','Friend','2025-09-07 22:43:58',NULL),(1694,'Randolph Beatey','2nd Floor','2021-06-25',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Drusy Colebourne','09348901234','Friend','2025-09-07 22:43:58',NULL),(1695,'Deina Houseman','Room 104','2020-01-13',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Mavra Nellen','09348901234','Child','2025-09-07 22:43:58',NULL),(1696,'Grace Crawshaw','Suite 58','2023-12-18',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Fifi Van Leijs','09215678901','Spouse','2025-09-07 22:43:58',NULL),(1697,'Maisie Speakman','3rd Floor','2016-09-16',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alex Greber','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1698,'Solly Pareman','Room 1163','2021-10-27',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jae McNeice','09348901234','Friend','2025-09-07 22:43:58',NULL),(1699,'Talbot Beasant','Suite 30','2023-11-18',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Emmi Mitroshinov','09171234567','Sibling','2025-09-07 22:43:58',NULL),(1700,'Kirstyn Janosevic','Apt 976','2018-02-09',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kelly Ranscome','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1701,'Gusta Cale','5th Floor','2025-05-23',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ranice Cosh','09348901234','Sibling','2025-09-07 22:43:58',NULL),(1702,'Muffin Heymes','PO Box 77078','2018-10-23',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Austin Wedmore.','09348901234','Spouse','2025-09-07 22:43:58',NULL),(1703,'Dawn Stepney','Suite 90','2024-10-02',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Whitby Gosnay','09171234567','Spouse','2025-09-07 22:43:58',NULL),(1704,'Shani Benedit','Suite 92','2023-12-19',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dix Skillman','09215678901','Sibling','2025-09-07 22:43:58',NULL),(1705,'Alikee Rantoull','Apt 460','2022-05-10',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Carolan Itzchaki','09348901234','Friend','2025-09-07 22:43:58',NULL),(1706,'Marty Boydell','Suite 40','2021-03-26',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Emyle Gillebride','09215678901','Mother','2025-09-07 22:43:59',NULL),(1707,'Whittaker Falco','Apt 1328','2018-12-01',6,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Burg Shemwell','09348901234','Father','2025-09-07 22:43:59',NULL),(1708,'Hana Laroux','12th Floor','2014-12-27',10,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Berky Cragell','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1709,'Mellisa Gavini','8th Floor','2024-01-17',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Carree Colvill','09348901234','Spouse','2025-09-07 22:43:59',NULL),(1710,'Ardene Booker','PO Box 49649','2019-10-20',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Martin Drohane','09215678901','Father','2025-09-07 22:43:59',NULL),(1711,'Theadora Olivetta','Suite 54','2019-04-14',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Tish MacKomb','09171234567','Father','2025-09-07 22:43:59',NULL),(1712,'Sybila Yurevich','PO Box 85795','2020-05-15',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jemmie Tettley','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1713,'Redd Weich','Room 1328','2022-05-20',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Annora Lufkin','09171234567','Friend','2025-09-07 22:43:59',NULL),(1714,'Gayleen Clowney','Apt 410','2023-10-24',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rene Brandon','09171234567','Friend','2025-09-07 22:43:59',NULL),(1715,'Amelie Rase','Apt 613','2022-01-03',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Anstice Kittley','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1716,'Cart Houldey','Room 92','2023-12-05',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Griffy Lyston','09348901234','Child','2025-09-07 22:43:59',NULL),(1717,'Buddie Beernaert','Room 1763','2015-05-16',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Gena While','09348901234','Mother','2025-09-07 22:43:59',NULL),(1718,'Ynez Reany','Suite 99','2015-12-12',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Raddie Perren','09348901234','Friend','2025-09-07 22:43:59',NULL),(1719,'Kary Cosby','Apt 1116','2016-01-05',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Freddy Hayfield','09348901234','Mother','2025-09-07 22:43:59',NULL),(1720,'Kennan Aps','5th Floor','2020-02-26',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rorie Eland','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1721,'Rebeca Court','Suite 15','2023-08-29',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Svend Lardnar','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1722,'Terrell Blanket','Room 1952','2023-08-28',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kingston Bartolacci','09171234567','Father','2025-09-07 22:43:59',NULL),(1723,'Zondra Ochiltree','Room 1948','2025-07-04',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Bernardine Laughtisse','09348901234','Father','2025-09-07 22:43:59',NULL),(1724,'Adah Twizell','PO Box 58860','2025-09-04',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Newton Atty','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1725,'Syd Percy','PO Box 6927','2016-06-28',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Elly Margrett','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1726,'Wolf Rennolds','Room 1326','2018-08-07',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Fran Thompkins','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1727,'Dedie Branford','Apt 467','2017-04-17',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Manolo Fursey','09215678901','Friend','2025-09-07 22:43:59',NULL),(1728,'Sosanna Feehery','12th Floor','2016-08-10',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Faulkner Thurlow','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1729,'Allyson Swalowe','Suite 45','2016-05-21',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Nicolette Beak','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1730,'Budd Oleksiak','Room 741','2015-01-31',10,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Andree Keneforde','09348901234','Mother','2025-09-07 22:43:59',NULL),(1731,'Sallie Izhakov','Apt 954','2016-10-21',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Frannie Sturt','09215678901','Mother','2025-09-07 22:43:59',NULL),(1732,'Fredericka Ballinghall','Suite 49','2018-04-07',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kinna Skates','09215678901','Child','2025-09-07 22:43:59',NULL),(1733,'Darbie Kilgour','Room 968','2016-02-25',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Gavin Kohnemann','09215678901','Child','2025-09-07 22:43:59',NULL),(1734,'Dick Trouel','Room 757','2018-05-22',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Farr Knowlman','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1735,'Cathi Philipson','Suite 27','2024-10-18',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Fayina Blenkinship','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1736,'Hephzibah Crockett','Room 1825','2022-04-30',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Robinett Gawkes','09215678901','Father','2025-09-07 22:43:59',NULL),(1737,'Erena O\'Carran','Room 59','2016-01-27',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Normand Lillegard','09171234567','Father','2025-09-07 22:43:59',NULL),(1738,'Layton Jeffery','10th Floor','2015-04-13',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Montgomery Metschke','09215678901','Mother','2025-09-07 22:43:59',NULL),(1739,'Mauricio Pappi','Apt 500','2019-02-23',6,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Iorgo Perin','09215678901','Child','2025-09-07 22:43:59',NULL),(1740,'Joey Kirkpatrick','PO Box 98342','2025-04-24',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hewie Willmont','09348901234','Child','2025-09-07 22:43:59',NULL),(1741,'Lief Roseburgh','Apt 1514','2025-08-07',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Enriqueta Solly','09348901234','Child','2025-09-07 22:43:59',NULL),(1742,'Caryl Simonaitis','Room 499','2020-06-16',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Christye Purvess','09215678901','Mother','2025-09-07 22:43:59',NULL),(1743,'Denise Jesson','Apt 1170','2015-03-28',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Gilbert Dawkes','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1744,'Seth Aujean','Apt 751','2014-12-06',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ulrikaumeko Sclanders','09215678901','Child','2025-09-07 22:43:59',NULL),(1745,'Rafaello Torns','Room 20','2022-04-20',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Cissy Timbs','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1746,'Howey Mingus','Suite 7','2024-10-26',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Madelon Dossit','09348901234','Spouse','2025-09-07 22:43:59',NULL),(1747,'Mercy Valentinetti','Suite 49','2015-07-28',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Coralie Booty','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1748,'Janessa Lambole','PO Box 17033','2023-05-07',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Mason Chisnell','09171234567','Child','2025-09-07 22:43:59',NULL),(1749,'Caryl Motherwell','1st Floor','2022-03-18',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Dierdre Philipsohn','09348901234','Spouse','2025-09-07 22:43:59',NULL),(1750,'Wynnie Dowse','20th Floor','2018-06-17',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Karrie Toupe','09215678901','Child','2025-09-07 22:43:59',NULL),(1751,'Adrianne Yellowlea','PO Box 98741','2023-10-01',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Peggie Sherbrook','09215678901','Father','2025-09-07 22:43:59',NULL),(1752,'Arie Cosham','Apt 1692','2024-01-18',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Mae Pozer','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1753,'Zaria Antonutti','Room 631','2024-08-29',1,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Krystle Huxley','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1754,'Barrie Bortoluzzi','16th Floor','2019-06-18',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Linea Adlington','09171234567','Child','2025-09-07 22:43:59',NULL),(1755,'Bax Yon','Room 1892','2014-09-29',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tedda Dansken','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1756,'Ella Pennoni','Apt 92','2016-11-27',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ally Semered','09215678901','Father','2025-09-07 22:43:59',NULL),(1757,'Billye Ladloe','8th Floor','2021-07-25',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Monika Sallan','09171234567','Father','2025-09-07 22:43:59',NULL),(1758,'Muriel Bothbie','PO Box 84153','2015-09-23',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Alyssa Kornalik','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1759,'Ethe Orry','16th Floor','2022-05-20',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Liv Scandrick','09171234567','Father','2025-09-07 22:43:59',NULL),(1760,'Nadiya Tommaseo','Suite 41','2020-11-30',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hetty Gilbeart','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1761,'Christiane Fouch','PO Box 50245','2023-11-03',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ursola Guinery','09171234567','Father','2025-09-07 22:43:59',NULL),(1762,'Bab D\'Alesio','11th Floor','2019-10-18',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Goldia Nunns','09215678901','Father','2025-09-07 22:43:59',NULL),(1763,'Brittan Tuffley','Room 709','2017-10-18',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Roseanna Harrigan','09215678901','Friend','2025-09-07 22:43:59',NULL),(1764,'Cleavland McGown','Room 868','2020-06-27',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rowan Bartley','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1765,'Helyn Haugh','10th Floor','2015-05-05',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Barny Lenchenko','09348901234','Child','2025-09-07 22:43:59',NULL),(1766,'Aubree Lawrenson','7th Floor','2023-05-23',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Norah Kellie','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1767,'Briant Stepney','PO Box 80031','2023-06-17',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Emlynne Lehrmann','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1768,'Georgette Gauche','17th Floor','2016-10-10',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jan Gallimore','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1769,'Quintana Duffer','PO Box 64373','2017-08-08',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kaila Fossick','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1770,'Ezekiel Merman','PO Box 5393','2016-11-20',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Aveline Dibbe','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1771,'Nessy Chisnall','Room 568','2019-02-08',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Morlee Traut','09171234567','Father','2025-09-07 22:43:59',NULL),(1772,'Mireielle Girdlestone','PO Box 17246','2016-11-15',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Matilde Strelitz','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1773,'Doralynn Mounch','18th Floor','2016-03-18',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Donovan Jemmett','09171234567','Friend','2025-09-07 22:43:59',NULL),(1774,'Alisun Cassels','PO Box 65802','2022-02-02',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jeramie Ridel','09348901234','Spouse','2025-09-07 22:43:59',NULL),(1775,'Nikita Southon','Suite 15','2021-11-09',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Iormina Fawltey','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1776,'Maison Claeskens','6th Floor','2025-06-12',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Vilhelmina Van Hove','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1777,'Karine Morling','Apt 1423','2020-01-19',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Pearce Aspey','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1778,'Zorana McKirdy','Suite 15','2019-09-20',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Alane Lentsch','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1779,'Imelda Yeude','Suite 41','2020-01-21',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Alano Dilleway','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1780,'Seth Oen','PO Box 19651','2014-09-12',11,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Grier Redbourn','09171234567','Father','2025-09-07 22:43:59',NULL),(1781,'Stevie Castagneto','PO Box 97484','2017-02-11',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Terrye Fackrell','09348901234','Child','2025-09-07 22:43:59',NULL),(1782,'Feodora Trundle','Apt 766','2016-06-23',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ettore Sandry','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1783,'Beatrisa Sonier','Room 1092','2021-10-23',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Viola Paulack','09171234567','Child','2025-09-07 22:43:59',NULL),(1784,'Drake Tonbridge','Apt 772','2024-11-29',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Shelba Bockings','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1785,'Lancelot Mount','Suite 75','2025-08-18',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Rebe Senechault','09215678901','Mother','2025-09-07 22:43:59',NULL),(1786,'Jordan Keel','Suite 71','2017-03-26',8,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Tate Fingleton','09215678901','Child','2025-09-07 22:43:59',NULL),(1787,'Dody Bortolutti','Room 1538','2017-04-18',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Dylan Derbyshire','09171234567','Mother','2025-09-07 22:43:59',NULL),(1788,'Fleur Gilkison','Room 1344','2019-12-04',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Salli Desporte','09215678901','Friend','2025-09-07 22:43:59',NULL),(1789,'Liva Hatry','11th Floor','2024-12-18',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Betty Kristufek','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1790,'Darcee Ianetti','PO Box 64689','2022-01-31',3,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jamesy Padwick','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1791,'Ashly Fountain','Suite 97','2025-05-06',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Eddie Lande','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1792,'Temple Oulet','Room 1431','2021-11-04',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Giff Bynert','09215678901','Friend','2025-09-07 22:43:59',NULL),(1793,'Brucie Howcroft','Apt 1289','2016-09-19',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Cam Parks','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1794,'Cedric Loidl','Apt 436','2019-04-18',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Octavia Benstead','09215678901','Mother','2025-09-07 22:43:59',NULL),(1795,'Feliks Keenlyside','Apt 432','2015-11-16',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Silvie Lucy','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1796,'Shea Konerding','5th Floor','2022-04-01',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Zerk Pentelo','09215678901','Child','2025-09-07 22:43:59',NULL),(1797,'Zarah Coulton','PO Box 65808','2016-05-14',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Aveline Divis','09215678901','Child','2025-09-07 22:43:59',NULL),(1798,'Holmes Sandiland','Room 324','2020-01-22',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Joann Ruddock','09171234567','Child','2025-09-07 22:43:59',NULL),(1799,'Isiahi Bullard','PO Box 55680','2016-10-24',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Bendick Eaves','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1800,'Stavros Sabathe','14th Floor','2022-02-20',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Camila Le Conte','09171234567','Child','2025-09-07 22:43:59',NULL),(1801,'Merilee Caustick','16th Floor','2016-10-17',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Izaak Fussey','09348901234','Mother','2025-09-07 22:43:59',NULL),(1802,'Alix Schubert','PO Box 27715','2019-05-20',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Clarence Holsey','09215678901','Child','2025-09-07 22:43:59',NULL),(1803,'Sanders Greeno','Suite 42','2019-10-26',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Cally Mailey','09171234567','Father','2025-09-07 22:43:59',NULL),(1804,'Hilarius Warrender','Suite 84','2024-10-03',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Duffy Manford','09215678901','Mother','2025-09-07 22:43:59',NULL),(1805,'Killian Siggins','Apt 808','2017-02-08',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hunter Simkiss','09215678901','Child','2025-09-07 22:43:59',NULL),(1806,'Kaitlynn Lack','Apt 163','2024-09-10',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Gibbie O\' Flaherty','09348901234','Child','2025-09-07 22:43:59',NULL),(1807,'Lockwood Bohje','20th Floor','2015-12-16',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Hillard Maccrea','09171234567','Child','2025-09-07 22:43:59',NULL),(1808,'Delcine Decaze','Room 1422','2017-06-15',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Tobit Lambshine','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1809,'Cecil Wallett','Room 1662','2022-03-30',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Joye Beardmore','09171234567','Mother','2025-09-07 22:43:59',NULL),(1810,'Wylie Bettley','PO Box 24866','2019-03-04',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jami Delyth','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1811,'Rabi Roffey','Apt 1351','2020-01-07',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Marcellus Arton','09215678901','Friend','2025-09-07 22:43:59',NULL),(1812,'Aguste Breen','Suite 67','2020-04-12',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Merrill Heatherington','09215678901','Child','2025-09-07 22:43:59',NULL),(1813,'Wilton Shearwood','Apt 1479','2019-09-16',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Edan Wolfendale','09171234567','Friend','2025-09-07 22:43:59',NULL),(1814,'Lynett Servis','18th Floor','2016-11-21',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Adriaens Hartill','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1815,'Krisha Verbeke','Suite 5','2020-08-03',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sonny Eminson','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1816,'Petronilla Eastmead','PO Box 18392','2017-11-14',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Eduard Petchell','09348901234','Spouse','2025-09-07 22:43:59',NULL),(1817,'Kirby Crew','Apt 632','2019-06-11',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Becki Scorthorne','09171234567','Child','2025-09-07 22:43:59',NULL),(1818,'Lenna Wisniewski','PO Box 65634','2023-09-02',2,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Hugibert Minot','09348901234','Child','2025-09-07 22:43:59',NULL),(1819,'Charlton Vaux','PO Box 17835','2017-03-22',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Mercy Jeffes','09215678901','Child','2025-09-07 22:43:59',NULL),(1820,'Ebony Petrovykh','Room 1430','2022-08-26',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rossy Meadmore','09348901234','Friend','2025-09-07 22:43:59',NULL),(1821,'Freddy Touhig','20th Floor','2020-01-28',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Greta Tesche','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1822,'Lammond Croad','Apt 1592','2024-08-22',1,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Olly Robard','09348901234','Child','2025-09-07 22:43:59',NULL),(1823,'Lorant Lakes','Apt 295','2017-09-18',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Malvin Bierton','09215678901','Mother','2025-09-07 22:43:59',NULL),(1824,'Bartolomeo Grigorian','6th Floor','2015-01-18',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Warden Lowcock','09215678901','Friend','2025-09-07 22:43:59',NULL),(1825,'Amata Colwell','Suite 14','2025-06-22',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lorri Wicklin','09171234567','Mother','2025-09-07 22:43:59',NULL),(1826,'Amii Manns','Suite 5','2018-10-13',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Doy Bricknall','09348901234','Child','2025-09-07 22:43:59',NULL),(1827,'Misti Haycox','Suite 14','2017-04-28',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lars Deery','09348901234','Father','2025-09-07 22:43:59',NULL),(1828,'Leola Caroll','Room 1400','2018-06-11',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Modesta MacBain','09348901234','Child','2025-09-07 22:43:59',NULL),(1829,'Tobin Noke','PO Box 74769','2022-03-17',3,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Venita Heifer','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1830,'Priscilla Elcoate','Apt 1729','2016-07-10',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Baxter Ricoald','09348901234','Friend','2025-09-07 22:43:59',NULL),(1831,'Carny Enochsson','Room 442','2017-12-30',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lynnea Khrishtafovich','09171234567','Friend','2025-09-07 22:43:59',NULL),(1832,'Garner Cowey','Suite 94','2023-04-13',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Reggy Shapcott','09171234567','Child','2025-09-07 22:43:59',NULL),(1833,'Lionello Nutt','Suite 61','2015-06-23',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Melania Siss','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1834,'Les Cundey','Suite 53','2018-10-25',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Cortney Trittam','09348901234','Friend','2025-09-07 22:43:59',NULL),(1835,'Iona Hedworth','PO Box 71954','2025-07-06',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Billye Zipsell','09215678901','Friend','2025-09-07 22:43:59',NULL),(1836,'Noella Arunowicz','5th Floor','2018-10-10',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Nate Clemitt','09215678901','Child','2025-09-07 22:43:59',NULL),(1837,'Rebecca Clew','Suite 81','2014-11-10',10,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rory Louis','09215678901','Father','2025-09-07 22:43:59',NULL),(1838,'Tonye Gwillyam','3rd Floor','2016-03-26',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sibelle Rouke','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1839,'Dominik Gerrell','Apt 571','2015-10-07',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Wilden Wigmore','09348901234','Friend','2025-09-07 22:43:59',NULL),(1840,'Marga Imore','12th Floor','2018-11-13',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Eleni Dahlman','09215678901','Friend','2025-09-07 22:43:59',NULL),(1841,'Deni Sprull','Suite 78','2015-09-06',10,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Libbey MacInerney','09171234567','Child','2025-09-07 22:43:59',NULL),(1842,'Kaleena Roll','Room 1865','2015-08-12',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Leann Neenan','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1843,'Harriette Whiles','Room 1950','2020-03-13',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Tarrance Kondrachenko','09171234567','Father','2025-09-07 22:43:59',NULL),(1844,'Elisa Deverock','8th Floor','2019-05-20',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Laverne McMonnies','09348901234','Mother','2025-09-07 22:43:59',NULL),(1845,'Kathleen Penvarne','PO Box 1841','2020-06-08',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sidonia Treweke','09215678901','Mother','2025-09-07 22:43:59',NULL),(1846,'Geoffrey Helbeck','Room 1559','2016-02-15',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Gardner Bensley','09215678901','Mother','2025-09-07 22:43:59',NULL),(1847,'Felix Whicher','PO Box 82810','2021-02-19',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ethelind Hambrick','09171234567','Friend','2025-09-07 22:43:59',NULL),(1848,'Romonda Blabber','Apt 775','2024-08-14',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Carolann Agius','09348901234','Mother','2025-09-07 22:43:59',NULL),(1849,'Benedicto Tuma','PO Box 53819','2020-02-22',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Dorolisa Fairfull','09215678901','Father','2025-09-07 22:43:59',NULL),(1850,'Brooks Hancell','Room 940','2019-06-18',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Mathilda Couth','09215678901','Child','2025-09-07 22:43:59',NULL),(1851,'Xever Sessuns','Suite 72','2017-09-23',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Humfrid Figge','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1852,'Danya Ferrandez','Room 419','2015-11-05',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Worthington Lysons','09215678901','Friend','2025-09-07 22:43:59',NULL),(1853,'Wally Runnalls','Suite 39','2017-12-07',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lorin Partington','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1854,'Thomasina Fereday','PO Box 76095','2022-06-20',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Franklin Jiru','09171234567','Mother','2025-09-07 22:43:59',NULL),(1855,'Flore Rivilis','Room 1428','2016-12-20',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jere Woodvine','09348901234','Father','2025-09-07 22:43:59',NULL),(1856,'Constanta Fante','Suite 69','2020-06-02',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Vivi Lafont','09171234567','Father','2025-09-07 22:43:59',NULL),(1857,'Cesaro Utridge','12th Floor','2025-09-03',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Martynne Hoffner','09215678901','Mother','2025-09-07 22:43:59',NULL),(1858,'Isabeau Muzzall','PO Box 97274','2021-08-05',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Caty Checci','09215678901','Friend','2025-09-07 22:43:59',NULL),(1859,'Felita Helix','Room 1779','2024-08-04',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Tandy Goude','09215678901','Child','2025-09-07 22:43:59',NULL),(1860,'Meade Agiolfinger','2nd Floor','2015-05-20',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lanita Creavin','09215678901','Mother','2025-09-07 22:43:59',NULL),(1861,'Haywood Balke','PO Box 90720','2023-06-05',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Oby Culkin','09171234567','Friend','2025-09-07 22:43:59',NULL),(1862,'Helsa Innis','Apt 1545','2018-07-24',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Valentia Lampitt','09171234567','Friend','2025-09-07 22:43:59',NULL),(1863,'Randie Rainy','Room 333','2022-09-09',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Winfred Marrion','09171234567','Mother','2025-09-07 22:43:59',NULL),(1864,'Elberta Peaurt','Apt 1127','2019-04-01',6,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ignazio Killcross','09215678901','Child','2025-09-07 22:43:59',NULL),(1865,'Chane Wittey','Apt 192','2020-06-06',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Anson Deval','09348901234','Mother','2025-09-07 22:43:59',NULL),(1866,'Linc Skyram','Suite 37','2021-11-22',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Emilee Scoble','09215678901','Friend','2025-09-07 22:43:59',NULL),(1867,'Hagan Kas','5th Floor','2017-06-20',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Porty Saich','09215678901','Child','2025-09-07 22:43:59',NULL),(1868,'Doloritas Bulch','Suite 81','2021-07-14',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Carver Abramowitz','09215678901','Mother','2025-09-07 22:43:59',NULL),(1869,'Elysia Edgson','PO Box 92640','2018-02-06',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Packston Middlemist','09215678901','Child','2025-09-07 22:43:59',NULL),(1870,'Pepi Gudgeon','Room 1250','2022-11-06',2,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Christie Wilfinger','09171234567','Child','2025-09-07 22:43:59',NULL),(1871,'Klement Haslen','Apt 626','2022-02-27',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hermon Fellona','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1872,'Ab Turgoose','Apt 818','2018-11-12',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Wilbert Kidman','09348901234','Mother','2025-09-07 22:43:59',NULL),(1873,'Amandie Dumphrey','PO Box 52750','2017-04-18',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Sam Alvarado','09215678901','Mother','2025-09-07 22:43:59',NULL),(1874,'Grant Meaders','Room 1575','2016-09-03',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ally Purton','09348901234','Mother','2025-09-07 22:43:59',NULL),(1875,'Suzi Reeme','4th Floor','2025-02-24',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Fancy Zuanazzi','09171234567','Father','2025-09-07 22:43:59',NULL),(1876,'Julia Sheasby','PO Box 22981','2021-05-12',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Chelsae Strognell','09348901234','Father','2025-09-07 22:43:59',NULL),(1877,'Guillermo Dagnan','Apt 298','2015-08-28',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Claude Laughrey','09171234567','Child','2025-09-07 22:43:59',NULL),(1878,'Welby McCaughen','Apt 810','2023-06-15',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Barnett Wyllis','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1879,'Bell Gush','Suite 62','2024-05-11',1,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Yorke Quinnet','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1880,'Marinna Daborne','1st Floor','2020-10-23',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hobard Chessill','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1881,'Leta Perrinchief','Suite 5','2019-01-22',6,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Donny Campbell','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1882,'Florenza Hearns','Apt 21','2020-08-04',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Burch Munden','09348901234','Friend','2025-09-07 22:43:59',NULL),(1883,'Lexis Davenall','Suite 52','2022-06-27',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Eustacia Addicott','09171234567','Child','2025-09-07 22:43:59',NULL),(1884,'Jephthah De Fraine','11th Floor','2016-06-17',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Eugine Maciejewski','09348901234','Friend','2025-09-07 22:43:59',NULL),(1885,'Isadore Petrillo','Room 1625','2024-04-06',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tera Scawen','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1886,'Tanya Gligoraci','PO Box 55570','2022-04-19',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kalina Swadlen','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1887,'Margarete Tilbrook','Suite 82','2018-10-13',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lyon Hrihorovich','09348901234','Mother','2025-09-07 22:43:59',NULL),(1888,'Erda Benes','Apt 1019','2021-06-26',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Teddie Bog','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1889,'Esma Purves','Room 73','2017-08-29',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Barrie Bowdler','09215678901','Child','2025-09-07 22:43:59',NULL),(1890,'Pasquale Dallimore','Suite 64','2019-10-14',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Rici Lampet','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1891,'Aileen Battson','Apt 1845','2021-01-30',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Angelico McIlwrick','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1892,'Malynda Amberger','Apt 1990','2020-06-01',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Patty Jersh','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1893,'Naoma Dreger','1st Floor','2018-09-17',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Karol Chatfield','09215678901','Father','2025-09-07 22:43:59',NULL),(1894,'Charley Ketteringham','1st Floor','2017-11-21',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Hurleigh Malia','09215678901','Child','2025-09-07 22:43:59',NULL),(1895,'Kristyn Eburne','PO Box 8954','2022-12-03',2,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Stevy Burel','09215678901','Child','2025-09-07 22:43:59',NULL),(1896,'Brett O\'Henehan','Apt 1323','2016-11-01',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Solly Stoakley','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1897,'Kiley Elliss','12th Floor','2015-07-03',10,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Caroline Recher','09348901234','Mother','2025-09-07 22:43:59',NULL),(1898,'Meredith Shillabear','PO Box 21512','2016-09-20',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Bank Lanigan','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1899,'Huntley Chesney','Suite 40','2023-04-07',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lissi Colnet','09348901234','Friend','2025-09-07 22:43:59',NULL),(1900,'Janaya Killoran','13th Floor','2018-05-20',7,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Nedda Nanuccioi','09348901234','Child','2025-09-07 22:43:59',NULL),(1901,'Barr Rodear','Apt 402','2016-10-03',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Jimmy Dignam','09171234567','Father','2025-09-07 22:43:59',NULL),(1902,'Mignon Brakewell','Suite 8','2018-01-05',7,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jacqueline Lemmon','09348901234','Father','2025-09-07 22:43:59',NULL),(1903,'Franklyn Leamon','Apt 407','2019-05-13',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Bidget Markie','09215678901','Child','2025-09-07 22:43:59',NULL),(1904,'Jedidiah Jaslem','Suite 10','2019-07-31',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Maryl McFaul','09348901234','Mother','2025-09-07 22:43:59',NULL),(1905,'Jimmy Newhouse','Room 1655','2022-10-23',2,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Joni Hessentaler','09171234567','Father','2025-09-07 22:43:59',NULL),(1906,'Rhonda Boother','PO Box 71783','2020-01-07',5,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Madelin Demcik','09171234567','Father','2025-09-07 22:43:59',NULL),(1907,'Eugen L\'oiseau','Room 449','2016-01-25',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Dick Kendred','09215678901','Father','2025-09-07 22:43:59',NULL),(1908,'Wendall Whines','Apt 1997','2017-09-03',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Taddeo Shapera','09215678901','Mother','2025-09-07 22:43:59',NULL),(1909,'Anjela O\' Connell','14th Floor','2023-11-18',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Faina Crumpe','09348901234','Mother','2025-09-07 22:43:59',NULL),(1910,'Feliks Padilla','2nd Floor','2022-01-23',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Iseabal Vanichev','09171234567','Father','2025-09-07 22:43:59',NULL),(1911,'Morey Hadny','Suite 91','2023-02-08',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Titus Wegner','09348901234','Friend','2025-09-07 22:43:59',NULL),(1912,'Charlena Iacovaccio','Room 60','2016-02-21',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Fulvia Ramsay','09348901234','Spouse','2025-09-07 22:43:59',NULL),(1913,'Byron Himpson','PO Box 19035','2020-07-14',5,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Royce Matyugin','09215678901','Friend','2025-09-07 22:43:59',NULL),(1914,'Nathalia McGloughlin','Apt 652','2016-07-19',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Davida d\' Elboux','09348901234','Child','2025-09-07 22:43:59',NULL),(1915,'Siouxie Ciciura','19th Floor','2020-08-29',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Chandra Lippini','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1916,'Abbi Arnaudet','PO Box 18068','2018-12-03',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Woodrow Scruby','09348901234','Spouse','2025-09-07 22:43:59',NULL),(1917,'Delaney De Giorgi','Suite 50','2017-06-03',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Wait Lasslett','09348901234','Mother','2025-09-07 22:43:59',NULL),(1918,'Andra Frammingham','Suite 99','2016-01-31',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jilleen Cremen','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1919,'Ellary Garioch','Room 1169','2021-01-25',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Emilee Feaster','09348901234','Child','2025-09-07 22:43:59',NULL),(1920,'Gwenora Tock','PO Box 56623','2023-10-22',1,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ellerey Mockford','09215678901','Father','2025-09-07 22:43:59',NULL),(1921,'Jerome Norridge','Apt 1654','2016-05-10',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Allsun Leason','09171234567','Father','2025-09-07 22:43:59',NULL),(1922,'Silvio Koppens','Apt 948','2022-01-26',3,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cordula Tower','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1923,'Heath Semorad','Suite 59','2022-01-19',3,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lyn Poppy','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1924,'Opal Jaggers','Suite 33','2025-07-07',0,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Yorgo Becker','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1925,'Shelby Blagdon','Apt 671','2017-12-27',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Sandi Bridgestock','09348901234','Child','2025-09-07 22:43:59',NULL),(1926,'Sheila-kathryn Mayling','Apt 249','2020-06-25',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Cindra Eager','09215678901','Mother','2025-09-07 22:43:59',NULL),(1927,'Martie Stobbs','Room 623','2023-07-24',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Reece Arsey','09348901234','Friend','2025-09-07 22:43:59',NULL),(1928,'Rani Coffin','Room 1038','2020-11-24',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Sigismondo Garth','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1929,'Ileana Brodnecke','Suite 46','2023-10-30',1,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Tabbi Robjents','09215678901','Friend','2025-09-07 22:43:59',NULL),(1930,'Marie-jeanne Brownett','6th Floor','2022-10-07',2,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Mitchael MacAlinden','09348901234','Father','2025-09-07 22:43:59',NULL),(1931,'Kristine D\'Hooge','7th Floor','2016-08-27',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kurt Nassie','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1932,'Obie Santino','Apt 1783','2020-07-23',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Misty Pobjoy','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1933,'Norah Snodin','Apt 438','2022-06-08',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Hoebart Mathon','09171234567','Friend','2025-09-07 22:43:59',NULL),(1934,'Odilia Klassman','PO Box 54263','2018-03-18',7,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Sandro Greste','09348901234','Mother','2025-09-07 22:43:59',NULL),(1935,'Merrili Oakenfull','Apt 138','2019-01-03',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Marijo Pond-Jones','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1936,'Imogene Jacobsen','Room 1861','2023-07-21',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Patrizia Drysdell','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1937,'Cassy Corington','18th Floor','2021-11-27',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Rafaelia Salatino','09215678901','Mother','2025-09-07 22:43:59',NULL),(1938,'Sylvia Larchier','PO Box 28803','2024-04-07',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Devan Telega','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1939,'Chere Maides','4th Floor','2022-03-09',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Armando Folli','09171234567','Mother','2025-09-07 22:43:59',NULL),(1940,'Meggi Strethill','Room 47','2016-08-13',9,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Goran Eyam','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1941,'Charisse Cage','Room 1242','2016-12-08',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Gregorio Sendall','09348901234','Spouse','2025-09-07 22:43:59',NULL),(1942,'Darryl Chatters','Suite 15','2015-02-12',10,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Ancell Naire','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1943,'Evonne Avo','15th Floor','2021-05-18',4,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Denis De Witt','09348901234','Child','2025-09-07 22:43:59',NULL),(1944,'Barbee Savege','13th Floor','2021-02-18',4,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Lark Donoher','09171234567','Father','2025-09-07 22:43:59',NULL),(1945,'Tait Hallybone','Apt 380','2015-04-18',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Luelle Hake','09215678901','Child','2025-09-07 22:43:59',NULL),(1946,'Horacio Gutcher','5th Floor','2017-09-27',7,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Prentice Brightey','09348901234','Friend','2025-09-07 22:43:59',NULL),(1947,'Rochester Kingwell','Apt 1664','2022-04-18',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Trescha Runham','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1948,'Babbette Cornfoot','PO Box 97138','2023-08-04',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kordula Lazonby','09171234567','Mother','2025-09-07 22:43:59',NULL),(1949,'Garrett Sudy','7th Floor','2020-01-13',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Blanca Perett','09215678901','Child','2025-09-07 22:43:59',NULL),(1950,'Sheridan Capnerhurst','PO Box 14005','2022-07-26',3,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Raynor Feehily','09171234567','Father','2025-09-07 22:43:59',NULL),(1951,'Malinda Jansens','PO Box 922','2017-09-11',8,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Andreana Cammis','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1952,'Julita Desvignes','Apt 432','2021-02-10',4,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Stacee Clother','09171234567','Father','2025-09-07 22:43:59',NULL),(1953,'Barby Butters','Suite 73','2017-11-05',7,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jessi Luter','09348901234','Sibling','2025-09-07 22:43:59',NULL),(1954,'Mandi Heard','Suite 90','2023-09-29',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Hadrian Iacoboni','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1955,'Langsdon Gellan','Suite 66','2016-10-26',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Arleta Kimmitt','09215678901','Friend','2025-09-07 22:43:59',NULL),(1956,'Jaimie Brushfield','Suite 77','2019-06-09',6,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Cobby Vannuccinii','09171234567','Father','2025-09-07 22:43:59',NULL),(1957,'Thekla Harmond','Apt 1980','2018-02-27',7,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Arlen McFater','09215678901','Child','2025-09-07 22:43:59',NULL),(1958,'Jorge Ackred','Room 424','2022-01-25',3,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Sanders Dunsmuir','09348901234','Mother','2025-09-07 22:43:59',NULL),(1959,'Joline Weed','19th Floor','2015-09-22',9,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Eachelle Kindon','09348901234','Mother','2025-09-07 22:43:59',NULL),(1960,'Roberto Reede','13th Floor','2021-04-18',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Selestina Cabell','09348901234','Friend','2025-09-07 22:43:59',NULL),(1961,'Adriane Millgate','PO Box 73204','2017-04-18',8,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Rosalyn Peddersen','09171234567','Father','2025-09-07 22:43:59',NULL),(1962,'Cleo Clem','5th Floor','2020-07-17',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Kelsi Hebden','09348901234','Friend','2025-09-07 22:43:59',NULL),(1963,'Gunter Bodleigh','7th Floor','2020-04-26',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Trip McParlin','09215678901','Mother','2025-09-07 22:43:59',NULL),(1964,'Shalom Fontenot','PO Box 11393','2024-08-10',1,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Stefania Cartwight','09171234567','Child','2025-09-07 22:43:59',NULL),(1965,'Ozzie Trimming','Room 49','2017-06-28',8,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Kathe McQuillin','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1966,'Tallou Aylett','Suite 42','2015-04-11',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Codie Tolan','09215678901','Father','2025-09-07 22:43:59',NULL),(1967,'Letta Oehm','Room 279','2020-05-08',5,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Fallon Kubecka','09171234567','Mother','2025-09-07 22:43:59',NULL),(1968,'Rutter Nucator','PO Box 54545','2019-10-29',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Agnes Neely','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1969,'Remy Pedler','14th Floor','2023-08-07',2,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Tobey Morewood','09348901234','Spouse','2025-09-07 22:43:59',NULL),(1970,'Ina Ennals','PO Box 37656','2025-06-19',0,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ben Vedyasov','09171234567','Friend','2025-09-07 22:43:59',NULL),(1971,'Shaw Cess','Apt 1070','2017-08-08',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Mandel Kernar','09348901234','Child','2025-09-07 22:43:59',NULL),(1972,'Deane Naisby','Room 941','2019-06-05',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Pieter Chant','09215678901','Child','2025-09-07 22:43:59',NULL),(1973,'Troy Gilliatt','Apt 179','2015-04-11',10,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Silvanus McEwen','09348901234','Friend','2025-09-07 22:43:59',NULL),(1974,'Gigi Coupar','Suite 61','2021-04-04',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Consolata Girth','09215678901','Child','2025-09-07 22:43:59',NULL),(1975,'Leonelle Toohey','PO Box 44793','2025-04-11',0,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Derward Axell','09171234567','Child','2025-09-07 22:43:59',NULL),(1976,'Charis Ruskin','Suite 21','2024-11-16',0,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Pattin Lead','09171234567','Father','2025-09-07 22:43:59',NULL),(1977,'Duncan Goulborne','PO Box 7062','2016-05-02',9,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Jena Hutcheson','09215678901','Mother','2025-09-07 22:43:59',NULL),(1978,'Dallas Jellings','Room 410','2022-02-04',3,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Ryan Fannin','09215678901','Child','2025-09-07 22:43:59',NULL),(1979,'Rebe Liffe','Apt 1594','2016-05-27',9,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Emily O\'Hagerty','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1980,'Annis Van der Hoven','PO Box 2635','2024-06-23',1,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Ericka Pattenden','09171234567','Mother','2025-09-07 22:43:59',NULL),(1981,'Stanleigh Dumsday','Room 1305','2015-02-04',10,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Kristofer Reedie','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1982,'Donelle Scohier','Apt 540','2020-06-09',5,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Olenolin Whittlesea','09215678901','Child','2025-09-07 22:43:59',NULL),(1983,'Dore Klimt','Room 149','2025-08-26',0,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Jourdan Wear','09348901234','Father','2025-09-07 22:43:59',NULL),(1984,'Robby Archambault','Suite 35','2017-09-04',8,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Heidi Nare','09171234567','Friend','2025-09-07 22:43:59',NULL),(1985,'Dolores Tidd','Suite 52','2022-12-28',2,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Mattias Jessep','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1986,'Francine Tidmarsh','Apt 1641','2020-08-06',5,'M','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lyndel Bulcroft','09171234567','Sibling','2025-09-07 22:43:59',NULL),(1987,'Micah Heditch','PO Box 93209','2016-09-01',9,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Filbert Forber','09215678901','Father','2025-09-07 22:43:59',NULL),(1988,'Knox Clever','Apt 952','2019-04-12',6,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Chrystal Nucciotti','09171234567','Friend','2025-09-07 22:43:59',NULL),(1989,'Margarita Connal','Suite 45','2020-12-31',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Cordula Ledeker','09215678901','Mother','2025-09-07 22:43:59',NULL),(1990,'Chrystel Rooke','PO Box 24364','2020-07-04',5,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Huey Whitmarsh','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1991,'Gwenny Rickis','Suite 17','2021-02-20',4,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Lacie Grieveson','09215678901','Sibling','2025-09-07 22:43:59',NULL),(1992,'Daphne Yallop','2nd Floor','2015-01-24',10,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Arnaldo Harmour','09215678901','Friend','2025-09-07 22:43:59',NULL),(1993,'Taylor Yoell','Suite 49','2025-01-02',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Sioux Hollyer','09171234567','Friend','2025-09-07 22:43:59',NULL),(1994,'Marven Pert','Suite 56','2021-08-25',4,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Tedra Cuttelar','09215678901','Spouse','2025-09-07 22:43:59',NULL),(1995,'York Sproat','Apt 1291','2022-12-01',2,'F','Widowed',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Alexina Greve','09215678901','Child','2025-09-07 22:43:59',NULL),(1996,'Gabriell Siggery','Apt 1603','2019-01-13',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Finlay Spridgeon','09215678901','Father','2025-09-07 22:43:59',NULL),(1997,'Immanuel Sheryne','7th Floor','2017-01-26',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Malena McInnery','09171234567','Mother','2025-09-07 22:43:59',NULL),(1998,'Florenza Bartalin','20th Floor','2016-02-08',9,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Harper Legh','09171234567','Spouse','2025-09-07 22:43:59',NULL),(1999,'Eveline Summergill','Suite 9','2021-01-04',4,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09348901234','Christopher Spiteri','09215678901','Sibling','2025-09-07 22:43:59',NULL),(2000,'Heather Bolderson','Apt 1001','2018-12-26',6,'F','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09215678901','Karen Bullan','09171234567','Mother','2025-09-07 22:43:59',NULL),(2001,'Giraud Audiss','13th Floor','2016-12-15',8,'M','Married',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09171234567','Lorelle Giacovazzo','09171234567','Sibling','2025-09-07 22:43:59',NULL),(2004,'Asdasd Asd A. ','Asd','2025-09-17',0,'M','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09511365191','Asd A Asd. ','09511365191','Father','2025-09-17 12:39:44',_binary 'ˇ\ÿˇ\‡\0JFIF\0\0`\0`\0\0ˇ\€\0C\0		\n\r\Z\Z $.\' \",#(7),01444\'9=82<.342ˇ\€\0C			\r\r2!!22222222222222222222222222222222222222222222222222ˇ¿\0\0¿\0¿\"\0ˇ\ƒ\0\0\0\0\0\0\0\0\0\0\0	\nˇ\ƒ\0µ\0\0\0}\0!1AQa\"q2Åë°#B±¡R\—$3brÇ	\n\Z%&\'()*456789:CDEFGHIJSTUVWXYZcdefghijstuvwxyzÉÑÖÜáàâäíìîïñóòôö¢£§•¶ß®©™≤≥¥µ∂∑∏π∫\¬\√\ƒ\≈\∆\«\»\…\ \“\”\‘\’\÷\◊\ÿ\Ÿ\⁄\·\‚\„\‰\Â\Ê\Á\Ë\È\ÍÒÚÛÙıˆ˜¯˘˙ˇ\ƒ\0\0\0\0\0\0\0\0	\nˇ\ƒ\0µ\0\0w\0!1AQaq\"2ÅBë°±¡	#3Rbr\—\n$4\·%Ò\Z&\'()*56789:CDEFGHIJSTUVWXYZcdefghijstuvwxyzÇÉÑÖÜáàâäíìîïñóòôö¢£§•¶ß®©™≤≥¥µ∂∑∏π∫\¬\√\ƒ\≈\∆\«\»\…\ \“\”\‘\’\÷\◊\ÿ\Ÿ\⁄\‚\„\‰\Â\Ê\Á\Ë\È\ÍÚÛÙıˆ˜¯˘˙ˇ\⁄\0\0\0?\0Òç\ÿîûrÉäç‘é˘Jî+Lô$uç `A\n\ZvFﬂ•5π2Z\…\0}y˜Ù¶ıVÇ§ ï^;PõY@=\ÈëvG∏\ÌÒ Úz¥Fπmßµw3-\0¥”öú9\∆Tx\Ã ˜4\’\‡u¨\Í+ö“ïô/òﬁô£\Œ9˚ºT}∫\–k+#mDwfa¥\‚¶ÌäÜ§=ií\ƒ+ìI”ÅN\‹4\„8ˆ†]/Ä)A\»\Õ7®\≈(\‡@\Ó/•Csê@©ªTw(“©≠E\'µbú)¥¥Üã\‰T/y1=˘¶ì¯\“‘í∂<äàvı,\Ÿ	öÅ∏pjì%§H†Ü¸(\…ëú©»ßïT\„\–\‹6;0\≈YNUâÖír=*D#\À¯®\–\r\‡LQ®¥ˆä}1C1™C\ÃkS-QQzç§£¸ı§=k†:‘ò\ q⁄£˜ß⁄¶ÅKaá üZx\‰ä@A\‰éiW≠;à=©F})sM\Ì\ÔHQç\Ë\À◊äL”ì\Ôt¶&R<ph¸)Û.\…X~4\ \–ú)æ‘¢ê\ÀÙáß¶ˆ,ë≤}\√\ÔUòÚ*Àèñ´Úä¥Idç\Î¥\’\œC\‘S–Ñˆ¶g,x\Á5°òF¿uM\0º|\”z~Üú\Áµ ;ç.Jdu\Êö2rGj	\Î\Ô“ê\…);\“r?˝TæıÉ\–\ÈN\ËJJ8£µ\0\ƒ^4ı9†ˆ†7Zw Z1M9$⁄úM\0§F˘á±£<TQêK˘°%º\\™\…¯\Z©Z<\ÀR;ék>≠1⁄îSi{\”(\–¿§ búE7ï$\r=1ö¨x\«÷¨∂p09™Ø÷öñ7)ˆ4à>r\‘\‰b\«~iÅ∞	ˆ≠ÆÅøéáä06Ç;S2p°•\‹qèzQ\—\‡åR6\‹)†êx=h\…?â§{c\Â<t§\«_†ßı¢≤ö\‘\ﬁ\Ë8QG•IB£v£\“c\Ê\‚ó$\Z\ƒRI¡¢é˘¶íqL&£è˝i˜ßdı®ÛâA˜™D≤ıπ\Ái\Ô\≈Rï6J\À\Ëx´1ùÆ)∑\À˚\≈q¸BöU•§•¶3Pèõß\∆\Z\ÏSE∑óvo†∞u`ê]<1(\ËM-ne©ld8\È¡®$\ÈW~\“\Íz\≈G%\Ÿ¡\¬/\ÂI6Qg@<R\‡Ç}iæcH	<c∞ıd˙é3VàhåH=)\Ó†˚äcpˇ\0≠J¸çæ¥\ƒ1Pzé):í=Ò@by\Ë(\∆\Á\„Ω\0.súr\rúÜ«øO≠.+9ö\”\n;Rä9\≈fma¡\Õ)+€öct™\Ÿ*x&©+ë\"\œC÷êı®<\∆ı\Õ/ò{äÆRnKöâ˛ˆiCå˜\÷9° -©\Ëi˜?=∏n\Íj\Œc8˘°eıõR¢ä*Üzãsﬁ∏ˇ\0.\›D„å®\«\Â]ï‘ìN\È1`\ÿ\Œ∆è\Á±\ÔY!_Ù\’\„9AZ\…\›\\\Âå\'fs\Î\–\Á4\…:T\‰c•E \‚≤\Ín»£?7\·R∞ )\«lT(>qı´\r\Ã*OPjë~dS\ÏiRGb\r9A\n†˚“ï˝\÷E2Dq\Œ}E4Ñw “ü_L\ZsïÙ4\0\Ìœ∑Ú•n¥£`zM\Í^ï3ZM\Í-%—ä\ƒ\‹ojÇLgäï∏®[5q&Ch¢ä≤Bä(bÚ\Ëj\ƒ}MV∑?z¨/\ﬁ•á[ï§da\ÔM©nœüZäÑQ\ﬂ€ºK.˚xÆf\›¿ë¯U˘lZ\ŒÒ>|Dˇ\0w˙\÷Èà≥1fc∏åÙ¨o\„d\r\ÓGÚ≠\‰≠8∂\‰Æsç\”÷¢~ï!\≈1π±GC+tj∞>da¯äè\À\ÎN\‰ghô4J@Ú—Ω˝*N\0\œNî3b\ƒ‰û¥]ìa¿üÅëO\Œ\‘S2√°†\0Ù•qÿìÆ\Ïw®\„\ÎöTdûy\ŒqH88˙è á∞-~4\ﬁ\›ih\ÌX(â™6\ÈR1ˆ¶’¢YQVHQE.3@\…m˛˛=EXZ$dzT∞8\ÃyÙ®g\ÈVà\ÃGöØI2íπ\–Õ¨]\»\ÔJ\Á˚ºU¶íSÛ±c\Ói¯§%vúı\ÏsZ\ﬁ\Á5íÿÑÉM\"•\»4\¬3»£f\rë\„öJ~)\Ì@\∆î\⁄y¶û\Î@&#¯\È¿\Ï)∏ßRs\ÔHcyß£ww\ÔM468€ü|“∏\ÏJ¿©Ù§\⁄q\Ë=M1_oΩG3π8c\∆=k>Ws[°]\’Oöàíi(´J\ƒ\‹\\\ZJ\\ÒIL\"\‰\”\»\n:åPÉ\Â•aIÄÿø\÷/÷≠UTˇ\0X1\ÎV\È0t5Tå9hTú\“E£EÄ\Õ4äêÆÊôåV€ú\√1ﬁêØ5.\‹ˆß(\√g\0˚\ZrΩ˝©1é\Ÿ˙\’ÿ≠ö\ÊM®ß$∫|∂†JÒìzë\≈\'ö\∆lÑ3\n¬¢#˙\’\ÈR\0ç\◊q\‰c†®ïbí=Æ\ƒ8<ÿ†|\⁄Ò\≈Nê;[≥´.\0\…\‰\”gåF˚CÜ¢£\Ì÷ëDtqKHjlRëænº˚\“—å\“\Zd%qEKäaS\‘t¶1îR\‡\“‚Åí†\Ër1JÁß≠D8\ÈKÉ\◊5 I¿-V;\’|\‚!\Ój\«a@\ƒ\ÔQ1Àü≠JNAH∏hlòÙ´0ióW$\·v˙\nÙKmO¥#l*\«’π´±¨Q\›∆´ÙΩ\—¿\Ê\Ópˆûæòf]±ès[væ≤à:Bˇ\0•tcw4Ö±ù«ßZoc;∂ ∂ömçìë”≠M<q∫:¥jA\ÈK\Á!\œJx˘ê¥Xz\ÿÛO\ÈB\¬ˇ\0Æ!ên_oZ¡qÇG•z?ä\Ï\Õ∆öePBs¯wØ>ï7ûÊÜ∫ó–®G4”äêäefj2ìΩ8\Á≠%!ç\"äp\Î\Õ6ÇÑ£¥RªA©@)∏•ôiçe\€Ù†/Àöy\‰~\≈\„ØJ!\Ë*\–9™ûI5e9E§\∆$á\n}\Íı$æûï(.\'∏ª@5û£∂EAíNI\Õ4ê:ö\ËHÛ5d\Õ3WÅMRXr~¢†2Ñ≠KÇE$s@tπ \∆´\›˝\r@∏ß@q#.x4u\Ãbh6\0Ü\ZÚΩBŸ≠n‰ÅøÅàØ[\∆;\n\·º[ß≤]%\¬.Dü)«≠2ì∂ß \À¡ˆ®Øµ[ö6çä∞¡EWa\ÕA≤dFõäêämIC(\≈:ó“Å\ﬁ\√q⁄Ä)qJìäAqÛ[I\0R\„ÜA®Ç\ÁÆ4w3™á¡Fi\Î¶\ y¶(ï#å\”vÛú÷¢in\«\ÊlS.Ù\„\0N\·ﬂäë©\≈xß´\·p{PF3H8¢\≈\‹F\…4îˆ;é}i1J∆äGÆ9e∏XòÚG@*À¨6Î∏åì\ÌHä<˜ùÅ\'¢\–>{ïÛëê=+Æ\—V<~i™áLï ûÿ™éÇ\‚â˜YsZÉ\0c•g›ê/bı ä\Ã\—6«éò†≤\€<\“g∞§<:ÉMΩå3Y∫ºmìú\—¸\ >ï§ç∫% v®§Ut!á\«Z[ßì\ﬂ\ f∏g#\’&≠¨\€}õPñ<`n»¨¬ÑìäMGb9¶ëS˘,\›Oã∞\‹zw©.\Âπ\ÌV\—\ÿéµØ\r¨AFWëW6/ñ\‹P&\⁄fUæìÛ\r\ÌWb\”\„Ñ\‰Æj⁄≤gäë¡d˜Ö∏\œ-B˝\ﬁ*=†t=\Íx\‹4x\'ßJâ∏|îÖ\Ê4\0öI\0ëH#û¿c\"òNO÷Å\ÿ≈ª±\∆Z>y\‰V{!G5\‘I#Ù™∑	<[óá≠&\ÕS9˛ÙT\“\¬—±V*<Ph¨ˇ\Ÿ'),(2005,'John Doe D. Jr.','Buntatala Jaro Iloilo City','2002-03-03',23,'F','Single',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'09511365191','Monica D Doe. ','09124433223','Mother','2025-09-17 13:52:01',_binary 'ˇ\ÿˇ\‡\0JFIF\0,,\0\0ˇ\·\0VExif\0\0MM\0*\0\0\0\0\Z\0\0\0\0\0\0\0>\0\0\0\0\0\0\0F(\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0,\0\0\0\0\0,\0\0\0ˇ\€\0C\0	\Z!\Z\"$\"$ˇ\€\0Cˇ¿\0h\0ˇ\ƒ\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0	ˇ\ƒ\0I\0\0\0!1A\"Qa2qÅë#R°±Br¡\—$3Sb\·CÇí%45c≤&\'¢Dsìˇ\ƒ\0\0\0\0\0\0\0\0\0\0ˇ\ƒ\0*\0\0\0\0\0!1A\"2Qa#q$CÅëˇ\⁄\0\0\0?\0ÙDqØ±\Ê1XkU5à\ZÅA™Ö†=*“àIj(¥†-(J-@ZT\–-*Ç“ä\ZQ•Ebhÿ¥)•\ÿwi°™Õ°M!\ &\›\¬\‘Dú&Å\ÈUCJ“Ü\√J\0\Z¢î•U\r(Ç“ä\ZP\r(l4†-!\rÜïiTÖ4lZF\ƒan¶ócä1Ÿ°N¶\Œ9\Ô>A]\'/ÛWI∞\”\ÊöJ\0\’A\ÈD\r(5\È@x@` 0ÖAÄÄ∞Ü\…-Cb,MC™Ö\ƒ\Ï≥a≥1\“\‰\ÓblÛiÄË¥ÄbêT6¯\‘!£Ë©¨â@Û\"D-±™#Ce∂5Aà\–bÄi@í\‘ZÄ¥†N@∞†-(°Ñ•a4\r(î•\0\¬,ò@m`êî\0j“Äi@a®îJ§ \ZB\¬“Äaa\0\¬ÑîÑ@a]Ñ\–M\0Ü∆Ü¿ < õlhªCe60Ä¿VD*îs\Ê\“@Ç°%®§†i\ÕP6Y∫iÇ!\‰í6/D\–Xç4ÉM.«°Sa•\rãJ\n) I\n\·aÄ∞Äê\ÿ!± @Hi`\‘\0*h\Zi4†Ä 46B,lnß8ÒMÑ6Æò\„0¸T\‹]l±ª\›{HÛ*\ƒ/(	 ; $\0@    \0Ä¡@P\Z]Ç®0P(EEJSnE6y†∫\rWLl†\‘—∞“ã±\È@X@D(P †A@í†IE\Ÿ9P\Ÿ9@Y@YCb\ \ (ee\0\ ©j@zê\0\‰R©zë@9\0‘Ç÷∫:Jg\À$±±≠qq\ÿ-\“\„6\„|{\⁄M\rí/o!˘±±óª\Ê6\Ê∏[k—é:r;Øk∑Q9ˆ&É]çXq.†;JFºiÌõà iÆäB”ê\◊\∆aì†sr\ÃSQ\”83∑´|≤6+£\ﬂ\0k˜?ê\Â\Í>aYïå\ﬁ9}:çü¥>\Z∏F.0\ƒ”∂∑<iœ©\Œ\ﬂ5πúrºv5QL\…bl±=Øç\„-sNA`≠±\Ëzê\rHlZêÿµÑXÛ@5†-hl5ÑZ\÷_™ÄwÅ\0\Ôö\ﬁ5AwÉ\Õ4x<\–y®H<\’ºj\ﬁ˙™J<\“	Gö†ƒ£\ÕâGö	Gö ˚\—\ÊÄwÉ\Õö!&Q\ÊÅQÊÅßJ<\—MôGöõ6\–\Í[dê§R‘†AzÜ\»/EŸ≤ÙMê^Ü\…/SK≤â†ì\"ô2 Iï\rãΩ@]Ú)=Ú wﬁ®æE˘]Ú.\≈ﬂ°∞\Ô\–\ÿw\Ël=°ThQIuNP\€\«=•pˇ\0Bˇ\0\⁄\Ï\0e¥ÒR;\Â\”\Ê±rt«é◊ú˚G\Ìö\Ô\ƒ\Z\‚§lîî`¯cxú?\ÃG%\œ\Õˆ\Ôé3&ØºW\’8˜µDÇsßY\∆UëvÇ˙\Íñl[®|HZò≥\ÿ\„*£ó\rë•ß°\'˘ß[«Äïékú\·\”\'u4´:zπ\ﬂLZ$p oÇw¯©•\€M\Ÿ\ÔkK¡W\€T\”€µ˝\Ì3\ﬂ\‡p\Îå˚ß\‘+%ûô∫æﬁ≤\Ïﬂ¥[7\ZZ˚\Í\nñ{C\Z∞ªg∑‰∑é[p\œã[\Ì%oN{¥\“\Ï^\–SI±{AM¥94ª¥—±{CìF\√\⁄\ÊöA{CìF\≈\ÌıWF\√\⁄Í¶çá¥94l]˚º\”F√øw™hÿª˜¶çá~\Ô2ÆóaﬂªÃ©£aﬂøÃ¶ìc=4æw™¶¿N\‰61;º\ .\ ?\’4l°3º\ i\nø\’˜èıWF\≈\ﬁ;\’4lì#˝SF\»/zhŸ∑9\È£fÀüî—∂\œ\n†a\0@H®*(P †A@ì\’@íÅ$ B\¬\¬)8DÅÑP\¬ ∞Äai@P\r(	\⁄Z\“\\pT3∑é\◊≈â\“ÿ¨\Õp\Õ7¯g\»5\«,˜\‚=|~7^i∏\\**f}U\\Øñy	sú˜díö\Á#∫≤Y2\›s<c†Zëw\€a¨ í‹åõqf˘\œ\\≠2Lg<ò>m¬¢E4ı0ù\‚/ã®i\Œ≤R[¥≥∂B\…cp\Î\”\Ê∞\‘å\ 2>Ïùét§*œÅo7Nº\≈p∑L\Ë* xŒìñüC\ËBñVjΩâ\Ÿ\«Z∏≤\ﬁ\«1ÌÇµ≠ZwÓ•æ`Æ∏gøo6|w\œH[`4\"G¢(hHÇ\—Ë™ãJ†y \ZHÚE\rJ°hîJ“ÄhPj•Ü†0\ƒ\Z®SZÅmj†=(X¨BK6@Ç\ƒ,\›\r5£íä<z @î	* Å\n	(P%Jî	!pÅ8@X@X@0Äê @0Äa\·k¥\À√¨r∫2f\«$0ûÏìèŸ∏˘\„eú\Ó£\\x\Ô-<#]U5eŒ¢™yù)π\œq\…sâ\‹¸I\\#⁄®ö°œêÌìüêWHi\∆i\\]ìÚVY\rlmÖ\ƒ\Ó◊π6ΩS)\Ë√Ü$N\Î¯\”Ye{\ÿLoì\‡Z\nΩ\”Òß[xZæG4∂2}tÛ˙)y#X\‘\—\¬±9\œ1\◊q˘¨˛IWÒXüTT”ë0p%∏\ÿnSæñq\ÌE~∞\ﬂ,q\≈T˘\·è¿˜ªôùµNY[\«9óá,¯\Ó>Vº∆í\€\Íô;\Ë\Âf\«\Z\ÿNˇ\01Ñ∏\ÿ\Ã’èbva\ƒ\—q/≤~>Vl\„¯áö\ÎÖ\‹y≥\«U¨¿[s¢\¬Ñ•\0“Äaa\0\¬\¬ a0Äa\0\¬\¬\0\¬\0\·ÄÅma ,*J†FîÙu\rëº˜YóbP\›T\"íP †AP †AP †A@íÅ%JêÅ%pÄ∞Ä∞Ä∞Ä∞Äa\0\¬\¬Ñ6A\Ê∂W\ÀUøÜißxa®ò\\\»˙r\\≤õÆ¸^&\ﬁu£ A#ã\ﬁ\‚\Ï{\À9;b∂≤p˝L¨Ô•è@w∫”æôı\\Ú\œ˙w\„\„æ\Í˛õÜA¿p\œ\…s\Ï\Ì0ZRp§DåáE;µ8\‚ÓÉÑ\È€á>VnM\Œ6ÇÉá\‡\»\ƒ\r\‡≥ŸÆç\r™\∆—¥rXµπåJ6\Ëù\ÔF\”ÚIR\‚r+|?\·¥|ñ•N•T\Ÿ\Èj`|R\¬\«n7\¬‹¨e#âˆâ\Ÿ‹ñ\Z©n∂∆Ω\‘2x∆åò\—\‡yyÖ\Ë√ì~+\«\…\√\◊\ÃZ}ûªB?\ƒ\"Ü\ÔS,v˜ì≠±ª\»¿;Ù\Á∞]ßáì,{=yi∏\—\\iÑ¥í\ÎÇ\“3\Ê\n\È,æú,≥\⁄f\≈iÑ@EÑ\0Ä∞Äa\0\¬\¬@0†@xT)®(\n†∞Å$\"ê[∫&å\–Uò\‰√éÀÑ≠\ﬂ-Ceh\ﬂu\÷]≥§ûäÑî(P ®z®P$†A@îP$†I@ú Ña\0@ \0 06A\„µ\r\\7.\÷kÛÇ(be8¿\Í£˘ï\√+\ÊΩ|sˆ\∆É-æ®O4XÖÑè\ƒV2∫èG;ªÆëMoåt.ΩR&\“\—4ø\ZVësGG\Ê\—ÙY\€r.)\ÈX#˜T\€zMßß\€l\0ÇLqcmñkRı8V%? ÙV2pAÅú-\ ≈Ü™Ë£®ß|R∞=Æ ékRπ\◊\Ì;Ç\„≤\›[YGm4\Œ\»-˝\«y||rﬂ∑ììéK∑a˚2Òï\‘\“Ÿ™_˝¶å\02}\Ë\œ\Ô/#”ó™\Ì\«^>\\|m›±≤\ÏÛã\n®êÑ@\¬( , @0Äê\0@ê\0Ä\–@°\…öÅa\0V \"íB\·Yß\ƒW-.÷ñWªΩ\“N\ ¡¢\Ë[	!\n†A@Ç†AVÅ%@íÅ\'`P!\0@îPÑÄ Ñ\n 03∞\ÊUÌû≠∑N”∏Ü¶òû\Ó[åë∞ûÅû\"º\÷˘Øfƒâ¸Z\0éU\Ï\‚ök°h¿\\∂\Ô\È\„\0ÚYÆí-)\0\∆B\ÀQeNvÙQR\‚\ÿ\‰rWB@\Êt‘•å\·A:\·ékQÉ\Õh;an9\⁄3\‘b≤}°[·¨±Mç \„ë]1ˆ\Âú\‹c˛\œÛCm\ÌRñ€Ωd@\”¯_\Ôë¡—á∑áñ~\⁄ı)^óåH¢@H\0Ä\"	P\0Ä \0Ä¡@h 0Ä\¬ä¿EÑ¯Ò,*}£˚ı!¶ëû\‡[BÅ≤ Ö\n	!@í	!1ÑB|\–(∞à	\0\¬ÑÄ 0êG∫M\Ï÷™∫Ä\‡\”x\'°\r\')}\€¿µ\“\÷I+\‹	ysú\ÔRN™ÚæÜ1∞·®¥5òπ\\rØWi\‚Óπª\ƒ\»_ó\Ï£QoB\ÕC--†à5™*KéCe§ó∂VT\Ì,~<ê¢¨b\0∑XîÙY\0éat\≈\«#ò\ZIZëä£\‚xLñ˘Z\0;n£ùûØÑ%/j6yôÜñ\÷7s∂	v1Û\…]Òx˘\'äıöı<B †e \0Ä @X@H @H\0ÄaÇê@aÇH\0™)FU˝VMµ\Ì!ö1\‡s\“\nÅ≤!¡@Ç ÖH@Ç	!apÄ∞Ä∞Ä∞Ä∞Ä∞Ñ @` @`ù†w≠\‡{”¢\Ÿ˛\≈.q˚•Lˇ\0ã\\\ <B\„4˙5Kº\’\Ó\≈\“xzôÒ\”\Îw3\…yÚØfn\◊\ÓNötï[\Ì∫ñOæúy©Ö©˘$†Y\·pc{\«q\…_\≈Y¸—™±ÒM≤\‚u!iÂÉ≤\≈\√NòrJ\’Si{qú¨∫l∑D\Â»¨\Ë\⁄TQwq\Îp\€\÷\Ê)rëQu\‚\€¶G2∫¨5\Õ-h\…s\rπ\Â…¢≠|a\√\◊&áR\\\‚$úiwÖ\ﬂC∫Ω,cº´jj\»\'$G+GëQ}äΩÇHè™F+í\ÿ\„íN\“mÃè\r{ÆÜá\r∂9^å^,˛ﬁ™\“¸{•zûK]¯J\·\ﬁE\0\rw\·)∞aØÚ)∞aè¸%˜oÚ)∞}\€¸ê\Ì\ﬁHh;∑y!°wnÙCB\–}\–h>a\rèPõ4->£\Íõ4,\ƒfÖ\·æ\‘\ÿ-L¸mMö\rq˛6˝SfÉΩè¨ç˙¶\ÕM¯çMöûˇ\01©∏hbx?\≈	∏h°Q¯°7\r⁄à?\ƒ	∏∫(O¯Å&Q4>˛ÒΩ°¢{\Ë?\ƒN\–\”8˚\≈3d\“\ÁÄ~*F\Ó5ccπS\…T]êóI\÷È≤ä≤.\Ïd\'i\–\Z\»|î\ÔD\Z\ÿ|ìºMkaÚSº4A≠á\…;\≈—≥]íwÜà5˛;\√D\Z¯\nwÜà7\nwÜà7\nù\·‘Érãßxu ‹¢¸)\ﬁ/RM\Œ/¿ß\‰â‘ìsè\'\‰áQ~“è\'‰ã°~”è\'\‰áRuåshNÒtâS\ƒˆ\Ísâ•c©W∫h\Ì-˙ñ°πÑµ\√–©\›z§õ?\0SÚC©Mπ0˛\ËWÚKm¡üÑ\'\‰:ñ\⁄¯ˇ\0N\ÁT>$©Ü~πBˆ\◊\“\»ˇ\0§©sñ5é>^\·xÑ∑=\'õ•$öÂüß≥éyuh\„B\Z¿`//\€\ﬂ<FNÒpºW◊ö+Td`\È/<æ%u\∆I\Ì\«,≠∫ÜGf◊ö˜á\…sÇ,Û\0\Êr1x≤©ÙΩî\◊@\Ã\…qÜS\◊\rr~X∏ÿõC\√ˇ\0±giöw´9kq≤\ÁñN∏a¶˛\◊wkö∆áÙ+ÖØLû\Z\€h\Ô\·\…ÚIYæ^dpßtmy\Õoô∏\Ì\À/|[t®ôÒ\‘«©\Œ$úÄªc\…#ñ|v≥Òˆ]\≈0\Œ\◊S\nJÄNu6M8˘ïπ…çy\ÔQmA≠kj+hekA√û◊á^òK%\\méµ`π\√vµ≤™<åå9ßõOí\Â|:˚`8(2~\ÿadå\Í h?\Ëª\œooFõ´ø\ÿ[¸è7R\r\’\ﬂ\Ï\'\‰:ín\ŒÛ¸ìÚa?µù\Á˘\'\‰:Ä∫ø\Ã˝π\‘b\Á)\Í~â\‹\Í?\⁄3Æ˙\'zΩG\Ì\”ˇ\0õ\Ëù\È\÷\€*G˝Ω:¡{MOì”Ω:\¬Lı\'êzù\È®Añ´\…\Èﬁö\'º™?∫Ù\ÌMAj´¸/N\’uöØ\¬ı;SPDU~\'jj¢®˛Îæ©⁄û\r\»⁄¶¥ù%^\’5û4‚™õ8!†ìÒWj\‡\€˝m\ÍúH\Z~©mV≤:z\◊78+;¶ãußßÊõ¶°m°≠ÚM\”¡\÷\–\’˘\›≤Ü´\—_\'ÉÃ¢™ÙW\ x,Q\’z+\‰?c™ÙO\'áΩV\’2Ò#Dè\∆\Â”æX\Ì§\Ïˆ\·,ójx%ë\ÿz{K≠;ΩN\“\\y)\”o>\À6\Ë¸\ tMêm±˘ï:.\…6\ÿ¸\ t6A∂G\ÊT\ËlÉkãÃßCdõT^e: ⁄¢ıS¢\Ïìiá\’:!&\”™t]ím0˘\Ël_≤`Ú*u6O\Ïò<äu6/\Ÿ0y\Íl_≤`Ú)\‘ÿç™\¬S©¥+ç<póc\nXªy{\Ìv4uÅî\’2FD£8w™\Î≈é\Ÿ ∫Waï¥\◊$Ní]N#´ó,±˝\ÕK\·\◊!†Å\√\¬\Ã¸°ÿ±C\08,W°ÿ∂PFyFS°\ÿ~\≈;∞Ñ\Ëv7]mé{}DZqÆ77?R\·\·f^^±5\‘\\gSL\ÌXÜ≤HÄ#gïåˇ\0ã\◊\«s¨\ÀynKr1êºØz\”R\⁄i_9h\r`\‰\‰Ù\n\Ôi#?r\‚ã\„\Ìµw\n∂)Ä\ÿm´|s\Á◊ûÀær\Õ\◊.Nn∑P\«Òe˙˚wÜ\ﬂ5]3\Ã\ÿZ\÷\‘I\ﬁZH8\∆€ûã∂<8\ﬂN\‰\Â7jˆææ¥T\…AU+úÒù,òxé9\Èw\Ô\Õp\œµ\Í\√9ú\Ìâ\ŒûcT\»\∆sûKœúw¬∫\ÂâÚ2î4ù¶8µ|´/≥\»\È\00r∑é1\Œ\Â\ÂŒ∏áân\‘4_¥X$\ÓL\›\Ãq@¿ùÇyª\–.∏Òo\Õc>\\qÒˆwÇ¯€ä´˚∑\«i≠|çÚ1Ÿé@ˆ≥\Z±§Ûªçg®ÛóÖˆ\Ë.$£Ω\”˜d\∆\‚ˆÚi\Ÿ¿s`é†ÆıÆ˝eõâ{{-≥Uòö\∆A \÷\–	€ûFæYÙ¨˚9\⁄oˆ•y™\»\ÀI´\ƒ22\Á\„o¢ıaáoüÀñûÖóá\Àsù9¯-˛ü∫∫¢\Ÿ›∏Ç\÷˝¸GqGgë\Ì\‘#\0zÑúGp6¨tEgw4hö√Ç\—ÙWÒ\Àe;3ç#Ëüà\Óy¥\Ï\Ë\—?\ÿÛiöG∫¸I\ÿ\ÏtA\€ß\„;¥≠å{øít\“ˆGkZO ≥\‘\⁄M%#&óI+0;.#≤“πû(˜]\'N\’\n∫\’x1åoÖãÑÑ\…*í\«N\ËÛ $ï¨x\·⁄£W\⁄#à¡\»eK\∆v™\Ê\“	\«$ò/f?èÆ\Ì≥Q∫B@\ﬂ\’f\‚Lûl\ÌKâ™+eï—ú4E¨0Kñ\›\'∞\ZóUZ\„88#üö\ŒXyje\·€©\È\‹\Ï\0ì≤tV˜ºx\·^â‹ô)Àì°\ÿB,ßC∞∂\nh\Ïv6©äv9~,≤LWi>≈ù\«%π\∆\Œ\‹:JZj€£õ\·\'ñ\ﬂ\∆MΩyg•Õé\‘\ ;˝&ëå8Æ∫\Á\€n\„of)[VOã\–IböQ\'Pû\ÌN†ª¥\Ílì\ZùMã∫N´≤{•:¶\≈\›z\'SbÓì™\Ï;î\Íl]œ¢u6\¬u\‹z)\‘\Ÿ.É\—:õS\ﬂ\Èˇ\0±ønÖf\¬Wà˛–°√à\“N\“ˇ\0%◊ã\”5±>0´µ\ﬂ)\ËßD„ÅÉ\…k,%Úõ{üÉ\\*\ÏëO¯ÜT\∆\Îckjò\ﬂ\ƒT∞ãjjh˚ÆAYåiæ(\ÿÒ≤j#ñˆì\⁄UeÇ¸\Î-ñ*7\ÕN\∆\…9®av¨\ÔÄ\€ªØ?\ \ÈüL~üS\‚|	\…\≈˘3˚y6˚O3{Fíµ\–2_Xglq∏ñ¥π\Ÿ;ù˘\Á\Í§œ∂-e\≈xÚ”ßEHyk◊åV^-,≠váí\Êg\›R])äk-dê\Ã$loicò\–\\9o\Ê∫\„\…c7Eo∞Xm2≠îl\‰i\÷\÷\È |WI\»\Ã\‚ü—©©¥∏π∞\∆\◊güº~•c,\Âtò&p¸Z+ÑØ\Ê6çªjM:•úÉ#\∆\Îx≠DÆà8\…öyÆıRc∂V\È\¬Tµq>	\„{©\\\‡\Ì ù éDcë[ô\÷o?p\ÔMØá§í¶\”0Çy#\Ó\…x/-nrCs\À\'ü¡u√õØ™Û\Á¡Ü_\ m2ãÑôGu}u$ò{ü©\„<˘¸w+œùπ]ªcdö\”KZ\r∂g;\0∂7>@¶5\À(Öˆ5´∂\≈_\ƒ˜*∫\⁄h$ê¡Oñf≥^\Ôq\0øEÙ8≤ò\ﬂ5Ûyq\À)\‚=_Wo¡#$/EØ*öyò˙ÄN1ïëi£\‰\„\n¡MYWe-j±ÛM≠˚*A0ø<ñj§D«ìûä	,%º∑?M.«íöQV8∫/Uõ\\\œ	Êπ¥ôE)dôQPW`\·mÓïÄDyz,\Â\ËHµ\›\"ñ®\‡\·1\»&∫†JHo,+D\ÿN•\∆˚}ÖÕ∑â3\·™üc\Œ|f¿\ÍwûøË∑ä:ó\Ÿj•\“Y\Ÿ9\“\Ï~k{k\·\Èz8N¶ç∑\n\∆WçcX\·\’hFØs;Ç7PW∞\ÔtM¢U7IX∞\Ÿ\Í\'x∞OEb|éãQ\'nâá©Îã¢!n_Û\Á\n=ˇ\0ÒUC^Ú@y\∆~+ü”¶^\›ÆkxÇêûZïjzu\⁄J\“<ñ\Áß\Ÿ\nÇ\»@úÖ\0\»Ú@úè$6N∂®¢\÷<êÿåçCb\»!∞\ÔZÿª\÷˙(l;\Êz!±w\ÃÙCb\Ô\Ÿ\Ê\ÿ\⁄˝[4Æç´xÖÆ2ﬁÖg,i∑Ö˛\–≈Ø\‚I\\√ë\ﬁUÆ(ïë\Ï∂.˜ç®#ÛrÈó¶~ü@x&Q\rÜÜFñÖ\À\·bUd\¬J∂}\’-X∏§ü\ÓíV¥èWô$¬ùóO:ˆ\ÂC\‹võ)¡û•\Í4\„˘/ãÚ±ˇ\0ûøI˙}\ﬂƒü\È\Õo±U\\(\\@l\–U0ππ›π\» ˝A]8Ø∑>lw™–∞e\ƒ)Wï\Z\Õ\Œ	R5£\‚m\ÿ˘¶Ù\◊Tjö@‹ónùöò™kX7¿\06ñ∑úT™∞\ÈñFøˆs^q\…k\Z\’,ÄÚC∑ÚZ©â\»Z\Óå|e\”W¶Ao;\ƒ\¬|∑2q\À\rt-kÄí1•}ÕÖ\÷˘\ÿF\Ê\'è»©<T±ã\·´L<?\¬P\ZjvKQ$m|∫πëìü™ú\Ÿ¯µ\€\‚qK¨]\„á\Âü˛∂˚CÛ/≥7WÙ_OÉwèˇ\0OÅÚu9≤\◊ˆvy\»q9]t·≥¢\„\'s§íUê⁄Ω”óH\\\„∫\‘CêMí™_|÷¨\ÌR†ù•° 7…©\ÀQí\·;¢úï\Ÿa5PN•\Œ∆•Jß8¬∏¢FWHàu⁄ú\“2≥ë∑≈åUj\Õ˘≠°\∆`5¡Eé7ˆÜ~ûì\Ê§ˆWõ∏ê\”zˇ\0E∏\Œ€ø≥Ot˘è\Í≥\…<Æ/UQTÅ§\„;)âSV—ù;-HõCôÔêúùñ¥\Zñ0Ñ\“#L˜HV*$\”5\€aßË®êiü(∆íJ∫j(&f@\nu≠mƒ®mSSÒï-€¨}:e\Ì™ù\ƒ]©ù\‰\Â®:\ÂñMT,9\Ë∑ìsÍÆÅ|\”ACj\Ë+-Û	†–Ä\„˜\⁄A+\ŸCs∫‹Ç$\Œ\“‹å´¶v:yòz¶çô¨î$¨ÿª0\'\€rSHoäiF\ÈIfFSBïNcÛ∫\≈v9õ32∑è§Öq(hµL\Ô&ï≠\Ë±\‡>⁄µ\ÀzÆë˝*à\r\÷1˛\ #\„\€kùÀº[\À\—Ù˜∑\r\ \◊\⁄##a\…rÙ%µ†\Œ+Q}G\Óπ$t#\0T`¨´â˝°iò8ˆÜq\Ã\⁄\∆~=\„á\ËæwÃüÚK˛üoÙ‹øÒ\Ó?\Ì≈ôoe=UM¡Æ\‘\ÁT08\Ào˘Æ<^\›˘ØâQ;/#\’n≥ÖOÄ·üöÀ§Y¿\÷l©kãCN¢≤\◊\”9_9|\›\‘^\'~ä\∆2£∂E\'~±\Ãn∂\Áˆ\ÍFπ¥-a\ﬂ+XC#s9Ãîó7¡\ÊôF∞L¶úúÆq¥\Ëc8]ds¥∞\“\–ZJ÷ò\…\nºf?Ñè\…F*óá©Á¨≤G\–\Ëû9\Ÿ\Ê5\0>kçù¶ûâg^¸i\‹\Í,˝\‘lcÄ∆ÜÅ_su4¸∂Wv⁄¢¨Å\—HCà!TX \»\‹HUàvîsYXî´EJÅÑ7í±\Z\ŒEj!\–\“\n¡#\"$—êJ\≈j@\nEH’≤\È‘õ©H:v`¨Fí\⁄HZ\”#dõ¶â\\o\Ì\‡l/ûöHZÛ\Â\Ó2hÛ˛˘-»çoŸ∂?\ÌÛ?™úìf/SQm~f*Náøê[ëìR=¿Ö≠&\“mnü\Â]m\Ï˜B∆†±ß•`o∫§4í\»X—∞¶\ﬁ\∆\Í)°\«+¥CóH\»\ÍºÒ\È\÷Íµï}ı\÷\‰V°ß`∞\…˝Öü∏\‡≥lÉu§úU\“$ ¶ÕΩ˘ê\0z®âlA\n6ãLµ;V\Á;,Obtç\Ÿ\ ÿ®©o0òVˆHpT+â¨\’%π”ïE\«*áb9aHàu\r\Z\»X¢mÜ^\ÍR“Æ7I∏íPmRÅ’•LØÜ\„¬ù≤\≈˝≤Ω\ƒo\Ì9¸\’\„J\«ˆr\›\\kmoú∏]2Ù\Œ\ﬁ˜\·Jr\€<@ú¯B\‚´8G $¨’ãJJˆwDtîƒï\Ã\Á=Vt”ç˝†§êÒU\Ì\Â5øDg<\‹◊ª#Û^ó?|Ø∑˙fØ\rˇ\0∑≠≠ö\nj®ßçÒ\…#Éôë\Ã\‰r^~)\Âﬂü]|-h™\Z˘¿\'õC∑]ré8d∑Ç@B\ÊÙb∞¶~¶\Ì\…gm™o≤±\Õ\∆Uf\÷]\”II3\ÂkDôn\'\n\»≈∫.\—}\ÔjCdá∏{H˝\‡Z~k}Xô∫Öñˇ\0¢õKt\Ó\‹n∫c‡∫¢ä\Î-ES\·ˆgwzs\ﬂ4\Á\»\Âg\'L!\«\‘:äxº^lW\·\÷]¥îU\rñ0\·åav¬∏\Âí@N|ñ\ÿT›™DmkK¥këå\’\Âóo˘e©õ¡ì_apsLÅ\ƒ\‚ñ˜Ω|úz\‚ÚÙ\\\–\Îı_{O\ 3\◊{Sú·íµ0\€6\ÈA¢F\‘w\nuÛ§\Ì\·oMc09\√uæë%µ*`;©\÷/î\ÿ,±0\Ó2¶¢Õ•\\:q§)®æ@[!\œ%|&©¡oá´Sp—™ö\⁄√§a5)\ÈIR¿\·\‰πX‹®ÒÚXãOc!tBt©`üEN\“7Z\«ñ§öPz-uMí(â\ŒuÜ‹É\Ìjy∞\ÃÑeN¨‹ú\ÎF]@6\Ëç5_gJB π6\€Y˝V\Ó;I^¢∑R\Âç€¢ì\⁄Íûë°ªÖ}&í[\»\"\ËOn\€M\Zlq\  §GÄ\ÊÆ\◊E;¢l—≥NI\…)\‡s˚ïñö™0çèí\·#¨\ ÌöìáE5{%â\«H<ä7\ﬁi\—læ\nÉ\‰∫GÒ&\n∞-\“sZ\€:6%;¶\”F]0ÉûE6∫ZA t:â\∆V~öÇ∑∞Ω¸˜S2F\ÂÖhV\œ2£_4yJê‹êù;n≤\–E-¬öXnJwï∞@H*\ƒ3UL\Ï\Ì\ÕK\‘N5˙î\“Cú@\¬-ÚÙ*X\ﬁ\ﬁ)\ÌñgØ8ﬂø\œÊµá∂k	¿åt|elsF˝¯\¬Èó¶^Ù\·ÜUõ$N, ñÖ\ÁÚ‹©\“UI∞iRcKD\À}[A\'8¯Æìî\ƒ\Ã{\Œr≥înT\–8 Ò\nà†-\n\\\…L\\pz∞ûô\€°r\Ê\‡¸ò¯ˆı¸/ó˛>~}_o4q\Õ<ÙV˘\Èßc‚íòh{dxp;ÉÛ_3uñ´\ÏrYp∂#[¶åIÀåa\«<\ \Ècñ5oK9pkr58\Ô\Ëπe¯\Í\¬k•-+ü+Ú\‡\‹\Èo2π„çµ\”,\‰éY\ƒu\’{!!§\·•«ß¡z?è/Â¥âç}e$Ç0\‡˛M¿\Ê¨\∆D∑*ü¬ú)V÷æjí^H\…\∆˘W¡èM$4\Ó´Ùßªc\‚\‘1\–ı\ﬂLí˙˚MO≥˝‰Ö≠`#\ﬁ=wVc*Kñ%‹ØŒ™£Ã¨tDn\«1\—sºmN]6\'Xe£h{Å$sú\‹v\ﬂiµ\À\‰\0\◊L\\r™éjÆ¥Ù37,ô\Ó.\rv\‡5ß?™\÷S√û˜7<\r\√\‚∂˝O1îPñ\Õ9\r‹ÜùÅ>§-¸~.\Ÿ\ÕzOì\Õ8¯\Ìæ\Ôßh_YLUñ˜N\»X≥ó¶2£O\ÌÄBªÚƒõçU\⁄!\n€µ\≈)Ø-\ﬁ\0ä3 ¬Ü\≈\ﬁ Px)£hı≤b3Öc5õ´wå\Âc%à¨x\…åÆöHéAÖπS@]ì≤¢\‚\ÿ\“Y∫\ﬁ5ö≤c\“\r\‰¶\◊Ng€¥L</Sëˇ\0,≠\œU\À/\‰Ûùt!\÷ˆm\Ã\ :5?g¯Cjeˇ\0öWYÈçΩ3måö}j,Zp\’\Z\\]å y≠YXV\0E\»\0Éõ∫i\Z›úWù£}\Î§x’éjlh\Ìø\‹\0ªcÈî≤“µ†ár¬°≠Úàjfù\—\È§êí0´mr4∞\„\ÕLU2GÄ\ﬁkA¨µ¿ïb!TóÚD\—£G%a§\ÏÄL\–\Ê\Ï\0¿\«TÄ\ÂãQ‰®ïH\÷5ºîxù\Õ˜\‡tA\„^\ÿb˝¿\Ó5ú}åOg0Ò˝ïÆ\Z¶\Ât∑\¬>Ñ\ÿ\Èce≤ \⁄B\ÃjCÓççv6P(¡òB\“iCv§cd\»\œ5õ-,¿6&É\‰êc˚]\Ïﬂá8û\Õr∫T\”K\∆*9^\… óGxˆ∞ñ\Î¡\‹\r˘˙Æ<ºe˚æﬁÆìûß\”»±ë,,ïØ\√\ƒaò¯\·x+\È\„V6\Íñ\0‹∏∑˙.yG\\*\ƒRâItòyîmÄ≥èÜ\Ôñ+âe´∞VG+m\r®§{¥	\€&4ûö∂¸˘/Fg=∏\Âº<\È¢\·ˇ\0⁄óq´ÜéF\◊\∆\–5d¯Òéù2≥fö«ö7\÷ø\Õ_UD⁄äVrtú\·ú,ˇ\0\”_\Âcå\⁄ÊáÜ\Ó\‚Çj\Á>ëèãV@i9 \‡˛àπ|¨-òœ≥w+]˛û	Âöéí°∞µÆ\Ÿ\⁄I~æKrycÛÒ\◊!ìå\‡\‚ye6\ﬁ¨l\“iíØ-\–Nq∞\ÍÆXu˚I˚Ω7ú\rQ4µ1µ∫\›õó\Â∏\0éG\Ë∏e\Â\”©Z∫⁄ê◊∫68j`$ÄÆ1ú≤#≥>∏ÒG\‘\‘EYQ–≥\ƒ˘v{\¬y\◊Ú^ú8o$‘Ø&_#ª6Ù\rÇ\œKc°4Ù\Â\œsé©$w7ü\‰=øãäq\ÕG\Õ\Ê\ÊÀó-\‘ˆª=WW\«\ÓN\È±µÜV~\÷O\rHç\÷\„+\niuçîYOêQKdyÊ¢ó›ÑŸ°ÜÑŸ£q\Ê2¨KäˆùnX\»\≈≠v£\Õpuâ¥ÄµG7Ê®ª∂∏i+x\÷±\ÓàR+õˆ\Èè¯N¨;¨etûúØ∑ù•n´c†\\£m7a2\„3¯•uå=+B~\È£\—e∏ü$(kpäZä\0@a\0A\Ã\Â˜Wû¥iá\∆>*Djmc\ÓFW|}2∞#-Z\Â\Ÿ\rê¡ù\—†` Iî6-$%ûQ[jù≠¡ººñqi6Y§êç˛KHì≠(†ónõR˚Ω∞•4Aâ!°ÜxQHkpı6ö<\‡trWfë$®tY\Ÿgbáä..ˆÄSl◊ï;Y\Z\ÁØıvS0<8ˆ\∆pMc\0?5\“ˇ\0ÙB\ƒ1lÖß£B\∆-ùû\"\Ád%Å∆¥ÜrV\nk†&Là≤µ«àFBH±*¢\ÕO$/˜dikæa4O\◊QõW\‹,ÚÄ&¢ûH]ë∞,qnW\À\Œiˆx≤ó\ 0â\⁄07,;;\œ7h±£Æ.ç∞∑:õÅ∑OUã\Zï6∫(™`tR4:9;¶7N\ﬁ\‚Æ\ﬂ\∆\ÃC¢Ä\»\Ÿ4\„ êF†\Ÿu\ﬁ\÷pq\Á\Ìπ\·\ﬁ8ø\“\\*g´∂¡<R\È,\Ór0\‹I8<ïû.\‹˘?O\‹˝µ£¶\Ì2†\€\Íiõ\√“â$\—\ﬁ<i\‘\Ô<n∫wÒ≠9c˙Vv\Ó\‰áæ^xéäkh¶m=DméM\À9åÙXπ\◊l>úÆ\’ﬂ∞\Ìˆ~m≤Ü(\‡àa§Å\’q\À-¥n\ÀP\ :31\‘2\ZR2¶1\«,¥DuOd\‘\Œ\Á\Ì≥\ÊÓøñIm”µ˝ù\ÌÚ\√¬µwIÄ\·R\\√él`\“?=K\È|lué\ﬂ/\‰\Âºı˝:5Ftú.\Ô1ò\\qπHÜ.o-Ñ≠D¨c\ﬁ_T\Ô<¨7Ù∞Ö\Ô\∆UM46óE®¢I\ÂjZ0ë≠\ŒJ)]@\’O˜n¯+ä_L\’hÒ8¨‰ò´¡√ä\‡\Îâˆªeb\Ë®\⁄urU4¥∂µ\Ÿ.òπ\’\‘C¬¥Bä\Á]∫S:N©x\Œ\Ã+r¯r\ yy\‚6\Ê\‘\√\Ëç7aP\ÊÒ?ˇ\0⁄∑Ù\«\€\“Ù0}\”s‰≥∑M&±°°CE\"Ä@h%jh9§á¬ºı£Qü¯®çu´xG¡w≈ïã[ï§\"XA\ *>ù9VD\Ÿ/asr´&*\‚\"§F∑Ç◊ê≥å]Æ\"h:VÙë9É\rY\”Déjh(çëv\r\0©£`ZJ@åe4õ;†¶ïYuå5§Ö,\Ó\"i4O9Xf«õ˚Pgˆä–µâ\⁄\√1§\‚´UKy\«T\«˚.üH˙%\√l¥\“~&˘,\‚\⁄ƒ™ùêR\›%\róin:°\—îäÚ\'⁄ÉÜß\·\Ó“ønCeæÚ\“Ò\'6˜¿}\„><ùÛ>K\≈œÜÆ\ﬁÔçûÊøß8£~≤\ÁÇ·úú\ÌÒ+\Àc›ç(N\ÿ\Áv3å\„m≤∞\ﬁ\◊V©ÖD:\\\0#\»\Âb¯u\«-¨¢\”˚\Êjoû∆∑€™e5u\r8ikÜ3Àí\È+s\‰kö\n\ d`Ø5©ïá˘û–òCsh\Áú.w&7rˆ≠ºM\›\¬\\\ÊÇ\“@>É?\Ï,I∫edä˘k\Í#0Cê6˜\‹˛EtìO-ª1QQ\ﬂ\÷\√n£å\‘I<≠Ü—í^]ÜÄ=r\\1\›p\œ=G¨¯R\—ãá(m?X•Ö±ó˛#\‘¸\ŒJ˙xŒ≥Oïï\Ìv∞ïÅ\Õ+[gF8\ÍõM!\›òHWic)0ˆá\Í;\Âq∑ÀÆ8¨!\—YSM\rî5î¯\Œ\ÎR¶ì\‹@’ÄFF+(\0\¬™7a\n\∆j∂xZF2j3Û06°\Õ\œ\"∏∂ùoÖéwàd+å]≠¢§a\0¥m*\Zv\«\»-\„4\≈Úí’§\Züj\‘\‚~ØoQè\‰Q-\”Ã¥çÕ°ü\¬ùµ=Ç\0oı,?\‚∑}3èÚzbúb!Xu:9 h\r\\p7A]WZ\Z˝\r;¨ZC\–\Ë¡\'ö‘Ü\‹\‚Iôåey\⁄nÒèäHç≠ñ<”É\Ëª\„Èï£YÖ•\∆\»\"∫2\\vV3°˜xjl\“%cáwè%-í\"R\»J`X≥É\ﬁh[ed\÷¯TlAª\ÂCCsvMÇhPCNP-£\Ï\÷˜$®åOê(_è5\Ãy”¥\∆Ê™≥>KX≤\ÂT\ŒªQ;\ V˛´∑\“>ÅviqäªÑ\Ëú\«[A˙.qº}4\«U$çä_’äj\»€´\ZäÕæW\’YŒ™\›\Ê\ÿ\≈5Fúo\Ìqo\Œ)\ﬂ\—\‹\Z¯ü’Æ\Ó\‰\«\À\ÕyæM\÷2Ω_\ŸY˛ûI¢≠1ó\√$b*∏\√Cö\„\Ó\Ô\œ\‡Wí\Õ˘{e˙\n™π.cÒπvv¸\÷4\ﬂe•Æ∏∂FD#m\∆\ﬂUõãxd\⁄CWK$\Zö\Ïí98.]k\”\⁄SB#Z\◊D\ÿ¡\'\'√±¯etûÚõ[\ÿh˚ÜáI[†\0¨ÚÙV˘1öjbû\”≥chœü˚ûùw$c¯Æ\‰⁄á\«úE^A¡#ó\”%t\√<‹ô\Ïäjëo∞M3X‹å9\«O\√\Í∑1\›r\Ì®\ﬁ}óx]∑\’O\‹\Zde3Chˆ\„Kùêd\«Çƒï\Î\‡í\Â\”\√Ú-ì˛ﬁå◊≠\‰&GÜ¥íÇ;\'kÛÇÇ-\Õˇ\0rpf≥TÚ¥\‘?<Ú∏\Ÿ\Â\”\·6G\‰Çê\⁄\÷\ﬁ\Ál\„)tÆxµ·î∏É¥®–ºZÒ\—E50%•Xï\n†<4\‡,’å\’WÜ°\‡Û\ \„}µm\Zâ\œD\≈Z=\’\÷3≥\‡Ï¥ÉÇ\«i\Œ-\‡€Ü9˜.˝\n∏±üßòm\Á6Ü¸êi˚aIR˛@J∫}9\œoL”ú\ƒﬂÇ\Ê\ÌP@@\‘\Ÿ\“pâTsB\„\\	\ZÚëw\Z#mt\·Oru∂\»\\@.¨\„Ü⁄∑ ∂\’\≈b¢•å\÷w)\”H\Óº5˜ñ\Ë\ﬂ\œ-Y4ëhZ™à∑!{°îM,ciU\\\œ	Yæç°fO$\≈0∑\”i•õ=’ñ†uD§r*\ƒPEE≠´\Ï.\∆P\€3sª\À&ZÄ≥j(Æ\Œ\Ô(∫¿\‡ßOR\ ±Gkœ∂”ë\“A˙Æ\Ï}Ωù\ÿ}dç±F\ÕG\‹^}˘uéßµi# ıZïtò\◊eõ≠#	∆êâ\Óc|\œ/kç\ŸY0(#†]k8ß(”ó}•@wR7©Øoˇ\0Ø\'\À˛˛\ﬁœÉ¸\Ô˝<ì\≈\÷nı˛\€ª©¢i√ò<G\»zØ_O&Ú\Ãw\œˆyc™dªin0\◊.ö€ñˇ\0≤©*\Ãm\Ââ	\‹|î\“\ \’p\Ìlè`\’#[ø\'Ω\÷n.∏\‰\ﬁXfcòuGç^&ûy\À\—gNùñsV≤7i|¨\ÁN˛_™∫;!\\´\r(kﬂÉ˚\œ4Ç75&,\‹\ÿ•k\ÊΩ\≈\ŒÒ8„õèê˝Ykñ\⁄\·˚óV\√Yw\›~8\‡iûg\Ã%\À^\"\„áo5\Í>«©ôOi¨∞1¢F0\09aø\ÍΩxØ/Ãø∫6\Ì;ï\Îx…ï∫öB±G\0`)†\Õs° +±UN0]s≥äÂüä\ﬁ\√Ko¶09\ﬁKxƒ´äxÉ#¬®x0&\ÕP\∆˘@†ÄàA≥\rtzÚT\‚9.\€X§Z\‰\“\‚\‘\≈Wë\»tÖæ…§»∑nV\‚ê\ﬁj°Mp<ä\«iƒû≠n61;?BÆ,\Â\È\ÊYˇ\0¬áßıDk;ê7àk\ƒÚ[ﬂÜdÚÙù\Ã->ãõ¨HêÄÇËÅ∑åÇàØ©:&¨\“$GSûi\ŸtÛ\Áj|˘˙Æ\‹^\ \≈≥ıOLq\È˘¨\Á\ÏzßÑá˛O¸e\"\„§\·\0$ ´∏É§¨P›µ£\n\‚\'∞x÷Ñ\∆{®™ Ääk9*\∆JjË†Æπ¥\ŒQc/p`\‘t¨SJ\ \Ê;ÿúp§K+µá{Qé≠X˚e\ƒ\”W§Éı]ò˚{±)Z\€M>v°yæ\›\„¥\¬ˆF0v[˙ı’éÑ8Äp≥\€Eå] ≠Û\◊∑\\Æ{≠L|5\n◊∂\0\◊nªÃ∑ëjn\0~\ÈWj\‡›∂Ò´o∑q`§\”\Ï¥.\÷\ÁuíM\⁄O¿n\Õx˛]‹ëÙ>:∂π}U êí@#\’x_F\∆&ˇ\0mÑ\ CÛ\›ns§\„\”kÆ5\√,T&Ç≤!\ﬂGx7¿\€ yï\”nZßh.BÉ$.kòr\›4L¥\”[¯Ä∫7æ9úá w>üöuk∫c/∞µÉY g$ósâ\’;ëUp©∫J)hi\ﬁ¯›ú∏\rº≤JI§\Ìo¶´Ñx6\"»§´cÖH\∆\‰\‰¥o˘l•ª\\q”•\⁄\ËôKé0÷±ª1£†Ë≥ß_•\ÁÒ\ƒ~;ãÖ*\ÀD5\‘\ﬁ\–\◊\‹~¢\‹¸6\ŸÒ}X˘ˇ\0.y€≤êΩ/!©ü†z*à\Ê†±SjãS>#v£Öv\ÀsîKui\Œ\Õ9\\3ÛìÆ#OlØci\∆]å\”·ä∫∑\Œ\'à<ÇµΩâD\·XÄe\0\Œ\»+n\ríBCBõ4°¨ÜH\ﬁ\\\Ê´X\√t2ÜOø\"\Z^A;t{√íñ∫\„4≤4\≈\Õu\¬¯s\ j™∏íµ\–Sb7a˘Y\œ;=&8\Ïé´ño\ÔïxÚ∑\⁄Y£||\∆\…\√UÄü˘N˝\nÌãûo+€ü¶\‹\Ê˘?5\"5=ãøˇ\0∫*á´Jôµ\«ˆÙ\Õ˛]üç%H\0q\0îFz\ÔR\rPc.k9®LyYn8\œi\Ã\’c?/\’u\·æR˚sæ©kj`fF\Œ#Û[\Œ9˜ñ\È\Î.:¨t\Á¸Åsj.QDÄäøí\nÀé4ä∑\Á1ZN†µ±6?uP5¢∑\Z\Í´%«íã Ä¢£\‘SôòBòªQKM.\\<\'ëY†\n-ÆYÙR#Åv\◊Dh\ÎfaÙ`å|Uû“ºˆ·™±£ˇ\0w˘Æ\ŒO\\ˆB4\ÿ\‡Û\–ö˚z#≤Ÿ≤\Í`I ∏©WHÅÖ\€tL†¡\\[¢¥ªW∂\⁄3Åç∏Ú]ÒÙ\‰ç\∆˜vŸ¨3\ŒÚ#Æ£\◊\‰7U®Ú\Â\≈\”\”ˆÅ ó˚öö∫2zπØ:øPºü+\–¯ul\ÿ[(\“\ÊÇ\nÒ\«\–S\ﬁm\Õ\√¿kã@¡\0go\Z\∆Qööè∫{±ù®Ôñåc˝ÙZˆÁ•Ö\rÇí¶Úx\„s]≥@\Ô¡Y\·: ∑ãÄm]\”@ß~¨{\¬LüEÆ\Ã˛9W6\ﬁµ¿\Z\Èhb\r\r\’˜ƒ∏¸<ΩRnùd]€≠QEª∏\„Òº<µ¨¿ó\È˛˜WK\ZöZVí4gPÁÅ≤äù†å`Ñ—∑\„k§îüh\À$@ê\œ\Ÿ_\\\ \ÏØO\∆Ò\Â\‚˘>|ßØ8&\‰nVHÃé\Ã\‚9<œë˘Ö\Î\ jº8\›≈ΩD%\Ï ñë£ß,˜õÖ âráTn\ŸTbÆtèé´Ω`9\ \Ágïï6\ﬁ\«\ÀMªHIm?1ÒS\È,\Ï∫c[<\Ï¨)l›®∞	¡P†<lÅ©h\'5v¢º\÷C†¥]\‰πd±ù\Ôp\‚VZâ’Æ◊ßr¨\«mw\“˛\ﬂ;\›+¶3Nw-´Ø˘s	;\·s\ÂçÒö\·©$8ì4é=ï√á™Äˇ\0˛ãŸÑyπ+\À4.˛\≈ ÚsøU\È•\Ï~°∞Òd\·\«\Z¥ïl\⁄cñ´\‘\…C\ÈòAË≥≠7¥¿SJ5D\·5˜qóy*õgk/2;SZ1\ÍπˆM+#ëœòóoì\Õeb\Óô\ÌÄRWy#évö\Ì6˙◊á€ñN]\¬\‘o5∞Àæ\Ô\'?5\€:Û\Ã,\…\ÎÆˇ\0\–)øÄ..\—vä†Kπ ¨∏\ÂbáhX\—¿&!¸49hHh\ŸP€Å’≤ \Œt†o´Ù#EÖûEM‰Çõâ\◊¿#\ŒVhÅCYDZ\„Ç\’p^\›gmMS\‹\ﬁMã˘´=£\ŒX\≈\œS5\€\È\œ\Ì\Î> ãød@G\·Õó∑l]^\”Taå4íGEû\⁄nM¨\Â/ñpy-˚\€\‘\'ø\'.x´Kp¶∑R∫¢Æf\≈NN\Á\·\Êªc\Èá1\‚\À˝E˙\Ê\Íô2\»[\·Ü<˚≠˛ß™\È#L\«⁄Ω¶\«\r\÷j©∂øº€õ£;=øM˛!N^>¸mrÙ\ÃÕ∞6X\⁄ˆê\Ê∏\Í\…÷üg∏õ-6[éòM+-x∑ÜH\Ô	,;ù\'uc6\Zµ\ \⁄YòKúb\Œ\Â\œ\‹Uø¶]\"\Ã\÷\÷\”2v?`¥g\Ê¶>ZÙôW£wvCÄ\0\Ív\ﬂ5\÷G<Æ¬ôíM \r\ŒiƒÑo\‰å∆Ü\√CF=T—≥r¯sìÑy\„y\≈w\⁄:\‹\ÿ˜ˆz\“GLdˇ\05ﬂÇxyπ˝Ω5\√7ZõQä™9Æ`a\‰Ò˛˙Ø•q\Ì*]V∆õè\Ïë±\÷I5èYY\‡\œÒ˛x\\∫WYú≠-U5dzJàßàÚ|o\Ê5¶ç\’\ƒ”ÑE\r∆ïÆ „™ï-Tç ÄëV\‘—µÉ\0*h˛2F0HG$\ni@2ÅcA\'ñcÆå®ykÜkä∂Vi¯,4üg•lèö\ÎÉ5ßÇ!XtE]\ﬁ \„Ö\œ<v\ÈÖ\—V(X\◊\„ex\Êò\Œ\Óó\∆QG%ñ¢7ÅÇ\√˙/F9˙y&ô\⁄YR\–}\Ÿ\\?5èµûö…©_W\∆\∆6ú\r\0ü™\ÈãoU[i[\r+æ@Xµ\◊ñ6Qtlá\Á¢ïls\·p\œE6åµ\\éB\“:Æ5`†a\ŒTˆ•k©\Ÿ\»˚QnÆõÉ*Ò_\›.>ÿÆÅ\ﬁ\œL\‚?yw\œ€ïzÇÙ\no\‡í\≈\‚\0Äè$	w$∑å\ÂfÜhd{Yå¨\‚ß\Ÿ\ﬁ9\Ÿ\›Qc\\π[àP∑d?gB\ÿQay@áü	AKs`sâ<\÷/µS\÷0H=+.\r⁄π\‘\È\…<òUºXyˆ°\⁄n\œÙõ˘ÆÛ\”o\\v8\ÊIeá¯Úg\Ì\ﬂYµ@\«Ï≥èö\È\Í.*%•£•|ı2\≈,s\‰pk@ı%z$sr?\Ì\níIM`åIçùU#|?Ù∑Øƒ©¯˜|¨∫s®\Í™kuT\‘\œ$\“<˚\œvvÙÚZìAM\".¨!í5IÇ◊∑+∂f\ \≈Bl\◊i-Ævaw\ﬁS¯sª~GoÜ\Õ˘<]2}_ã\Õ\ﬂ\Ãp˜ç\»^m=R´\Ó¥\‡í7¯(\÷\‘N∑Ä\“\“\ÕG¢}2\“peC\È£}$\Ì{K]©õ\Ï¶7\Àw\”K!\Ôs\‰ivØ≈π\'\Õvé5gGM{µöI\ÿ\Ó´)áª\ÏºCZ\ *I&q\r8Qc\œ\\ÆÒ\⁄˝\ﬁ\Ëˇ\0`¶\’\œ˛ÄØWÚÚs◊ß-Cºµ\¬\ÔÚÙgß\À\ j£\◊—≤Vñ∏d\"¶\ﬂ\⁄\—Pf∑\’\œN\Ì^¸N¿#\‘u˘´≠ù¥\◊\Ÿ;HÆá\Ó/\Ï™`\ÿ\Õ\–ˇ\0ò\‰%\ŒÒO¶\Êm|AmπµÆ£©kúO∏\Ôá\…p\œãx\ŸWñ∑\ÂõÛR*\∆<ì≤™{8\Ztª\ÏÅ\≈\Á\– X\'\n\ƒP1Tˇ\0\›Efk\”PB\„ö\„k\Z4.m&X_å.∏_÷Å≤<\÷ˆàı-§ïEuèé∞\‡\Ï±/í°ˆá_,\\;R\Êú\Ï\ÔÚ]±Æy\Õ«ï\Ë§%ï9\Í\‚Tæ\∆ﬂ±\'¸t]Kèvô[ó\√oTBOt‹å,;Aìæ‰¢â\ÿ¡DEöF\0A(ä+ûó;bπR\"\∆BAaõ›éK§Ù9øXj/ñ\„I[©∏.\«%Àé\Í\ \Ôï\÷\“\Ïù\√o†â≤=\“=Éô]r\Œ\◊\'N\·∫f∂E\‰—ÑîY™∏lÇ≤ΩÖ\Œ\¬≈ÅTë47tÇ[@\ná\⁄·Ö†E\√(v\»yÀïåçáQaA¡M®§p\r)±QZ¸íπ’å\Â\ŒC\›»å\◊\Ì?h˛µã>\›e\⁄_\„wûúÔ∑®;¨pµB\“p4\‰\‰ˆÙ`\Ëó=°µBb°g∂’ÅÅÉà\⁄}O_Ä[\‚\‚\ ˘≠eîû\Áâo∑{\‰\∆{•[\Êh9lCh\Ÿo/\ÊΩ]5ˆ\œ^À©≠íJ=Ú›Ωë-\–wt3\ 6˛ãó∂\·˝8&Y\ﬁYR¯\ÿ\œ\“gZΩæèΩÑiã\Ô`#©˝\Ê¸\«ÚNn/…Üæ◊ÉóÒÂø•%ñ±≥D2|X\‹y/ëc\Ï\„|,+#è+67*=+&A	¶ó445\Õêzô\ÈwMb\Î#ïJf¨ô™ù±∞ípã«µ\Ÿm∫X\⁄\Óâ!X\ﬁ√®O≤\\ÆO:öΩ ˙4cı%z˛<xπÔó£l\‚\—#}!{\Áß\Õ\ ˘ë\ÓUHfhÙ\ƒ\Ác`Æ)Y©b{%xv\Ï\'oE≠Üµé?Oë≥¶£Id\‚ÂΩ£∫®Q‹îgÛ\Êπ\ﬁ9ZôX\ŸY∏˛\◊!\‹·ñÇO\ƒF¶ò\‹};\≈g¶\Êq´¶´¶¨Éæ§û)\„<ùÉá\‰πŸ¶\Áí48ªÆ±§JÖr@áìçÇõ*ƒù\Ÿ:JÕ´#-RÛ\ÌDu\ \Âï\\gí™)\Á0óasÚ\È\‡v\‰v\Õi8]1a|u\∆\0p\¬\Ÿ\Ïz_#H\0ï≠≥•|ë∫)¡sp±ø+•hÑ∫¡P:\”lyfñl>•æD≠∞\ÈgJ∫a≈µ1\»\Êâ_tg®\…\ \ﬁ8\Ì\œzØPµ\Õ\Ó¡\œEçyv\⁄4ÛÜº`´\È6r)\⁄G5ïÇöp\÷U\r]Kù)¡+5îW8º¨)MåÄU\–S2\÷\„u∏á\ÈbÜùÄ\09\·ªwJñ®∏i`¿MããA˛\»’º}\"j\–	H\"TÄIY°¶?KT\ÿ##éwY\⁄\»~6\Êµ)†\Ô|K[d\ÎNZ™íF\Ë\…e†îX5\Œ\ÊVtß_[å´°ÆçÅÑ\‰\ÂN£7pÉi\Êi\√˚LÇG\÷T¡˜p÷åíµã\ZÚ\Êt›õ‹Æ7\'\‘\÷\Ã\ (	\„ê¸πô^åwY∏∫u∂ïñ\ Q¿_›Äå\ÓÔä≥éK∂∑˝,\‚§sZ$ü\r\€\‹>+¥û˚!∏˚ENÜÄ\”\»,_5π\‚+8Ω£\ÿ\Ã%´<$[µÅëÅ\–\0∏\È\“RKrS¥\ÍÚWl\Â\È~˙ò°°\Ô*fÜç\⁄˘d\Ão\Õwé,_D-\◊H\Ó§:í≥\ƒtÜ?Ø/>UÛæO\\ªO∑\”¯úΩ±\Î}≈ΩÕ®ßnvBÒΩ∞B≤@GDn,\Èf#\√u#VxY@\Ìc}ñ„ïá‰ï±FJ2˜´ñ\Z\◊l:®≤9’∫†H\'¢\”5∫\Ï\Œ\œ\Ï\n*b\ﬂ\Ô˛\'ü\’}èÜ±|\Œ|∑kØ\⁄(cã\‘ÒS\“«úîDZ®ã\Èe`\ÊXqÒ¬±4\‘SH\–\È	ÙUQ/nÇ=a6∞˝émQÜπENÆè\‹zÑå£\€Êûä_h¢®ñöPy\∆\Ïg\‚:•í¨∂zt„ò•¶ΩÜ¡!Ÿµ\rcøàt>ºæé\\_”Æ9ˇ\0m\ƒndåcö\Ê∏dr\\]\ÊÄ5É\…\'èTd%,´Ä\›¿#l\Óº˜\€s\“˙8Ûv\∆YŸõ+Y\ﬁH\Z\0√ìõX’¥c™µa˙HÄèí∏\¬\‘\‰a∞Å\…g9\‡ïÇ\Ì±±ÿ¶qpB∏\÷myIµ´´\∆¿í2∫0º\Ï÷≤hxŒë–ºµ\ƒc ØO\«\ K\Â\Á\Â\∆\Ÿ\·\ÎªEmK\Ì—π\Ó\'n™\Á\◊ma\€A5DÖ˘\ Úg|Ω\œ	¥{˘î\«\ ‘©¢vìíÆëSQóïõf\·n\Îôt¯9-\ÓÿÆì\“y\…,\…9\\] ˝üˇ\0,L}	\Î@ íªíqM\Î.p’åÅ\Ê∏\ŒL{ıﬂî\⁄D∞FX| 0∫Y4±^\Ì≤&\·PB´¢§\›^\…\’\"ú¯V±¨Ÿ°\»\–rµ¥\“MSDdÇ±\"=5\ﬁ;;\nL¢\Í¨pß{u5˘Wh®º\ﬁ\È\‚çƒ∏5£õúpñ£z\„Z65ÒPDj\Â<\ﬁ|1è\ÊS,≤ˆªëâ®ñj˙áT\Œ[©\Á˜ÄΩqH\≈\»)\ÈÃíÄ]=F=”∂*GL˜U\ \ﬁ^\‡ÚLf¸ôx+\‘\⁄Açß\ƒy˙-etò\√∫}-/wUåcV™8•Ñ±¯∑Z©ç[IÇÿΩX\n\ÁZÑ\È/0lKé¿|\÷z\€\È{IÌûΩq#®…ß¥D\Ÿ\Ê˝\Íá±ü\¬?x˙ùæ+∂zˆ\Ás\€%r≥V\\\Ê7: ô\Î$\Ê\ÓÒ\Â≈ü\–|N±ù∑ùá\nIi\Ê´wÄÜ\«@\√H\Ê>i1U¸r—äôcô≠å±\ÿl¨¿xÛˇ\0`Ø77\∆\√?^+ø\…œè«∏∞lQ\…¶ΩØoB”êæ_\'\\wY>Ø6<ìxÑQ4\Às”º©1\ \“\Ïu	)bÍ¨≤2\—\ÕRF2Ò?\›\Ás\Ë\ÿ\Ë\r\∆˜ZsÆO,\rÒÛ[\„«µs\ÂÀÆ.¡\¬pfüXfwmÍæ∑¯ºó\ÀkDYØ{Å\’¿-πl˚dé_\Ó\‰cˇ\0Ö¿˛â§6ÊÅü\ÕXà\›\ÿ\rUb≤¯¿iIEä;n¶\ \‰Z§ÖDWJ\√ûä°\∆\0XF2ÇU£àØ\0\÷\—NäF¶|ºæK9a2ˆ\÷9X\Íú%\ƒ0_®]+#0\Õ±ìú\‘°y≤\¬\‚\Ìé]ó:Ç\À@H¡AéøµüµÅo-≥ı^l˝µè•≠;ÅÑ`\ÌÖ\÷_ö°ÖŒ™y`¿\ Ap)Ú¢µ†\„†`ï÷Ñ[îOñù¿\rΩVrÛ\Ìm\Œm≤°ö¿Ú\ \Á=•õyå\»_8\œ0Wt\”CŸå¨<aF\‚Fêy´Ω$õ{\Õ$\⁄\√\ﬁA]\”P©§ßy\0Y±©R®+i[∞xWñ¶\œ_L\0\Z\∆˛´Zf‰•¨∏\“˜\ÿ\÷1ÒY±%De‚ëÑ\Â\„oU\ÀMlßqa&F˝UÜÃ∑à(HŒ∂ö\Ì\'Ü.ZX\ƒ@çy\›D<\‘“Ø,“é\ÁIVg%\’m Ö\“YA™ºxJó\–\√\›\›+¯âë∞∏h\‹`\·~K\Ê\Áû®cå˙Ú\Âv\”\”:~\‡	esé:Ø\“Òˆ\Î\Ê∫\¬%\0\ŒΩ5(òˆ¥l\n\Ásë©N1\Ïìl©és/\rm&6\‡lΩ\«;vf°Ö˘\0¶ìh2P9\Ï∆¢ßMµ2—ÅiÒj\›ja§πZ\…Òı˛>c)©_ï≤\⁄]¥C\Õ\√◊†Zò≤\Á\’5ï˜i-mK\Âa#Ky4|\Ôáéw!H¡©¥Ò\0˛x\ËOÙ\Ã˛\÷q\”2A8\ZB¨™ûp∏6ùô\–IS\›\”s\ƒ\€ES¶äÉDMV˝G9\Ê≥\Ìß2I\ﬁIπ>küI≠è\r8* Æ\ÂB˙Ü∏còC\—T\Ì}=7s$í\È\≈\«\rhôIåﬂí\ÂS\ÎhDë˜.>{†`}lun\Î9Widr;\ÌZLL¡\Ë\ÂµπåÛ.w√¥Ú≤}™	ò*\È\Z\Z:\„˜J\‘Ú«òDLñù\«TzòNvVz\È©v∑≥U¿\È\‹‹Å¨`É∂\n\·\œ\≈\ﬂ\r;¸~Nô\Ì`KA<Ü\Î\‰_kø&É¿ë\ƒes˚u˙Q\ﬁ\Á\rsãà\0jí≤\“¡=is¿-ßæv\œ\√˙Æ\‹|?7\”\Õ\ÀÚ1\√\ƒˆüb¢lO\”a\œy\«.^´\€«åû#\Árgoõ]\ZÜüE+X\Z\Õ8\0uı^\Ãc≈ïWVpı∫°\Â\”S±\ﬂ\”nqûüÑmí\\≥6JY\Zr\Ÿ!ycö|¡	®≤÷ö\«ˇ\0\€\'éû∂_\€.8;\r®ã\„\—\„ÛY≤.\⁄)\\ùˆıYj).S˜ür\›\«T\–b\Z}\0#Q6ç\ƒdEDÅW°êê—à\ÿ\‡1ÖATDLy#®DK∂UU[*#™§ê±\Ì rpÚ>äeé·ç≤∫ïû\‡Àïæ:®â\ZÜ\‹˚Æ\Íã,l∫z±ªá\Áñp\¬\ZrV-≠xRõeEMAë˚g©X\ÈjnE\’\"åë\Ê∫cÜô∑i1wlv\Z}àê√ê™ï≤\nû%ªSZ\Ì\“O6ßê”Ü¥dïåÆ†Ò\œj=•\œrø\÷P\«£kNU\«¶\‹\ﬂ\\èê…ús]4\Œ◊ú5R\Í\Z\ÿ\Í#ë≠sw<’ëù∫\Õj5pQ∂≤2@\Ê^µ® ™ø¥\Î\Î\Â!ΩŒì\»‰®∞™N\—.Ò«©ıL.=0vI4ó\…\Êˆãzîú\÷\0?Ä´¥\Í\'qï|æıc\…<\»jûH≥Òk¡™õU5(3]Æ5≥ˇ\0‹öáîSpØ\…˛\€?˝\ÂX≈õz¢yª∏π≤ÛZÙ†~\”c]ámîå€¶≥á\\\Ÿ\Ë\√\«5õÑ…®¥hs	\ﬂ!cr\„ø\ÈNµ\‡ØF9Jï†Dç\'*_C,ªà\‰\0çM#\‰æòeÚÚ≥€ñ\Ê\◊bp[à∑ı_V\Á\„\√{0ˆπ\Á:◊ãóΩæ\”b˘\ÃT\«ØºñS\—E#7Œ°\Êªc≈ñ>Zô&¡&F\nˆq\Âπ\Â6wuP\0y ¿vï\«?≤ú˚Mù\Ì5\ÿ\ƒ\”s˙Û~ä…±\»e´®t≤Ω\“9\Œ\‘˜<\‰∏ı\…]0≈õt∂ççÜ&ù@ù\◊w$\€5/ΩS(\›‹≤§-7y®\ƒe≠‰ñò§p\Õw™<o\‰ò\ƒ\ \Ì:\Ë\‹\ƒí\“b©ßg{Q§rYoiïL\ÓŸÄå\ƒ@\·¨T\⁄(\ﬁ4å´=49~J\÷\”F]Gú¿VT—ö´dnÖ\Õ\“äö%Qç\√1ìÇí\ŒÙﬁ∑\”\—\«%91n”∏=B\È.\‹˝ TZª\‡eÉ\√+Fre\–\‚öI©údO˚¡¯õ¯æK\Ê|\ﬁÒˇ\0\Î\Í|ë´\”/˛#\\.t‘îŒñY\Z0<\◊\Õ\≈ÙÍöññ{õ˝∂Ω•êg1Bz˙ª˙/°¡Ò∑˚≤|ˇ\0ëÚµ˚p?\‹\Õ]Vii£/-ivê^º¶¸Gázõ≠Op¯¶\ƒı=¯Ÿ£ê˛´xqıˆ\Âü.¸FÅë\Á9]v\‚çYÜ1\Á\»$]*≠L\Ôk\\ÛÊ≠§iZ\–\÷¸ñv®µ#¿IQb®1ØòÜåû•3π:¡\rõ\r¡@\„FF\Í-ëè$\ÿ\\êá0uHÅQ\0\–1Ê¥ãû∏{\–R\ \ÏAQÜüGt?\…q\Â\√snúyj\È\—K,/3∫9ys\ÀbawØD\⁄h\Î sá\ﬁ;\‰UNÜ≤6ÌÜÑÒ]\Îâ=\ÿ\»ÛP$H˝X{æA6)∏“ó⁄¨s±£≈§\·,\‹GÇ8˛/g\„\⁄\÷8`ìïº=3P£!mîò\\Lâ\ 	Qú®öHâ¡Q&7àﬁã≥Õë˝π´E€™è\\J÷∫1û´«ùz∏Ò\›b∏\“v\—9éi#|.s?\r\Á¡\Áqæ\‡\nñ∫\œú\ÓkØ[q÷ö≠m-\ŒF_áH\–Ú\ \·|_\0ùT\∆4óK¸úx\ÊÚM©\Ó¸Aº1\Ÿp√ü\Í∏_y¨eûú⁄ä\Í\È8ÇWπ\Á\\á%†ØëÒπ/\Á\À+ˆ\„2Ú\Ÿ\√tkbB\ \∆ı_[/ùÜÀ§\»\≈M}\ﬁ@]MJ\ZﬁöäÚ˛•Ú?ıÒ˙n´È∏Ç\Í\ ˆ\”OJ]ìå∑|._ınnNNôÒ\Î˛ìu¥†´,∏ë\ÊøG\«\…$tï>\ƒÒñ∏nª\„´\È®|.ä\…ˆï\≈#á\Ì}\Õ+á\Ì\nêD_˚m\ÍÛ¸Ω~8sµ\»˜=\Ó.{é\\\‚rIÛ]1â≥Ù\Ï¿]1öb≠`Ä9≠\»\ÎÃÆé{Yë¶<Ä+bß5’úæ\Èá\Í≤◊¶ä\∆\Z9\rñ£\ÏCb?\"-¶0\»F\ÂE¥wc(EwPQ•Ω∫¨f§\‘7l˙*ê\‘\'tºxJ\"∂∫ç≤ç@n`≠\“\›Iª\nJXû^\»<8\‘\◊\Ó\“:≠À∑5-\⁄h)\‰ˆ®ú\÷\»\œyÆ\‰\Êû`•õûZ\∆\Í¯d≠\Ÿ.7ô™\ÍOyGü\ŸNué?C\·ïÛ∏˛,\«;oØß\”\œ\Â‹∞í{˚lK˜8#C€ßÉg¯b\—-=dı≤∑C^\Œ\Ì≠<\»\ŒI?E1\∆À¥\œ-\Õ4dÌÅ∞[s\—MiB+.ß1êäb\Ã\Õ\')HΩË≤∫W\\\‹\Ì\ZZR\€)tí]π(©\'N¡\r\–:¿†SI/\0\"§Ü\‰\Ó¨@ëπ8Uì:^$»Ö:ÖÜ§W\⁄i\Í^u=\Õ\√ˇ\0àlWè,utıcwß®ÇùπñF∞y¢≈≤*≤¢Ú]ñ\“\«ˇ\0Sˇ\0¢\≈\‰˛óHå™ïÚáLÚ\„\Î\…Ië•Ω<°—Æí°πúyÖ(çpê>ç\‡˘ak\Zï\·ûﬂ®iµ`\√%n°ı]5ß<n\„#û®ß\‚ë\»dıDKä@†ë®éM\”hí\…tXóUååIû®∫y¢«ßxz¸\⁄\⁄f\‰8;¢Ú¸ô¯\Óûﬂèé\Ÿ\Œ\‘^\÷”áÒ[˙Ø\’\…5\Z>¨ïñöa∂ .˜.∏\ÌÚπ/ñﬁîM%)\‘˜n6 ÚL3\Ìäc4µO∑\◊=í\Ã˜4ùµ\Á\‚\∆\ÁeªâΩT˚¥˝ı+Ωü.ynp˝KãÚÒYèµ∑˙`jdpJj¢ñ7ì\Õ\„\Ëø=˙O\√œãä˛Iwø∑øµ\¬ŸØrºùMf\≈ﬂà˘/g˘*c<∫æ:xŒßê\Áïı8~ˆæ\›bE\∆˜AD\Õ/{IÚ^ûLxÒÒV\Ÿ∂MMVD\Ì\“\√\Ã\„√éyÑ\’=|®ê“π¥\‡9\ƒskõ-\„®eXvÒE\Í\—TWt%\ÿ\Œ\¬\‰˘\‹ˇ\0/3√å\œ)\Ìº¥ÒP>™wwlçÖ\Ô\'†uˆ˛\'Àú\ÿ\Ì\Ë\«\'\‚[µEˆ˜Qqúüºv\ﬂ¿¡\Ó∑Ëæé1\—6dÆ\“0ù8fV§aq\07Àö\È¶\nôé®=\”6o\Ôïûû\Óñ!cs≤2ëù°\‹#3J…£ö,•0√Å‰ä®®ê\…)uEò(´;v\‡+©≥±VP¡\”\"*G6¢\Z#9EGû Fz†Æ¨Æ\¬\Ë\Ê>`˘}&∂£ß∂\\x™•Üù∫h¢ìgªHy∫<˝U∑d\÷>\⁄\€}£\ÿGw#C\\›àp\ﬂ\È\—&1.v¨°Åõ:\"$∏\‡\06\n\0¡∫(\ﬁp“Ç\rS57t\rQ7K∞ëo\ÀTTZàµ\ 9 ìC\ZÇ-CrJv ÄöÌä°\⁄1©\‰˘(&cl´§ç\…Ú§∏ \“pEdçlÙA\‰˜ç¯ı˛K\œ\Õ>›∏\Ô\—˚é°9sâ\'Ãï\‚\ ;J]&\›1Ü ëáº\Zy-XJ∑¢ç\›\ﬁ\Îr!\ÈcvìÖlWÉ,TÚ||\ƒx\◊\Ì6æ2â\ŒÁ†è\Õu\¬\Ó1&ú\Ê9=VÉÒ\ÀÍäïﬁ®$\«?™öbõ\’˚&CG\„ü\’søıHÖ	ˆ\Ê®.ˇ\0\’Vtı÷ën™tëe≠\÷	_;\Êgoó\÷¯\”\'⁄ùWˆ03\ F~´«Ü[∫zy˝ªn{-du6H&~ñÏΩΩfS\À\·\Â\ÌÆ∫\◊\Z(<\0ë\”+\«\Õr\·õ\ƒﬁòk\Ì\’\’dF>öÜ\À\„¸üüñZ˙r∑kÆŒÆÒI4\‘uì#H-q<\¬ˆ~üÛÒ\Â∑\Ôñ¯Ø∏∂\Ì¶Ç®íG0ûÏÜÅÃï\Ï˘\ﬂ\'ãèé›∫gèç∏M¢¸hí\‡\“úguÚæ?>±\ÏÚLµ4]YY+åRha\ÁÖﬂá\Áe\…uô\€ZãMí¶\Ï\—,Æ\ÀORw^\‹xo\'ö\Î1\€oe≥\n{∂\‰]¡^\Œ.å\‘tìKjhc\◊\‚\◊e\◊#Qä,tıî/ià8\‰±Ú>.\ÿ\\lL∞ör˚ùUE∫\À5ù\≈ƒ∫\\j<Ù\rº?•¸|∏≠\«/§\‚å\Î0W\ﬁ\≈÷¶—≥QÆ.v≠#á%iÑämÙZdÛ^\ÿb.v\ )ö=U=\„˘g`≤µt¡\·Z`\Ã\√îUe\¬m,¿*5hc2<∏å®¥ıH\rv$K∂ÚV%Ox\ÀUDI\‚E(;¿P4\‚¿B\Áå˜$¢1\\J%®©äÜô™%lL§\·+X¯t∫:h-Ò\—\≈OMcG\"Nsü^™\∆g®=Úª\ƒ\„\œ^ò.\0÷∞\0r£Pßo∫ã\∆rÄûr}!ô«Å\"£¿0ıR,\·wÖE9\¬æ\Í\‹\‘à√ãU&~áÜ†üoió®%ÅøEY5üoR¢ó•´,˛\ÀuÇRp\›Xw¿\Ïπ\Á7\√\≈j\Óq≥\'í\ÂÖs$Ú+\Ë-ïmuΩ¢ÓÇ©èf\≈nUJt≠\“Uÿ¢ø\Œ\√NÒéã6¶û+˚M}\ﬂSºmíV∏\Ë\ÊëK\·££ó\’¸r˙†ë»±&9ΩT\«6»áõ6:¶ÇΩ£n|ïÙÅ\Ì>®\⁄}TŸß≥oè◊¨yÛ>Wß\”¯û\Ârn\”\Â&77\…\√ı^º\Ï}/ëá¸;t^\≈%G\ƒC∞\‹`Ø©\÷\◊\Ê\Ô∑Bû\‘◊≥%˚˘-\Œˆucx™\Ã\—ØÑx±\»uˇ\0U\·˘ßÒÚcu∏π)øT[∏â±ô4ºm\Â®/\¬|˛>_âï±Øèè¸ëg\∆\◊⁄ô\Ï•Œêñ\„8\ ˘?ıL˛G,\«:˙\ﬂ#ÇN;ß\'∑›ç∆∂F«ú˛Ò\'`øc\≈˚ß\\_ü◊ñ√Ñid5¨Ç\Á`t+\›Ò¯&>öìN˜\¬4ıê”¥T\∆F¡ßí˚\\X\ÂØ.\ÿ∆î\ƒ¸l\Áà]ı¶Å°\·\Ÿ\ \Ÿbpí\"\«®\‘q\—gé^(©Üwp\ÔnÆˇ\0?\—\\1ì\“…¶~Fë=B\Ô\'ÑYÿæÛö\ﬁyØfãCC\⁄6\ÍF\”\‡9\Õ∫+\≈^©*DCí\ÕXvD5LÑs(øK\»˝\’XG´8CVÛ,˙˚®\‘Y\—A\›\≈\…hïß\ÔpXôoj±™àïiî!ó(±<∞:,z+ì∑E\ﬁvôCÜDQK0°∏¸ï´Ù\Ërèª ©∂\“Ñt\ \–r6¥çL\€\—Mö83å\"¿hQJ“àbqÜ f.h±.ùêJh\Ÿ@D(çä∞G|~\"p®¶´~j~h/(\€˜,	ó;\√#.;R#—ì#ù)\Â\—EKo\"~àF\Î5cI-S™(`î\‹¡üè%\‡‰ö∂=8\›\√:ß%rTW8då´≈¶P\Z7]1r<h;≠¶‘ó=.ç\‡ûã\€\»?k[}$\Õ\Á\ﬁ`≠qƒµ\« ì1Éûã™dû®lû®h\Ïr˙¢ƒò¶ıP>\…˝P(O∑5a†\ÔÒ\’\ﬂ˙¢èøıSFû‚ëùÛ\‰´\ÊÛM\Ì\Ô\·Ω\\õ¥¯H2˙Ûxºr\◊\⁄\Êõ¯ÕüdUÛPp\‹N\r\‘7\Ÿ}é⁄è\ŒÒÒÃ≤Æ©\√\◊\'])Déå3ß<≠Òrwõ9xˇ\0\—W\ÀxöñAçÒ\Ã-\Â<8\È\ÂN\‘Y%\ƒ\…Ai◊ñªÚ¸\«\Í¸3ì±ø\Ì8ˇ\0o$YÒD¨ˇ\0Ü\\Iﬂª_\Ã>\'X¸ù_\Ì˜˘¨ºU\«¯6\·5Ue˛\"]Üèö˛õÒí?5\'öÙGbˆ9&sk\';<\Zzˆ>/\›o¶\›÷ñ(†ç≠\r¡\«5Ù§\”rë˛«ö\Áù\‘]	í4è\„\‰ó\≈MÆ™äÜÇ¶µ\ƒiÜ7<¸Ç\Ôü\'ëÛ\‘I4á/ë\≈\Œ>dúÆò®\‹ﬂ∫]X=\√R\È©tGò*Ò¯g\Ê6Of∏2<óG≠ktKÒZƒ§\Œ¿\Ÿ;ﬁ∏Rë]G!ö\Ï	<ä\«€ß\”X¡\·ZrW^\'∞å\ÓQdB¥R∫Y;\◊\rîj\›-™KaÑé® ñB_6TiiD\‹5iî\·Ó†ãP\ﬂ\nú≤ärü\ƒ\·Ö£Ü|ïFf\√ˇ\0ìeêç\€A&>ob¥ûõw\réTE={p\‚U\rRK\‚\∆vQS\⁄ÜP\–\⁄\Ãu@≠\"#\‘Qà“ÇM)A1ºñAêÄ¥´S78ı\¬mÚ\¬j~j™˛!¶6ˇ\0\nFj\‚f≥DYÀà\ŒDö”Ç\Ó™4ó\ =EA…¨ª\…*≠\Ì–µúÙ8Ö\‡\ÁÒì\—\≈7èo\›r^}ªiO3^vIR\√∂`∑ÇfxhendÕå\Áˆµ`¥B\Óˆ∂=@{†‰Æ≤±\\\ﬁ\Á\€\Ì¢BÒ\Ì\«Òd<ºˇ\0\€3ä+\⁄#ê=çvF\n\ﬁ3I¶VövòáàrZ!\Ê\‘0~E,U\∆?|&\‘b∫!˚\Ëe\∆,\‡9‚Ω†g(h\€\ÓÒ3ôD—ì{ã°Sk¢?m¥Ú	¥\Í∂æ	µ\”\Ë]•˝\ÌD\ÕÚ_;/olÒ7.˝≥U#\ÍòDE\€7\Õx¯˛>Wí\Â^˛_ó\'\¬{ZSÿ¢¥⁄ªòö\Z÷çÇ˙6k\Ã‚øπ†\Ï˚{Ãß\«˛-|Ø\Ê\’H÷ñ\ÓK\“Úº\”ˆ£ääïÙµ#@®\Ôs9\„©_\'\ÁÒvÙñ9%\ÎâL\÷S\0ì$≥Ú≥Ùk˘;k\Ì\Èˇ\0\"\‹t\Áv∑\À\rCû	vW\Ëx˛6O.8yzã\Ï\Ÿ\≈1N\Á\”UIó±°¨_OÇ\\=∫e$ÙÙ)\"H€§\‰Û^≠≥a$aK6∫1QN\ﬂ\nÚe\«eió\Ì\Z¶J^ñ2HuLçã\Â\Ã˛ã\◊\«mâß&krWßÕß¥¯H]E†=\’Ÿ££\∆\’=\∆\ÊÜ]P\‡\ÔÖ\“8\‘Z\÷\ËónYZàçZ¸Sì‰îä´e∫í9∫\Á=∫\ﬂMîélPó8\„i\…G2\\kà=\ÿ*5\Èw¶:H0\0\nßµ]LÜWytQb,m&\\®´ZaÅÖ¶Rá∫Å©ZPAìbQa\Àx˚\≈\∆<\n¢Ç\Õ8˛°\ÿˇ\0¯\'ˇ\0õU§j\ﬁvPUW7rë\Ã%≤X±•ì#\n	å\‹(F ¢%HULi\…@\‰[Çc2≤1î\ÁtUÇî\«˝´óUEõ\»icOñUå\÷rZëQx\'!\Ô\√†˛KVxF¶6\·≠n6\r\n∫A\'¢A\nêóF\Á©E\œ˜Ú≤x†f¢qÙ^?ë\«r≤«£á9%ïv\Í+´£B\“~+\Õ¯rv¸ëUpµqX\Á\«H«ü ˝\÷p\‹qÆŸ∏¥*õD\Ô∂–∏d\Ëîj\¬\È«éÆÚåe|<\∆U\0˘ Æív\ \«a\Ïy9\’zr÷º1é\ŸFK ;8¨6ª\·\€\∆˜(87\'\0∏•\ÀC¢Z{\„j¯¡§}#¸Åq\…gÚ©\Ï¥òé=Ç\'yÙô\ÏD=àˆì®∑ˆP\œÒ≠K¥\‹5YÿüiT\‘œ®ñ\“44d\‚M\’\’N\—\œ.\‹-uœ£Æç\Ã√á5\»\‘Ú\€∑gºc\ƒV\ﬂl†¢&2\›G\ móN\‚Jë°´∑T≤l\‡\0\¬rõ#§vy\ÿm\Í\Ô\›\‘\‹\ËjM;∞KZ\Ì\'\nñ∫%◊∞K<E‘∂Z˛˘£`N£\Îïuôd\–p\œa6Ûhå\Àg{^N\·\„%O∫\Ó<\∆Os©sCN¿˘ØyW∑í\Î\ÿWò`nv\‘y\⁄\…}⁄†Ωº∫ÜG\"π\Á\È”á˘\Ï\‡[5t\…Z¯ÛˆµÚö\Áâ\Í‰°¥\œP\∆\Í,a .÷º\Ôvë\≈%ø\‘T÷∏\ÂØ,csê\–	\ÿ.W	óöøÈíë˘fí§\‚â∞\Zr˙El˚π\’\“Òï,pJX◊∏\09Ó≥î}Ω\Õ√•œ†ç\Ô‹ñç\÷ÒÙ-q∑%£bsr≥cúv\œ(\€iW>B>Å^9\‰µ\Œ\ÃX\‹/TéG\ZÃµj\"≤Ω¶)\„ò~\„Å*Uç≥A˝û\…)›áÜá#\ËWX\·LK3*(Ñ\Ã¡\‹y\Ã+]\Œ]4\Œﬂ¢_F$pXíIÃì˙Æq\”&Ü°íVK\›4·ÉôUô\·9åääü\0§eSS;¶ê\Ó´Rß\r$¨®©ŸæJ±\‡U\…@4ÛA•ò(•–å;(-á∏™(mÆè¶gS@˘µZì\”N˛EEW\’íÇ™m§  ~ë\ƒ µÄ\Â´!\Ãl¥à”åîSE∏ 6c(%\∆2’êº 0Ä=πaAY\›h˘≠\"\'WGoÉ[›á9öZ<\ \ﬁld®\‡à[Q%|ªÜú7\…2±≤\‘÷Ç˜ú5º\÷\ZR\’\Œ˙ ≠-ŸÄ\Ïı`\»ÙAÄ:(´\Ó\Œ$\r∫\‘\ƒyæG\…\ﬂ\Í∏Ú˙t\‚ˆ\ﬁ.\‡Ç\÷\ﬂm3\„8kà\ÿ\·A\‡ü¥\ﬂg/U\\™i¢uQ\‘\…\À>£°S}Téab\‡*õ¸≈¥08Å¯BªVˇ\0Üm<3Yq°ô\Î\ÁÊ•íèOv9pßºSG/∞˜@Å\»\Âs\–\Ï\—“à∞czÖ†\ƒ\¬\›u±Éä\ÀUx™¥\ZGµ\∆<cñ\ ˜âßôªM\‡E˚â_qm@1\·ÁÇπ\\\⁄\«:g\0\Àfµ\⁄Y\√!sÄ\’q\œ˚K\‹!µ\÷›ÖSõ¥ùâ\¬\\”´ejªZii\√c®çé\«%fqt∞ÇıA+∆∫∂\”d\Ïig‚Åå¿™g\’^»Ø\·\ nöY[ú;rπ\Ãz›ª\Âüi§˘\ÁtÛ8¶\‹\—o_˙lü2Ù\È\≈¸ì˚4¡±4\Á|ïÆ\‚\◊?ÛZÒc\Z˚K]’Öu\À”å|¸\‚(˚ª\’s?\rC\«ˇ\0±X\«\—Umï†\√ ™\—ˆW+\"\„zH\‡\÷˜ç\…?å˝${\„Ü\Í)\›mãC\⁄Fë\’\\oÉK7T@—ª\€ıZ\‹M\Z}}3FL≠˙ßh9giıë\◊Ò-<q\ÊC\0dì˝N/52Òöòåd\ËsÇçæ`árÄ>7tJëo\√\’&[+ZOé°\ÀXzs\Œj£NÛIqtÚ™Å«£¿\»˙åè¢\⁄{ä˚ú\‡\”˙&^åS8*7:óP˝\‚π\∆Ú≠åMe<Zä¨)\ÓFit∑íõjBcfë∫ÅN\ﬂ`äTcQ*eHîº\ Û7*ÑSåIÑ]-	j\"Çü\r\Ì%≠w[$\œˇ\0\Ë≈´\È#T\Ôueb∫±¡πUé¡$îR\‚8AcJ¸±DI\Ë®j@\n,7çê 7≈î\‡>ü`Ñ\ni jH|a¡X9\Ájï∑..≥PSπÕÑ”ΩÚúˇ\0ú\—u\¬\Í1[k5$\Îs fZ\›\ \≈ÛVxàµ’é®qN5Ù˘§ö≠\‘B˚\…]‘©j»íÚ\«\» ï¡Úò8éòÙêò\œ\Ã,rM\‚\ﬂÚtºØ+\–,˙ ,ü$\—8\”\∆v\Á\—]!ç\√\Êπ\Âçé\Œ˚\·˛eÊì∂≥ï&7\Ï]qüe¸;\ƒ\‘^\œ]H\◊\0rÿÇØZ%WPp\Õ;i\ÈÜ7aîêi™\∆Dr@\ŸQ\Ã8\ﬁ˝KMS\›\ZÄ\◊\»9q¥c´nT\œ:Ω§Ã¶ñ*Â¨¢\ŒÛ∑\Íì±±[@7\ÔõıZòßb]v∑Gˇ\09øUf)\Ÿ^%∑Eüºi˘´‘óhsq\’9\rx˙ßTE=£”ÉÄ\Âz’èNKÅí},W±\ﬁ\"∞\—ìˇ\0ÜILΩ7\«\Ìà\·=ä\«QSGTIc∂8r◊á_ëèÿ∏˜µ˙Ÿ¶ÜåfG4Å\‚µªyf\ﬁW∫\»\È\Î&ù\‹\‰yq˘ïd“™¶Í®äÚÅ\À|Ú\”\÷2hIi\»¬Ñéõj\Ìgç\Ë\ËôMG\rT≠\¬<Æ}±ç\Ã2©o\Ì\'¥ °‡¶∏å˘G˛ãì\Ìø≈êô\ƒ™\÷\‰2ñ\Í\Ï˙¸ñ.\⁄\Œ,ù7Üa¶£m\≈\Ó}XÖ¢b„ìØÖÙ8=<‹ümŒü1á\0Ω68cP°èlj4EL\'Alâ*/\»)\ÓíS<\‚:Ü\‡|S™π\Õ¡_\Ê\”\Œ\“\”<;\Ë¢\Ôß\œ›™æ\‰Ä\Êi3≤\Œ^ö≈ø\·:F\”[\„\»\‰–π∆≠9w≠\Ê∆î$W”ÇN£\’EJ¸\–8\∆\‡egt!V2í9(\‘S2ç\’<yEX3\›H\À*\Êˆ©M¯M≤oüâãW\“F¡§π\Ôf¶±±\¬\È\Ây\“¿@\ÿu$êß¢M™∏ÄIH\Íy\\Ò$,\◊¿\∆\Ÿ¡\»\È∫O1u§8@x\»#t9•£($\“;pOf\·D%\„uBvE$\√\…@µÅToPÄµ&ëK=®\‚AV\·ìç?WI|3}ëv©\√˚¢\‚\ÿ\€\ÂÃ§àL\‘\ÃC\Zy\·g&¢\‚π˝\’1\∆=bò§:©I\ÍJ∑ÿëmwst•óñôò5úøç\\}∫ê^G®(+\Óµ\Êç¿\\Ú£ëq\ﬂT\—	\"\–ˆÙ\»+û≠6Û∑\Z_k\Ín™eLö≥»πjcNØäØ,6L¸÷∫ä©¯≤ˆIÃøö¶çªän\‚2L˝¢i]\'›éC™O\’oDà5M^s™¶O™\ \È>\"óV_Q!¯î0Ò,06ˇ\0fëÙûc˜%pX©dû3øU#Au~mí¸\À\”X&7Üª.ß\‚- æw∂9ùñµæKÀé\Â|=πÚaåÚOˆiä\—+\ÌÚK\ﬂ5§ÇJ\ﬂ^N?6∏\À«üâ_ΩQ\…G_QI :·ê±\ﬂW´•õyÆ6]*%çƒü	˙+∏u®ØÇC…á\Ëù\‚\Ã+kÿè\rS_x\Ê\nKãqN¢\Ôo\…y˛G,ò¯v\·\‚ª\›{r\≈\¬<;EEp[iÄkFÄßM\”>l•\‘[Gi∂F<4p˙\Î¯8\Á”ü\Â\œ˚\n∂P\—QORaâ≠ä7<\Ï9\0J≥èé}\'|ˇ\0∑§ê∫∑ºw7;W\’{8\Êú3Û\Z£öó\ŒÀªä≠òûZyt*.\ tE—ì\ÕR(nê∫)\Ã\Ÿ\ÏvAQπ\Á\¬?HEF\«0\—(Úr\Ìç\‹p≥Uè∂N.\ZJ@s≠\Õ.ˇ\0§ˇ\0¢\Œ^ö\≈\◊]P\⁄Z&∞ù+$Ú¶\÷\È\Â.\'eñı§\»¿k}Q!oRâ≤›ÄÑ7rUTàé2ë\È∞\„F\≈@áç’äT#tD¶$˚ù4å\„{=cF\Z\Ëj!w˝°\√ÙZ˙Fçò\‰Üñni\Ê\“\ƒ\"ß≥“Ü\Ï˙ööÄ\Ÿ\Âh$05ÅçcrHha¸˘´<A£\…Eâ\r~¶\‡†vü\ﬁ\ŸåG!ß\rê \Ïã\∆BÅ\ËÜH\Ÿ .JÄ4í§’ïÇüΩ~q\—tëç≤≤\’I]]\›0ù\œEΩj#sg§¥låÜÎçªn\"\›j\Ê7êVA6ñ=4¨n9\Óß⁄éF\È√ò9S\ËtõulîÕñ)\ZÏÅê\‡˘/\Œn«≥W[Hs\⁄—í\‡\Ì!™ç- Ü#á\‘\ƒﬂãî\ÌH\“Ò¢?~æÒxNDõåxz!ó\›)F<\‰\nwã•=√¥\Œ¶7ZbGì¡YºáZ\«Òl\\=°\—\√T\Ÿ˘wXπZ\‘\≈\≈xˇ\0çv{˝íûWÉ\‘5\\Y\Í\Ê\ÌºTºñ\—Kø¢‹∞\ÎUì\Ÿ/\“\r®_∫Ω¢ı®\ÁÉ8û¢7I!\r\Œ˚\'x≥\n\À^\·∏⁄¶0V\∆XN\√\’\\l¨Ÿ§[uU∆©∞S∑Sû~ãV¯FÚõ±é$¨§lÏë°Æj\Â˘#é†Vv/≈∞\ÁCc»Ö!¯\Í±˝îÒì[\ÏM>π*Ã∂\œW“ìö\"÷ê\n≈§õT>\ﬂU\‹\‚\Ë\»œû\Î3\ÀV∏8˛œï§n\÷^ìhùòÒ4îÒTPUE#£âˇ\0v\Ê∑;y/˘ã/O£ü«úíYZ{\Áæjb££ù\œp¿.n\Á\ÕÛr\Àaç^âé7ye\"\ÈŸùu}|ıo¶\Z\Êy{æ$Ø&9¸ùkOm\«\„hø˝ ¨q˛\ÈÉ\‰∑?…øL\Ô\„√ëv9R}\‡\—ÚNü\"Ø\Â¯ÒmcÏ≤≤\◊Z\ \ YLr∞\Ï\‡o\«\Á\ j\“|û	Ù\ËÙgãbÖ±{k@h\«˜kx|ô52qÀõ\„_=JëúU\'Ωtê@[ˇ\0\‰_y≥˛G«û±Tﬂ°º\”[\ﬁ˙ÀïD¨ê\Ë,.\ÿ\Âz>\'\ƒ\œYñYm\«\‰¸¨2\„≥t ñò¶kó€û%≠¥H$¶\÷9R´iÉÅ  çFJEV\ﬁ)sà	Lj¶\Zh\Ó6Z\€D\«Fí\√¯]\–˝U¡2ünc\Ÿ˚\›K\⁄îuáD∞\≈&ñûÆ\»˙¸\’»ë\’gù\”;\ƒp¿π÷§\“E<{\r*\ƒJ:boãrÅ\ËÉ\‚à\…\∆02äqõ\’e&\"¢üh\Ÿáç\’Öª®%0+±TRJG˜rù˛,pUìí˚Ö]$9y%hëÑ\ﬁ\‚Äã©ﬂÅ∫	\‘\Ô\ \"XjÅjÄN∞xRT(6\ÂDj™Ü\≈\ﬁ„Ü±•\Œ\'†´\"Z\Áw[\€j\∆!\'ﬂö\Ì&úˆΩ\‡{a.5S™\Áù˙kÿΩ\¬8_)\Í0>úiö3W¯|\÷˛Üö&aç†öì3|(*\ÍùYèuL∞πﬂÅ\ÿ_\Âq_\Àl˚}oçúºrX\œU\“Ò\rSûeæ\\=;\‚?E\¬q\Â˜^û¯˝EO\‘\’H_5∆±\‰Û\’;ø™\Ôé\‰”ÜR_\"è≥\⁄c˝\‰≤?¯§%jZŒ¢T=ûZ«ΩO\≈]\‘\‘LãÄ≠\Ó#ˇ\0µ<≤Diπfs¯U1}©≠˛\·üˆ´§ÑªÖ\Ìmv\–7Ë¢úˇ\0Ü\Ì°π7?4\Ìv\ÍzI\\ h\ﬂeMºoˆä™¶w∂öò\0\∆Nn9®\„íd2\ƒ8∫ë≤¥∏ÅÉÒ[\œ—è∑π,4Ù¶\’òôç#¢ÛGjí˙*7sÖøESf]k†\'=\√~ãLZ\ÿw\⁄\"\‘$\ xL=≥n\‚s˚\\\—˜!¿ı\ \Á\≈mu\‰í$\◊?º¶î˘µw≥√é>\œˆmC©ûÚ¡ù[\Ïπ·ÑÆôÚ]∂¬é{É\Ë∫˛8\Áﬁè\Ÿ!¸\Ëù!⁄á≤\¬?p+\÷&ËΩû/¿¨7I0G¯BuÜ\È&~¶ê€ò\ﬂ É\⁄C∆ä\Za˚\œs\œ\»c˘ÆºS\ eÈãØÉ¡®\Ë≥√ú´®\Ó\…\‰Æ7\√9FÖ\Õn9≠0É$aØ%¢=l;sEg\€≤\‹r6§>ú≥∂õ|∂é\"£\‚ZÃçvG\„?\ÍW\“OÈ∑°π\≈qßßöô¿\≈4m{q\‰FV+q®ß\”\r0\'ûîa)ö_@QS\‚¿Öç\Œz P\ÂÑAÉ∫	\Á%\ÿ˘\" ÒaP\‰-\¬	- ØºTòjm∞˘ızO¿1\Á˘®â{¢£\Œ—ù\—\‡úy!_.!ñø\ƒ0Çm<öPXS\»0¢C§d\"í89(öÛT+ln¶àm˚`\«Ò\Â\‚ö&6uMZ÷ú\«˚\ƒ˘y|\◊\\1˚b\’o\Õ∂\»\ÀG&óek,¥ímæ£Ç8)õm¿\\[Ùâ}©\Ó\È\ÀZUƒ™æg{T\ÍÆDj\€\…sR$n\»+\Î[  6^ïè\Ó\€\€Ò≤˝∂pÀßßdlÅMV!m!T)\Ó\ƒg\’XÑR\0eœí\"axö©] .\Ê≤dh\—büâ\Í\r™y˝“©^\ÌJ∞\’Ò\’tö≤=¢O	=ü\’{?P…úb@/Kè∑∫¯>Ωí\ÿ\·:á∫ô\›jj\Ÿ¯Çm\rö\÷g\ﬂUv\≈ˇ\Ÿ');
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
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prescription`
--

LOCK TABLES `prescription` WRITE;
/*!40000 ALTER TABLE `prescription` DISABLE KEYS */;
/*!40000 ALTER TABLE `prescription` ENABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `queue`
--

LOCK TABLES `queue` WRITE;
/*!40000 ALTER TABLE `queue` DISABLE KEYS */;
INSERT INTO `queue` VALUES (29,990,1,'waiting','2025-09-16 16:01:02',NULL,NULL),(30,1000,2,'waiting','2025-09-16 16:01:05',NULL,NULL),(31,2,1,'done','2025-09-23 02:16:51',NULL,'2025-09-23 02:17:15'),(32,1010,2,'waiting','2025-09-23 02:16:52',NULL,NULL),(33,1071,3,'waiting','2025-09-23 02:17:08',NULL,NULL),(34,1329,1,'waiting','2025-09-30 18:27:34',NULL,NULL),(35,1329,1,'waiting','2025-10-01 19:38:30',NULL,NULL),(36,1002,2,'waiting','2025-10-01 19:55:30',NULL,NULL),(37,1006,3,'waiting','2025-10-01 19:55:30',NULL,NULL),(38,1009,4,'waiting','2025-10-01 19:55:31',NULL,NULL),(39,2,1,'done','2025-10-02 22:20:48',NULL,'2025-10-02 22:21:02'),(40,990,2,'skipped','2025-10-02 22:20:51',NULL,NULL);
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
) ENGINE=InnoDB AUTO_INCREMENT=153 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stock_movements`
--

LOCK TABLES `stock_movements` WRITE;
/*!40000 ALTER TABLE `stock_movements` DISABLE KEYS */;
INSERT INTO `stock_movements` VALUES (131,34,'IN',120,'2025-09-27 18:03:37','2025-09-20 02:02:59',6.61,NULL),(132,30,'IN',120,'2025-09-27 18:03:46','2025-10-11 02:02:59',12.00,NULL),(133,30,'IN',150,'2025-09-27 18:03:51','2025-10-11 02:02:59',12.00,NULL),(135,30,'OUT',1,'2025-09-27 18:34:42',NULL,12.00,NULL),(136,30,'OUT',1,'2025-09-30 10:11:31',NULL,12.00,NULL),(137,31,'OUT',2,'2025-09-30 10:11:31',NULL,15.00,NULL),(138,31,'OUT',2,'2025-09-30 10:11:31',NULL,15.00,NULL),(139,32,'OUT',2,'2025-09-30 10:11:31',NULL,12.00,NULL),(140,34,'OUT',2,'2025-09-30 10:11:31',NULL,6.61,NULL),(141,35,'IN',120,'2025-10-01 11:44:03',NULL,150.00,NULL),(142,30,'OUT',1,'2025-10-01 13:25:24',NULL,12.00,NULL),(143,33,'OUT',2,'2025-10-01 13:25:24',NULL,12.31,NULL),(144,34,'OUT',1,'2025-10-01 13:25:24',NULL,6.61,NULL),(145,34,'OUT',1,'2025-10-01 13:25:24',NULL,6.61,NULL),(146,30,'OUT',1,'2025-10-01 13:25:24',NULL,12.00,NULL),(147,34,'OUT',5,'2025-10-01 13:25:24',NULL,6.61,NULL),(148,35,'OUT',1,'2025-10-01 13:25:24',NULL,150.00,NULL),(149,33,'OUT',1,'2025-10-01 13:25:24',NULL,12.31,NULL),(150,34,'OUT',1,'2025-10-01 13:25:24',NULL,6.61,NULL),(151,33,'OUT',1,'2025-10-01 13:26:23',NULL,12.31,NULL),(152,34,'OUT',1,'2025-10-01 13:26:23',NULL,6.61,NULL);
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
) ENGINE=InnoDB AUTO_INCREMENT=275 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stock_movements_history`
--

LOCK TABLES `stock_movements_history` WRITE;
/*!40000 ALTER TABLE `stock_movements_history` DISABLE KEYS */;
INSERT INTO `stock_movements_history` VALUES (9,12,30,'IN',120,NULL,'INSERT','2025-09-27 02:44:59',NULL,120),(10,13,30,'IN',120,NULL,'INSERT','2025-09-27 02:50:13',NULL,120),(11,12,30,'IN',120,NULL,'DELETE','2025-09-27 02:52:01',120,NULL),(12,12,30,'IN',120,NULL,'DELETE','2025-09-27 02:52:01',120,NULL),(13,13,30,'IN',120,NULL,'DELETE','2025-09-27 02:52:06',120,NULL),(14,13,30,'IN',120,NULL,'DELETE','2025-09-27 02:52:06',120,NULL),(15,14,30,'IN',120,NULL,'INSERT','2025-09-27 02:52:50',NULL,120),(16,15,30,'IN',120,NULL,'INSERT','2025-09-27 02:52:54',NULL,120),(17,16,30,'IN',120,NULL,'INSERT','2025-09-27 02:52:56',NULL,120),(18,17,30,'OUT',120,NULL,'INSERT','2025-09-27 02:53:33',NULL,120),(19,16,30,'IN',120,NULL,'DELETE','2025-09-27 02:53:47',120,NULL),(20,16,30,'IN',120,NULL,'DELETE','2025-09-27 02:53:47',120,NULL),(21,14,30,'IN',120,NULL,'DELETE','2025-09-27 02:54:26',120,NULL),(22,17,30,'OUT',120,NULL,'DELETE','2025-09-27 02:54:49',120,NULL),(23,18,30,'IN',120,NULL,'INSERT','2025-09-27 02:55:32',NULL,120),(24,19,30,'IN',10,NULL,'INSERT','2025-09-27 02:58:09',NULL,10),(25,20,30,'IN',120,NULL,'INSERT','2025-09-27 02:58:26',NULL,120),(26,15,30,'IN',120,NULL,'DELETE','2025-09-27 02:58:47',120,NULL),(27,18,30,'IN',120,NULL,'DELETE','2025-09-27 02:58:47',120,NULL),(28,19,30,'IN',10,NULL,'DELETE','2025-09-27 02:58:47',10,NULL),(29,20,30,'IN',120,NULL,'DELETE','2025-09-27 02:58:47',120,NULL),(30,21,30,'IN',120,NULL,'INSERT','2025-09-27 02:58:52',NULL,120),(31,22,30,'IN',120,NULL,'INSERT','2025-09-27 02:58:58',NULL,120),(32,21,30,'IN',120,NULL,'DELETE','2025-09-27 02:59:14',120,NULL),(33,22,30,'IN',120,NULL,'DELETE','2025-09-27 02:59:14',120,NULL),(34,23,30,'IN',120,NULL,'INSERT','2025-09-27 02:59:54',NULL,120),(35,23,30,'IN',120,NULL,'DELETE','2025-09-27 03:00:00',120,NULL),(36,25,30,'IN',120,NULL,'INSERT','2025-09-27 03:07:33',NULL,120),(37,25,30,'IN',120,NULL,'DELETE','2025-09-27 03:07:38',120,NULL),(38,26,30,'IN',120,NULL,'INSERT','2025-09-27 03:09:22',NULL,120),(39,26,30,'IN',120,NULL,'DELETE','2025-09-27 03:09:25',120,NULL),(40,27,30,'IN',120,NULL,'INSERT','2025-09-27 03:10:48',NULL,120),(41,27,30,'IN',120,NULL,'DELETE','2025-09-27 03:10:50',120,NULL),(42,28,30,'IN',120,NULL,'INSERT','2025-09-27 03:15:30',NULL,120),(43,28,30,'IN',120,NULL,'DELETE','2025-09-27 03:15:33',120,NULL),(44,29,30,'IN',120,NULL,'INSERT','2025-09-27 03:19:10',NULL,120),(45,29,30,'IN',120,NULL,'DELETE','2025-09-27 03:19:17',120,NULL),(46,30,30,'IN',120,NULL,'INSERT','2025-09-27 03:20:08',NULL,120),(47,30,30,'IN',120,NULL,'DELETE','2025-09-27 03:20:11',120,NULL),(48,31,30,'IN',120,NULL,'INSERT','2025-09-27 03:25:43',NULL,120),(49,32,30,'IN',120,NULL,'INSERT','2025-09-27 03:25:45',NULL,120),(50,31,30,'IN',120,NULL,'DELETE','2025-09-27 03:25:59',120,NULL),(51,32,30,'IN',120,NULL,'DELETE','2025-09-27 03:26:09',120,NULL),(52,33,30,'IN',120,NULL,'INSERT','2025-09-27 03:27:11',NULL,120),(53,33,30,'IN',120,NULL,'DELETE','2025-09-27 03:27:17',120,NULL),(54,34,30,'IN',120,NULL,'INSERT','2025-09-27 03:39:15',NULL,120),(55,34,30,'IN',120,NULL,'UPDATE','2025-09-27 03:39:32',120,140),(56,34,30,'IN',140,NULL,'DELETE','2025-09-27 03:39:39',140,NULL),(57,35,30,'IN',120,NULL,'INSERT','2025-09-27 03:45:06',NULL,120),(58,35,30,'IN',120,NULL,'DELETE','2025-09-27 03:45:12',120,NULL),(59,36,30,'IN',120,NULL,'INSERT','2025-09-27 03:46:58',NULL,120),(60,36,30,'IN',120,NULL,'DELETE','2025-09-27 03:47:05',120,NULL),(61,37,30,'IN',120,NULL,'INSERT','2025-09-27 03:53:40',NULL,120),(62,37,30,'IN',120,NULL,'DELETE','2025-09-27 03:53:55',120,NULL),(63,38,30,'IN',120,NULL,'INSERT','2025-09-27 03:57:55',NULL,120),(64,38,30,'IN',120,NULL,'DELETE','2025-09-27 03:58:02',120,NULL),(65,39,30,'IN',120,NULL,'INSERT','2025-09-27 03:59:46',NULL,120),(66,39,30,'IN',120,NULL,'DELETE','2025-09-27 03:59:51',120,NULL),(67,40,30,'IN',120,NULL,'INSERT','2025-09-27 04:01:52',NULL,120),(68,41,30,'IN',80,'2026-01-17','INSERT','2025-09-27 04:04:57',NULL,80),(69,42,30,'IN',80,'2026-01-17','INSERT','2025-09-27 04:06:07',NULL,80),(70,42,30,'IN',80,'2026-01-17','DELETE','2025-09-27 04:06:16',80,NULL),(71,40,30,'IN',120,NULL,'DELETE','2025-09-27 04:06:18',120,NULL),(72,41,30,'IN',80,'2026-01-17','DELETE','2025-09-27 04:06:19',80,NULL),(73,43,33,'IN',120,NULL,'INSERT','2025-09-27 04:11:56',NULL,120),(74,44,31,'IN',120,'2025-10-11','INSERT','2025-09-27 04:12:04',NULL,120),(75,43,33,'IN',120,NULL,'DELETE','2025-09-27 04:12:12',120,NULL),(76,44,31,'IN',120,'2025-10-11','DELETE','2025-09-27 04:12:13',120,NULL),(77,45,30,'IN',0,NULL,'INSERT','2025-09-27 04:20:50',NULL,0),(78,46,30,'IN',12,NULL,'INSERT','2025-09-27 04:21:02',NULL,12),(79,47,34,'IN',100,'2025-12-20','INSERT','2025-09-27 04:22:49',NULL,100),(80,48,34,'IN',100,'2025-12-20','INSERT','2025-09-27 04:22:52',NULL,100),(81,49,34,'IN',100,'2025-12-20','INSERT','2025-09-27 04:22:59',NULL,100),(82,50,34,'IN',100,'2025-12-20','INSERT','2025-09-27 04:23:06',NULL,100),(83,51,34,'IN',100,'2025-12-20','INSERT','2025-09-27 04:23:16',NULL,100),(84,51,34,'IN',100,'2025-12-20','DELETE','2025-09-27 04:37:39',100,NULL),(85,50,34,'IN',100,'2025-12-20','DELETE','2025-09-27 04:37:40',100,NULL),(86,49,34,'IN',100,'2025-12-20','DELETE','2025-09-27 04:37:41',100,NULL),(87,48,34,'IN',100,'2025-12-20','DELETE','2025-09-27 04:37:42',100,NULL),(88,47,34,'IN',100,'2025-12-20','DELETE','2025-09-27 04:37:43',100,NULL),(89,46,30,'IN',12,NULL,'DELETE','2025-09-27 04:37:44',12,NULL),(90,45,30,'IN',0,NULL,'DELETE','2025-09-27 04:37:45',0,NULL),(91,52,34,'IN',100,NULL,'INSERT','2025-09-27 04:38:50',NULL,100),(92,52,34,'IN',100,NULL,'DELETE','2025-09-27 04:38:53',100,NULL),(93,53,34,'IN',100,NULL,'INSERT','2025-09-27 04:40:51',NULL,100),(94,54,34,'IN',100,NULL,'INSERT','2025-09-27 04:40:59',NULL,100),(95,55,34,'IN',100,NULL,'INSERT','2025-09-27 04:41:10',NULL,100),(96,56,34,'IN',100,NULL,'INSERT','2025-09-27 04:43:10',NULL,100),(97,57,30,'IN',100,NULL,'INSERT','2025-09-27 04:49:36',NULL,100),(98,58,30,'IN',100,NULL,'INSERT','2025-09-27 04:49:44',NULL,100),(99,59,34,'IN',500,NULL,'INSERT','2025-09-27 04:53:15',NULL,500),(100,60,34,'IN',500,NULL,'INSERT','2025-09-27 04:53:20',NULL,500),(101,60,34,'IN',500,NULL,'DELETE','2025-09-27 04:55:44',500,NULL),(102,59,34,'IN',500,NULL,'DELETE','2025-09-27 04:55:45',500,NULL),(103,53,34,'IN',100,NULL,'DELETE','2025-09-27 04:55:46',100,NULL),(104,58,30,'IN',100,NULL,'DELETE','2025-09-27 04:55:48',100,NULL),(105,54,34,'IN',100,NULL,'DELETE','2025-09-27 04:55:48',100,NULL),(106,55,34,'IN',100,NULL,'DELETE','2025-09-27 04:55:48',100,NULL),(107,56,34,'IN',100,NULL,'DELETE','2025-09-27 04:55:49',100,NULL),(108,57,30,'IN',100,NULL,'DELETE','2025-09-27 04:55:49',100,NULL),(109,61,34,'IN',12,NULL,'INSERT','2025-09-27 04:58:14',NULL,12),(110,62,34,'IN',12,NULL,'INSERT','2025-09-27 04:59:06',NULL,12),(111,63,34,'IN',12,NULL,'INSERT','2025-09-27 04:59:09',NULL,12),(112,61,34,'IN',12,NULL,'DELETE','2025-09-27 04:59:43',12,NULL),(113,62,34,'IN',12,NULL,'DELETE','2025-09-27 04:59:43',12,NULL),(114,63,34,'IN',12,NULL,'DELETE','2025-09-27 04:59:43',12,NULL),(115,64,34,'OUT',2,NULL,'INSERT','2025-09-27 12:59:02',NULL,2),(116,65,30,'OUT',1,NULL,'INSERT','2025-09-27 12:59:02',NULL,1),(117,66,31,'OUT',1,NULL,'INSERT','2025-09-27 12:59:02',NULL,1),(118,67,33,'OUT',4,NULL,'INSERT','2025-09-27 12:59:02',NULL,4),(119,68,32,'OUT',3,NULL,'INSERT','2025-09-27 12:59:02',NULL,3),(120,69,34,'OUT',1,NULL,'INSERT','2025-09-27 13:03:37',NULL,1),(121,70,32,'OUT',2,NULL,'INSERT','2025-09-27 13:03:37',NULL,2),(122,71,33,'OUT',3,NULL,'INSERT','2025-09-27 13:03:37',NULL,3),(123,72,34,'OUT',1,NULL,'INSERT','2025-09-27 13:05:49',NULL,1),(124,73,32,'OUT',2,NULL,'INSERT','2025-09-27 13:05:49',NULL,2),(125,74,33,'OUT',3,NULL,'INSERT','2025-09-27 13:05:49',NULL,3),(126,75,34,'OUT',1,NULL,'INSERT','2025-09-27 13:06:36',NULL,1),(127,76,32,'OUT',2,NULL,'INSERT','2025-09-27 13:06:36',NULL,2),(128,77,34,'OUT',1,NULL,'INSERT','2025-09-27 13:30:06',NULL,1),(129,78,32,'OUT',2,NULL,'INSERT','2025-09-27 13:30:06',NULL,2),(130,79,33,'OUT',3,NULL,'INSERT','2025-09-27 13:30:06',NULL,3),(131,80,34,'OUT',1,NULL,'INSERT','2025-09-27 13:30:56',NULL,1),(132,81,32,'OUT',2,NULL,'INSERT','2025-09-27 13:30:56',NULL,2),(133,82,33,'OUT',3,NULL,'INSERT','2025-09-27 13:30:56',NULL,3),(134,83,34,'OUT',1,NULL,'INSERT','2025-09-27 13:34:33',NULL,1),(135,84,32,'OUT',2,NULL,'INSERT','2025-09-27 13:34:33',NULL,2),(136,85,33,'OUT',3,NULL,'INSERT','2025-09-27 13:34:33',NULL,3),(137,86,34,'OUT',1,NULL,'INSERT','2025-09-27 13:35:02',NULL,1),(138,87,32,'OUT',2,NULL,'INSERT','2025-09-27 13:35:02',NULL,2),(139,88,33,'OUT',3,NULL,'INSERT','2025-09-27 13:35:02',NULL,3),(140,89,34,'OUT',1,NULL,'INSERT','2025-09-27 13:35:36',NULL,1),(141,90,32,'OUT',2,NULL,'INSERT','2025-09-27 13:35:36',NULL,2),(142,91,33,'OUT',3,NULL,'INSERT','2025-09-27 13:35:36',NULL,3),(143,92,34,'OUT',1,NULL,'INSERT','2025-09-27 13:42:30',NULL,1),(144,93,32,'OUT',2,NULL,'INSERT','2025-09-27 13:42:30',NULL,2),(145,94,33,'OUT',3,NULL,'INSERT','2025-09-27 13:42:30',NULL,3),(146,95,34,'OUT',1,NULL,'INSERT','2025-09-27 13:45:11',NULL,1),(147,96,32,'OUT',2,NULL,'INSERT','2025-09-27 13:45:12',NULL,2),(148,97,33,'OUT',3,NULL,'INSERT','2025-09-27 13:45:12',NULL,3),(149,98,34,'OUT',1,NULL,'INSERT','2025-09-27 13:47:49',NULL,1),(150,99,32,'OUT',2,NULL,'INSERT','2025-09-27 13:47:49',NULL,2),(151,100,33,'OUT',3,NULL,'INSERT','2025-09-27 13:47:49',NULL,3),(152,101,34,'OUT',1,NULL,'INSERT','2025-09-27 14:22:54',NULL,1),(153,102,32,'OUT',2,NULL,'INSERT','2025-09-27 14:22:54',NULL,2),(154,103,33,'OUT',3,NULL,'INSERT','2025-09-27 14:22:54',NULL,3),(155,104,34,'OUT',1,NULL,'INSERT','2025-09-27 14:25:45',NULL,1),(156,105,32,'OUT',2,NULL,'INSERT','2025-09-27 14:25:45',NULL,2),(157,106,33,'OUT',3,NULL,'INSERT','2025-09-27 14:25:45',NULL,3),(158,107,34,'OUT',1,NULL,'INSERT','2025-09-27 14:30:24',NULL,1),(159,108,32,'OUT',2,NULL,'INSERT','2025-09-27 14:30:24',NULL,2),(160,109,33,'OUT',3,NULL,'INSERT','2025-09-27 14:30:24',NULL,3),(161,110,34,'OUT',1,NULL,'INSERT','2025-09-27 14:34:49',NULL,1),(162,111,32,'OUT',2,NULL,'INSERT','2025-09-27 14:34:49',NULL,2),(163,112,33,'OUT',3,NULL,'INSERT','2025-09-27 14:34:49',NULL,3),(164,113,34,'OUT',1,NULL,'INSERT','2025-09-27 15:22:08',NULL,1),(165,114,32,'OUT',2,NULL,'INSERT','2025-09-27 15:22:08',NULL,2),(166,115,33,'OUT',3,NULL,'INSERT','2025-09-27 15:22:08',NULL,3),(167,116,34,'OUT',1,NULL,'INSERT','2025-09-27 15:24:31',NULL,1),(168,117,32,'OUT',2,NULL,'INSERT','2025-09-27 15:24:31',NULL,2),(169,118,33,'OUT',3,NULL,'INSERT','2025-09-27 15:24:31',NULL,3),(170,119,34,'OUT',1,NULL,'INSERT','2025-09-27 15:38:52',NULL,1),(171,120,32,'OUT',2,NULL,'INSERT','2025-09-27 15:38:52',NULL,2),(172,121,33,'OUT',3,NULL,'INSERT','2025-09-27 15:38:52',NULL,3),(173,122,34,'OUT',1,NULL,'INSERT','2025-09-27 16:06:32',NULL,1),(174,123,32,'OUT',2,NULL,'INSERT','2025-09-27 16:06:32',NULL,2),(175,124,33,'OUT',3,NULL,'INSERT','2025-09-27 16:06:32',NULL,3),(176,125,34,'OUT',1,NULL,'INSERT','2025-09-27 16:14:36',NULL,1),(177,126,32,'OUT',2,NULL,'INSERT','2025-09-27 16:14:36',NULL,2),(178,127,33,'OUT',3,NULL,'INSERT','2025-09-27 16:14:36',NULL,3),(179,128,34,'OUT',1,NULL,'INSERT','2025-09-27 16:58:29',NULL,1),(180,129,32,'OUT',2,NULL,'INSERT','2025-09-27 16:58:29',NULL,2),(181,130,33,'OUT',3,NULL,'INSERT','2025-09-27 16:58:29',NULL,3),(182,64,34,'OUT',2,NULL,'DELETE','2025-09-27 18:03:09',2,NULL),(183,65,30,'OUT',1,NULL,'DELETE','2025-09-27 18:03:10',1,NULL),(184,66,31,'OUT',1,NULL,'DELETE','2025-09-27 18:03:10',1,NULL),(185,67,33,'OUT',4,NULL,'DELETE','2025-09-27 18:03:10',4,NULL),(186,68,32,'OUT',3,NULL,'DELETE','2025-09-27 18:03:10',3,NULL),(187,69,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:10',1,NULL),(188,70,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:10',2,NULL),(189,71,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:10',3,NULL),(190,72,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:11',1,NULL),(191,73,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:11',2,NULL),(192,74,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:11',3,NULL),(193,75,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:11',1,NULL),(194,76,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:11',2,NULL),(195,77,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:11',1,NULL),(196,78,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:11',2,NULL),(197,79,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:12',3,NULL),(198,80,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:12',1,NULL),(199,81,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:12',2,NULL),(200,82,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:12',3,NULL),(201,83,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:12',1,NULL),(202,84,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:13',2,NULL),(203,85,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:13',3,NULL),(204,86,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:13',1,NULL),(205,87,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:13',2,NULL),(206,88,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:13',3,NULL),(207,89,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:13',1,NULL),(208,90,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:13',2,NULL),(209,91,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:14',3,NULL),(210,92,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:14',1,NULL),(211,93,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:14',2,NULL),(212,94,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:14',3,NULL),(213,95,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:14',1,NULL),(214,97,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:16',3,NULL),(215,96,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:17',2,NULL),(216,98,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:17',1,NULL),(217,99,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:17',2,NULL),(218,100,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:17',3,NULL),(219,101,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:17',1,NULL),(220,102,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:17',2,NULL),(221,103,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:18',3,NULL),(222,104,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:18',1,NULL),(223,105,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:18',2,NULL),(224,106,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:18',3,NULL),(225,107,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:19',1,NULL),(226,108,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:19',2,NULL),(227,109,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:19',3,NULL),(228,110,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:19',1,NULL),(229,111,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:19',2,NULL),(230,112,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:19',3,NULL),(231,113,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:20',1,NULL),(232,114,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:20',2,NULL),(233,115,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:20',3,NULL),(234,116,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:20',1,NULL),(235,117,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:20',2,NULL),(236,118,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:20',3,NULL),(237,119,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:21',1,NULL),(238,120,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:21',2,NULL),(239,121,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:21',3,NULL),(240,122,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:21',1,NULL),(241,123,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:21',2,NULL),(242,124,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:21',3,NULL),(243,125,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:22',1,NULL),(244,126,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:22',2,NULL),(245,127,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:22',3,NULL),(246,128,34,'OUT',1,NULL,'DELETE','2025-09-27 18:03:22',1,NULL),(247,129,32,'OUT',2,NULL,'DELETE','2025-09-27 18:03:22',2,NULL),(248,130,33,'OUT',3,NULL,'DELETE','2025-09-27 18:03:22',3,NULL),(249,131,34,'IN',120,'2025-10-11','INSERT','2025-09-27 18:03:37',NULL,120),(250,132,30,'IN',120,'2025-10-11','INSERT','2025-09-27 18:03:46',NULL,120),(251,133,30,'IN',150,'2025-10-11','INSERT','2025-09-27 18:03:51',NULL,150),(252,134,34,'OUT',1,NULL,'INSERT','2025-09-27 18:34:42',NULL,1),(253,135,30,'OUT',1,NULL,'INSERT','2025-09-27 18:34:42',NULL,1),(254,136,30,'OUT',1,NULL,'INSERT','2025-09-30 10:11:31',NULL,1),(255,137,31,'OUT',2,NULL,'INSERT','2025-09-30 10:11:31',NULL,2),(256,138,31,'OUT',2,NULL,'INSERT','2025-09-30 10:11:31',NULL,2),(257,139,32,'OUT',2,NULL,'INSERT','2025-09-30 10:11:31',NULL,2),(258,140,34,'OUT',2,NULL,'INSERT','2025-09-30 10:11:31',NULL,2),(259,134,34,'OUT',1,NULL,'DELETE','2025-10-01 05:26:10',1,NULL),(260,141,35,'IN',120,NULL,'INSERT','2025-10-01 11:44:03',NULL,120),(261,142,30,'OUT',1,NULL,'INSERT','2025-10-01 13:25:24',NULL,1),(262,143,33,'OUT',2,NULL,'INSERT','2025-10-01 13:25:24',NULL,2),(263,144,34,'OUT',1,NULL,'INSERT','2025-10-01 13:25:24',NULL,1),(264,145,34,'OUT',1,NULL,'INSERT','2025-10-01 13:25:24',NULL,1),(265,146,30,'OUT',1,NULL,'INSERT','2025-10-01 13:25:24',NULL,1),(266,147,34,'OUT',5,NULL,'INSERT','2025-10-01 13:25:24',NULL,5),(267,148,35,'OUT',1,NULL,'INSERT','2025-10-01 13:25:24',NULL,1),(268,149,33,'OUT',1,NULL,'INSERT','2025-10-01 13:25:24',NULL,1),(269,150,34,'OUT',1,NULL,'INSERT','2025-10-01 13:25:24',NULL,1),(270,151,33,'OUT',1,NULL,'INSERT','2025-10-01 13:26:23',NULL,1),(271,152,34,'OUT',1,NULL,'INSERT','2025-10-01 13:26:23',NULL,1),(272,131,34,'IN',120,'2025-10-11','UPDATE','2025-10-01 16:49:35',120,120),(273,131,34,'IN',120,'2025-11-11','UPDATE','2025-10-01 16:52:32',120,120),(274,131,34,'IN',120,'2025-09-11','UPDATE','2025-10-01 16:53:02',120,120);
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
  `setting_key` varchar(100) NOT NULL,
  `setting_value` varchar(200) NOT NULL,
  `description` text,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`setting_id`),
  UNIQUE KEY `setting_key` (`setting_key`)
) ENGINE=InnoDB AUTO_INCREMENT=76 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `system_settings`
--

LOCK TABLES `system_settings` WRITE;
/*!40000 ALTER TABLE `system_settings` DISABLE KEYS */;
INSERT INTO `system_settings` VALUES (21,'default_currency','PHP','Default currency of the system','2025-09-12 17:11:00','2025-09-12 17:11:00'),(22,'currency_symbol','‚Ç±','Currency symbol for displaying prices','2025-09-12 17:11:00','2025-09-12 17:11:00'),(23,'invoice_prefix','INV','Prefix used when generating invoice numbers','2025-09-12 17:11:00','2025-09-12 17:11:00'),(52,'allow_negative_stock','0',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(53,'low_stock_threshold','10',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(54,'clinic_name','MA. CANDIE PEARL O. BASCOS-VILLENA, MD. FPSO-HNS',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(55,'clinic_address','388 E. Lopez St., Jaro, Iloilo City',NULL,'2025-09-24 08:17:22','2025-09-24 08:30:18'),(56,'clinic_tel','329-1796',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(57,'clinic_mobile','0925-5000149',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(58,'clinic_hours','Monday, Tuesday, Thursday, Friday, Saturday 11:00 AM ‚Äì 2:00 PM',NULL,'2025-09-24 08:17:22','2025-09-24 08:40:51'),(59,'clinic_affiliations','St. Paul‚Äôs Hospital, Iloilo Doctors‚Äô Hospital, Iloilo Mission Hospital, Western Visayas Medical Center, WVSU Med Center, Medicus Ambulatory, Metro Iloilo Hospital & Med. Center, Inc.',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(60,'report_header','ENT CLINIC ',NULL,'2025-09-24 08:17:22','2025-10-01 12:03:16'),(61,'report_footer','ENT Clinic System @2025','MA. CANDIE PEARL O. BASCOS-VILLENA, MD. FPSO-HNS','2025-09-24 08:17:22','2025-10-01 14:07:12'),(62,'date_format','yyyy-MM-dd',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(63,'time_format','hh:mm tt',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(64,'records_per_page','20',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(65,'markup_percentage','50',NULL,'2025-09-24 08:17:22','2025-09-24 08:17:22'),(66,'clinic_subtitle','Fellow, Phil. Society of Otolaryngology, Head & Neck Surgery',NULL,'2025-09-24 08:28:15','2025-09-24 08:53:37'),(67,'clinic_email','cpbascosmd@yahoo.com',NULL,'2025-09-24 08:42:52','2025-09-24 08:45:01'),(68,'license_number','LIC. NO. 102585',NULL,'2025-09-25 06:27:50','2025-09-25 06:27:50'),(69,'printer_name','XP-58',NULL,'2025-09-27 13:34:46','2025-09-27 14:23:29');
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

    -- 1Ô∏è‚É£ Decrease stock quantity in items table
    UPDATE items
    SET stock_quantity = stock_quantity - NEW.quantity
    WHERE item_id = NEW.item_id;

    -- 2Ô∏è‚É£ Insert into stock_movements table as OUT
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

-- Dump completed on 2025-10-03  1:35:19
