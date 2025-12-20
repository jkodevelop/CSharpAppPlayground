using CSharpAppPlayground.DBClasses.PostgresExamples;
using Mysqlx.Crud;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

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
    }
}
