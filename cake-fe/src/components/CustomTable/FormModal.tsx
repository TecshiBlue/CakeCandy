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
import { ReactNode, useEffect, useState } from "react";

interface FormField<T> {
  key: keyof T;
  label: string;
  placeholder?: string;
  type?: string;
  options?: { value: string; label: string }[];
}

export interface FormModalProps<T> {
  trigger?: ReactNode;
  title: string;
  fields: FormField<T>[];
  initialData?: T;
  readonly?: boolean;
  open: boolean;
  onOpenChange: (isOpen: boolean) => void;
  onSave?: (values: T) => void;
}

export default function FormModal<T>({
  trigger,
  title,
  fields,
  initialData,
  readonly = false,
  open,
  onOpenChange,
  onSave,
}: FormModalProps<T>) {
  const [values, setValues] = useState<Partial<T>>({});

  useEffect(() => {
    setValues(initialData ?? {});
  }, [initialData]);

  const handleChange = (key: keyof T, value: string) => {
    setValues((v) => ({ ...v, [key]: value }));
  };

  const handleSave = () => {
    if (onSave) onSave(values as T);
    onOpenChange(false);
     window.location.reload(); 
  };

  const handleCancel = () => {
    onOpenChange(false);
    window.location.reload();
  };

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      {trigger && <DialogTrigger asChild>{trigger}</DialogTrigger>}
      <DialogContent className="sm:max-w-lg">
        <DialogHeader>
          <DialogTitle>{title}</DialogTitle>
        </DialogHeader>

        <div className="grid gap-4 py-4">
          {fields.map(({ key, label, placeholder, type, options }) => {
            const val = values[key] ?? "";
            const sharedProps = {
              value: String(val),
              onChange: (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) =>
                handleChange(key, e.target.value),
              disabled: readonly,
              className: "w-full",
            };

            return (
              <div key={String(key)} className="space-y-2">
                <label className="block text-sm font-medium">{label}</label>

                {type === "select" && options ? (
                  <select {...sharedProps} className="rounded-md border px-3 py-2">
                    <option value="">Selecciona una opción</option>
                    {options.map((opt) => (
                      <option key={opt.value} value={opt.value}>
                        {opt.label}
                      </option>
                    ))}
                  </select>
                ) : (
                  <Input
                    {...sharedProps}
                    placeholder={placeholder}
                    type={type}
                  />
                )}
              </div>
            );
          })}
        </div>

        <DialogFooter>
          <Button variant="outline" onClick={handleCancel}>
            {readonly ? "Cerrar" : "Cancelar"}
          </Button>
          {!readonly && <Button onClick={handleSave}>Guardar</Button>}
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}
