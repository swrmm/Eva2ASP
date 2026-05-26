CREATE DATABASE IF NOT EXISTS farmacia_db
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE farmacia_db;

CREATE TABLE IF NOT EXISTS medicamentos (
    id INT NOT NULL AUTO_INCREMENT,
    nombre VARCHAR(120) NOT NULL,
    descripcion VARCHAR(500) NOT NULL,
    precio DECIMAL(10,2) NOT NULL,
    stock INT NOT NULL,
    fecha_vencimiento DATE NOT NULL,
    laboratorio VARCHAR(120) NOT NULL,
    categoria VARCHAR(80) NOT NULL,
    creado_en TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO medicamentos
    (nombre, descripcion, precio, stock, fecha_vencimiento, laboratorio, categoria)
VALUES
    ('Paracetamol 500 mg', 'Analgesico y antipiretico para dolor leve o fiebre.', 1990, 80, DATE_ADD(CURDATE(), INTERVAL 2 YEAR), 'Chilefarma', 'Analgesicos'),
    ('Ibuprofeno 400 mg', 'Antiinflamatorio no esteroidal de uso comun.', 3490, 45, DATE_ADD(CURDATE(), INTERVAL 18 MONTH), 'Andes Lab', 'Antiinflamatorios'),
    ('Loratadina 10 mg', 'Antihistaminico para sintomas de alergia.', 2790, 35, DATE_ADD(CURDATE(), INTERVAL 20 MONTH), 'Salud Sur', 'Antialergicos');
