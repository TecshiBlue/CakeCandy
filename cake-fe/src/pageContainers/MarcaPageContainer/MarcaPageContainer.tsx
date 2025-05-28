import { useState } from "react";
import CustomTable from "@/components/CustomTable/CustomTable";
import useCallApi from "@/hooks/useCallApi";
import { createDynamicColumns } from "@/utils/createDynamicColumns";
import { callApi } from "@/utils/callApi";

type Marca = {
  idMarca: number;
  nombreMarca: string;
  paisOrigenMarca: string;
};

const formFields = [
  {
    key: "nombreMarca",
    label: "Nombre de Marca",
    placeholder: "Ingresa tu Marca",
  },
  {
    key: "paisOrigenMarca",
    label: "Pais de origen",
    placeholder: "Ingresa el pais de origen",
  }
] satisfies { key: keyof Marca; label: string; placeholder?: string }[];


const MarcaPageContainer = () => {
  const [isSubmitting, setIsSubmitting] = useState(false);

  const {
    data: marcas = [],
    loading,
    error,
    refetch,
  } = useCallApi<Marca[]>({
    url: "/api/marca",
    methodType: "GET",
  });

  const handleAdd = async (newItem: Omit<Marca, "idMarca">) => {
    try {
      setIsSubmitting(true);
      await callApi<Marca, typeof newItem>({
        url: "/api/marca",
        methodType: "POST",
        body: newItem,
      });
      await refetch();
    } catch (error) {
      console.error("Error al agregar el Marca:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleEdit = async (updatedItem: Marca) => {
    try {
      setIsSubmitting(true);
      await callApi<Marca, Marca>({
        url: `/api/marca/${updatedItem.idMarca}`,
        methodType: "PUT",
        body: updatedItem,
      });
      await refetch();
    } catch (error) {
      console.error("Error al editar Marca:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleDelete = async (item: Marca) => {
    if (!confirm(`¿Eliminar "${item.nombreMarca}"?`)) return;

    try {
      setIsSubmitting(true);
      await callApi<void, void>({
        url: `/api/marca/${item.idMarca}`,
        methodType: "DELETE",
      });
      await refetch();
    } catch (error) {
      console.error("Error al eliminar Marca:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleView = (item: Marca) => {
    console.log("Ver:", item);
  };

  if (error) return <div>Error al cargar datos</div>;
  if (loading || isSubmitting) return <div>Cargando...</div>;


  if (!marcas || marcas.length === 0) {
    return (
    <CustomTable
      tableTitle="Marcas"
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


  const columns = createDynamicColumns(marcas);
  return (
    <CustomTable
      tableTitle="Marcas"
      data={marcas}
      columns={columns}
      formFields={formFields}
      onAdd={handleAdd}
      onEdit={handleEdit}
      onView={handleView}
      onDelete={handleDelete}
    />
  );
};

export default MarcaPageContainer;
