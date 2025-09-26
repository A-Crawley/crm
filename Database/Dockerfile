FROM postgres:15-alpine

COPY ./Migrations/*.sql /docker-entrypoint-initdb.d/

ENV POSTGRES_PASSWORD=password
ENV POSTGRES_USERNAME=postgres

HEALTHCHECK --interval=5s --timeout=5s --retries=5 CMD pg_isready -U postgres || exit 1