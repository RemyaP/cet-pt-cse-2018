-- MySQL Workbench Synchronization
-- Generated: 2018-03-18 12:33
-- Model: New Model
-- Version: 1.0
-- Project: Name of the project
-- Author: Remya

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

CREATE SCHEMA IF NOT EXISTS `real` DEFAULT CHARACTER SET utf8 ;

CREATE TABLE IF NOT EXISTS `real`.`user` (
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
    REFERENCES `real`.`status` (`status_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real`.`seller` (
  `seller_id` INT(11) NOT NULL AUTO_INCREMENT,
  `seller_type` INT(11) NOT NULL,
  `user_id` INT(10) UNSIGNED NOT NULL,
  PRIMARY KEY (`seller_id`),
  INDEX `fk_seller_user_idx` (`user_id` ASC),
  CONSTRAINT `fk_seller_user`
    FOREIGN KEY (`user_id`)
    REFERENCES `real`.`user` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real`.`buyer` (
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
    REFERENCES `real`.`user` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real`.`plot` (
  `plot_id` INT(11) NOT NULL AUTO_INCREMENT,
  `area` DOUBLE UNSIGNED NULL DEFAULT NULL,
  `latitude` DOUBLE NULL DEFAULT NULL,
  `longitude` DOUBLE NULL DEFAULT NULL,
  `min_price` INT(10) UNSIGNED NULL DEFAULT NULL,
  `seller_id` INT(11) NOT NULL,
  PRIMARY KEY (`plot_id`),
  INDEX `fk_plot_seller1_idx` (`seller_id` ASC),
  CONSTRAINT `fk_plot_seller1`
    FOREIGN KEY (`seller_id`)
    REFERENCES `real`.`seller` (`seller_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real`.`bid` (
  `bid_id` INT(11) NOT NULL AUTO_INCREMENT,
  `plot_id` INT(11) NOT NULL,
  `buyer_id` INT(11) NOT NULL,
  `status` INT(10) UNSIGNED NULL DEFAULT NULL,
  `amount` DOUBLE UNSIGNED NULL DEFAULT NULL,
  PRIMARY KEY (`bid_id`),
  INDEX `fk_purchase_plot1_idx` (`plot_id` ASC),
  INDEX `fk_purchase_buyer1_idx` (`buyer_id` ASC),
  CONSTRAINT `fk_purchase_plot1`
    FOREIGN KEY (`plot_id`)
    REFERENCES `real`.`plot` (`plot_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_purchase_buyer1`
    FOREIGN KEY (`buyer_id`)
    REFERENCES `real`.`buyer` (`buyer_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real`.`institutes` (
  `id` INT(10) UNSIGNED NOT NULL,
  `name` VARCHAR(45) NULL DEFAULT NULL,
  `latitude` DOUBLE NULL DEFAULT NULL,
  `longitude` DOUBLE NULL DEFAULT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real`.`landmark` (
  `plot_id` INT(11) NOT NULL,
  `landmark_id` INT(10) UNSIGNED NOT NULL,
  INDEX `fk_landmark_plot_idx` (`plot_id` ASC),
  INDEX `fk_landmark_landmark_idx` (`landmark_id` ASC),
  PRIMARY KEY (`plot_id`, `landmark_id`),
  CONSTRAINT `fk_landmark_plot1`
    FOREIGN KEY (`plot_id`)
    REFERENCES `real`.`plot` (`plot_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_landmark_table11`
    FOREIGN KEY (`landmark_id`)
    REFERENCES `real`.`institutes` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_landmark_hospital1`
    FOREIGN KEY (`landmark_id`)
    REFERENCES `real`.`hospital` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_landmark_railway1`
    FOREIGN KEY (`landmark_id`)
    REFERENCES `real`.`railway` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_landmark_temple1`
    FOREIGN KEY (`landmark_id`)
    REFERENCES `real`.`temple` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_landmark_mosque1`
    FOREIGN KEY (`landmark_id`)
    REFERENCES `real`.`mosque` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_landmark_church1`
    FOREIGN KEY (`landmark_id`)
    REFERENCES `real`.`church` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real`.`hospital` (
  `id` INT(10) UNSIGNED NOT NULL,
  `name` VARCHAR(45) NULL DEFAULT NULL,
  `latitude` DOUBLE NULL DEFAULT NULL,
  `longitude` DOUBLE NULL DEFAULT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real`.`railway` (
  `id` INT(10) UNSIGNED NOT NULL,
  `name` VARCHAR(45) NULL DEFAULT NULL,
  `latitude` DOUBLE NULL DEFAULT NULL,
  `longitude` DOUBLE NULL DEFAULT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real`.`temple` (
  `id` INT(10) UNSIGNED NOT NULL,
  `name` VARCHAR(45) NULL DEFAULT NULL,
  `latitude` DOUBLE NULL DEFAULT NULL,
  `longitude` DOUBLE NULL DEFAULT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real`.`mosque` (
  `id` INT(10) UNSIGNED NOT NULL,
  `name` VARCHAR(45) NULL DEFAULT NULL,
  `latitude` DOUBLE NULL DEFAULT NULL,
  `longitude` DOUBLE NULL DEFAULT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real`.`church` (
  `id` INT(10) UNSIGNED NOT NULL,
  `name` VARCHAR(45) NULL DEFAULT NULL,
  `latitude` DOUBLE NULL DEFAULT NULL,
  `longitude` DOUBLE NULL DEFAULT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real`.`images` (
  `plot_id` INT(11) NOT NULL,
  `image` BLOB NOT NULL,
  INDEX `fk_images_plot1_idx` (`plot_id` ASC),
  CONSTRAINT `fk_images_plot1`
    FOREIGN KEY (`plot_id`)
    REFERENCES `real`.`plot` (`plot_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `real`.`status` (
  `status_id` INT(11) NOT NULL AUTO_INCREMENT,
  `status` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`status_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
