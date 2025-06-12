CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

create or replace function uuid_generate_v7()
returns uuid
as $$
select encode(
    set_bit(
      set_bit(
        overlay(uuid_send(gen_random_uuid())
                placing substring(int8send(floor(extract(epoch from clock_timestamp()) * 1000)::bigint) from 3)
                from 1 for 6
        ),
        52, 1
      ),
      53, 1
    ),
    'hex')::uuid;
$$
language SQL
volatile;

-- Tabella principale delle posizioni finanziarie
CREATE TABLE positions (
    Id UUID PRIMARY KEY DEFAULT uuid_generate_v7(),
    InstrumentId VARCHAR(20) NOT NULL,
    Quantity DECIMAL(18, 8) NOT NULL,
    InitialRate DECIMAL(18, 8) NOT NULL,
    Side INT NOT NULL, -- +1 per BUY, -1 per SELL
    OpenedAt TIMESTAMPTZ NOT NULL,
    IsClosed BOOLEAN NOT NULL DEFAULT FALSE
);

-- Tabella opzionale per tracciare le rivalutazioni successive
CREATE TABLE position_valuations (
    Id UUID PRIMARY KEY DEFAULT uuid_generate_v7(),
    PositionId UUID NOT NULL,
    CurrentRate DECIMAL(18, 8) NOT NULL,
    ProfitLoss DECIMAL(18, 8) NOT NULL,
    CalculatedAt TIMESTAMPTZ NOT NULL,
    FOREIGN KEY (PositionId) REFERENCES Positions(Id) ON DELETE CASCADE
);
