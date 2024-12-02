namespace Lab1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Saches",
                c => new
                    {
                        Ma = c.String(nullable: false, maxLength: 128),
                        Ten = c.String(),
                        DonGia = c.Int(nullable: false),
                        SoTrang = c.String(),
                    })
                .PrimaryKey(t => t.Ma);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Saches");
        }
    }
}
