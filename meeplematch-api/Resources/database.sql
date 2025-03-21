-- CREATE DATABASE MeepleMatch;

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE role
(
    id_role SERIAL PRIMARY KEY,
    name    VARCHAR(50) NOT NULL UNIQUE
);

INSERT INTO role (name)
VALUES ('user'),
       ('admin');

CREATE TABLE "user"
(
    id_user    SERIAL PRIMARY KEY,
    uuid       UUID      DEFAULT gen_random_uuid() NOT NULL UNIQUE,
    username   VARCHAR(50)                         NOT NULL UNIQUE,
    email      VARCHAR(100)                        NOT NULL UNIQUE,
    password   VARCHAR(255)                        NOT NULL,
    role_id    INT                                 NOT NULL REFERENCES role (id_role),
    is_banned  BOOLEAN   DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE event
(
    id_event         SERIAL PRIMARY KEY,
    uuid             UUID      DEFAULT gen_random_uuid() UNIQUE,
    name             VARCHAR(100) NOT NULL,
    type             VARCHAR(50)  NOT NULL,
    game             VARCHAR(100) NOT NULL,
    location         VARCHAR(255) NOT NULL,
    event_date       TIMESTAMP    NOT NULL,
    capacity         INT,
    min_participants INT       DEFAULT 2,
    created_by       INT          NOT NULL REFERENCES "user" (id_user),
    created_at       TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at       TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE event_participant
(
    id_event_participant SERIAL PRIMARY KEY,
    id_event             INT NOT NULL REFERENCES event (id_event),
    id_user              INT NOT NULL REFERENCES "user" (id_user),
    joined_at            TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE (id_event, id_user)
);

CREATE TABLE event_comment
(
    id_event_comment SERIAL PRIMARY KEY,
    event_id         INT  NOT NULL REFERENCES event (id_event),
    user_id          INT  NOT NULL REFERENCES "user" (id_user),
    comment          TEXT NOT NULL,
    created_at       TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at       TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE event_rating
(
    id_event_rating SERIAL PRIMARY KEY,
    event_id        INT NOT NULL REFERENCES event (id_event) ON DELETE CASCADE,
    user_id         INT NOT NULL REFERENCES "user" (id_user) ON DELETE CASCADE,
    rating          INT NOT NULL CHECK (rating >= 1 AND rating <= 5),
    created_at      TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE (event_id, user_id)
);

CREATE TABLE report_status
(
    id_report_status SERIAL PRIMARY KEY,
    title            VARCHAR(20) UNIQUE
);

CREATE TABLE report
(
    id_report   SERIAL PRIMARY KEY,
    reported_by INT  NOT NULL REFERENCES "user" (id_user),
    event_id    INT REFERENCES event (id_event),
    reason      TEXT NOT NULL,
    status      INT  NOT NULL REFERENCES report_status (id_report_status),
    created_at  TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE notification
(
    id_notification SERIAL PRIMARY KEY,
    user_id         INT  NOT NULL REFERENCES "user" (id_user),
    message         TEXT NOT NULL,
    is_read         BOOLEAN   DEFAULT FALSE,
    created_at      TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE global_message
(
    id_global_message SERIAL PRIMARY KEY,
    message           TEXT NOT NULL,
    created_by        INT  NOT NULL REFERENCES "user" (id_user),
    created_at        TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE telemetry
(
    id_telemetry SERIAL PRIMARY KEY,
    user_id      INT REFERENCES "user" (id_user) ON DELETE CASCADE,
    event_type   VARCHAR(100) NOT NULL,
    event_data   JSONB,
    created_at   TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE subscription_plan
(
    id_subscription_plan SERIAL PRIMARY KEY,
    name                 VARCHAR(100)   NOT NULL,
    price                NUMERIC(10, 2) NOT NULL,
    duration             INT            NOT NULL, -- duration in days
    created_at           TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE user_subscription
(
    id_user_subscription SERIAL PRIMARY KEY,
    user_id              INT       NOT NULL REFERENCES "user" (id_user),
    subscription_plan_id INT       NOT NULL REFERENCES subscription_plan (id_subscription_plan),
    start_date           TIMESTAMP NOT NULL,
    end_date             TIMESTAMP NOT NULL,
    created_at           TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Indexes for optimization
CREATE INDEX idx_user_email ON "user" (email);
CREATE INDEX idx_user_username ON "user" (username);
CREATE INDEX idx_user_banned ON "user" (is_banned);
CREATE INDEX idx_event_date ON event (event_date);
CREATE INDEX idx_event_type ON event (type);
CREATE INDEX idx_event_game ON event (game);
CREATE INDEX idx_event_participant_event ON event_participant (id_event);
CREATE INDEX idx_event_participant_user ON event_participant (id_user);
CREATE INDEX idx_event_comment_event ON event_comment (event_id);
CREATE INDEX idx_event_rating_event ON event_rating (event_id);
CREATE INDEX idx_report_event ON report (event_id);
CREATE INDEX idx_report_reason ON report (reason);
CREATE INDEX idx_notification_user ON notification (user_id);
CREATE INDEX idx_telemetry_user ON telemetry (user_id);
CREATE INDEX idx_user_subscription_user ON user_subscription (user_id);


/*

-- Table for storing OAuth providers
CREATE TABLE oauth_provider
(
    id   SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE
);

-- Prepopulate OAuth providers
INSERT INTO oauth_provider (name)
VALUES ('google'),
       ('facebook'),
       ('github');

-- Table for storing OAuth logins
CREATE TABLE oauth_login
(
    id                SERIAL PRIMARY KEY,
    user_id           INT          NOT NULL REFERENCES "user" (id) ON DELETE CASCADE,
    oauth_provider_id INT          NOT NULL REFERENCES oauth_provider (id) ON DELETE CASCADE,
    oauth_id          VARCHAR(255) NOT NULL, -- Unique ID from OAuth provider
    created_at        TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE (oauth_provider_id, oauth_id)
);

-- Table for storing 2FA codes
CREATE TABLE two_factor_auth
(
    id         SERIAL PRIMARY KEY,
    user_id    INT         NOT NULL REFERENCES "user" (id) ON DELETE CASCADE,
    code       VARCHAR(10) NOT NULL,
    expires_at TIMESTAMP   NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

*/