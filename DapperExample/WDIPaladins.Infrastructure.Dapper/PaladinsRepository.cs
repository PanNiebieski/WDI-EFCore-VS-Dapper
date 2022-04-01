using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDIPaladins.Application;
using WDIPaladins.Domain;
using Dapper;
using Microsoft.Data.Sqlite;

namespace WDIPaladins.Infrastructure.Dapper
{
    public class PaladinsRepository : IPaladinsRepository
    {

        private IDBContext _dbContext;

        public PaladinsRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Paladin> AddAsync(Paladin entity)
        {
            using var connection = new SqliteConnection
                (_dbContext.ConnectionString);

            var q = @"INSERT INTO Paladins 
                (UniqueId, Name, Title,
                MonasteryId)
                VALUES(@UniqueId, @Name, 
                @Title, @MonasteryId);

                SELECT seq From sqlite_sequence Where Name='Paladins'";

            var r1 = await connection
                .QueryAsync<int>(q, new
                {
                    @UniqueId = entity.UniqueId.ToString(),
                    @Name = entity.Name,
                    @Title = entity.Title,
                    @MonasteryId = entity.Monastery.Id,
                });

            entity.Id = r1.FirstOrDefault();

            foreach (var item in entity.Items)
            {
                var q1 = @"INSERT INTO PaladinsItems 
                (PaladinId, ItemId)
                VALUES(@PaladinId, @ItemId);

                SELECT seq From sqlite_sequence Where Name='PaladinsItems'";

                var r2 = await connection.QueryAsync<int>(q1,
                    new
                    {
                        @PaladinId = entity.Id,
                        @ItemId = item.Id,
                    });

            }

            foreach (var item in entity.Skills)
            {
                var q1 = @"INSERT INTO PaladinsSkills 
                (PaladinId, SkillId)
                VALUES(@PaladinId, @SkillId);

                SELECT seq From sqlite_sequence Where Name='PaladinsSkills'";

                var r2 = await connection.QueryAsync<int>(q1,
                    new
                    {
                        @PaladinId = entity.Id,
                        @SkillId = item.Id,
                    });

            }


            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            using var connection =
                new SqliteConnection(_dbContext.ConnectionString);

            //var q1 = "DELETE FROM PaladinsItems WHERE PaladinId=@Id;";
            //var q2 = "DELETE FROM PaladinsSkills WHERE PaladinId=@Id;";

            //await connection.QueryAsync<int>(q1,
            //new
            //{
            //    @Id = id
            //});

            //await connection.QueryAsync<int>(q2,
            //new
            //{
            //    @Id = id
            //});

            var q = "DELETE FROM Paladins WHERE Id=@Id;";


            var result = await connection.QueryAsync<int>(q,
            new
            {
                @Id = id
            });
        }

