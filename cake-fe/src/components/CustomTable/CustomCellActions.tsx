import React, { useState, useCallback } from "react";
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
  fields: Array<{ 
    key: keyof T; 
    label: string; 
    placeholder?: string; 
    type?: string;
    options?: Array<{ value: string; label: string }>;
  }>;
}

export function CustomCellActions<T extends Record<string, any>>({
  row,
  onEdit,
  onView,
  onDelete,
  fields,
}: CustomCellActionsProps<T>) {
  const [editOpen, setEditOpen] = useState(false);
  const [viewOpen, setViewOpen] = useState(false);

  const handleEdit = useCallback((item: T) => {
    onEdit(item);
    setEditOpen(false);
  }, [onEdit]);

  const handleView = useCallback(() => {
    onView(row);
  }, [onView, row]);

  const handleDelete = useCallback(() => {
    onDelete(row);
  }, [onDelete, row]);

  const openEditModal = useCallback(() => {
    setEditOpen(true);
  }, []);

  const openViewModal = useCallback(() => {
    setViewOpen(true);
  }, []);

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
          <DropdownMenuItem onClick={openEditModal}>
            <Pen className="mr-2 h-4 w-4" />
            <span>Editar</span>
          </DropdownMenuItem>
          <DropdownMenuItem onClick={openViewModal}>
            <EyeIcon className="mr-2 h-4 w-4" />
            <span>Ver</span>
          </DropdownMenuItem>
          <DropdownMenuItem onClick={handleDelete}>
            <TrashIcon className="mr-2 h-4 w-4" />
            <span>Eliminar</span>
          </DropdownMenuItem>
        </DropdownMenuContent>
      </DropdownMenu>

      {/* Only render modals when they're open to prevent unnecessary renders */}
      {editOpen && (
        <FormModal
          open={editOpen}
          onOpenChange={setEditOpen}
          title="Editar item"
          initialData={row}
          fields={fields}
          onSave={handleEdit}
        />
      )}

      {viewOpen && (
        <FormModal
          open={viewOpen}
          onOpenChange={setViewOpen}
          title="Ver item"
          initialData={row}
          fields={fields}
          readonly
          onSave={handleView}
        />
      )}
    </TableCell>
  );
}

export default CustomCellActions;
