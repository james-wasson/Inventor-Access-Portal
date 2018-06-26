-- DROP SSMA timestamp
IF COL_LENGTH('[$(TO_DATABASE)].dbo.[$(TABLE_NAME)]', 'SSMA_TimeStamp') IS NOT NULL
    ALTER TABLE [$(TO_DATABASE)].dbo.[$(TABLE_NAME)] DROP COLUMN [SSMA_TimeStamp];

IF COL_LENGTH('[$(FROM_DATABASE)].dbo.[$(TABLE_NAME)]', 'SSMA_TimeStamp') IS NOT NULL
    ALTER TABLE [$(FROM_DATABASE)].dbo.[$(TABLE_NAME)] DROP COLUMN [SSMA_TimeStamp];

-- ADD Data source
IF COL_LENGTH('[$(TO_DATABASE)].dbo.[$(TABLE_NAME)]', 'DataSource') IS NULL
    ALTER TABLE [$(TO_DATABASE)].dbo.[$(TABLE_NAME)] ADD DataSource VARCHAR(50) NOT NULL DEFAULT('$(ORIGIN_DATABASE)');

IF COL_LENGTH('[$(FROM_DATABASE)].dbo.[$(TABLE_NAME)]', 'DataSource') IS NULL
    ALTER TABLE [$(FROM_DATABASE)].dbo.[$(TABLE_NAME)] ADD DataSource VARCHAR(50) NOT NULL DEFAULT('$(ORIGIN_DATABASE)');
GO

SELECT * INTO [$(FROM_DATABASE)].dbo.#tempTable FROM [$(FROM_DATABASE)].dbo.[$(TABLE_NAME)];
GO

while (Select Count(*) From [$(FROM_DATABASE)].dbo.#tempTable) > 0
BEGIN
    BEGIN TRY  -- try to import the data, will throw error if key already exists
        INSERT INTO [$(TO_DATABASE)].dbo.[$(TABLE_NAME)] SELECT TOP(1) * FROM [$(FROM_DATABASE)].dbo.#tempTable;
    END TRY 
    BEGIN CATCH -- else try to make the table
        BEGIN TRY
            SELECT TOP(1) * INTO [$(TO_DATABASE)].dbo.[$(TABLE_NAME)] FROM [$(FROM_DATABASE)].dbo.#tempTable;
        END TRY BEGIN CATCH END CATCH;
    END CATCH; -- if error just continue, ignoring the conflict
    Delete Top(1) [$(FROM_DATABASE)].dbo.#tempTable;
END
GO

DROP TABLE [$(FROM_DATABASE)].dbo.#tempTable;
GO
