namespace MVConlineshopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Article",
                c => new
                    {
                        ArticleID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        MarqueID = c.Int(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        oldeprice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        image = c.Binary(),
                    })
                .PrimaryKey(t => t.ArticleID)
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Marque", t => t.MarqueID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.MarqueID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Marque",
                c => new
                    {
                        MarqueID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MarqueID);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ClientID = c.Int(nullable: false, identity: true),
                        Prenom = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        login = c.String(),
                        MotDePasse = c.String(),
                    })
                .PrimaryKey(t => t.ClientID);
            
            CreateTable(
                "dbo.Commande",
                c => new
                    {
                        CommandeID = c.Int(nullable: false, identity: true),
                        etat = c.String(),
                        dateCommande = c.DateTime(nullable: false),
                        PanierID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommandeID)
                .ForeignKey("dbo.Client", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Panier", t => t.PanierID, cascadeDelete: true)
                .Index(t => t.PanierID)
                .Index(t => t.ClientID);
            
            CreateTable(
                "dbo.Panier",
                c => new
                    {
                        PanierID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.PanierID);
            
            CreateTable(
                "dbo.Articlequantite",
                c => new
                    {
                        ArticlequantiteID = c.Int(nullable: false, identity: true),
                        Article_ArticleID = c.Int(),
                        Panier_PanierID = c.Int(),
                    })
                .PrimaryKey(t => t.ArticlequantiteID)
                .ForeignKey("dbo.Article", t => t.Article_ArticleID)
                .ForeignKey("dbo.Panier", t => t.Panier_PanierID)
                .Index(t => t.Article_ArticleID)
                .Index(t => t.Panier_PanierID);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Credits = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Enrollment",
                c => new
                    {
                        EnrollmentID = c.Int(nullable: false, identity: true),
                        Band = c.Decimal(precision: 18, scale: 2),
                        DepartmentID = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EnrollmentID)
                .ForeignKey("dbo.Department", t => t.DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.DepartmentID)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        JoiningDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Email",
                c => new
                    {
                        EmailID = c.Int(nullable: false, identity: true),
                        FromEmail = c.String(nullable: false),
                        EMailBody = c.String(nullable: false),
                        EmailSubject = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EmailID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollment", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Enrollment", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Commande", "PanierID", "dbo.Panier");
            DropForeignKey("dbo.Articlequantite", "Panier_PanierID", "dbo.Panier");
            DropForeignKey("dbo.Articlequantite", "Article_ArticleID", "dbo.Article");
            DropForeignKey("dbo.Commande", "ClientID", "dbo.Client");
            DropForeignKey("dbo.Article", "MarqueID", "dbo.Marque");
            DropForeignKey("dbo.Article", "CategoryID", "dbo.Category");
            DropIndex("dbo.Enrollment", new[] { "EmployeeID" });
            DropIndex("dbo.Enrollment", new[] { "DepartmentID" });
            DropIndex("dbo.Articlequantite", new[] { "Panier_PanierID" });
            DropIndex("dbo.Articlequantite", new[] { "Article_ArticleID" });
            DropIndex("dbo.Commande", new[] { "ClientID" });
            DropIndex("dbo.Commande", new[] { "PanierID" });
            DropIndex("dbo.Article", new[] { "MarqueID" });
            DropIndex("dbo.Article", new[] { "CategoryID" });
            DropTable("dbo.Email");
            DropTable("dbo.Employee");
            DropTable("dbo.Enrollment");
            DropTable("dbo.Department");
            DropTable("dbo.Articlequantite");
            DropTable("dbo.Panier");
            DropTable("dbo.Commande");
            DropTable("dbo.Client");
            DropTable("dbo.Marque");
            DropTable("dbo.Category");
            DropTable("dbo.Article");
        }
    }
}
