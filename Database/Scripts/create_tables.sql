CREATE TABLE cashiers
(
    id        uuid,
    full_name TEXT  NOT NULL,
    bilans    FLOAT8 NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE orders
(
    id            uuid,
    cashier_id    uuid,
    status        TEXT NOT NULL,
    created_at    TIMESTAMP    NOT NULL,
    price         FLOAT8        NOT NULL,
    ticket_number INT          NOT NULL,

    PRIMARY KEY (id),
    FOREIGN KEY (cashier_id) REFERENCES cashiers (id),
    CHECK(status = 'Preparing' OR status = 'Serving' OR status = 'Finished' OR status = 'Canceled')
);

CREATE TABLE products
(
    id     uuid,
    name   TEXT           NOT NULL,
    price  FLOAT8          NOT NULL,
    status TEXT NOT NULL,

    PRIMARY KEY (id),
    CHECK(status = 'Available' OR status = 'Withdrawn' OR )
);

CREATE TABLE order_items
(
    order_id   uuid,
    product_id uuid,
    quantity   INT NOT NULL,
    price      FLOAT8,

    PRIMARY KEY (order_id, product_id),
    FOREIGN KEY (order_id) REFERENCES orders (id),
    FOREIGN KEY (product_id) REFERENCES products (id)
);

CREATE TABLE discounts
(
    id                 uuid,
    is_available       BOOLEAN NOT NULL,
    set_price          FLOAT8,
    price_drop_amount  FLOAT8,
    price_drop_percent FLOAT8,
    PRIMARY KEY (id),
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

CREATE TABLE users(
    id uuid,
    username TEXT NOT NULL,
    password_hash TEXT NOT NULL,
    user_type TEXT NOT NULL,
    PRIMARY KEY (id),
    CHECK(user_type = 'Admin' OR user_type = 'Manager')
)