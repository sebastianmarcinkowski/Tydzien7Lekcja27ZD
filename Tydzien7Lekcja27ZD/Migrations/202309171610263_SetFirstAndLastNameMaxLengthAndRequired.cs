namespace Tydzien7Lekcja27ZD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetFirstAndLastNameMaxLengthAndRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "FirstName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Students", "LastName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "LastName", c => c.String());
            AlterColumn("dbo.Students", "FirstName", c => c.String());
        }
    }
}
