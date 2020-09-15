
-- VSITA PARA EL INIICIO Y ELECCION DE LA SUCURSAL CORRIENTE

CREATE OR REPLACE VIEW    
VISTA_SUCURSALES AS
SELECT S.ID_SUCURSAL,(SELECT T.TIPO_SUCURSAL FROM TIPO_SUCURSAL T WHERE ID_TIPO = S.ID_TIPO) AS TIPO,S.NOMBRE_SUCURSAL,S.DIRECCION_SUCURSAL,
S.TELEFONO_SUCURSAL FROM SUCURSAL S
WHERE S.ESTADO_TUPLA = TRUE;


-- VISTA PARA FUNCIONAMINETO DE TODO EL MODULO DE CLIENTE
CREATE OR REPLACE VIEW 
VISTA_CLIENTES AS
SELECT C.ID_CLIENTE, C.NOMBRE_CLIENTE, C.APELLIDO_CLIENTE,C.DIRECCION_CLIENTE,C.TELEFONO_CLIENTE,
 T.TIPO, C.LIMITE_CREDITO, C.CREDITO_DISPONIBLE, C.DIAS_CREDITO, C.FECHA_INGRESO FROM CLIENTE C
INNER JOIN TIPO_CLIENTE T
ON C.ID_TIPO_CLIENTE = T.ID_TIPO_CLIENTE AND C.ESTADO_TUPLA = TRUE;

  CREATE OR REPLACE VIEW
	VISTA_CLIENTE_EDIT 
    AS SELECT C.ID_CLIENTE, C.NOMBRE_CLIENTE, C.APELLIDO_CLIENTE, C.DIRECCION_CLIENTE,C.TELEFONO_CLIENTE,
    D.TIPO, C.LIMITE_CREDITO, C.DIAS_CREDITO, C.CREDITO_DISPONIBLE FROM CLIENTE C
    INNER JOIN TIPO_CLIENTE D
    ON D.ID_TIPO_CLIENTE = C.ID_TIPO_CLIENTE;
    

-- VISTA PARA EL FUCNIONAMINTO DEL MODULO DE PROVEEDORES

CREATE OR REPLACE VIEW  
VISTA_PROVEEDORES AS
SELECT P.ID_PROVEEDOR,P.NOMBRE_PROVEEDOR,P.EMAIL_PROVEEDOR,P.DIRECCION_PROVEEDOR,P.TELEFONO_PROVEEDOR FROM PROVEEDOR P 
WHERE P.ESTADO_TUPLA = TRUE;


 CREATE OR REPLACE VIEW 
   VISTA_FACTURAS_CLIENTE
    AS SELECT 
      CASE 
            WHEN F.NUMERO = NULL THEN F.ID_FACTURA
            ELSE F.NUMERO
        END NUMERO ,
         F.FECHA_EMISION_FACTURA, F.TIPO_FACTURA,
    F.FECHA_VENCIMIENTO_FACTURA, F.TOTAL_FACTURA,F.ID_CLIENTE 
    FROM FACTURA F
    INNER JOIN CLIENTE C
    ON F.ID_CLIENTE = C.ID_CLIENTE
    INNER JOIN USUARIO U
    ON F.VENDEDOR = U.ID_USUARIO
    INNER JOIN SUCURSAL S
    ON F.ID_SUCURSAL = S.ID_SUCURSAL;
    
    CREATE OR REPLACE VIEW 
 VISTA_COMPRA AS
  SELECT C.ID_COMPRA,P.NOMBRE_PROVEEDOR, C.FECHA_COMPRA,
  IF((SELECT ID_COMPRA FROM CUENTA_POR_PAGAR WHERE ID_COMPRA = C.ID_COMPRA) ,'COMPRA CREDITO', 'COMPRA EFECTIVO') AS TIPO_COMPRA, 
  C.SUBTOTAL_COMPRA, C.DESCUENTO, C.TOTAL_FINAL, S.NOMBRE_SUCURSAL,C.ID_USUARIO  FROM COMPRA C 
  INNER JOIN SUCURSAL S
  ON C.ID_SUCURSAL = S.ID_SUCURSAL AND C.ESTADO_TUPLA = TRUE
  INNER JOIN PROVEEDOR P 
  ON C.ID_PROVEEDOR = P.ID_PROVEEDOR;
  
  
   CREATE OR REPLACE VIEW TOTAL_DEUDA AS
 SELECT C.TOTAL_FINAL, D.TOTAL_PAGADO, 
 (SELECT NOMBRE_PROVEEDOR FROM PROVEEDOR WHERE ID_PROVEEDOR = C.ID_PROVEEDOR) AS PROVEEDOR FROM COMPRA C 
 INNER JOIN CUENTA_POR_PAGAR D 
 ON C.ID_COMPRA = D.ID_COMPRA AND C.ESTADO_TUPLA = TRUE AND D.ESTADO_CUENTA = TRUE AND D.ESTADO_TUPLA = TRUE;
    
     
