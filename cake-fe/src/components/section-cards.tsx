import CardShortCut from '@/components/card-shortcut'
import {
  SquareLibrary,
  ShoppingBasket,
} from "lucide-react"




export function SectionCards() {
  return (
      <div className="*:data-[slot=card]:shadow-xs @xl/main:grid-cols-2 @5xl/main:grid-cols-4 grid grid-cols-1 gap-4 px-4 *:data-[slot=card]:data-[slot=card]:from-primary/5 *:data-[slot=card]:to-card dark:*:data-[slot=card]:bg-card lg:px-6">
      <CardShortCut
        url="/productos"
        title="Productos"
        icon={<ShoppingBasket/>}
      />
      <CardShortCut
        url="/categorias"
        title="Categorias"
        icon={<SquareLibrary/>}
      />
    </div>
  )
}