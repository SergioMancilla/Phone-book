CREATE TABLE contact_type (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);

CREATE TABLE contact (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    phone_number VARCHAR(15) NOT NULL,
    text_comments TEXT,
    contact_type_id INT NOT NULL,
    FOREIGN KEY (contact_type_id) REFERENCES contact_type(id)
);

CREATE TABLE private_organization_contact (
    id SERIAL PRIMARY KEY,
    office_address VARCHAR(255),
    fax VARCHAR(15),
    contact_id INT UNIQUE NOT NULL,
    FOREIGN KEY (contact_id) REFERENCES contact(id)
);

CREATE TABLE public_organization_contact (
    id SERIAL PRIMARY KEY,
    webpage_url VARCHAR(255) NOT NULL,
    industrial_sector VARCHAR(100),
    contact_id INT UNIQUE NOT NULL,
    FOREIGN KEY (contact_id) REFERENCES contact(id)
);

CREATE TABLE person_contact (
    id SERIAL PRIMARY KEY,
    relationship VARCHAR(100),
    email VARCHAR(255),
    contact_id INT UNIQUE NOT NULL,
    FOREIGN KEY (contact_id) REFERENCES contact(id)
);


CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(100) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO contact_type (id, name) VALUES (1, 'Person');
INSERT INTO contact_type (id, name) VALUES (2, 'Public organization');
INSERT INTO contact_type (id, name) VALUES (3, 'Private Organization');