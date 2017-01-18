namespace TesteRepositorioFotos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testefotos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fotos",
                c => new
                    {
                        FotosId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.FotosId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Fotos");
        }
    }
}
