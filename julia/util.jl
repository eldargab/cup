
function tok()
    out = Vector{Char}()
    while true
        c = read(stdin, Char)
        if isspace(c)
            continue
        end
        push!(out, c)
        break
    end
    while !eof(stdin)
        c = read(stdin, Char)
        if isspace(c)
            break
        else
            push!(out, c)
        end
    end
    String(out)
end


function tok(T::Type)
    parse(T, tok())
end


function tok(parse)
    parse(tok())
end


function vectok(len, T::Type)
    a = Vector{T}(undef, len)
    for i = 1:len
        a[i] = tok(T)
    end
    a
end