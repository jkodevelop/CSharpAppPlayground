using CSharpAppPlayground.DBClasses.PostgresExamples;
using Mysqlx.Crud;
using RepoDb.Enumerations;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CSharpAppPlayground
{
    public partial class FormDBPostgres : Form
    {
        _connPostgres connPostgres;

        PostgresBaseExamples baseExamples = new PostgresBaseExamples();
        PostgresEnumExample enumExample = new PostgresEnumExample();

        public FormDBPostgres()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
            connPostgres = new _connPostgres();
        }

        private void btnPostgresStatus_Click(object sender, EventArgs e)
        {
            string serverVersion = connPostgres.getServerVersion();
            Debug.Print($"{serverVersion}");
        }

        private void btnPostgresBasicExample_Click(object sender, EventArgs e)
        {
            // running basic example: insert, select, update, delete
            baseExamples.RunBasicExample();
        }

        private void btnEnumExample_Click(object sender, EventArgs e)
        {
            enumExample.DemoInsertAndSelect();
        }

        private void btnBenchBulkUpdate_Click(object sender, EventArgs e)
        {
            //TODO new benchmark for bulk update

            // compareA: insert on conflict update
            //INSERT INTO users(id, name, email, age)
            //VALUES(1, 'John', 'john@example.com', 30)
            //ON CONFLICT(id) DO UPDATE
            //SET name = excluded.name, email = excluded.email;

            // compareB: temp table + update join
            //UPDATE table_name AS t
            //SET
            //    duration = v.duration,
            //    height = v.height,
            //    width = v.width,
            //    status = v.status,
            //    parsedby = v.parsedby
            //FROM(VALUES(1,...), (2,....)) AS v(id, duration, height, width, status, parsedby)
            //    WHERE t.id = v.id; ";

            // compareC: DML queries source: https://tapoueh.org/blog/2013/03/batch-update/)

            //CREATE TEMP TABLE source(LIKE target INCLUDING ALL) ON COMMIT DROP;
            //
            //COPY source FROM STDIN;
            //
            //UPDATE target t
            //   SET counter = t.counter + s.counter,
            //FROM source s
            //WHERE t.id = s.id

            //WITH upd AS(
            //    UPDATE target t
            //       SET counter = t.counter + s.counter,
            //      FROM source s
            //     WHERE t.id = s.id
            // RETURNING s.id
            //)
            //INSERT INTO target(id, counter)
            //     SELECT id, sum(counter)
            //       FROM source s LEFT JOIN upd t USING(id)
            //      WHERE t.id IS NULL
            //   GROUP BY s.id
            //  RETURNING t.id
        }

        private void TODO_Benchmark_SELECT_UNIQUE()
        {
            // Compare different ways to select multiple items by (name, size) tuple
            // using VIEW for N > 100 ~ 5000 items
            // using WHERE for N < 100 items
            //
            // Find just one entry of the a list of objects fitting the query parameters.

            // OPTION 1: using where
            // 
            //SELECT DISTINCT ON(filename, filesizebyte)
            //    id,
            //    filename,
            //    filesizebyte
            //FROM file
            //WHERE(name, filesizebyte) IN(
            //    ('nameA', 100),
            //    ('nameB', 989),
            //    ('nameC', 500)
            //)
            //ORDER BY filename, filesizebyte, id ASC;

            // OPTION 2: using JOIN with VALUES (temporary view) + postgres in-memory map search
            //
            //SELECT DISTINCT ON(search_data.search_name, search_data.search_size)
            //    f.id,
            //    f.filename,
            //    f.filesizebyte
            //FROM(
            //    VALUES
            //        ('nameA', 100),
            //        ('nameB', 989),
            //        ('nameC', 500)-- Add your N items here
            //) AS search_data(search_name, search_size)
            //JOIN file as f
            //    ON f.name = search_data.search_name
            //    AND f.filesizebyte = search_data.search_size
            //ORDER BY search_data.search_name, search_data.search_size, f.id ASC;

            // alt variantions 
            // 
            //SELECT DISTINCT ON(search_data.search_name, search_data.search_size)
            //    f.*
            //FROM(
            //    VALUES
            //        ('nameA', 100),
            //        ('nameB', 989),
            //        ('nameC', 500)-- Add your N items here
            //    ) AS search_data(search_name, search_size)
            //JOIN file AS f
            //    ON f.name = search_data.search_name
            //    AND f.filesizebyte = search_data.search_size
            //WHERE f.status = 0 
            //ORDER BY search_data.search_name, search_data.search_size, f.id ASC;

        }
    }
}
