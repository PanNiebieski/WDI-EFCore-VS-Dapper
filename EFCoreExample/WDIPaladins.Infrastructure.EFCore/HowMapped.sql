CREATE TABLE "Items" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Items" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Attack" INTEGER NOT NULL,
    "Defense" INTEGER NOT NULL,
    "Speed" INTEGER NOT NULL,
    "Mana" INTEGER NOT NULL
);


CREATE TABLE "Monasteries" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Monasteries" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL
);


CREATE TABLE "Skills" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Skills" PRIMARY KEY AUTOINCREMENT,
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


CREATE TABLE "PaladinItems" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_PaladinItems" PRIMARY KEY AUTOINCREMENT,
    "PaladinId" INTEGER NOT NULL,
    "ItemId" INTEGER NOT NULL,
    CONSTRAINT "FK_PaladinItems_Paladins_PaladinId" FOREIGN KEY ("PaladinId") REFERENCES "Paladins" ("Id") ON DELETE CASCADE
);


CREATE TABLE "PaladinSkills" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_PaladinSkills" PRIMARY KEY AUTOINCREMENT,
    "PaladinId" INTEGER NOT NULL,
    "SkillId" INTEGER NOT NULL,
    CONSTRAINT "FK_PaladinSkills_Paladins_PaladinId" FOREIGN KEY ("PaladinId") REFERENCES "Paladins" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_PaladinSkills_Skills_SkillId" FOREIGN KEY ("SkillId") REFERENCES "Skills" ("Id") ON DELETE CASCADE
);


CREATE INDEX "IX_PaladinItems_PaladinId" ON "PaladinItems" ("PaladinId");


CREATE INDEX "IX_Paladins_MonasteryId" ON "Paladins" ("MonasteryId");


CREATE INDEX "IX_PaladinSkills_PaladinId" ON "PaladinSkills" ("PaladinId");


CREATE INDEX "IX_PaladinSkills_SkillId" ON "PaladinSkills" ("SkillId");