        public async Task<IReadOnlyList<Paladin>> GetAllAsync()
        {
            using var connection = new SqliteConnection
                           (_dbContext.ConnectionString);

            //var q = @$"SELECT
            //    p.Id,p.UniqueId,p.Name,p.Title,p.MonasteryId
            //    FROM Paladin as p";

            //var r = await connection.QueryAsync<Paladin>(q);

            var q2 = @$"SELECT
                p.Id, p.UniqueId, p.Name, p.Title,p.MonasteryId,
                m.Name as MonasteryName,
                i.Name as ItemName,
                i.Attack as ItemAttack,
                i.Defense as ItemDefense,
                i.Speed as  ItemSpeed,
                i.Mana as ItemMana,
                i.Id as ItemId,
                s.Name as SkillName,
                s.Id as SkillId
                FROM Paladins as p
                INNER JOIN PaladinsItems as pi ON p.Id = pi.PaladinId
                INNER JOIN PaladinsSkills as ps ON p.Id = ps.PaladinId
                INNER JOIN Skills as s ON s.Id = ps.SkillId
                INNER JOIN Items as i ON i.Id = pi.ItemId
                INNER JOIN Monasteries as m ON m.Id = p.MonasteryId";

            var r = await connection.QueryAsync<PaladinTemp>(q2);

            var paladinstemp = r.ToList();

            List<Paladin> list = new List<Paladin>();

            foreach (var temp in paladinstemp)
            {
                var find =
                    list.FirstOrDefault(k => k.Id == temp.Id);

                if (find is null)
                {
                    Paladin p = new Paladin();

                    p.Name = temp.Name;
                    p.Title = temp.Title;
                    p.Id = temp.Id;
                    p.UniqueId = temp.UniqueId;
                    p.Monastery = new Monastery()
                    {
                        Id = temp.MonasteryId,
                        Name = temp.MonasteryName
                    };

                    Skill s = new Skill()
                    {
                        Id = temp.SkillId,
                        Name = temp.SkillName,
                    };

                    p.Skills.Add(s);

                    Item item = new Item()
                    {
                        Name = temp.Name,
                        Id = temp.ItemId,
                        Attack = temp.ItemAttack,
                        Defense = temp.ItemDefense,
                        Mana = temp.ItemMana,
                        Speed = temp.ItemSpeed
                    };

                    p.Items.Add(item);


                    list.Add(p);
                }
                else
                {
                    var skillfind =
                        find.Skills.FirstOrDefault(k => k.Id == temp.SkillId);

                    if (skillfind is null)
                    {
                        Skill s = new Skill()
                        {
                            Id = temp.SkillId,
                            Name = temp.SkillName,
                        };

                        find.Skills.Add(s);
                    }



                    var itemfind =
                        find.Items.FirstOrDefault(k => k.Id == temp.ItemId);

                    if (itemfind is null)
                    {
                        Item item = new Item()
                        {
                            Name = temp.Name,
                            Id = temp.ItemId,
                            Attack = temp.ItemAttack,
                            Defense = temp.ItemDefense,
                            Mana = temp.ItemMana,
                            Speed = temp.ItemSpeed
                        };

                        find.Items.Add(item);
                    }


   

                }

            }

            return list.AsReadOnly();
        }

        public async Task<Paladin> GetByIdAsync(long id)
        {
            using var connection = new SqliteConnection
                           (_dbContext.ConnectionString);

            var q = @$"SELECT
                p.Id, p.UniqueId, p.Name, p.Title,p.MonasteryId,
                m.Name as MonasteryName
                FROM Paladins as p
                INNER JOIN Monasteries as m ON m.Id = p.MonasteryId
                WHERE p.id = @id";

            var r = await connection.
                QueryFirstOrDefaultAsync<PaladinTemp>
                (q, new { @id = id });

            Paladin p = new Paladin();

            p.Name = r.Name;
            p.Title = r.Title;
            p.Id = r.Id;
            p.UniqueId = r.UniqueId;
            p.Monastery = new Monastery()
            {
                Id = r.MonasteryId,
                Name = r.MonasteryName
            };


            var q2 = @$"SELECT
                pi.Id, pi.PaladinId, pi.ItemId,
                i.Name,
                i.Attack,
                i.Defense, 
                i.Speed, 
                i.Mana
                FROM PaladinsItems as pi
                INNER JOIN Items as i ON i.Id = pi.ItemId
                WHERE pi.PaladinId = @id";

            var r2 = await connection.
            QueryAsync
            (q2, new { @id = id });

            foreach (var dynamicItem in r2)
            {
                Item ite = new Item();

                ite.Name = dynamicItem.Name;
                ite.Attack = dynamicItem.Attack;
                ite.Defense = dynamicItem.Defense;
                ite.Mana = dynamicItem.Mana;
                ite.Speed = dynamicItem.Speed;
                ite.Id = dynamicItem.Id;
                p.Items.Add(ite);
            }

            var q3 = @$"SELECT
                ps.Id, ps.PaladinId, ps.SkillId,
                s.Name
                FROM PaladinsSkills as ps
                INNER JOIN Skills as s ON s.Id = ps.SkillId
                WHERE ps.PaladinId = @id";

            var r3 = await connection.
            QueryAsync
            (q3, new { @id = id });


            foreach (var dynamicSkill in r3)
            {
                Skill skill = new Skill();

                skill.Id = dynamicSkill.Id;
                skill.Name = dynamicSkill.Name;

                p.Skills.Add(skill);
            }

            return p;
        }

