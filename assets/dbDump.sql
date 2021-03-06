create table `develop2020`.`alt2022_booking` (
	`ID` INT NOT NULL AUTO_INCREMENT,
    `NAME_CLIENT` VARCHAR(60),
    `DT_START` DATE NOT NULL,
    `DT_END` DATE NOT NULL,
    `DT_RESERVATION` DATE NOT NULL,
    `ACTIVE` BIT,
    PRIMARY KEY (`ID`)
);

--
CREATE PROCEDURE STP_ALT2022_GET_AVAILABILITY(Pinitial_date date, Pfinal_date date)
BEGIN
	select * from alt2022_booking where DT_START >= Pinitial_date and DT_START <= Pfinal_date;
END;

--
CREATE PROCEDURE STP_ALT2022_INSERT_BOOKING(Pname_client varchar(60), Pstart_date date, Pend_date date, Preservation_date date, Pactive bit)
BEGIN
	INSERT INTO `alt2022_booking`
		(
			`NAME_CLIENT`,
			`DT_START`,
            `DT_END`,
            `DT_RESERVATION`,
            `ACTIVE`
        )
	VALUES
		(
            Pname_client,
            Pstart_date,
            Pend_date,
            Preservation_date,
            Pactive
        );
END;

--
CREATE PROCEDURE STP_ALT2022_DELETE_BOOKING(Pid int)
BEGIN
	UPDATE `alt2022_booking` SET `ACTIVE` = FALSE
    where ID = Pid;
END;

--
CREATE PROCEDURE STP_ALT2022_UPDATE_BOOKING(Pid int, Pname_client varchar(60), Pstart_date date, Pend_date date, Preservation_date date, Pactive bit)
BEGIN
	UPDATE `alt2022_booking`
	SET
	`NAME_CLIENT` = Pname_client,
	`DT_START` = Pstart_date,
	`DT_END` = Pend_date,
	`DT_RESERVATION` = Preservation_date,
	`ACTIVE` = Pactive
	WHERE `ID` = Pid;
END;