CREATE TABLE cashiers
(
    id        uuid,
    full_name TEXT  NOT NULL,
    bilans    FLOAT NOT NULL,
    PRIMARY KEY (id)
);

CREATE TYPE order_status AS ENUM ('Preparing', 'Serving', 'Finished', 'Canceled');

CREATE TABLE orders
(
    id            uuid,
    cashier_id    uuid,
    status        order_status NOT NULL,
    created_at    TIMESTAMP    NOT NULL,
    price         FLOAT        NOT NULL,
    ticket_number INT          NOT NULL,

    PRIMARY KEY (id),
    FOREIGN KEY (cashier_id) REFERENCES cashiers (id)
);

CREATE TYPE product_status AS ENUM ('Available', 'Withdrawn', 'Paused');

CREATE TABLE products
(
    id     uuid,
    name   TEXT           NOT NULL,
    price  FLOAT          NOT NULL,
    status product_status NOT NULL,

    PRIMARY KEY (id)
);

CREATE TABLE order_items
(
    order_id   uuid,
    product_id uuid,
    quantity   INT NOT NULL,
    price      FLOAT,

    PRIMARY KEY (order_id, product_id),
    FOREIGN KEY (order_id) REFERENCES orders (id),
    FOREIGN KEY (product_id) REFERENCES products (id)
);

CREATE TYPE discount_type AS ENUM ('Items set', 'Price drop', 'Percentage price drop');

CREATE TABLE discounts
(
    id                 uuid,
    type               discount_type NOT NULL,
    set_price          FLOAT,
    price_drop_amount  FLOAT,
    price_drop_percent FLOAT,
    PRIMARY KEY (id)
);

CREATE TABLE order_discount
(
    order_id    uuid,
    discount_id uuid,
    PRIMARY KEY (order_id, discount_id),
    FOREIGN KEY (order_id) REFERENCES orders (id),
    FOREIGN KEY (discount_id) REFERENCES discounts (id)
);

create TABLE discounts_set_items(
    discount_id uuid,
    product_id uuid,
    quantity INT NOT NULL,
    PRIMARY KEY (discount_id, product_id),
    FOREIGN KEY (discount_id) REFERENCES discounts(id),
    FOREIGN KEY (product_id) REFERENCES products(id)
);

CREATE TYPE user_type as ENUM ('Admin', 'Manager');

CREATE TABLE users(
    id uuid,
    username TEXT NOT NULL,
    password_hash TEXT NOT NULL,
    user_type user_type NOT NULL,
    PRIMARY KEY (id)
)