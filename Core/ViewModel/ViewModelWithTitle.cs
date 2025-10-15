using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ion.Core;

public record class ViewModelWithTitle() : ViewModel()
{
    public virtual string Title { get => Get("Untitled"); set => Set(value); }
}
