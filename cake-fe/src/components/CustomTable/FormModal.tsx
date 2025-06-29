import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { ReactNode, useState, useEffect } from "react";

interface FormField<T> {
  key: keyof T;
  label: string;
  placeholder?: string;
  type?: "text" | "email" | "password" | "number" | "select";
  options?: Array<{ value: string; label: string }>;
}

export interface FormModalProps<T> {
  trigger?: ReactNode;
  onSave?: (values: T) => void;
  initialData?: T;
  readonly?: boolean;
  title: string;
  fields: FormField<T>[];
  open?: boolean;
  onOpenChange?: (open: boolean) => void;
}

function FormModal<T extends Record<string, any>>({
  trigger,
  onSave,
  initialData,
  readonly = false,
  title,
  fields,
  open: controlledOpen,
  onOpenChange,
}: FormModalProps<T>) {
  const [internalOpen, setInternalOpen] = useState(false);
  const [formValues, setFormValues] = useState<Partial<T>>({});

  const isControlled = controlledOpen !== undefined;
  const isOpen = isControlled ? controlledOpen : internalOpen;

  useEffect(() => {
    if (isOpen && initialData) {
      setFormValues(initialData);
    } else if (isOpen && !initialData) {
      setFormValues({});
    }
  }, [isOpen, initialData]);

  const handleOpenChange = (open: boolean) => {
    if (isControlled && onOpenChange) {
      onOpenChange(open);
    } else {
      setInternalOpen(open);
    }
  };

  const handleInputChange = (key: keyof T, value: string) => {
    setFormValues(prev => ({
      ...prev,
      [key]: value
    }));
  };

  const handleSave = () => {
    if (onSave) {
      onSave(formValues as T);
    }
    handleOpenChange(false);
  };

  const handleCancel = () => {
    handleOpenChange(false);
  };

  const renderField = (field: FormField<T>) => {
    const value = formValues[field.key];
    const stringValue = value !== undefined ? String(value) : "";

    if (field.type === "select" && field.options) {
      return (
        <div key={String(field.key)} className="space-y-2">
          <label className="block text-sm font-medium">
            {field.label}
          </label>
          <select
            value={stringValue}
            onChange={(e) => handleInputChange(field.key, e.target.value)}
            className="w-full rounded-md border border-input bg-background px-3 py-2"
            disabled={readonly}
          >
            <option value="">Selecciona una opción</option>
            {field.options.map((option) => (
              <option key={option.value} value={option.value}>
                {option.label}
              </option>
            ))}
          </select>
        </div>
      );
    }

    return (
      <div key={String(field.key)} className="space-y-2">
        <label className="block text-sm font-medium">
          {field.label}
        </label>
        <Input
          value={stringValue}
          placeholder={field.placeholder}
          type={field.type || "text"}
          onChange={(e) => handleInputChange(field.key, e.target.value)}
          className="w-full"
          disabled={readonly}
        />
      </div>
    );
  };

  const renderFooter = () => {
    if (readonly) {
      return (
        <Button variant="outline" onClick={handleCancel}>
          Cerrar
        </Button>
      );
    }

    return (
      <>
        <Button variant="outline" onClick={handleCancel}>
          Cancelar
        </Button>
        <Button onClick={handleSave}>
          Guardar
        </Button>
      </>
    );
  };

  return (
    <Dialog open={isOpen} onOpenChange={handleOpenChange}>
      {trigger && <DialogTrigger asChild>{trigger}</DialogTrigger>}
      <DialogContent className="sm:max-w-lg">
        <DialogHeader>
          <DialogTitle>{title}</DialogTitle>
        </DialogHeader>
        <div className="grid gap-4 py-4">
          {fields.map(renderField)}
        </div>
        <DialogFooter>
          {renderFooter()}
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}

export default FormModal;