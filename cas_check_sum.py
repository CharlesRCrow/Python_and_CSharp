import openpyxl 

input_file='cas.xlsx'
wb=openpyxl.load_workbook(input_file)
sh=wb['Sheet1']
list_cas=[]
for row in sh['A']:
    list_cas.append(row.value)

print(list_cas)
 


def is_cas(cas):
    cas=list(cas)
    cas_list=''.join(ch for ch in cas if ch.isdigit())
    if len(cas_list)>10 or len(cas_list)<5:
        return False
    else:
        cas_list=[int(i) for i in cas_list  ]
    return True, cas_list

def format_cas(cas_num, check_digit):
    cas_num=''.join([str(i) for i in cas_num])
    check_digit=str(check_digit)
    first_half=cas_num[:-2]
    second_half=cas_num[-2:]
    return first_half+'-'+second_half+'-'+check_digit

def check_sum(cas):
    cas=is_cas(cas)
    if cas is False:
        return 'Not a Cas Number:'
    cas=cas[1]
    cas_num=cas[:-1]
    check_digit=cas[-1]
    rev_cas_num=cas_num[::-1]
    i=1
    total=0
    clean_cas=format_cas(cas_num,check_digit)
    for digit in rev_cas_num:
        it=i*digit
        i+=1
        total+=it
    return clean_cas,(total%10==check_digit)  

cas_check=input('Enter CAS Number: ')
result=check_sum(cas_check)
print(result)
