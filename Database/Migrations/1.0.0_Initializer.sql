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