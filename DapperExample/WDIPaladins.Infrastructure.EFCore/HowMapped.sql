CREATE TABLE "Monasteries" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Monasteries" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL
);


CREATE TABLE "Paladins" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Paladins" PRIMARY KEY AUTOINCREMENT,
    "UniqueId" TEXT NOT NULL,
    "Name" TEXT NOT NULL,
    "Title" TEXT NOT NULL,
    "MonasteryId" INTEGER NOT NULL,
    CONSTRAINT "FK_Paladins_Monasteries_MonasteryId" FOREIGN KEY ("MonasteryId") REFERENCES "Monasteries" ("Id") ON DELETE CASCADE
);


CREATE TABLE "Items" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Items" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Attack" INTEGER NOT NULL,
    "Defense" INTEGER NOT NULL,
    "Speed" INTEGER NOT NULL,
    "Mana" INTEGER NOT NULL,
    "PaladinId" INTEGER NULL,
    CONSTRAINT "FK_Items_Paladins_PaladinId" FOREIGN KEY ("PaladinId") REFERENCES "Paladins" ("Id")
);


CREATE TABLE "Skills" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Skills" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "PaladinId" INTEGER NULL,
    CONSTRAINT "FK_Skills_Paladins_PaladinId" FOREIGN KEY ("PaladinId") REFERENCES "Paladins" ("Id")
);


CREATE INDEX "IX_Items_PaladinId" ON "Items" ("PaladinId");


CREATE INDEX "IX_Paladins_MonasteryId" ON "Paladins" ("MonasteryId");


CREATE INDEX "IX_Skills_PaladinId" ON "Skills" ("PaladinId");