        public async Task<Paladin> GetByUniqueIdAsync(Guid id)
        {
            using var connection = new SqliteConnection
                           (_dbContext.ConnectionString);

            var q = @$"SELECT
                p.Id, p.UniqueId, p.Name, p.Title,p.MonasteryId
                m.Name as MonasteryName,
                FROM Paladins as p
                INNER JOIN Monasteries as m ON m.Id = p.MonasteryId
                WHERE p.UniqueId = @id";

            var r = await connection.
                QueryFirstOrDefaultAsync<PaladinTemp>
                (q, new { @id = id.ToString() });

            Paladin p = new Paladin();

            p.Name = r.Name;
            p.Title = r.Title;
            p.Id = r.Id;
            p.UniqueId = r.UniqueId;
            p.Monastery = new Monastery()
            {
                Id = r.MonasteryId,
                Name = r.MonasteryName
            };

            var q2 = @$"SELECT
                pi.Id, pi.PaladinId, pi.ItemId,
                i.Name,
                i.Attack,
                i.Defense, 
                i.Speed, 
                i.Mana
                FROM PaladinsItems as pi
                INNER JOIN Items as i ON i.Id = pi.ItemId
                WHERE pi.PaladinId = @id";

            var r2 = await connection.
            QueryAsync
            (q2, new { @id = p.Id });

            foreach (var dynamicItem in r2)
            {
                Item ite = new Item();

                ite.Id = dynamicItem.Id;
                ite.Name = dynamicItem.Name;
                ite.Attack = dynamicItem.Attack;
                ite.Defense = dynamicItem.Defense;
                ite.Mana = dynamicItem.Mana;
                ite.Speed = dynamicItem.Speed;

                p.Items.Add(ite);
            }

            var q3 = @$"SELECT
                ps.Id, ps.PaladinId, ps.SkillId,
                s.Name
                FROM PaladinsSkills as ps
                INNER JOIN Skills as s ON s.Id = ps.SkillId
                WHERE pi.PaladinId = @id";

            var r3 = await connection.QueryAsync
            (q3, new { @id = p.Id });


            foreach (var dynamicSkill in r3)
            {
                Skill skill = new Skill();

                skill.Name = dynamicSkill.Name;
                skill.Id = dynamicSkill.Id;
                p.Skills.Add(skill);
            }

            return p;
        }

        public async Task UpdateAsync(Paladin entity)
        {
            var old = await GetByIdAsync(entity.Id);


            using var connection = new SqliteConnection
                 (_dbContext.ConnectionString);

            var q = @"UPDATE Paladins
                SET Name = @Name, Title = @Title,
                WHERE UniqueId = @UniqueId;";

            var r1 = await connection
                .ExecuteAsync(q,
                new
                {
                    @Name = entity.Name,
                    @Title = entity.Title,
                    @UniqueId = entity.UniqueId
                });


            foreach (var skill in entity.Skills)
            {
                if (skill.ToInsert)
                {
                    var q2 = @"INSERT INTO PaladinsSkills 
                    (SkillId, PaladinId)
                    VALUES(@SkillId, @PaladinId);

                    SELECT seq From sqlite_sequence Where Name='PaladinsSkills'";

                    var r2 = await connection.QueryAsync<int>(q2,
                        new
                        {
                            @SkillId = skill.Id,
                            @PaladinId = entity.Id,
                        });
                }
            }

            foreach (var item in entity.Items)
            {
                if (item.ToInsert)
                {
                    var q2 = @"INSERT INTO PaladinsItems 
                    (ItemId, PaladinId)
                    VALUES(@ItemId, @PaladinId);

                    SELECT seq From sqlite_sequence Where Name='PaladinsItems'";

                    var r2 = await connection.QueryAsync<int>(q2,
                        new
                        {
                            @ItemId = item.Id,
                            @PaladinId = entity.Id,
                        });
                }
            }


            foreach (var item in DeleteFlags.GetDeletedItems())
            {
                var q3 = "DELETE FROM PaladinsItems WHERE PaladinId=@pId AND ItemId= @iId ;";

                if (item.paladinId == entity.Id)
                {
                    await connection.ExecuteAsync(q3,
                    new
                    {
                        @pId = item.paladinId,
                        @iId = item.itemId
                    });
                    DeleteFlags.Clearitem(entity.Id);
                }
            }


            foreach (var item in DeleteFlags.GetDeletedSkills())
            {
                var q3 = "DELETE FROM PaladinsSkills WHERE PaladinId=@pId AND SkillId= @sId ;";

                if (item.paladinId == entity.Id)
                {
                    await connection.ExecuteAsync(q3,
                    new
                    {
                        @pId = item.paladinId,
                        @sId = item.skillId
                    });
                    DeleteFlags.ClearSkill(entity.Id);
                }
            }
        }
    }
}
