-- MySQL Workbench Synchronization
-- Generated: 2018-03-24 15:21
-- Model: New Model
-- Version: 1.0
-- Project: Name of the project
-- Author: Remya

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

CREATE SCHEMA IF NOT EXISTS `real_estate` DEFAULT CHARACTER SET utf8 ;

CREATE TABLE IF NOT EXISTS `real_estate`.`user` (
  `user_id` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `user_name` VARCHAR(20) NOT NULL,
  `password` VARCHAR(20) NOT NULL,
  `first_name` VARCHAR(45) NULL DEFAULT NULL,
  `last_name` VARCHAR(45) NULL DEFAULT NULL,
  `address` VARCHAR(45) NULL DEFAULT NULL,
  `pan_no` VARCHAR(45) NULL DEFAULT NULL,
  `mob_no` VARCHAR(45) NULL DEFAULT NULL,
  `email` VARCHAR(45) NULL DEFAULT NULL,
  `status_id` INT(11) NOT NULL,
  PRIMARY KEY (`user_id`),
  INDEX `fk_user_status1_idx` (`status_id` ASC),
  CONSTRAINT `fk_user_status1`
    FOREIGN KEY (`status_id`)
    REFERENCES `real_estate`.`status` (`status_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real_estate`.`seller` (
  `seller_id` INT(11) NOT NULL AUTO_INCREMENT,
  `seller_type` INT(11) NOT NULL,
  `user_id` INT(10) UNSIGNED NOT NULL,
  PRIMARY KEY (`seller_id`),
  INDEX `fk_seller_user_idx` (`user_id` ASC),
  CONSTRAINT `fk_seller_user`
    FOREIGN KEY (`user_id`)
    REFERENCES `real_estate`.`user` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real_estate`.`buyer` (
  `buyer_id` INT(11) NOT NULL AUTO_INCREMENT,
  `min_area` DOUBLE UNSIGNED NULL DEFAULT NULL,
  `max_area` DOUBLE UNSIGNED NULL DEFAULT NULL,
  `min_cost` INT(10) UNSIGNED NULL DEFAULT NULL,
  `max_cost` INT(10) UNSIGNED NULL DEFAULT NULL,
  `user_id` INT(10) UNSIGNED NOT NULL,
  PRIMARY KEY (`buyer_id`),
  INDEX `fk_buyer_user1_idx` (`user_id` ASC),
  CONSTRAINT `fk_buyer_user1`
    FOREIGN KEY (`user_id`)
    REFERENCES `real_estate`.`user` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real_estate`.`property` (
  `property_id` INT(11) NOT NULL AUTO_INCREMENT,
  `area` DOUBLE UNSIGNED NULL DEFAULT NULL,
  `latitude` DOUBLE NULL DEFAULT NULL,
  `longitude` DOUBLE NULL DEFAULT NULL,
  `seller_id` INT(11) NOT NULL,
  `category` INT(11) NOT NULL,
  PRIMARY KEY (`property_id`),
  INDEX `fk_plot_seller1_idx` (`seller_id` ASC),
  INDEX `fk_plot_category1_idx` (`category` ASC),
  CONSTRAINT `fk_plot_seller1`
    FOREIGN KEY (`seller_id`)
    REFERENCES `real_estate`.`seller` (`seller_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_plot_category1`
    FOREIGN KEY (`category`)
    REFERENCES `real_estate`.`category` (`category_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real_estate`.`bid` (
  `bid_id` INT(11) NOT NULL AUTO_INCREMENT,
  `property_id` INT(11) NOT NULL,
  `buyer_id` INT(11) NOT NULL,
  `status` INT(10) UNSIGNED NULL DEFAULT NULL,
  PRIMARY KEY (`bid_id`),
  INDEX `fk_purchase_plot1_idx` (`property_id` ASC),
  INDEX `fk_purchase_buyer1_idx` (`buyer_id` ASC),
  CONSTRAINT `fk_purchase_plot1`
    FOREIGN KEY (`property_id`)
    REFERENCES `real_estate`.`property` (`property_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_purchase_buyer1`
    FOREIGN KEY (`buyer_id`)
    REFERENCES `real_estate`.`buyer` (`buyer_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real_estate`.`property_has_landmark` (
  `property_id` INT(11) NOT NULL,
  `landmark_id` INT(10) UNSIGNED NOT NULL,
  INDEX `fk_landmark_plot_idx` (`property_id` ASC),
  PRIMARY KEY (`property_id`, `landmark_id`),
  CONSTRAINT `fk_landmark_plot1`
    FOREIGN KEY (`property_id`)
    REFERENCES `real_estate`.`property` (`property_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_landmark_railway1`
    FOREIGN KEY (`landmark_id`)
    REFERENCES `real_estate`.`landmark` (`landmark_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real_estate`.`landmark` (
  `landmark_id` INT(10) UNSIGNED NOT NULL,
  `name` VARCHAR(45) NULL DEFAULT NULL,
  `latitude` DOUBLE NULL DEFAULT NULL,
  `longitude` DOUBLE NULL DEFAULT NULL,
  `landmark_type` INT(11) NOT NULL,
  PRIMARY KEY (`landmark_id`),
  INDEX `fk_landmark_landmarktype1_idx` (`landmark_type` ASC),
  CONSTRAINT `fk_landmark_landmarktype1`
    FOREIGN KEY (`landmark_type`)
    REFERENCES `real_estate`.`landmarktype` (`type_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real_estate`.`images` (
  `property_id` INT(11) NOT NULL,
  `image` BLOB NOT NULL,
  INDEX `fk_images_plot1_idx` (`property_id` ASC),
  CONSTRAINT `fk_images_plot1`
    FOREIGN KEY (`property_id`)
    REFERENCES `real_estate`.`property` (`property_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real_estate`.`status` (
  `status_id` INT(11) NOT NULL AUTO_INCREMENT,
  `status` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`status_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real_estate`.`landmarktype` (
  `type_id` INT(11) NOT NULL AUTO_INCREMENT,
  `type_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`type_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real_estate`.`category` (
  `category_id` INT(11) NOT NULL AUTO_INCREMENT,
  `type` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`category_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real_estate`.`min_price` (
  `property_id` INT(11) NOT NULL,
  `plot_price` DOUBLE NULL DEFAULT NULL,
  `apartment_price` DOUBLE NULL DEFAULT NULL,
  PRIMARY KEY (`property_id`),
  CONSTRAINT `fk_min_price_property1`
    FOREIGN KEY (`property_id`)
    REFERENCES `real_estate`.`property` (`property_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real_estate`.`bid_price` (
  `bid_id` INT(11) NOT NULL,
  `plot_price` DOUBLE NULL DEFAULT NULL,
  `apartment_price` DOUBLE NULL DEFAULT NULL,
  PRIMARY KEY (`bid_id`),
  INDEX `fk_bid_price_bid1_idx` (`bid_id` ASC),
  CONSTRAINT `fk_bid_price_bid1`
    FOREIGN KEY (`bid_id`)
    REFERENCES `real_estate`.`bid` (`bid_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