CREATE OR REPLACE VIEW 
VISTA_CONTROL_COMPRAS  AS
 SELECT  C.ID_MATERIA_PRIMA, M.DESCRIPCION,C.CANTIDAD_COMPRA, 
 C.PRECIO_COMPRA, 
 C.OTROS_GASTOS, C.SUBTOTAL, C.ID_COMPRA  FROM  CONTROL_COMPRA C
  INNER JOIN MATERIA_PRIMA M 
  ON C.ID_MATERIA_PRIMA = M.ID_MATERIA_PRIMA
  INNER JOIN COMPRA D
  ON D.ID_COMPRA = C.ID_COMPRA AND C.ESTADO_TUPLA = TRUE;
  



-- VISTA PARA MOSTRAR LA MATERIA PRIMA CUANDO SE VA INGRESAR O EDITAR
CREATE OR REPLACE VIEW 
VISTA_MATERIA_PRIMA AS 
 SELECT M.ID_MATERIA_PRIMA, M.DESCRIPCION ,
 (SELECT NOMBRE_PROVEEDOR FROM PROVEEDOR WHERE ID_PROVEEDOR = M.ID_PROVEEDOR) AS PROVEEDOR,S.PRECIO, S.TOTAL_UNIDADES,
 (SELECT NOMBRE_SUCURSAL FROM SUCURSAL WHERE ID_SUCURSAL = S.ID_SUCURSAL) AS SUCURSAL,
 (SELECT NOMBRE FROM UNIDAD_MEDIDA WHERE ID_UNIDAD_MEDIDA = M.ID_UNIDAD_MEDIDA) AS MEDIDA
 FROM MATERIA_PRIMA M 
 INNER JOIN SUCURSAL_HAS_MATERIA_PRIMA S 
 ON M.ID_MATERIA_PRIMA = S.ID_MATERIA_PRIMA;
 


-- VSITA PARA MOSTRAR EL HISTORIAL DE PRECIOS DE MATERIA PRIMA
CREATE OR REPLACE VIEW 
 VISTA_KARDEX_COMPRA
  AS SELECT C.FECHA_COMPRA, M.DESCRIPCION, C.ID_COMPRA, P.NOMBRE_PROVEEDOR, N.NOMBRE_SUCURSAL, 
  S.CANTIDAD_COMPRA, S.PRECIO_COMPRA, M.ID_MATERIA_PRIMA FROM COMPRA C
  INNER JOIN CONTROL_COMPRA S
  ON S.ID_COMPRA = C.ID_COMPRA 
  INNER JOIN MATERIA_PRIMA M
  ON M.ID_MATERIA_PRIMA = S.ID_MATERIA_PRIMA AND C.ESTADO_TUPLA = TRUE AND C.ID_SUCURSAL
  INNER JOIN sucursal_has_materia_prima L 
  ON M.ID_MATERIA_PRIMA = L.ID_MATERIA_PRIMA
  INNER JOIN SUCURSAL N 
  ON N.ID_SUCURSAL = L.ID_SUCURSAL 
  INNER JOIN PROVEEDOR P 
  ON C.ID_PROVEEDOR = P.ID_PROVEEDOR;

