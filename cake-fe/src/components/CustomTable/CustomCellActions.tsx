
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { Button } from "@/components/ui/button";
import { Pen, EyeIcon, TrashIcon } from "lucide-react";
import { TableCell } from "@/components/ui/table";
import FormModal from "./FormModal";

interface CustomCellActionsProps<T> {
  row: T;
  onEdit: (item: T) => void;
  onView: (item: T) => void;
  onDelete: (item: T) => void;
  fields: { key: keyof T; label: string; placeholder?: string; type?: string }[];
}

export function CustomCellActions<T>({
  row,
  onEdit,
  onView,
  onDelete,
  fields,
}: CustomCellActionsProps<T>) {
  return (
    <TableCell>
      <DropdownMenu>
        <DropdownMenuTrigger asChild>
          <Button variant="ghost" size="icon">
            <Pen className="h-4 w-4" />
            <span className="sr-only">Actions</span>
          </Button>
        </DropdownMenuTrigger>
        <DropdownMenuContent align="end">
          <DropdownMenuItem asChild>
            <FormModal
              trigger={
                <div
                  className="focus:bg-accent focus:text-accent-foreground relative flex cursor-default items-center gap-2 rounded-sm px-2 py-1.5 text-sm outline-hidden select-none"
                >
                  <Pen className="mr-2 h-4 w-4" />
                  <span>Editar</span>
                </div>
              }
              title="Editar item"
              initialData={row}
              fields={fields}
              onSave={onEdit}
            />
          </DropdownMenuItem>
           <DropdownMenuItem asChild>
            <FormModal
              trigger={
                <div
                  className="focus:bg-accent focus:text-accent-foreground relative flex cursor-default items-center gap-2 rounded-sm px-2 py-1.5 text-sm outline-hidden select-none"
                >
                  <EyeIcon className="mr-2 h-4 w-4" />
                  <span>Ver</span>
                </div>
              }
              title="Ver item"
              initialData={row}
              fields={fields}
              readonly
            />
          </DropdownMenuItem>
          <DropdownMenuItem onClick={() => onDelete(row)}>
            <TrashIcon className="mr-2 h-4 w-4" />
            Delete
          </DropdownMenuItem>
        </DropdownMenuContent>
      </DropdownMenu>
    </TableCell>
  );
}

export default CustomCellActions;
