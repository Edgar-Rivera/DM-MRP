drop database dm_occidente;
create database dm_occidente;
use dm_occidente;


INSERT INTO ROL VALUES (111,'ADMINISTRADOR', TRUE);
INSERT INTO ROL VALUES (112,'VENDEDOR', TRUE );
INSERT INTO ROL VALUES (113,'CAJERO', TRUE);


INSERT INTO USUARIO VALUES ('riveraedgar',111,'1195','Edgar Rivera','riveraambrocio.edgar@gmail.com',TRUE,0,TRUE);


INSERT INTO TIPO_CLIENTE VALUES (1001,'CREDITO',TRUE);
INSERT INTO TIPO_CLIENTE VALUES (NULL,'EFECTIVO',TRUE);

INSERT INTO TIPO_SUCURSAL VALUES (NULL,'CENTRAL',TRUE);
INSERT INTO TIPO_SUCURSAL VALUES (NULL,'SUCURSAL',TRUE);
INSERT INTO TIPO_SUCURSAL VALUES (NULL,'BODEGA',TRUE);



INSERT INTO SUCURSAL VALUES(1003,3,'Bodega Sur','Mazatenango 12 Avenida','22457884',TRUE);

ALTER TABLE  materia_prima ADD FOREIGN KEY(id_proveedor) REFERENCES Proveedor(id_proveedor);

select * from clasificacion_mercaderia;

insert into clasificacion_mercaderia values (1,'Basico',1);
insert into clasificacion_mercaderia values (2,'Liquido / Masa',1);

SELECT * FROM UNIDAD_MEDIDA;

insert into unidad_medida values (1,'cm',1);
insert into unidad_medida values (2,'m',1);
insert into unidad_medida values (3,'lb',1);
insert into unidad_medida values (4,'g',1);
insert into unidad_medida values (5,'mg',1);
insert into unidad_medida values (6,'oz',1);
insert into unidad_medida values (7,'ml',1);
insert into unidad_medida values (8,'unidad',1);


describe TRASLADO_MATERIA_PRIMA;