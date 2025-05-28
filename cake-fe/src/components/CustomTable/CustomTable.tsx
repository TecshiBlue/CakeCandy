import {
  ColumnDef,
  useReactTable,
  getCoreRowModel,
  getSortedRowModel,
  getFilteredRowModel,
  flexRender,
} from "@tanstack/react-table";
import {
  Table,
  TableHeader,
  TableRow,
  TableHead,
  TableBody,
  TableCell,
} from "@/components/ui/table";
import { Card, CardContent } from "@/components/ui/card";
import CustomCellActions from "./CustomCellActions";
import { CustomTitleTable } from "./CustomTitleTable";

export interface TableProps<T> {
  data: T[];
  tableTitle: string;
  columns: ColumnDef<T>[];
  onRowClick?: (row: T) => void;
  initialSorting?: { columnId: string; desc: boolean };
  onAdd: (item: T) => void;
  onEdit: (item: T) => void;
  onView: (item: T) => void;
  onDelete: (item: T) => void;
  formFields: {
    key: keyof T;
    label: string;
    placeholder?: string;
    type?: string;
  }[];
}

const CustomTable = <T,>({
  data,
  columns,
  tableTitle,
  initialSorting,
  onAdd,
  onEdit,
  onView,
  onDelete,
  formFields,
}: TableProps<T>) => {
  const table = useReactTable({
    data,
    columns,
    initialState: {
      sorting: initialSorting
        ? [{ id: initialSorting.columnId, desc: initialSorting.desc }]
        : [],
    },
    getCoreRowModel: getCoreRowModel(),
    getSortedRowModel: getSortedRowModel(),
    getFilteredRowModel: getFilteredRowModel(),
  });

  return (
    <div className="px-4 lg:px-6">
      <Card>
        <CardContent className="pt-6">
          <div className="flex flex-col gap-6">
            <CustomTitleTable
              title={tableTitle}
              onAdd={onAdd}
              addFields={formFields}
            />
            <div className="overflow-x-auto rounded-lg border shadow-sm">
              <Table>
                {!table ? (
                  <TableHeader>
                    <TableRow>
                      <TableHead colSpan={columns.length + 1}>
                        No hay datos disponibles
                      </TableHead>
                    </TableRow>
                  </TableHeader>
                ) : (
                  <>
                    <TableHeader>
                      {table.getHeaderGroups().map((hg) => (
                        <TableRow key={hg.id}>
                          {hg.headers.map((header) => (
                            <TableHead
                              key={header.id}
                              onClick={header.column.getToggleSortingHandler()}
                            >
                              <div className="flex items-center justify-center">
                                {flexRender(
                                  header.column.columnDef.header,
                                  header.getContext()
                                )}
                                {header.column.getIsSorted() === "asc" && " ↑"}
                                {header.column.getIsSorted() === "desc" && " ↓"}
                              </div>
                            </TableHead>
                          ))}
                          <TableHead>Acciones</TableHead>
                        </TableRow>
                      ))}
                    </TableHeader>
                    <TableBody>
                      {table.getRowModel().rows.map((row) => (
                        <TableRow key={row.id}>
                          {row.getVisibleCells().map((cell) => (
                            <TableCell key={cell.id}>
                              {flexRender(
                                cell.column.columnDef.cell,
                                cell.getContext()
                              )}
                            </TableCell>
                          ))}
                          <CustomCellActions
                            row={row.original}
                            onEdit={onEdit}
                            onView={onView}
                            onDelete={onDelete}
                            fields={formFields}
                          />
                        </TableRow>
                      ))}
                    </TableBody>
                  </>
                )}
              </Table>
            </div>
          </div>
        </CardContent>
      </Card>
    </div>
  );
};

export default CustomTable;
