#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdint.h>
#include <unistd.h>
#include <sqlite3.h>

#include "model.h"


void db_insert_orders(
        const arburg_result_t *p_result,
        int order_count)
{
    sqlite3 *p_db = NULL;
    //int ret = sqlite3_open_v2("file:dataset.sql", &p_db, SQLITE_OPEN_READWRITE | SQLITE_OPEN_URI, NULL);
    int ret = sqlite3_open("dataset.db", &p_db);


    if(ret != SQLITE_OK)
    {
        return;
    }

    sqlite3_exec(p_db, "begin transaction ;", NULL, NULL, NULL);

    char *sql = "insert into orders(m, idx, value) values(?, ?, ?)";
    sqlite3_stmt *p_pp_sql = NULL;


    sqlite3_prepare(p_db, sql, -1, &p_pp_sql, NULL);

    for(int i = 0; i < order_count - 1; ++i)
    {
        const arburg_result_t *p = (p_result + i);


        for(int32_t i = 0; i < p->m_count; ++i)
        {
            sqlite3_bind_int(p_pp_sql, 1, p->m_count);
            sqlite3_bind_int(p_pp_sql, 2, i);
            sqlite3_bind_double(p_pp_sql, 3, *(p->a + i));

            ret = sqlite3_step(p_pp_sql);
            if(ret != SQLITE_DONE)
                break;

            sqlite3_reset(p_pp_sql);
        }
    }

    sqlite3_exec(p_db, "commit", NULL, NULL, NULL);

    sqlite3_finalize(p_pp_sql);

    sqlite3_close(p_db);
}
