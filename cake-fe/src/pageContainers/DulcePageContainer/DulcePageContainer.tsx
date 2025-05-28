import { useEffect, useState } from "react";
import CustomTable from "@/components/CustomTable/CustomTable";
import useCallApi from "@/hooks/useCallApi";
import { createDynamicColumns } from "@/utils/createDynamicColumns";
import { callApi } from "@/utils/callApi";

type Dulce = {
  idDulce: number;
  nombreDulce: string;
  precioDulce: number;
  stockDulce: number;
  idSabor: number;
  idCategoria: number;
  idMarca: number;
  idPresentacion: number;
};

type Category = {
  idCategoria: number;
  nombreCategoria: string;
};

type Marca = {
  idMarca: number;
  nombreMarca: string;
  paisOrigenMarca: string;
};

type Presentacion = {
  idPresentacion: number;
  nombrePresentacion: string;
};

type Sabor = {
  idSabor: number;
  nombreSabor: string;
};

const formFields = (
  categorias: Category[],
  marcas: Marca[],
  presentaciones: Presentacion[],
  sabores: Sabor[]
) => [
  {
    key: "nombreDulce" as const,
    label: "Nombre de Dulce",
    placeholder: "Ingresa tu Dulce",
    type: "text",
  },
  {
    key: "precioDulce" as const,
    label: "Precio del dulce",
    type: "number",
    placeholder: "Ingresa el precio del dulce",
  },
  {
    key: "stockDulce" as const,
    label: "Stock del dulce",
    type: "number",
    placeholder: "Ingresa el stock del dulce",
  },
  {
    key: "idSabor" as const,
    type: "select",
    label: "Sabor del dulce",
    placeholder: "Selecciona el sabor del dulce",
    options: sabores.map((sabor) => ({
      value: sabor.idSabor.toString(),
      label: sabor.nombreSabor,
    })),
  },
  {
    key: "idCategoria" as const,
    label: "Categoría del dulce",
    type: "select",
    placeholder: "Selecciona la categoría del dulce",
    options: categorias.map((categoria) => ({
      value: categoria.idCategoria.toString(),
      label: categoria.nombreCategoria,
    })),
  },
  {
    key: "idMarca" as const,
    label: "Marca del dulce",
    type: "select",
    placeholder: "Selecciona la marca del dulce",
    options: marcas.map((marca) => ({
      value: marca.idMarca.toString(),
      label: marca.nombreMarca,
    })),
  },
  {
    key: "idPresentacion" as const,
    label: "Presentación del dulce",
    type: "select",
    placeholder: "Selecciona la presentación del dulce",
    options: presentaciones.map((presentacion) => ({
      value: presentacion.idPresentacion.toString(),
      label: presentacion.nombrePresentacion,
    })),
  },
] satisfies { 
  key: keyof Dulce; 
  label: string; 
  placeholder?: string; 
  type?: string; 
  options?: { value: string; label: string }[] 
}[];

