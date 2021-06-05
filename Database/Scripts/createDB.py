import psycopg2

sql1 = """
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
    CHECK(status = 'Available' OR status = 'Withdrawn' OR status = 'Paused')
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

CREATE TABLE users(
    id uuid,
    username TEXT NOT NULL,
    password_hash TEXT NOT NULL,
    user_type TEXT NOT NULL,
    PRIMARY KEY (id),
    CHECK(user_type = 'Admin' OR user_type = 'Manager')
)
"""

sql2 = """
INSERT INTO products VALUES
    ('00000000-0000-0000-0000-000000000001', 'MockBurger1', 1.99, 'Available'),
    ('00000000-0000-0000-0000-000000000002', 'MockBurger2', 4.99, 'Available'),
    ('00000000-0000-0000-0000-000000000003', 'MockBurger3', 2.99, 'Withdrawn'),
    ('00000000-0000-0000-0000-000000000004', 'MockBurger4', 3.99, 'Paused');
INSERT INTO discounts VALUES
    ('00000000-0000-0000-0000-000000000005', TRUE, 10.99, NULL, NULL),
    ('00000000-0000-0000-0000-000000000006', FALSE, 3.99, NULL, NULL),
    ('00000000-0000-0000-0000-000000000007', TRUE, NULL, 1.00, NULL),
    ('00000000-0000-0000-0000-000000000008', TRUE, NULL, NULL, 5);
INSERT INTO discounts_set_items VALUES
    ('00000000-0000-0000-0000-000000000005', '00000000-0000-0000-0000-000000000001', 1),
    ('00000000-0000-0000-0000-000000000005', '00000000-0000-0000-0000-000000000002', 2),
    ('00000000-0000-0000-0000-000000000006', '00000000-0000-0000-0000-000000000002', 2);
INSERT INTO cashiers VALUES
    ('00000000-0000-0000-0000-000000000007', 'Jamese Smith', 100),
    ('00000000-0000-0000-0000-000000000008', 'Suzanna Smith', -2.99);
INSERT INTO orders VALUES
    ('00000000-0000-0000-0000-000000000009', '00000000-0000-0000-0000-000000000007', 'Preparing', '2018-01-08 04:05:06', 10.99, 15),
    ('00000000-0000-0000-0000-00000000000A', '00000000-0000-0000-0000-000000000007', 'Serving', '2018-01-08 13:15:56', 8.98, 03),
    ('00000000-0000-0000-0000-00000000000B', '00000000-0000-0000-0000-000000000008', 'Finished', '2017-12-10 23:55:46', 0.99, 99),
    ('00000000-0000-0000-0000-00000000000C', '00000000-0000-0000-0000-000000000008', 'Canceled', '2017-12-11 11:32:46', 2.99, 01);
INSERT INTO order_items VALUES
    ('00000000-0000-0000-0000-00000000000A', '00000000-0000-0000-0000-000000000002', 3, 14.97),
    ('00000000-0000-0000-0000-000000000009', '00000000-0000-0000-0000-000000000002', 2, 8.98),
    ('00000000-0000-0000-0000-000000000009', '00000000-0000-0000-0000-000000000001', 1, 1.99),
    ('00000000-0000-0000-0000-00000000000B', '00000000-0000-0000-0000-000000000001', 1, 1.99),
    ('00000000-0000-0000-0000-00000000000C', '00000000-0000-0000-0000-000000000003', 1, 2.99);
INSERT INTO order_discount VALUES
    ('00000000-0000-0000-0000-000000000009', '00000000-0000-0000-0000-000000000005'),
    ('00000000-0000-0000-0000-00000000000A', '00000000-0000-0000-0000-000000000006'),
    ('00000000-0000-0000-0000-00000000000B', '00000000-0000-0000-0000-000000000006');
INSERT INTO users VALUES
    ('DA244771-6634-4A55-B978-21FA779D915E', 'Admin1', 'e64b78fc3bc91bcbc7dc232ba8ec59e0', 'Admin'),
    ('FA684D6E-D2C6-481D-8617-9EA5963230C4', 'Manager1', '9b50bb25814ff13d1a38c5ec2393bceb', 'Manager');
"""

#establishing the connection
conn = psycopg2.connect(
   database="postgres", user='postgres', password='mysecretpassword', host='127.0.0.1', port= '5432'
)
conn.autocommit = True

#Creating a cursor object using the cursor() method
cursor = conn.cursor()

#Preparing query to create a database
sql = '''SELECT * FROM orders; '''
sql0 = '''CREATE database mydatabase;'''

#------------------------------------------------------
#Creating a database
#cursor.execute(sql0)
#cursor.execute(sql1)
#cursor.execute(sql2)
cursor.execute(sql)
#------------------------------------------------------

rows = cursor.fetchall()
for row in rows:
    print(row)

print("Database created successfully........")

#Closing the connection
conn.close()