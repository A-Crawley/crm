CREATE TABLE IF NOT EXISTS "user"(
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    email VARCHAR(100) NOT NULL,
    phone_number VARCHAR(50),
    password_hash TEXT NOT NULL,
    created_at TIMESTAMPTZ NOT NULL,
    created_by INT REFERENCES "user"(id),
    updated_at TIMESTAMPTZ,
    updated_by INT REFERENCES "user"(id),
    archived_at TIMESTAMPTZ,
    archived_by INT REFERENCES "user"(id)
);

CREATE TABLE IF NOT EXISTS "login_session"(
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    user_id INT NOT NULL REFERENCES "user"(id),
    refresh_token TEXT NOT NULL,
    expiry TIMESTAMPTZ NOT NULL,
    created_at TIMESTAMPTZ NOT NULL,
    created_by INT REFERENCES "user"(id),
    updated_at TIMESTAMPTZ,
    updated_by INT REFERENCES "user"(id),
    archived_at TIMESTAMPTZ,
    archived_by INT REFERENCES "user"(id)
);

CREATE TABLE IF NOT EXISTS "audit_log"(
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    "table" TEXT NOT NULL,
    "reference" INT NOT NULL,
    old_value TEXT,
    new_value TEXT,
    who INT REFERENCES "user"(id),
    "when" TIMESTAMPTZ NOT NULL
);

CREATE INDEX audit_log_table_ref ON audit_log ("table", "reference");

CREATE TABLE IF NOT EXISTS "contact"(
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    first_name VARCHAR(255) NOT NULL,
    middle_name VARCHAR(255) NOT NULL,
    last_name VARCHAR(255) NOT NULL,
    email VARCHAR(255),
    mobile_number VARCHAR(255),
    landline VARCHAR(255),
    created_at TIMESTAMPTZ NOT NULL,
    created_by INT REFERENCES "user"(id),
    updated_at TIMESTAMPTZ,
    updated_by INT REFERENCES "user"(id),
    archived_at TIMESTAMPTZ,
    archived_by INT REFERENCES "user"(id)
);