-- VISTA PARA PODER MSTRAR LAS CUENTAS POR PAGAR
  CREATE OR REPLACE VIEW 
  VISTA_PAGAR 
   AS SELECT P.ID_CUENTA_POR_PAGAR, P.ID_PROVEEDOR , C.NOMBRE_PROVEEDOR, C.DIRECCION_PROVEEDOR,COUNT(*), 
   (SELECT SUM(TOTAL_PAGADO) FROM CUENTA_POR_PAGAR WHERE ID_PROVEEDOR = P.ID_PROVEEDOR 
	AND ESTADO_TUPLA = TRUE) AS PAGADO,
   P.FECHA_PAGO, P.ESTADO_TUPLA
   FROM CUENTA_POR_PAGAR P
   INNER JOIN COMPRA D 
   ON D.ID_COMPRA = P.ID_COMPRA
   INNER JOIN PROVEEDOR C
   ON P.ID_PROVEEDOR = C.ID_PROVEEDOR WHERE P.ESTADO_TUPLA = TRUE AND P.ESTADO_CUENTA = TRUE GROUP BY
   P.ID_PROVEEDOR;
   
   -- VISTA PAR AMOSGTRAR LAS CUENTAS CCANCELADAS
    CREATE OR REPLACE VIEW 
   VISTA_CUENTA_PAGARF 
     AS SELECT   C.ID_COMPRA, F.FECHA_COMPRA, C.DESCRIPCION, 
     C.FECHA_PAGO, C.TOTAL_COMPRA, C.TOTAL_PAGADO, C.ID_CUENTA_POR_PAGAR, 
     S.ID_PROVEEDOR FROM CUENTA_POR_PAGAR C
     INNER JOIN COMPRA F 
     ON C.ID_COMPRA = F.ID_COMPRA AND C.ESTADO_TUPLA = FALSE AND C.ESTADO_CUENTA = TRUE
     INNER JOIN PROVEEDOR S 
     ON C.ID_PROVEEDOR = S.ID_PROVEEDOR;
     
     -- MUESTRA LAS COMOPRAS PENDIENTES 
        
   CREATE OR REPLACE VIEW 
   VISTA_CUENTA_PAGAR 
     AS SELECT   C.ID_COMPRA, F.FECHA_COMPRA, C.DESCRIPCION, 
     C.FECHA_PAGO, C.TOTAL_COMPRA, C.TOTAL_PAGADO, C.ID_CUENTA_POR_PAGAR, 
     S.ID_PROVEEDOR FROM CUENTA_POR_PAGAR C
     INNER JOIN COMPRA F 
     ON C.ID_COMPRA = F.ID_COMPRA AND C.ESTADO_TUPLA = TRUE AND C.ESTADO_CUENTA = TRUE
     INNER JOIN PROVEEDOR S 
     ON C.ID_PROVEEDOR = S.ID_PROVEEDOR;
     
     -- VISTA DE DETALLE DE ABNOS DE UNA CUENTA EN ESPCIFICO
       CREATE VIEW 
   VISTA_ABONOSP
    AS SELECT P.ID_PAGO, P.ID_CUENTA_POR_PAGAR, P.FECHA_PAGO, P.SUBTTOTAL_PAGO,
    P.DESCUENTO, P.TOTAL_PAGO FROM PAGO_CUENTA P WHERE P.ESTADO_TUPLA = TRUE ;
     
	-- MUESTRA LOS ABONOS REALIZADOS A LOS PROVEEDORES
    CREATE OR REPLACE VIEW 
    RECORD_ABONOS_PROVEEDOR AS
   SELECT S.ID_PROVEEDOR, S.NOMBRE_PROVEEDOR,
    C.ID_COMPRA,P.ID_PAGO, P.ID_CUENTA_POR_PAGAR, P.TOTAL_PAGO, P.FECHA_PAGO FROM PAGO_CUENTA P 
   INNER JOIN CUENTA_POR_PAGAR C
   ON P.ID_CUENTA_POR_PAGAR = C.ID_CUENTA_POR_PAGAR AND P.ESTADO_TUPLA = TRUE
   INNER JOIN PROVEEDOR S 
   ON S.ID_PROVEEDOR = C.ID_PROVEEDOR;
   
   -- VSITA EN EL FORMUALRIO DE LA MATERIA PRIMA
   CREATE OR REPLACE VIEW 
