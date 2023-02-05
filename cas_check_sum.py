def is_cas(cas):
    cas=list(cas)
    cas_list=''.join(ch for ch in cas if ch.isdigit())
    if len(cas_list)>10 or len(cas_list)<5:
        return False
    else:
        cas_list=[int(i) for i in cas_list  ]
    return True, cas_list
    
          