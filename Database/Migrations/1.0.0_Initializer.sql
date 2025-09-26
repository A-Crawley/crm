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