VISTA_MATERIA_PRIMA_FORM AS 
 SELECT M.ID_MATERIA_PRIMA, M.DESCRIPCION , (SELECT NOMBRE FROM UNIDAD_MEDIDA WHERE ID_UNIDAD_MEDIDA = M.ID_UNIDAD_MEDIDA) AS MEDIDA,
 (SELECT NOMBRE_PROVEEDOR FROM PROVEEDOR WHERE ID_PROVEEDOR = M.ID_PROVEEDOR) AS PROVEEDOR,S.PRECIO, S.TOTAL_UNIDADES,
 (SELECT NOMBRE_SUCURSAL FROM SUCURSAL WHERE ID_SUCURSAL = S.ID_SUCURSAL) AS SUCURSAL,
 IF(S.ESTADO_TUPLA,"ACTIVO","INACTIVO") AS ESTADO
 FROM MATERIA_PRIMA M 
 INNER JOIN SUCURSAL_HAS_MATERIA_PRIMA S 
 ON M.ID_MATERIA_PRIMA = S.ID_MATERIA_PRIMA;
 
 -- vsita de traslados de materia prima
  CREATE OR REPLACE VIEW VISTA_TRASLADOS_MATERIA
  AS SELECT T.ID_TRASLADO, T.FECHA_TRASLADO, S.NOMBRE_USUARIO, 
  (SELECT NOMBRE_SUCURSAL FROM SUCURSAL WHERE ID_SUCURSAL = T.TRASLADO_DE_SUCURSAL) AS TRASLADO_DE ,
  (SELECT NOMBRE_SUCURSAL FROM SUCURSAL WHERE ID_SUCURSAL = T.TRASLADO_A_SUCURSAL) AS TRASLADO_DESTINO,
  (SELECT COUNT(ID_DETALLE_TRASLADO) FROM MATERIA_TRASLADO WHERE ID_TRASLADO = T.ID_TRASLADO AND ESTADO_TUPLA = TRUE) AS NUMERO_PRODUCTOS
  , IF(T.ESTADO_TUPLA, "RECIBIDO", "NO RECIBIDO") AS ESTADO_TRASLADO
  FROM TRASLADOS_MATERIA_PRIMA T 
  INNER JOIN USUARIO S 
  ON T.ID_USUARIO = S.ID_USUARIO AND T.ESTADO_TUPLA = TRUE;
  
  -- MUESTRA EL DETALLE DE TRASLADO AL DAR DOBLE CLICK
    CREATE OR REPLACE VIEW VISTA_DETALLE_TRASLADO_MATERIA
   AS SELECT P.ID_DETALLE_TRASLADO, M.ID_MATERIA_PRIMA, M.DESCRIPCION,
   (SELECT NOMBRE_PROVEEDOR FROM PROVEEDOR WHERE ID_PROVEEDOR = M.ID_PROVEEDOR) AS PROVEEDOR, 
   (SELECT NOMBRE_SUCURSAL FROM SUCURSAL WHERE ID_SUCURSAL = T.TRASLADO_DE_SUCURSAL) AS SUCURSAL_ORIGEN,
   P.UNIDADES_TRASLADADAS, P.ID_TRASLADO FROM MATERIA_TRASLADO P 
   INNER JOIN MATERIA_PRIMA M 
   ON P.ID_MATERIA_PRIMA = M.ID_MATERIA_PRIMA 
   INNER JOIN TRASLADOS_MATERIA_PRIMA T 
   ON P.ID_TRASLADO = T.ID_TRASLADO;


-- MUESTRA LAS SUCURSALES 
CREATE OR REPLACE VIEW VISTA_SUCURSALES_PRECIOS_MATERIA AS
    SELECT S.ID_SUCURSAL, S.NOMBRE_SUCURSAL, M.id_materia_prima FROM SUCURSAL S
    INNER JOIN sucursal_has_materia_prima M ON M.ID_SUCURSAL = S.ID_SUCURSAL
    ORDER BY ID_MATERIA_PRIMA;



-- VISTA QUE MUESTRA EL DISPNBIEL EN LAS SUCURSALES

