import { useState } from "react";
import CustomTable from "@/components/CustomTable/CustomTable";
import useCallApi from "@/hooks/useCallApi";
import { createDynamicColumns } from "@/utils/createDynamicColumns";
import { callApi } from "@/utils/callApi";

type Category = {
  idCategoria: number;
  nombreCategoria: string;
};

const formFields = [
  {
    key: "nombreCategoria",
    label: "Nombre de Categoría",
    placeholder: "Ej: Bebidas",
  },
] satisfies { key: keyof Category; label: string; placeholder?: string }[];


const CategoriesPageContainer = () => {
  const [isSubmitting, setIsSubmitting] = useState(false);

  const {
    data: categories = [],
    loading,
    error,
    refetch,
  } = useCallApi<Category[]>({
    url: "/api/categoria",
    methodType: "GET",
  });

  const handleAdd = async (newItem: Omit<Category, "idCategoria">) => {
    try {
      setIsSubmitting(true);
      await callApi<Category, typeof newItem>({
        url: "/api/categoria",
        methodType: "POST",
        body: newItem,
      });
      await refetch();
    } catch (error) {
      console.error("Error al agregar categoría:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleEdit = async (updatedItem: Category) => {
    try {
      console.log("updateItem: ", updatedItem)
      setIsSubmitting(true);
      await callApi<Category, Category>({
        url: `/api/categoria/${updatedItem.idCategoria}`,
        methodType: "PUT",
        body: updatedItem,
      });
      await refetch();
    } catch (error) {
      console.error("Error al editar categoría:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleDelete = async (item: Category) => {
    if (!confirm(`¿Eliminar "${item.nombreCategoria}"?`)) return;

    try {
      setIsSubmitting(true);
      await callApi<void, void>({
        url: `/api/categoria/${item.idCategoria}`,
        methodType: "DELETE",
      });
      await refetch();
    } catch (error) {
      console.error("Error al eliminar categoría:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleView = (item: Category) => {
    console.log("Ver:", item);
  };

  if (error) return <div>Error al cargar datos</div>;
  if (loading || isSubmitting) return <div>Cargando...</div>;

  if (!categories || categories.length === 0) {
    return (
    <CustomTable
      tableTitle="Categorias"
        data={[]}
        columns={[]}
      formFields={formFields}
      onAdd={handleAdd}
      onEdit={handleEdit}
      onView={handleView}
      onDelete={handleDelete}
    />
  );
  }

  const columns = createDynamicColumns(categories);
  return (
    <CustomTable
      tableTitle="Categorías"
      data={categories}
      columns={columns}
      formFields={formFields}
      onAdd={handleAdd}
      onEdit={handleEdit}
      onView={handleView}
      onDelete={handleDelete}
    />
  );
};

export default CategoriesPageContainer;