const DulcePageContainer = () => {
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [categorias, setCategorias] = useState<Category[]>([]);
  const [marcas, setMarcas] = useState<Marca[]>([]);
  const [presentaciones, setPresentaciones] = useState<Presentacion[]>([]);
  const [sabores, setSabores] = useState<Sabor[]>([]);

  const {
    data: datas = [],
    loading,
    error,
    refetch,
  } = useCallApi<Dulce[]>({
    url: "/api/dulce",
    methodType: "GET",
  });

  const {
    data: categoriasData = [],
    loading: categoriasLoading,
  } = useCallApi<Category[]>({
    url: "/api/categoria",
    methodType: "GET",
  });

  const {
    data: marcasData = [],
    loading: marcasLoading,
  } = useCallApi<Marca[]>({
    url: "/api/marca",
    methodType: "GET",
  });

  const {
    data: presentacionesData = [],
    loading: presentacionesLoading,
  } = useCallApi<Presentacion[]>({
    url: "/api/presentacion",
    methodType: "GET",
  });

  const {
    data: saboresData = [],
    loading: saboresLoading,
  } = useCallApi<Sabor[]>({
    url: "/api/sabor",
    methodType: "GET",
  });

  useEffect(() => {
    if (categoriasData) {
      setCategorias(categoriasData);
    }
  }, [categoriasData]);

  useEffect(() => {
    if (marcasData) {
      setMarcas(marcasData);
    }
  }, [marcasData]);

  useEffect(() => {
    if (presentacionesData) {
      setPresentaciones(presentacionesData);
    }
  }, [presentacionesData]);

  useEffect(() => {
    if (saboresData) {
      setSabores(saboresData);
    }
  }, [saboresData]);

  const handleAdd = async (newItemRaw: Omit<Dulce, "idDulce">) => {
    try {
      setIsSubmitting(true);
      console.log("Agregando nuevo dulce:", newItemRaw);
      
      // Process numeric fields
      const newItem = { ...newItemRaw };
      // Convert string values from select inputs to numbers
      ["precioDulce", "stockDulce", "idSabor", "idCategoria", "idMarca", "idPresentacion"].forEach(field => {
        newItem[field as keyof typeof newItem] = Number(newItem[field as keyof typeof newItem]);
      });

      await callApi<Dulce, typeof newItem>({
        url: "/api/dulce",
        methodType: "POST",
        body: newItem,
      });
      
      await refetch();
    } catch (error) {
      console.error("Error al agregar el Dulce:", error);
      alert("Error al agregar: " + (error as Error).message);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleEdit = async (updatedItemRaw: Dulce) => {
    try {
      setIsSubmitting(true);
      console.log("Editando dulce:", updatedItemRaw);
      
      // Process numeric fields
      const updatedItem = { ...updatedItemRaw };
      // Convert string values from select inputs to numbers
      ["precioDulce", "stockDulce", "idSabor", "idCategoria", "idMarca", "idPresentacion"].forEach(field => {
        updatedItem[field as keyof typeof updatedItem] = Number(updatedItem[field as keyof typeof updatedItem]);
      });

      await callApi<Dulce, typeof updatedItem>({
        url: `/api/dulce/${updatedItem.idDulce}`,
        methodType: "PUT",
        body: updatedItem,
      });
      
      await refetch();
    } catch (error) {
      console.error("Error al editar Dulce:", error);
      alert("Error al editar: " + (error as Error).message);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleDelete = async (item: Dulce) => {
    if (!confirm(`¿Eliminar "${item.nombreDulce}"?`)) return;

    try {
      setIsSubmitting(true);
      await callApi<void, void>({
        url: `/api/dulce/${item.idDulce}`,
        methodType: "DELETE",
      });
      await refetch();
    } catch (error) {
      console.error("Error al eliminar Dulce:", error);
      alert("Error al eliminar: " + (error as Error).message);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleView = (item: Dulce) => {
    console.log("Ver:", item);
    
    // Find the related data to display readable names instead of just IDs
    const categoria = categorias.find(c => c.idCategoria === item.idCategoria)?.nombreCategoria || 'Desconocida';
    const marca = marcas.find(m => m.idMarca === item.idMarca)?.nombreMarca || 'Desconocida';
    const presentacion = presentaciones.find(p => p.idPresentacion === item.idPresentacion)?.nombrePresentacion || 'Desconocida';
    const sabor = sabores.find(s => s.idSabor === item.idSabor)?.nombreSabor || 'Desconocido';
    
    alert(
      `Detalles de ${item.nombreDulce}\n\n` +
      `Precio: $${item.precioDulce}\n` +
      `Stock: ${item.stockDulce}\n` +
      `Categoría: ${categoria}\n` +
      `Marca: ${marca}\n` +
      `Presentación: ${presentacion}\n` +
      `Sabor: ${sabor}`
    );
  };

  const isReferenceDataLoading = categoriasLoading || marcasLoading || presentacionesLoading || saboresLoading;

  if (error) return <div className="p-8 text-red-500">Error al cargar datos: {String(error)}</div>;
  if (loading || isSubmitting || isReferenceDataLoading) return <div className="p-8">Cargando...</div>;

  if (!datas || datas.length === 0) {
    return (
      <CustomTable
        tableTitle="Dulces"
        data={[]}
        columns={[]}
        formFields={formFields(categorias, marcas, presentaciones, sabores)}
        onAdd={handleAdd}
        onEdit={handleEdit}
        onView={handleView}
        onDelete={handleDelete}
      />
    );
  }
  
  const columns = createDynamicColumns(datas);
  
  return (
    <CustomTable
      tableTitle="Dulces"
      data={datas}
      columns={columns}
      formFields={formFields(categorias, marcas, presentaciones, sabores)}
      onAdd={handleAdd}
      onEdit={handleEdit}
      onView={handleView}
      onDelete={handleDelete}
    />
  );
};

export default DulcePageContainer;