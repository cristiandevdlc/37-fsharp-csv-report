open System
open System.IO

let file = if fsi.CommandLineArgs.Length > 1 then fsi.CommandLineArgs[1] else "sales.csv"
let rows = File.ReadAllLines(file) |> Array.skip 1 |> Array.choose (fun line -> match line.Split(',') with | [|category; amount|] -> match Double.TryParse(amount) with | true, value -> Some(category, value) | _ -> None | _ -> None)
rows |> Array.groupBy fst |> Array.map (fun (category, values) -> category, values |> Array.sumBy snd) |> Array.iter (fun (category, total) -> printfn "%s: %.2f" category total)
printfn "Total: %.2f" (rows |> Array.sumBy snd)
