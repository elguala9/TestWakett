CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE OR REPLACE FUNCTION uuid_generate_v7()
RETURNS uuid
AS $$
SELECT encode(
    set_bit(
      set_bit(
        overlay(uuid_send(gen_random_uuid())
                placing substring(int8send(floor(extract(epoch FROM clock_timestamp()) * 1000)::bigint) FROM 3)
                FROM 1 FOR 6
        ),
        52, 1
      ),
      53, 1
    ),
    'hex')::uuid;
$$
LANGUAGE SQL
VOLATILE;

-- Tabella per i tassi recuperati da CoinMarketCap
CREATE TABLE rates (
    Id UUID PRIMARY KEY DEFAULT uuid_generate_v7(),
    Symbol VARCHAR(20) NOT NULL,
    Value DECIMAL(18, 8) NOT NULL,
    Timestamp TIMESTAMPTZ NOT NULL
);

-- Indicizzazione utile per interrogazioni sulle ultime 24h per simbolo
CREATE INDEX IX_Rates_Symbol_Timestamp ON Rates(Symbol, Timestamp);

-- Tabella opzionale per tracciare le notifiche inviate al PositionsService
CREATE TABLE rate_change_notifications (
    Id UUID PRIMARY KEY DEFAULT uuid_generate_v7(),
    Symbol VARCHAR(20) NOT NULL,
    OldRate DECIMAL(18, 8) NOT NULL,
    NewRate DECIMAL(18, 8) NOT NULL,
    ChangePercent FLOAT NOT NULL,
    NotifiedAt TIMESTAMPTZ NOT NULL
);