CREATE OR REPLACE VIEW 
 VISTA_GENERAL_MATERIA 
  AS SELECT  M.ID_MATERIA_PRIMA, 
  (SELECT DESCRIPCION FROM MATERIA_PRIMA WHERE ID_MATERIA_PRIMA = M.ID_MATERIA_PRIMA )AS DESCRIPCION, 
  S.NOMBRE_SUCURSAL,M.TOTAL_UNIDADES, M.PRECIO FROM SUCURSAL_HAS_MATERIA_PRIMA M
  INNER JOIN SUCURSAL S
  ON M.ID_SUCURSAL = S.ID_SUCURSAL;
  
  
  
    CREATE VIEW VISTA_GASTOS AS 
    SELECT G.IDGASTO, S.NOMBRE_SUCURSAL, U.NOMBRE_USUARIO, G.FECHA_GASTO, G.DESCRIPCION ,
    G.OBSERVACIONES, G.MONTO FROM GASTOS G 
    INNER JOIN SUCURSAL S 
    ON G.ID_SUCURSAL = S.ID_SUCURSAL
    INNER JOIN USUARIO U 
    ON G.ID_USUARIO = U.ID_USUARIO AND G.ESTADO_TUPLA = TRUE;
    
       CREATE VIEW VISTA_GASTOS_BUSCAR AS 
    SELECT G.IDGASTO, G.DESCRIPCION, U.NOMBRE_USUARIO, G.FECHA_GASTO,  
    G.MONTO, G.OBSERVACIONES,S.NOMBRE_SUCURSAL  FROM GASTOS G 
    INNER JOIN SUCURSAL S 
    ON G.ID_SUCURSAL = S.ID_SUCURSAL
    INNER JOIN USUARIO U 
    ON G.ID_USUARIO = U.ID_USUARIO AND G.ESTADO_TUPLA = TRUE;
    

-- VISTA PARA MOSTRAR LA MATERIA PRIMA CUANDO SE VA INGRESAR O EDITAR
CREATE OR REPLACE VIEW 
VISTA_MERCADERIA AS 
 SELECT M.ID_MERCADERIA, M.DESCRIPCION ,
 S.PRECIO, S.TOTAL_UNIDADES,
 (SELECT NOMBRE_SUCURSAL FROM SUCURSAL WHERE ID_SUCURSAL = S.ID_SUCURSAL) AS SUCURSAL,
 M.ID_CLASIFICACION
 FROM MERCADERIA M 
 INNER JOIN SUCURSAL_HAS_MERCADERIA S 
 ON M.ID_MERCADERIA = S.ID_MERCADERIA;
 
 -- CARGA LAS SUCRUSALES EN DONDE SE EN UENTRA EL PRODUCTO O MERCADERIA EN PRECIOS DE MERCADERIA
 CREATE OR REPLACE VIEW VISTA_SUCURSALES_PRECIOS_MERCADERIA AS
    SELECT S.ID_SUCURSAL, S.NOMBRE_SUCURSAL, M.id_mercaderia FROM SUCURSAL S
    INNER JOIN sucursal_has_mercaderia M ON M.ID_SUCURSAL = S.ID_SUCURSAL
    ORDER BY id_mercaderia;
 
-- VISTA EN EL FORMUALRIO DE MERCADERIA 
CREATE OR REPLACE VIEW 
VISTA_MERCADERIA_GENERAL AS 
 SELECT M.ID_MERCADERIA, M.DESCRIPCION ,
 S.PRECIO, S.TOTAL_UNIDADES, (SELECT tipo_mercaderia FROM clasificacion_mercaderia where id_tipo = m.id_clasificacion) as clasificacion,
 (SELECT NOMBRE_SUCURSAL FROM SUCURSAL WHERE ID_SUCURSAL = S.ID_SUCURSAL) AS SUCURSAL,
 IF(S.ESTADO_TUPLA , "ACTIVO","INACTIVO") AS ESTADO, M.FECHA_INGRESO
 FROM MERCADERIA M 
 INNER JOIN SUCURSAL_HAS_MERCADERIA S 
 ON M.ID_MERCADERIA = S.ID_MERCADERIA;
 
 
 
 